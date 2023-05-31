using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Windows;

namespace PhotoOrder_Updater.Updater
{
    public class UpdaterInitialize
    {
        /// <summary>
        /// Parent form
        /// </summary>
        private Window ParentWindow;

        /// <summary>
        /// Parent assembly
        /// </summary>
        private Assembly ParentAssembly;

        /// <summary>
        /// Parent name
        /// </summary>
        private string ParentPath;

        /// <summary>
        /// Holds the program-to-update's info
        /// </summary>
        private UpdaterValues[] LocalApplicationInfos;

        /// <summary>
        /// Holds all the jobs defined in update xml
        /// </summary>
        private UpdaterXml[] JobsFromXML;

        /// <summary>
        /// Total number of jobs
        /// </summary>
        private int Num_Jobs = 0;

        /// <summary>
        /// Lists containing all informtion for files update
        /// </summary>
        private List<string> tempFilePaths = new List<string>();
        private List<string> currentPaths = new List<string>();
        private List<string> newPaths = new List<string>();
        private List<string> launchArgss = new List<string>();
        private List<UpdaterTagType> jobtypes = new List<UpdaterTagType>();

        private int acceptJobs = 0;

        /// <summary>
        /// Thread to find update
        /// </summary>
        private BackgroundWorker BgWorker;

        /// <summary>
        /// Uri of the update xml on the server
        /// </summary>
        private Uri UpdateXmlLocation;

        /// <summary>
        /// Have a application latest version
        /// </summary>
        private bool LastVersionMessage { get; set; }

        /// <summary>
        /// Creates a new SharpUpdater object
        /// </summary>
        /// <param name="a">Parent ssembly to be attached</param>
        /// <param name="owner">Parent form to be attached</param>
        /// <param name="XMLOnServer">Uri of the update xml on the server</param>
        public UpdaterInitialize(Assembly assembly, Window owner, Uri XMLOnServer, bool lastVersionMessage) {
            ParentWindow = owner;
            ParentAssembly = assembly;
            ParentPath = assembly.Location;
            this.LastVersionMessage = lastVersionMessage;
            UpdateXmlLocation = XMLOnServer;

            // Set up backgroundworker
            BgWorker = new BackgroundWorker();
            BgWorker.DoWork += new DoWorkEventHandler(BgWorker_DoWork);
            BgWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(BgWorker_RunWorkerCompleted);
        }

        /// <summary>
        /// Checks for an update for the files passed.
        /// If there is an update, a dialog asking to download will appear
        /// </summary>
        public void DoUpdate() {
            if (!BgWorker.IsBusy)
                BgWorker.RunWorkerAsync();
        }

        /// <summary>
        /// Checks for/parses update.xml on server
        /// </summary>
        private void BgWorker_DoWork(object sender, DoWorkEventArgs e) {
            // Check for update on server
            if (!UpdaterXml.ExistsOnServer(UpdateXmlLocation))
                e.Cancel = true;
            else // Parse update xml
                e.Result = UpdaterXml.Parse(UpdateXmlLocation);
        }

        /// <summary>
        /// After the background worker is done, prompt to update if there is one
        /// </summary>
        private void BgWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e) {
            // If there is a file on the server
            if (!e.Cancelled) {
                JobsFromXML = (UpdaterXml[])e.Result;
                // Check if the update is not null and is a newer version than the current application
                if (JobsFromXML != null) {
                    Console.WriteLine("Number of updates from XML: " + JobsFromXML.Length);

                    // create local app info according to update xml
                    Num_Jobs = JobsFromXML.Length;
                    LocalApplicationInfos = new UpdaterValues[Num_Jobs];
                    for (int i = 0; i < Num_Jobs; ++i) {
                        if (Path.GetFileName(ParentPath).CompareTo(Path.GetFileName(JobsFromXML[i].FilePath)) == 0) {
                            LocalApplicationInfos[i] = new UpdaterValues(JobsFromXML[i], ParentAssembly, ParentWindow);
                        } else {
                            LocalApplicationInfos[i] = new UpdaterValues(JobsFromXML[i]);
                        }
                        LocalApplicationInfos[i].Print();
                    }

                    // validate all update jobs
                    List<int> validJobs = new List<int>();
                    for (int i = 0; i < Num_Jobs; ++i) {
                        if (JobsFromXML[i].Tag == UpdaterTagType.UPDATE) {
                            if (!JobsFromXML[i].IsNewerThan(LocalApplicationInfos[i].Version))
                                continue;
                        }
                        validJobs.Add(i);
                    }

                    // let user choose to accept update jobs
                    int count = 0;
                    bool showMsgBox = true;
                    foreach (int i in validJobs) {
                        count++;
                        acceptJobs++;
                        showMsgBox = false;
                        DownloadUpdate(JobsFromXML[i], LocalApplicationInfos[i]); // Do the update
                    }

                    if (showMsgBox) {
                        if (LastVersionMessage)
                            MessageBox.Show("En son sürüm mevcuttur.", "Güncelleme Mesajı", MessageBoxButton.OK, MessageBoxImage.Information);
                        InstallUpdate();
                    } else {
                        if (acceptJobs > 0)
                            InstallUpdate();
                    }
                } else {
                    if (LastVersionMessage)
                        MessageBox.Show("En son sürüm mevcuttur.", "Güncelleme Mesajı", MessageBoxButton.OK, MessageBoxImage.Information);
                    InstallUpdate();
                }
            } else {
                MessageBox.Show("İndirme iptal edildi - İnternet Bağlatınızı Kontrol Ediniz!", "Güncelleme Mesajı", MessageBoxButton.OK, MessageBoxImage.Information);
                Application.Current.Shutdown();
            }
        }

        /// <summary>
        /// Download the update
        /// </summary>
        /// <param name="update">The update xml info</param>
        /// <param name="applicationInfo">An SharpUpdateInfo object containing application's info</param>
        private void DownloadUpdate(UpdaterXml update, UpdaterValues applicationInfo) {
            if (update.Tag == UpdaterTagType.REMOVE) {
                tempFilePaths.Add("");
                currentPaths.Add("");
                newPaths.Add(Path.GetFullPath(applicationInfo.ApplicationPath));
                launchArgss.Add(update.LaunchArgs);
                jobtypes.Add(update.Tag);
                return;
            }
            var updaterWindow = new WindowUpdater(update.Uri, update.MD5, applicationInfo.ApplicationIcon);
            updaterWindow.ShowInTaskbar = false;
            bool? result = updaterWindow.ShowDialog();
            if (result ?? true) {
                string currentPath = (update.Tag == UpdaterTagType.UPDATE) ? applicationInfo.ApplicationAssembly.Location : "";
                string newPath = (update.Tag == UpdaterTagType.UPDATE) ? Path.GetFullPath(Path.GetDirectoryName(currentPath).ToString() + update.FilePath) : Path.GetFullPath(applicationInfo.ApplicationPath);
                Directory.CreateDirectory(Path.GetDirectoryName(newPath));

                tempFilePaths.Add(updaterWindow.TempFilePath);
                currentPaths.Add(currentPath);
                newPaths.Add(newPath);
                launchArgss.Add(update.LaunchArgs);
                jobtypes.Add(update.Tag);
            } else {
                MessageBox.Show(ParentWindow, "Güncelleme gerçekleştirilemedi. Lütfen internet bağlantınızı kontrol ediniz veya daha sonra tekrar deneyiniz", "Güncelleme Mesajı", MessageBoxButton.OK, MessageBoxImage.Information);
                Application.Current.Shutdown();
            }
        }

        /// <summary>
        /// Install all the updates
        /// </summary>
        private void InstallUpdate() {
            UpdateApplications();

            Thread thread = new Thread(InstalUpdateThread);
            thread.Start();
        }
        private void InstalUpdateThread()
        {
            UpdaterValues.save_log();
            Thread.Sleep(3000);
            Application.Current.Dispatcher.Invoke(new Action(() => {
                Application.Current.Shutdown();
            }));
        } 

        /// <summary>
        /// Hack to close program, delete original, move the new one to that location
        /// </summary>
        private void UpdateApplications() {
            try {
                string argument_start = "/C choice /C Y /N /D Y /T 4 & Start \"\" /D \"{0}\" \"{1}\"";
                string argument_update = "/C choice /C Y /N /D Y /T 4 & Del /F /Q \"{0}\" & choice /C Y /N /D Y /T 2 & Move /Y \"{1}\" \"{2}\"";
                string argument_update_start = argument_update + " & Start \"\" /D \"{3}\" \"{4}\" {5}";
                string argument_add = "/C choice /C Y /N /D Y /T 4 & Move /Y \"{0}\" \"{1}\"";
                string argument_remove = "/C choice /C Y /N /D Y /T 4 & Del /F /Q \"{0}\"";
                string argument_complete = "";

                int curAppidx = -1;


                for (int i = 0; i < acceptJobs; ++i) {
                    string curName = Path.GetFileName(currentPaths[i]);
                    if (curName.CompareTo("") != 0 && Path.GetFileName(ParentPath).CompareTo(curName) == 0) {
                        curAppidx = i;
                        continue;
                    }

                    if (jobtypes[i] == UpdaterTagType.ADD) {
                        argument_complete = string.Format(argument_add, tempFilePaths[i], newPaths[i]);
                        Console.WriteLine("add: " + argument_complete);
                    } else if (jobtypes[i] == UpdaterTagType.UPDATE) {
                        argument_complete = string.Format(argument_update, currentPaths[i], tempFilePaths[i], newPaths[i]);
                        Console.WriteLine("update: " + argument_complete);
                    } else {
                        argument_complete = string.Format(argument_remove, newPaths[i]);
                        Console.WriteLine("remove: " + argument_complete);
                    }

                    ProcessStartInfo cmd = new ProcessStartInfo {
                        Arguments = argument_complete,
                        WindowStyle = ProcessWindowStyle.Hidden,
                        CreateNoWindow = true,
                        FileName = "cmd.exe"
                    };
                    Process.Start(cmd);
                }

                if (curAppidx > -1) {
                    argument_complete = string.Format(argument_update_start, currentPaths[curAppidx], tempFilePaths[curAppidx], newPaths[curAppidx], Path.GetDirectoryName(newPaths[curAppidx]), Path.GetFileName(newPaths[curAppidx]), launchArgss[curAppidx]);
                    Console.WriteLine("Update and run main app: " + argument_complete);
                } else {
                    argument_complete = string.Format(argument_start, Path.GetDirectoryName(ParentPath), WindowLoader.appName);
                    Console.WriteLine("Run main app: " + argument_complete);
                }

                ProcessStartInfo cmd_main = new ProcessStartInfo {
                    Arguments = argument_complete,
                    WindowStyle = ProcessWindowStyle.Hidden,
                    CreateNoWindow = true,
                    FileName = "cmd.exe"
                };

                Process.Start(cmd_main);

            } catch (Exception) {
                MessageBox.Show("Yükleme iptal edildi!", "Güncelleme Mesajı", MessageBoxButton.OK, MessageBoxImage.Information);
                Application.Current.Shutdown();
            }
        }
    }
}
