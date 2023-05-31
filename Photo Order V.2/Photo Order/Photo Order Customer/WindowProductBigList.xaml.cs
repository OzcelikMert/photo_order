using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Photo_Order_Customer
{
    /// <summary>
    /// Interaction logic for WindowProductBigList.xaml
    /// </summary>
    public partial class WindowProductBigList : Window
    {
        public views.tools.VirtualKeyboard virtualKeyboard = new views.tools.VirtualKeyboard();
        private List<config.db.functions.Select.ListProduct> listProducts { get; set; }
        public static int selectedIndex = 0;
        private string pathImage { get; set; }
        private string pathImageHigh { get; set; }
        private double imageRotate = 0;
        private bool result = false;
        public WindowProductBigList(List<config.db.functions.Select.ListProduct> listProducts, long id = 0) {
            InitializeComponent();
            setSelectedImage();
            setImageSize();
            this.gridMain.Children.Add(views.tools.VirtualKeyboard.createKeyboard(virtualKeyboard));
            virtualKeyboard.hideKeyboard();
            this.count.PreviewTextInput += AppLibrary.Element.numberValidationTextBoxInt;
            this.count.GotFocus += virtualKeyboard.textboxFocus;
            this.listProducts = listProducts;
            selectedIndex = listProducts.FindIndex((item) => item.id == id);
            selectedIndex = (selectedIndex < 0) ? 0 : selectedIndex;
            this.setProductCount();
            this.setImage();
        }
        private void setProductCount() {
            int totalProductCount = 0;
            config.Values.listBasket.ForEach((item) => { totalProductCount += item.count; });
            this.totalCount.Text = totalProductCount.ToString();
        }
        private void setImageSize() {
            double maxWidth = System.Windows.SystemParameters.PrimaryScreenWidth;
            double maxHeight = System.Windows.SystemParameters.PrimaryScreenHeight;
            this.image.Height = maxHeight;
            this.image.Width = maxWidth - 285;
        }
        private void setSelectedImage() {
            BitmapImage bi = new BitmapImage();
            bi.BeginInit();
            bi.UriSource = new Uri(config.Values.pathImagesFolder + "success-tick.png");
            bi.EndInit();
            this.imageSelected.Source = bi;
        }
        private void setImage() {
            AppLibrary.Values.logger.loggerFunction(() => {
                this.imageRotate = 0;
                this.count.Text = "1";
                var product = listProducts[selectedIndex];
                this.pathImage = config.Values.pathUploadsProductsFolder(product.groupId) + product.image;
                this.pathImageHigh = config.Values.remotePaths.json.pathImageHigh + product.imageHigh;
                AppLibrary.Image.setImage(this.image, this.pathImage);
                this.checkIsInBasket();
                AppLibrary.GC.GCCollect();
            });
        }
        private void checkIsInBasket() {
            var product = listProducts[selectedIndex];
            int index = config.Values.listBasket.FindIndex((item) => item.productImage == product.image);
            if (index != -1) {
                var basketItem = config.Values.listBasket[index];
                this.count.Text = basketItem.count.ToString();
                this.gridSelected.Visibility = Visibility.Visible;
                this.addBasketButton.Visibility = Visibility.Collapsed;
                this.removeBasketButton.Visibility = Visibility.Visible;
            } else { 
                this.gridSelected.Visibility = Visibility.Collapsed;
                this.addBasketButton.Visibility = Visibility.Visible;
                this.removeBasketButton.Visibility = Visibility.Collapsed;
            }
        }
        private void addBasket(object sender, RoutedEventArgs e) {
            AppLibrary.Values.logger.loggerFunction(() => {
                if (this.count.Text.Length < 1) return;
                if (config.Values.customerId > 0 && (new config.db.functions.Select()).getCustomer(customerId: config.Values.customerId) == 0)
                {
                    MessageBox.Show(
                                    AppLibrary.App.getLanguageText("langAccountNotFound"),
                                    AppLibrary.App.getLanguageText("langSessionStatus"),
                                    MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                var product = listProducts[selectedIndex];
                int findIndex = config.Values.listBasket.FindIndex((item) => item.productImage == product.image);
                if (findIndex != -1) {
                    if (MessageBox.Show(
                                 AppLibrary.App.getLanguageText("langDeleteFromBasketQuestion"),
                                 AppLibrary.App.getLanguageText("langDeleteProduct"),
                                 MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                    {
                        if (config.Values.customerId > 0) (new config.db.functions.Delete()).setBasket(product.id);
                        config.Values.listBasket.RemoveAt(findIndex);
                    }
                } else {
                    long insertId = 0;
                    if (config.Values.customerId > 0) insertId = (new config.db.functions.Insert()).setBasket(product.id, config.Values.customerId, Convert.ToInt32(this.count.Text));
                    config.Values.listBasket.Add(new config.db.functions.Select.ListBasket {
                        id = insertId,
                        productGroupId = product.groupId,
                        productId = product.id,
                        count = Convert.ToInt32(this.count.Text),
                        createDate = (DateTime.Now).ToString("yyyy-MM-dd"),
                        productImage = product.image,
                        productImageHigh = product.imageHigh,
                        productOwnerName = product.ownerName,
                        productOwnerId = product.ownerId,
                        priceTurkishLira = product.priceTurkishLira,
                        pricePound = product.pricePound,
                        priceDollar = product.priceDollar,
                        priceEuro = product.priceEuro
                    });
                }
                
                this.checkIsInBasket();
                this.setProductCount();
                config.Values.tabControl.setBasketCount();
                this.result = true;
            });
        }
        private void getImage(object sender, RoutedEventArgs e) {
            AppLibrary.Values.logger.loggerFunction(() => {
                this.rotateImage();
                string nextType = ((Button)sender).Name;
                switch (nextType) {
                    case "before": if (selectedIndex > 0) selectedIndex--; break;
                    case "after": if (selectedIndex < this.listProducts.Count - 1) selectedIndex++; break;
                }
                this.setImage();
            });
        }
        private void turnImage(object sender, RoutedEventArgs e) {
            AppLibrary.Values.logger.loggerFunction(() => {
                string rotate = ((Button)sender).Name.Replace("turn", "");

                BitmapImage bi = new BitmapImage();
                bi.BeginInit();
                bi.CreateOptions = BitmapCreateOptions.IgnoreImageCache;
                bi.CacheOption = BitmapCacheOption.OnLoad;
                bi.UriSource = new Uri(this.pathImage);
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
        private void rotateImage() {
            if (this.imageRotate != 0) {
                RotateFlipType rotateFlipType = AppLibrary.Image.convertToFlipRotate(Convert.ToInt32(this.imageRotate));
                AppLibrary.File.rotateImage(this.pathImage, rotateFlipType);
                AppLibrary.File.rotateImage(this.pathImageHigh, rotateFlipType);
                this.result = true;
            }
        }
        private void close(object sender, RoutedEventArgs e) => this.Close();
        private void windowClosing(object sender, System.ComponentModel.CancelEventArgs e) {
            this.rotateImage();
            this.DialogResult = this.result;
        }
        private void windowMouseMove(object sender, MouseEventArgs e)
        {
            WindowLanguage.sleepSeconds = 0;
        }
    }
}
