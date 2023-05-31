using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Windows;
using PhotoOrder_Updater.Updater;

namespace PhotoOrder_Updater {
    /// <summary>
    /// Interaction logic for Loader.xaml
    /// </summary>
    public partial class WindowLoader : Window
    {
        public static string appName { get; set; }
        private Thread thread;
        public WindowLoader() {
            InitializeComponent();
            // Check is Running
            if (Process.GetProcessesByName(AppController.Values.appUpdaterName.Replace(".exe", "")).Length > 1) {
                // If ther is more than one, than it is already running.
                System.Windows.Application.Current.Shutdown();
            }
            if (App.mArgs != null) {
                foreach (string arg in App.mArgs) appName += arg + " ";
                appName = appName.Substring(0, appName.Length - 1);
            }
            else Application.Current.Shutdown();
        }

        public bool IsProcessOpen(string name)
        {
            foreach (Process clsProcess in Process.GetProcesses())
            {
                if (clsProcess.ProcessName.ToLower().Contains(name.ToLower()))
                {
                    return true;
                }
            }
            return false;
        }

        private void windowLoaded(object sender, RoutedEventArgs e) {
            if (appName.Length < 1) return;
            this.thread = new Thread(threadUpdater);
            thread.Start();
        }
        private void threadUpdater()
        {
            Thread.Sleep(1000);
            Dispatcher.Invoke(new Action(() => {
                string link = (appName == AppController.Values.appMainName) ? AppLinks.Values.urlUpdaterInfo : AppLinks.Values.urlUpdaterInfoCustomer;
                UpdaterInitialize updaterInitialize = new UpdaterInitialize(Assembly.GetExecutingAssembly(), this, new Uri(link), false);
                updaterInitialize.DoUpdate();
            }));
        }

        private void windowClosing(object sender, System.ComponentModel.CancelEventArgs e) {
            if(this.thread != null) this.thread.Abort();
        }
    }
}
