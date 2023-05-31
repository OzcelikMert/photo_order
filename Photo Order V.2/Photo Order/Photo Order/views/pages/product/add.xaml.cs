using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Microsoft.Win32;

namespace Photo_Order.views.pages.product
{
    public partial class add : Page {
        public add() {
            InitializeComponent();
            if(config.Values.selectedItemId > 0){
                this.setInputs();
            }
        }
        OpenFileDialog chooseImage { get; set; }
        private string imageName { get; set; }
        private string imageNameHigh { get; set; }
        private double imageRotate = 0;
        private void saveClick(object sender, RoutedEventArgs e) {
            AppLibrary.Values.logger.loggerFunction(() => {
                this.save.IsEnabled = false;
                views.tools.LoaderProgress loaderProgress = new views.tools.LoaderProgress((worker) => {
                    string image = "";
                    List<string> images = new List<string>();
                    string imageHigh = "";
                    List<string> imagesHigh = new List<string>();
                    bool isSliderActive = true;

                    Dispatcher.Invoke(() => {
                        isSliderActive = (bool)this.isActiveSlider.IsChecked;
                    });

                    if (config.Values.selectedItemId > 0) {
                        image = this.imageName;
                        imageHigh = this.imageNameHigh;
                    }

                    if (!(AppLibrary.Var.isNullOrEmpty(this.chooseImage) || AppLibrary.Var.isNullOrEmpty(this.chooseImage.FileName))) {
                        if (config.Values.selectedItemId > 0) {
                            string fileName = this.chooseImage.FileName;
                            if (fileName.IndexOf(config.Values.pathImageHigh) == -1) {
                                MessageBox.Show(
                                    String.Format("{0}\nGecerli bir konum degil! Gecerli bir konum ornegi:\n{1}", fileName, config.Values.pathImageHigh),
                                    "Gecersiz Konum", MessageBoxButton.OK, MessageBoxImage.Warning);
                                return;
                            }
                            imageHigh = fileName.Replace(config.Values.pathImageHigh, "");
                            image = AppLibrary.File.saveImage(config.Values.pathUploadsProductsFolder(list.selectedGroupId), this.chooseImage);
                        } else {
                            foreach (var fileName in this.chooseImage.FileNames) {
                                if (fileName.IndexOf(config.Values.pathImageHigh) == -1) {
                                    MessageBox.Show(
                                        String.Format("{0}\nGecerli bir konum degil! Gecerli bir konum ornegi:\n{1}", fileName, config.Values.pathImageHigh),
                                        "Gecersiz Konum", MessageBoxButton.OK, MessageBoxImage.Warning);
                                    return;
                                }
                               imagesHigh.Add(fileName.Replace(config.Values.pathImageHigh, ""));
                            }
                            int index = 0;
                            foreach (var name in this.chooseImage.FileNames) {
                                images.Add(AppLibrary.File.saveImage(config.Values.pathUploadsProductsFolder(list.selectedGroupId), name: name));
                                index++;
                                worker.ReportProgress(index);
                            }
                        }
                    }

                    if (config.Values.selectedItemId == 0) {
                        // Add
                        (new config.db.functions.Insert()).setProducts(list.selectedGroupId, images.ToArray(), imagesHigh.ToArray(), isSliderActive);
                    } else {
                        // Edit
                        (new config.db.functions.Update()).setProduct(config.Values.selectedItemId,
                            image,
                            imageHigh,
                            isSliderActive
                        );
                    }

                    if (this.imageRotate != 0) {
                        RotateFlipType rotateFlipType = AppLibrary.Image.convertToFlipRotate(Convert.ToInt32(this.imageRotate));
                        AppLibrary.File.rotateImage(config.Values.pathUploadsProductsFolder(list.selectedGroupId) + image, rotateFlipType);
                        AppLibrary.File.rotateImage(config.Values.pathImageHigh + imageHigh, rotateFlipType);
                    }

                    MessageBox.Show("İşlem başarılı ürün kayıt edildi.", "Ürün Kaydedildi", MessageBoxButton.OK, MessageBoxImage.Information);
                    config.Values.refreshList = true;

                    Dispatcher.Invoke(() => {
                        config.Values.windowEdit.Close();
                    });
                }, () => {
                    this.save.IsEnabled = true;
                });
                if (!(AppLibrary.Var.isNullOrEmpty(this.chooseImage) || AppLibrary.Var.isNullOrEmpty(this.chooseImage.FileName))) { 
                    loaderProgress.progressMax = this.chooseImage.FileNames.Length;
                }
                loaderProgress._loaderTextInfo = "Seçilen resim {0} Yüklenen resim {1}";
                loaderProgress.Run();
                this.save.IsEnabled = false;
            });
        }
        private void setInputs() {
            AppLibrary.Values.logger.loggerFunction(() => {
                var products = (new config.db.functions.Select()).getProducts(config.Values.selectedItemId);
                foreach (var product in products) {
                    this.imageName = product.image;
                    this.imageNameHigh = product.imageHigh;
                    if (!AppLibrary.Var.isNullOrEmpty(product.image)) {
                        AppLibrary.Image.setImage(this.image, config.Values.pathUploadsProductsFolder(list.selectedGroupId) + product.image);
                    }
                    this.isActiveSlider.IsChecked = product.isSliderActive;
                }
                this.turnLeft.Visibility = Visibility.Visible;
                this.turnRight.Visibility = Visibility.Visible;
            });
        }
        private void chooseImageClick(object sender, RoutedEventArgs e) {
            AppLibrary.Values.logger.loggerFunction(() => {
                this.chooseImage = new OpenFileDialog();
                if (config.Values.selectedItemId == 0)
                {
                    this.chooseImage.Multiselect = true;
                }
                this.chooseImage.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.webp;";
                if (this.chooseImage.ShowDialog() == true) {
                    this.imageRotate = 0;
                    if (this.chooseImage.FileNames.Length > 1) {
                        this.turnLeft.Visibility = Visibility.Collapsed;
                        this.turnRight.Visibility = Visibility.Collapsed;
                        this.image.Visibility = Visibility.Collapsed;
                        this.imagesMain.Visibility = Visibility.Visible;
                        this.images.Children.Clear();
                        int count = 1;
                        foreach (var name in this.chooseImage.FileNames) {
                            TextBlock textBlock = new TextBlock();
                            textBlock.Text = String.Format("{0}- {1}", count, name);
                            this.images.Children.Add(textBlock);
                            count++;
                        }
                    } else {
                        this.turnLeft.Visibility = Visibility.Visible;
                        this.turnRight.Visibility = Visibility.Visible;
                        this.image.Visibility = Visibility.Visible;
                        this.imagesMain.Visibility = Visibility.Collapsed;
                        AppLibrary.Image.setImage(this.image, this.chooseImage.FileName);
                    }
                }
            });
        }
        private void turnImage(object sender, RoutedEventArgs e) {
            AppLibrary.Values.logger.loggerFunction(() => {
                string rotate = ((Button)sender).Name.Replace("turn", "");

                BitmapImage bi = new BitmapImage();
                bi.BeginInit();
                bi.CreateOptions = BitmapCreateOptions.IgnoreImageCache;
                bi.CacheOption = BitmapCacheOption.OnLoad;
                if (config.Values.selectedItemId > 0) {
                    bi.UriSource = new Uri(config.Values.pathUploadsProductsFolder(list.selectedGroupId) + this.imageName);
                }

                if (!(AppLibrary.Var.isNullOrEmpty(this.chooseImage) || AppLibrary.Var.isNullOrEmpty(this.chooseImage.FileName))) {
                    bi.UriSource = new Uri(this.chooseImage.FileName);
                }
                bi.EndInit();

                TransformedBitmap tb = new TransformedBitmap();
                tb.BeginInit();
                tb.Source = bi;
                RotateTransform transform = new RotateTransform();
                switch (rotate)
                {
                    case "Left":
                        transform.Angle = this.imageRotate - 90;
                        break;
                    case "Right":
                        transform.Angle = this.imageRotate + 90;
                        break;
                }
                this.imageRotate = transform.Angle;
                tb.Transform = transform;
                tb.EndInit();
                this.image.Source = tb;
            });
        }
    }
}
