using System;
using System.Windows;
using System.Windows.Controls;

namespace Photo_Order.views.pages.orders
{
    public partial class edit : Page {
        public edit() {
            InitializeComponent();
            this.setInputs();
        }
        private void setInputs() {
            AppLibrary.Values.logger.loggerFunction(() => {
                var orderProducts = (new config.db.functions.Select()).getOrderProduct(id: config.Values.selectedItemId);
                foreach (var orderProduct in orderProducts)
                {
                    this.priceTurkishLira.Text = orderProduct.priceTurkishLira.ToString();
                    this.priceDollar.Text = orderProduct.priceDollar.ToString();
                    this.priceEuro.Text = orderProduct.priceEuro.ToString();
                    this.pricePound.Text = orderProduct.pricePound.ToString();
                }
            });
        }
        private void saveClick(object sender, RoutedEventArgs e) {
            AppLibrary.Values.logger.loggerFunction(() => {
                string priceTurkishLira = this.priceTurkishLira.Text.ToString(),
                    priceDollar = this.priceDollar.Text.ToString(),
                    priceEuro = this.priceEuro.Text.ToString(),
                    pricePound = this.pricePound.Text.ToString();
                if (AppLibrary.Var.isNullOrEmpty(priceTurkishLira, priceDollar, priceEuro, pricePound))
                {
                    MessageBox.Show("Lütfen gerekli yerleri doldurunuz!", "Hatalı Giriş", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    if (MessageBox.Show("Urunu guncellemek istediginizden emin misiniz?", "Siparis Urunu Guncelleme", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                    {
                        (new config.db.functions.Update()).setOrderProduct(config.Values.selectedItemId,
                            Convert.ToDouble(priceTurkishLira),
                            Convert.ToDouble(priceDollar),
                            Convert.ToDouble(priceEuro),
                            Convert.ToDouble(pricePound)
                        );

                        MessageBox.Show("İşlem başarılı urun guncellendi.", "Siparis Urunu Guncelleme", MessageBoxButton.OK, MessageBoxImage.Information);
                        config.Values.refreshList = true;
                        config.Values.windowEdit.Close();
                    }

                }
            });
        }
    }
}
