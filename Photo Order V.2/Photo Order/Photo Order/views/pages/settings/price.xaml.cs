using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Photo_Order.views.pages.settings
{
    /// <summary>
    /// Interaction logic for price.xaml
    /// </summary>
    public partial class price : Page
    {
        public price() {
            InitializeComponent();
        }
        public void initPage() {
            this.priceTurkishLira.Text = AppLibrary.Var.toStringDecimalFormat(AppLibrary.Values.settings.json.priceTurkishLira);
            this.priceDollar.Text = AppLibrary.Var.toStringDecimalFormat(AppLibrary.Values.settings.json.priceDollar);
            this.priceEuro.Text = AppLibrary.Var.toStringDecimalFormat(AppLibrary.Values.settings.json.priceEuro);
            this.pricePound.Text = AppLibrary.Var.toStringDecimalFormat(AppLibrary.Values.settings.json.pricePound);
        }
        private void save(object sender, RoutedEventArgs e) {
            AppLibrary.Values.logger.loggerFunction(() =>
            {
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
                    AppLibrary.Values.settings.json.priceTurkishLira = Convert.ToDouble(priceTurkishLira);
                    AppLibrary.Values.settings.json.priceDollar = Convert.ToDouble(priceDollar);
                    AppLibrary.Values.settings.json.priceEuro = Convert.ToDouble(priceEuro);
                    AppLibrary.Values.settings.json.pricePound = Convert.ToDouble(pricePound);
                    AppLibrary.Values.settings.write();
                    config.Values.tabControl.pageProducts.initPage();
                    MessageBox.Show("İşlem kayıt edildi.", "Kaydedildi", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            });
        }
        private void numberPreviewTextInput(object sender, TextCompositionEventArgs e) => AppLibrary.Element.numberValidationTextBox(sender, e);
    }
}
