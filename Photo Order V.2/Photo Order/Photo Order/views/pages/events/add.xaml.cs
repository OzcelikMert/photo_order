using Microsoft.Win32;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Photo_Order.views.pages.events
{
    /// <summary>
    /// add.xaml etkileşim mantığı
    /// </summary>
    public partial class add : Page
    {
        public add()
        {
            InitializeComponent();
            if (config.Values.selectedItemId > 0) {
                this.setInputs();
            }
        }
        OpenFileDialog chooseImage { get; set; }
        private string imageName { get; set; }
        private void saveClick(object sender, RoutedEventArgs e)
        {
            AppLibrary.Values.logger.loggerFunction(() =>
            {
                string name = this.name.Text.ToString();
                if (AppLibrary.Var.isNullOrEmpty(name))
                {
                    MessageBox.Show("Lütfen gerekli yerleri doldurunuz!", "Hatalı Giriş", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    string image = "";
                    if (config.Values.selectedItemId > 0)
                    {
                        image = this.imageName;
                    }

                    if (!(AppLibrary.Var.isNullOrEmpty(this.chooseImage) || AppLibrary.Var.isNullOrEmpty(this.chooseImage.FileName)))
                    {
                        image = AppLibrary.File.saveImage(config.Values.pathUploadsEventsFolder, this.chooseImage);
                    }

                    if (config.Values.selectedItemId == 0)
                    {
                        // Add
                        (new config.db.functions.Insert()).setEvents(image, name);
                    }
                    else
                    {
                        // Edit
                        (new config.db.functions.Update()).setEvents(config.Values.selectedItemId, image, name);
                    }

                    MessageBox.Show("İşlem başarılı etkinlik kayıt edildi.", "Etkinlik Kaydedildi", MessageBoxButton.OK, MessageBoxImage.Information);
                    config.Values.refreshList = true;

                    if (config.Values.selectedItemId > 0)
                    {
                        config.Values.windowEdit.Close();
                    }
                }
            });
            
        }
        private void setInputs() {
            AppLibrary.Values.logger.loggerFunction(() =>
            {
                var events = (new config.db.functions.Select()).getEvents(config.Values.selectedItemId);
                foreach (var _event in events)
                {
                    this.name.Text = _event.name;
                    this.imageName = _event.image;
                    if (!AppLibrary.Var.isNullOrEmpty(_event.image))
                    {
                        BitmapImage bi = new BitmapImage();
                        bi.BeginInit();
                        bi.CacheOption = BitmapCacheOption.OnLoad;
                        bi.CreateOptions = BitmapCreateOptions.IgnoreImageCache;
                        bi.UriSource = new Uri(config.Values.pathUploadsEventsFolder + _event.image);
                        bi.EndInit();
                        image.Source = bi;
                    }
                }
            });
            
        }
        private void chooseImageClick(object sender, RoutedEventArgs e) {
            this.chooseImage = new OpenFileDialog();
            this.chooseImage.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.webp;";
            if (this.chooseImage.ShowDialog() == true) {
                AppLibrary.Image.setImage(this.image, this.chooseImage.FileName);
            }
        }
    }
}
