using System;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Diagnostics;

namespace Photo_Order_Customer
{
    /// <summary>
    /// Interaction logic for WindowLanguage.xaml
    /// </summary>
    public partial class WindowLanguage : Window
    {
        public static int sleepSeconds = 0;
        private int sleepMaxSeconds = 60;
        private Thread thread;
        public WindowLanguage() {
            InitializeComponent();
            if (Process.GetProcessesByName(AppController.Values.appCustomerName.Replace(".exe", "")).Length > 1) {
                // If ther is more than one, than it is already running.
                System.Windows.Application.Current.Shutdown();
            }
            bool status = true;
            AppLibrary.Values.logger.loggerFunction(() => {
                if (!this.checkApp()) {
                    status = false;
                    Application.Current.Shutdown();
                }
            });

            AppLibrary.Values.logger.loggerFunction(() => {
                if (!status) return;
                config.Values.remotePaths = new config.RemotePaths();
                AppLibrary.Values.settings = new AppLibrary.Settings(config.Values.remotePaths.json.pathPublicDocument);
                AppLibrary.Values.dbInfo = new AppLibrary.db.DBInfo(config.Values.remotePaths.json.pathPublicDocument, onlyRead: true);
                this.setSleepThread();
                if (!AppLibrary.Var.isNullOrEmpty(AppLibrary.Values.settings.json.backgroundLogo)) AppLibrary.Image.setImage(this.backgroundLogo, config.Values.pathUploadsBackgroundFolder + AppLibrary.Values.settings.json.backgroundLogo);
            });
        }
        private bool checkApp() {
            bool status = true;

            if (AppController.Network.isThere()) {
                AppController.Version version = new AppController.Version();
                if (version.isConnected) {
                    if (version.result.app != AppController.Values.versionApp) {
                        status = false;
                        ProcessStartInfo processStartInfo = new ProcessStartInfo {
                            Arguments = AppController.Values.appCustomerName,
                            WindowStyle = ProcessWindowStyle.Hidden,
                            CreateNoWindow = false,
                            FileName = AppController.Values.appUpdaterName,
                        };
                        Process.Start(processStartInfo);
                    }
                }
            }

            return status;
        }
        private void setSleepThread() {
            this.thread = new Thread(this.getWindowSlider);
            this.thread.Start();
        }
        private void getWindowSlider() {
            AppLibrary.Values.logger.loggerFunction(() => {
                while (true) {
                    Thread.Sleep(TimeSpan.FromSeconds(1));
                    this.Dispatcher.BeginInvoke(new Action(() => {
                        sleepSeconds++;
                        if (sleepSeconds >= this.sleepMaxSeconds) {
                            this.thread.Abort();
                            WindowSlider windowSlider = new WindowSlider();
                            windowSlider.ShowDialog();
                            sleepSeconds = 0;
                            this.setSleepThread();
                        }
                    }));
                }
            });
        }
        private void languageClick(object sender, RoutedEventArgs e) {
            AppLibrary.Values.logger.loggerFunction(() =>
            {
                //this.thread.Abort();
                string language = ((Button)sender).Name.ToString().Replace("language_", ""),
                    culture = "tr-TR";
                switch (language){
                    case "tr": culture = "tr-TR"; break;
                    case "ru": culture = "ru-RU"; break;
                    case "en": culture = "en-US"; break;
                    case "ar": culture = "ar-SA"; break;
                    case "de": culture = "de-DE"; break;
                    case "fr": culture = "fr-FR"; break;
                }
                App.SelectCulture(culture);
                if (config.Values.customerId == 0)
                {
                    WindowLogin windowLogin = new WindowLogin();
                    if (windowLogin.ShowDialog() == false)
                    {
                        return;
                    }
                }

                WindowMain windowMain = new WindowMain();
                this.Hide();
                windowMain.ShowDialog();
                this.Show();
            });
        }
        private void clickExit(object sender, RoutedEventArgs e) {
            this.Dispatcher.Invoke(() => {
                this.Hide();
                if (this.thread != null) this.thread.Abort();
                Environment.Exit(0);
            });
        }
        private void windowClosing(object sender, System.ComponentModel.CancelEventArgs e) {
            if(this.thread != null) this.thread.Abort();
        }
        private void windowMouseMove(object sender, MouseEventArgs e) {
            sleepSeconds = 0;
        }
    }
}
