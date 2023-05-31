using System;
using System.ComponentModel;
using System.IO;
using System.Net;
using System.Windows;
using System.Windows.Media;

namespace PhotoOrder_Updater.Updater
{
    /// <summary>
    /// Interaction logic for UpdaterWindow.xaml
    /// </summary>
    public partial class WindowUpdater : Window
    {
        /// <summary>
        /// Loader Infos Window
        /// </summary>
        private WindowLoader loader;
        /// <summary>
        /// The web client to download the update
        /// </summary>
        private WebClient webClient;

        /// <summary>
        /// The thread to hash the file on
        /// </summary>
        private BackgroundWorker backgroundWorker;

        /// <summary>
        /// The MD5 hash of the file to download
        /// </summary>
        private string md5;

        /// <summary>
        /// Gets the temp file path for the downloaded file
        /// </summary>
        internal string TempFilePath { get; }

        /// <summary>
        /// Creates a new CSharpUpdaterDownloadProgress
        /// </summary>
        public WindowUpdater(Uri location, string md5, ImageSource applicationIcon)
        {
            InitializeComponent();

            if (applicationIcon != null)
                this.Icon = applicationIcon;

            progressDownloadBar.IsIndeterminate = false;
            UpdateLoaderInfos("Güncellemeler Yükleniyor...", "", "", 0);

            // Set the temp file name and create new 0-byte file
            TempFilePath = Path.GetTempFileName();

            this.md5 = md5;

            webClient = new WebClient();
            webClient.DownloadProgressChanged += new DownloadProgressChangedEventHandler(webClient_DownloadProgressChanged);
            webClient.DownloadFileCompleted += new AsyncCompletedEventHandler(webClient_DownloadFileCompleted);

            backgroundWorker = new BackgroundWorker();
            backgroundWorker.DoWork += new DoWorkEventHandler(backgroundWorker_DoWork);
            backgroundWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(backgroundWorker_RunWorkerCompleted);

            try
            {
                webClient.DownloadFileAsync(location, this.TempFilePath);
            }
            catch (Exception)
            {
                this.DialogResult = false;
                this.Close();
            }
        }

        /// <summary>
        /// Formats the byte count to closest byte type
        /// </summary>
        /// <param name="bytes">The amount of bytes</param>
        /// <param name="decimalPlaces">How many decimal places to show</param>
        /// <param name="showByteType">Add the byte type on the end of the string</param>
        /// <returns>The bytes formatted as specified</returns>
        private string FormatBytes(long bytes, int decimalPlaces, bool showByteType)
        {
            double newBytes = bytes;
            string formatString = "{0";
            string byteType = "B";

            if (newBytes > 1024 && newBytes < 1048576)
            {
                newBytes /= 1024;
                byteType = "KB";
            }
            else if (newBytes > 1048576 && newBytes < 1073741824)
            {
                newBytes /= 1048576;
                byteType = "MB";
            }
            else
            {
                newBytes /= 1073741824;
                byteType = "GB";
            }

            if (decimalPlaces > 0)
                formatString += ":0.";

            for (int i = 0; i < decimalPlaces; i++)
                formatString += "0";

            formatString += "}";

            if (showByteType)
                formatString += byteType;

            return string.Format(formatString, newBytes);
        }

        private void webClient_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            //  Application.Current.Dispatcher.Invoke(() => {
            UpdateLoaderInfos(
                "Güncellemeler Yükleniyor...",
                String.Format("Toplam: {0}", FormatBytes(e.TotalBytesToReceive, 1, true)),
                String.Format("İndirilen: {0}", FormatBytes(e.BytesReceived, 1, true)),
                e.ProgressPercentage
            );
            // });
        }

        private void webClient_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                this.DialogResult = false;
                this.Close();
            }
            else if (e.Cancelled)
            {
                this.DialogResult = false;
                this.Close();
            }
            else
            {
                progressDownloadBar.IsIndeterminate = true;
                UpdateLoaderInfos("Güncellemeler Yükleniyor...", "", "", 0);

                backgroundWorker.RunWorkerAsync(new string[] { this.TempFilePath, this.md5 });
            }
        }

        private void backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            string file = ((string[])e.Argument)[0];
            string updateMD5 = ((string[])e.Argument)[1];

            if (UpdaterHasher.HashFile(file, HashType.MD5).ToLower() != updateMD5.ToLower())
                e.Result = false;
            else
                e.Result = true;
        }

        private void backgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.DialogResult = (bool)e.Result;
            this.Close();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            if (webClient.IsBusy)
            {
                UpdaterValues.save_log();
                webClient.CancelAsync();
                this.DialogResult = false;
            }

            if (backgroundWorker.IsBusy)
            {
                backgroundWorker.CancelAsync();
                this.DialogResult = false;
            }
        }

        public void UpdateLoaderInfos(string infoText, string totalSize, string downloadedSize, int percentage)
        {
            statusText.Content = infoText;
            totalSizeText.Content = totalSize;
            downloadedSizeText.Content = downloadedSize;
            progressDownloadBar.Value = percentage;
        }
    }
}
