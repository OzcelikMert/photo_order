using System.Windows;
using System.Windows.Controls;

namespace Photo_Order.views.pages.customers {
    public partial class edit : Page {
        public edit() {
            InitializeComponent();
        }
        private void saveClick(object sender, RoutedEventArgs e) {
            AppLibrary.Values.logger.loggerFunction(() =>
            {
                string newPassword = this.newPassword.Password.ToString();
                if (AppLibrary.Var.isNullOrEmpty(newPassword))
                {
                    MessageBox.Show("Lütfen gerekli yerleri doldurunuz!", "Hatalı Giriş", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    if (MessageBox.Show("Şifreyi guncellemek istediginizden emin misiniz?", "Müşteri Şifresi Güncelleme", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                    {
                        (new config.db.functions.Update()).setCustomer(config.Values.selectedItemId, newPassword);

                        MessageBox.Show("İşlem başarılı şifre güncellendi.", "Müşteri Şifresi Güncelleme", MessageBoxButton.OK, MessageBoxImage.Information);
                        config.Values.refreshList = true;
                        config.Values.windowEdit.Close();
                    }

                }
            });
        }
    }
}
