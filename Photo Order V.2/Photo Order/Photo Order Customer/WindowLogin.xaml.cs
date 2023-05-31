using System.Windows;
using System.Windows.Input;

namespace Photo_Order_Customer
{
    /// <summary>
    /// Interaction logic for WindowLogin.xaml
    /// </summary>
    public partial class WindowLogin : Window
    {
        private bool isNewAccount = false;
        private bool hideVisitorLogin { get; set; }
        public views.tools.VirtualKeyboard virtualKeyboard = new views.tools.VirtualKeyboard();
        public WindowLogin(bool hideVisitorLogin = false) {
            InitializeComponent();
            this.hideVisitorLogin = hideVisitorLogin;
            if (this.hideVisitorLogin) {
                this.loginVisitor.Visibility = Visibility.Collapsed;
            }
            this.gridMain.Children.Add(views.tools.VirtualKeyboard.createKeyboard(virtualKeyboard));
            virtualKeyboard.hideKeyboard();
            this.name.GotFocus += virtualKeyboard.textboxFocus;
            this.email.GotFocus += virtualKeyboard.textboxFocus;
            this.room.GotFocus += virtualKeyboard.textboxFocus;
            this.password.GotFocus += virtualKeyboard.textboxFocus;
            this.stackPanelNewAccount.Visibility = Visibility.Hidden;
        }

        private void clickCreateNewAccount(object sender, RoutedEventArgs e) {
            this.isNewAccount = !this.isNewAccount;
            if (this.isNewAccount) {
                this.stackPanelNewAccount.Visibility = Visibility.Visible;
                if (!this.hideVisitorLogin) this.loginVisitor.Visibility = Visibility.Collapsed;
                this.createNewAccount.Visibility = Visibility.Collapsed;
                this.login.Visibility = Visibility.Collapsed;
                this.alreadyHaveAccount.Visibility = Visibility.Visible;
                this.createAndLogin.Visibility = Visibility.Visible;
            }
            else {
                this.stackPanelNewAccount.Visibility = Visibility.Hidden;
                if (!this.hideVisitorLogin) this.loginVisitor.Visibility = Visibility.Visible;
                this.createNewAccount.Visibility = Visibility.Visible;
                this.login.Visibility = Visibility.Visible;
                this.alreadyHaveAccount.Visibility = Visibility.Collapsed;
                this.createAndLogin.Visibility = Visibility.Collapsed;
            }
        }

        private void clickLogin(object sender, RoutedEventArgs e) {
            AppLibrary.Values.logger.loggerFunction(() =>
            {
                string room = this.room.Text.ToString(),
                password = this.password.Password.ToString();
                if (this.isNewAccount)
                {
                    string name = this.name.Text.ToString(),
                        email = this.email.Text.ToString();
                    if ((new config.db.functions.Select()).getCustomer(room, password) > 0)
                    {
                        MessageBox.Show(
                            AppLibrary.App.getLanguageText("langAlreadyRoomNumber"),
                            AppLibrary.App.getLanguageText("langLogin"),
                            MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                    config.Values.customerId = (new config.db.functions.Insert()).setCustomer(name, email, room, password);
                    this.DialogResult = true;
                    this.Close();
                }
                else
                {
                    config.Values.customerId = (new config.db.functions.Select()).getCustomer(room, password);
                    if (config.Values.customerId == 0)
                    {
                        MessageBox.Show(
                            AppLibrary.App.getLanguageText("langWrongRoomNumberOrPassword"),
                            AppLibrary.App.getLanguageText("langLogin"),
                            MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                    this.DialogResult = true;
                    this.Close();
                }
            });
        }

        private void clickLoginVisitor(object sender, RoutedEventArgs e) {
            config.Values.customerId = 0;
            this.DialogResult = true;
            this.Close();
        }
        private void clickClose(object sender, RoutedEventArgs e) {
            this.Close();
        }
        private void windowMouseMove(object sender, MouseEventArgs e)
        {
            WindowLanguage.sleepSeconds = 0;
        }
    }
}
