using System;
using System.IO;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Windows;
using System.Windows.Media;

namespace PhotoOrder_Updater.Updater
{
    /// <summary>
    /// The interface that all applications need to implement in order to user CSharpUpdater
    /// </summary>
    class UpdaterValues
    {
        /// <summary>
        /// The path of your application
        /// </summary>
        public string ApplicationPath { get; }

        /// <summary>
        /// The name of your application as you want it displayed on the update form
        /// </summary>
        public string ApplicationName { get; }

        /// <summary>
        /// The current assembly
        /// </summary>
        public Assembly ApplicationAssembly { get; }

        /// <summary>
        /// The application's icon to be displayed in the top left
        /// </summary>
        public ImageSource ApplicationIcon { get; }

        /// <summary>
        /// The context of the program.
        /// For Windows Forms Applications, use 'this'
        /// Console Apps, reference System.Windows.Forms and return null.
        /// </summary>
        public Window Context { get; }

        /// <summary>
        /// The version of your application
        /// </summary>
        public Version Version { get; }

        /// <summary>
        /// Tag to distinguish types of updates
        /// </summary>
        public UpdaterTagType Tag;

        public UpdaterValues(UpdaterXml job, Assembly assembly, Window window)
        {
            ApplicationPath = job.FilePath;
            ApplicationName = Path.GetFileNameWithoutExtension(ApplicationPath);
            ApplicationAssembly = assembly;
            ApplicationIcon = window.Icon;
            Context = window;
            if (job.Tag == UpdaterTagType.UPDATE)
            {
                try
                {
                    ApplicationAssembly = Assembly.Load(ApplicationPath);
                }
                catch (Exception)
                {
                    ApplicationAssembly = null;
                    job.Tag = UpdaterTagType.ADD;
                }

            }
            Tag = job.Tag;
        }

        static public string log = ""; 
        public UpdaterValues(UpdaterXml job)
        {
            ApplicationPath = (Directory.GetCurrentDirectory()) +"\\"+ job.FilePath;
            log += ApplicationPath + "\n";
            if (File.Exists(ApplicationPath)){
                log += "yes file: ";
                string file_md5 = CalculateMD5(ApplicationPath);
                if(file_md5 == job.MD5) {
                        log += "same\n"; 
                }else {
                    log += "file difrent\n";
                    job.Tag = UpdaterTagType.ADD;
                }
            }else{
                log += "no file";
                job.Tag = UpdaterTagType.ADD;
            }
            
            ApplicationName = Path.GetFileNameWithoutExtension(ApplicationPath);
            ApplicationAssembly =  null;
            ApplicationIcon = null;
            Context = null;
            Version = job.Version;
            Tag = job.Tag;
            log += "---\n";
        }
        public string CalculateMD5(string filename)
        {
            using (var md5 = MD5.Create())
            {
                using (var stream = File.OpenRead(filename))
                {
                    var hash = md5.ComputeHash(stream);
                    return BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();
                }
            }
        }
        public void Print()
        {
            string head = "========== CSharpUpdaterValues ==========";
            string tail = "=============================================";
            string toPrint = string.Format("{0}\nTag Type: {1}\nApplicationPath: {2}\nApplicationName: {3}\nAssemblyName: {4}\nFormName: {5}\nVersion: {6}\n{7}",
                head, Tag.ToString(), ApplicationPath == null ? "null" : ApplicationPath,
                ApplicationName == null ? "null" : ApplicationName,
                ApplicationAssembly == null ? "null" : ApplicationAssembly.FullName,
                Context == null ? "null" : Context.Name,
                Version == null ? "null" : Version.ToString(), tail);
            Console.WriteLine(toPrint);
        }

       static public void save_log(string cmd = "x") {
            string filePath = Path.Combine((Directory.GetCurrentDirectory()), "log_updater.log");
            string filePath_cmd = Path.Combine((Directory.GetCurrentDirectory()), "cmd.log");

            using (StreamWriter writer = new StreamWriter(filePath))
            {  writer.Write(log); }
            using (StreamWriter writer = new StreamWriter(filePath_cmd))
            { writer.Write(cmd); }
        }
    }
}
