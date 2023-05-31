using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Photo_Order.views.pages.settings
{
    /// <summary>
    /// Interaction logic for general.xaml
    /// </summary>
    public partial class general : Page {
        OpenFileDialog chooseImage { get; set; }
        public List<Image> images = new List<Image>();
        private string image = "";
        public general() {
            InitializeComponent();
        }
        public void initPage() {
            this.chooseImage = null;
            this.backgroundLogo.Source = null;
            this.companyName.Text = AppLibrary.Values.settings.json.companyName;
            this.image = AppLibrary.Values.settings.json.backgroundLogo;
            this.licenseDateEnd.Text = AppLibrary.Var.toStringDateFormatLabel(Convert.ToDateTime(Properties.Settings.Default.licenseDateEnd));
            if (!AppLibrary.Var.isNullOrEmpty(this.image)) {
                AppLibrary.Image.setImage(this.backgroundLogo, config.Values.pathUploadsBackgroundFolder + this.image);
            }
            AppLibrary.GC.GCCollect();
        }
        private void chooseImageClick(object sender, RoutedEventArgs e) {
            this.chooseImage = new OpenFileDialog();
            this.chooseImage.Filter = "Image Files|*.png;";
            if (this.chooseImage.ShowDialog() == true) {
                AppLibrary.Image.setImage(this.backgroundLogo, this.chooseImage.FileName);
            }
        }
        private void setBackgroundLogoDefault(object sender, RoutedEventArgs e) {
            this.chooseImage = null;
            this.backgroundLogo.Source = null;
            this.image = "";
        }
        private void setImages() {
            foreach (var image in this.images) {
                if (!AppLibrary.Var.isNullOrEmpty(AppLibrary.Values.settings.json.backgroundLogo)) {
                    AppLibrary.Image.setImage(image, config.Values.pathUploadsBackgroundFolder + AppLibrary.Values.settings.json.backgroundLogo);
                } else {
                    image.Source = null;
                }
            }
        }
        private void save(object sender, RoutedEventArgs e) {
            AppLibrary.Values.logger.loggerFunction(() => {
                if (!(AppLibrary.Var.isNullOrEmpty(this.chooseImage) || AppLibrary.Var.isNullOrEmpty(this.chooseImage.FileName))) {
                    this.image = AppLibrary.File.saveImage(config.Values.pathUploadsBackgroundFolder, this.chooseImage, copy: true);
                }
                if (!AppLibrary.Var.isNullOrEmpty(AppLibrary.Values.settings.json.backgroundLogo)) AppLibrary.File.removeImage(config.Values.pathUploadsBackgroundFolder, AppLibrary.Values.settings.json.backgroundLogo);
                AppLibrary.Values.settings.json.companyName = this.companyName.Text;
                AppLibrary.Values.settings.json.backgroundLogo = this.image;
                AppLibrary.Values.settings.write();
                this.setImages();
                MessageBox.Show("İşlem kayıt edildi.", "Kaydedildi", MessageBoxButton.OK, MessageBoxImage.Information);
            });
        }
    }
}
