using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using System.Threading;

namespace Photo_Order_Customer
{
    /// <summary>
    /// Interaction logic for WindowSlider.xaml
    /// </summary>
    public partial class WindowSlider : Window {
        List<config.db.functions.Select.ListProduct> listProduct = new List<config.db.functions.Select.ListProduct>();
        int showIndex = 0;
        int imageChangeSeconds = 60;
        Thread thread;
        public WindowSlider() {
            InitializeComponent();
            this.getProducts();
            thread = new Thread(setImage);
            thread.Start();
        }
        public void getProducts() {
            AppLibrary.Values.logger.loggerFunction(() => {
                this.listProduct = (new config.db.functions.Select()).getProducts(orderBy: "desc", limit: 2000, isSliderActive: 1);
            });
        }
        public void setImage() {
            AppLibrary.Values.logger.loggerFunction(() =>
            {
                while (true)
                {
                    this.Dispatcher.BeginInvoke(new Action(() => {
                        if (this.listProduct.Count > 0) {
                            var product = listProduct[this.showIndex];
                            AppLibrary.Image.setImage(this.image, config.Values.pathUploadsProductsFolder(product.groupId) + product.image);
                            if (this.showIndex == this.listProduct.Count - 1)
                            {
                                this.showIndex = 0;
                            }
                            else
                            {
                                this.showIndex++;
                            }
                        }else {
                            emptySlilder.Visibility = Visibility.Visible;
                        }
                    }));
                    Thread.Sleep(TimeSpan.FromSeconds(this.imageChangeSeconds));
                    AppLibrary.GC.GCCollect();
                }
            });
        }

        private void windowClosing(object sender, System.ComponentModel.CancelEventArgs e) {
            this.thread.Abort();
        }

        private void windowMouseDown(object sender, MouseButtonEventArgs e) {
            this.Close();
        }
    }
}
