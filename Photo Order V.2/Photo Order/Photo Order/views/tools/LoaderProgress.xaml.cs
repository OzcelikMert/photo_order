using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Photo_Order.views.tools
{
    /// <summary>
    /// Interaction logic for LoaderProgress.xaml
    /// </summary>
    public partial class LoaderProgress : Window
    {
        public delegate void _doWork(BackgroundWorker worker);
        public delegate void _completed();

        BackgroundWorker worker;
        public string _loaderText = "Yükleniyor Lütfen Bekleyiniz...";
        public string _loaderTextInfo = "";
        public double progressMax = 100;
        public LoaderProgress(_doWork doWork, _completed completed = null, bool showProgressBar = true) {
            InitializeComponent();
            if(!showProgressBar) {
                this.loaderProgress.Visibility = Visibility.Collapsed;
                this.loaderTextInfo.Visibility = Visibility.Collapsed;
            }
            worker = new BackgroundWorker();
            worker.ProgressChanged += Worker_ProgressChanged;
            worker.DoWork += (o, e) => {
                doWork((BackgroundWorker)o);
            };
            worker.WorkerReportsProgress = true;
            worker.RunWorkerCompleted += (o, e) => {
                Worker_RunWorkerCompleted(o, e);
                if(completed != null) completed();
            };
        }
        public void Run() {
            this.loaderProgress.Maximum = progressMax;
            this.loaderText.Text = _loaderText;
            if (_loaderTextInfo.Length > 0) this.loaderTextInfo.Text = String.Format(_loaderTextInfo, this.progressMax, 0);
            this.Show();
            worker.RunWorkerAsync();
        }
        void Worker_ProgressChanged(object sender, ProgressChangedEventArgs e) {
            this.loaderProgress.Value = e.ProgressPercentage;
            if (_loaderTextInfo.Length > 0) this.loaderTextInfo.Text = String.Format(_loaderTextInfo, this.progressMax, e.ProgressPercentage);
        }
        private void Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e) {
            this.worker.Dispose();
            this.Close();
        }
    }
}
