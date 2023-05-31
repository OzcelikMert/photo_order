using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Photo_Order {
    /// <summary>
    /// MainWindow.xaml etkileşim mantığı
    /// </summary>
    public partial class WindowMain : Window
    {
        public WindowMain() {
            InitializeComponent();
            if (Process.GetProcessesByName(AppController.Values.appMainName.Replace(".exe", "")).Length > 1) {
                // If ther is more than one, than it is already running.
                System.Windows.Application.Current.Shutdown();
            }
            bool status = true;
            if (AppLibrary.Var.isNullOrEmpty(config.Values.ipV4Address)) {
                MessageBox.Show("Lütfen bir ağa bağlı olduğunuzdan emin olunuz!", "Ağ Bulunamadı", MessageBoxButton.OK, MessageBoxImage.Error);
                status = false;
            }

            AppLibrary.Values.logger.loggerFunction(() => {
                if (status && !this.checkApp()) {
                    status = false;
                }
            });

            AppLibrary.Values.logger.loggerFunction(() => {
                if (!status) return;
                if(!Directory.Exists(config.Values.pathUploadsFolder)) Directory.CreateDirectory(config.Values.pathUploadsFolder);
                AppLibrary.Values.dbInfo = new AppLibrary.db.DBInfo(config.Values.pathUserDocumentFolder, config.Values.ipV4Address);
                AppLibrary.Values.settings = new AppLibrary.Settings(config.Values.pathUserDocumentFolder);
                (new config.db.Create()).init();
                config.Values.tabControl = new ComponentTabControl(this.backgroundLogo);
                this.gridMain.Children.Add(config.Values.tabControl.controlMain);
                if (!AppLibrary.Var.isNullOrEmpty(AppLibrary.Values.settings.json.backgroundLogo)) AppLibrary.Image.setImage(this.backgroundLogo, config.Values.pathUploadsBackgroundFolder + AppLibrary.Values.settings.json.backgroundLogo);
            });

            if(!status) Application.Current.Shutdown();
        }
        private bool checkApp() {
            bool status = true;
            if(AppLibrary.Var.isNullOrEmpty(
                Properties.Settings.Default.licenseDateEnd
            )) {
                status = false;
            }

            if (AppController.Network.isThere()) {
                AppController.License license = new AppController.License(AppLibrary.App.getProcessId());
                if (license.isConnected) {
                    Properties.Settings.Default.licenseDateEnd = license.result.licenseDateEnd;
                    Properties.Settings.Default.Save();
                    if (license.result.errorCode == AppController.License.ErrorCodes.WrongLicenseCode) {
                        status = false;
                        MessageBox.Show(license.result.message, "Lisans Onaylama", MessageBoxButton.OK, MessageBoxImage.Error);
                    }else {
                        status = true;
                    }
                }
                if (status) {
                    AppController.Version version = new AppController.Version();
                    if (version.isConnected) {
                        if(!(version.result.app == AppController.Values.versionApp && version.result.db == AppController.Values.versionDB)) {
                            status = false;
                            ProcessStartInfo processStartInfo = new ProcessStartInfo {
                                Arguments = AppController.Values.appMainName,
                                WindowStyle = ProcessWindowStyle.Hidden,
                                CreateNoWindow = false,
                                FileName = AppController.Values.appUpdaterName,
                            };
                            Process.Start(processStartInfo);
                        }
                    }
                }
            } else {
                if(!status) {
                    MessageBox.Show("Programin lisansını onaylamak için lütfen bir internete bağlanın\n(Sadece ilk yükleme sonrası ilk girişte gereklidir)", "Lisans Onaylama", MessageBoxButton.OK, MessageBoxImage.Error);
                }else {
                    if ((Convert.ToDateTime(Properties.Settings.Default.licenseDateEnd) - DateTime.Now).Days < 0) {
                        status = false;
                        MessageBox.Show("Programın lisans süresi dolmuştur! Lütfen süresini uzatmak için destek ekibi ile iletişime geçiniz.", "Lisans Onaylama", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }

            return status;
        }
        public class ComponentTabControl {
            public TabControl controlMain { get; set; }
            public views.pages.customers.list pageCustomers = new views.pages.customers.list();
            public views.pages.product.list pageProducts = new views.pages.product.list();
            public views.pages.productOwner.list pageProductOwners = new views.pages.productOwner.list();
            public views.pages.events.list pageEvents = new views.pages.events.list();
            public views.pages.orders.list pageOrdersList = new views.pages.orders.list();
            public views.pages.orders.old pageOrdersOld = new views.pages.orders.old();
            public views.pages.settings.general pageSettingsGeneral = new views.pages.settings.general();
            public views.pages.settings.price pageSettingsPrice = new views.pages.settings.price();
            public ComponentTabControl(Image image) {
                TabControl tabControlMain = this.tabControlMain();
                TabControl tabControlOrders = this.tabControlOrders();
                tabControlOrders.Items.Add(this.tabItem("Aktif", this.pageOrdersList));
                tabControlOrders.Items.Add(this.tabItem("Ödenmiş", this.pageOrdersOld));
                TabControl tabControlSettings = this.tabControlSettings();
                this.pageSettingsGeneral.images.Add(image);
                tabControlSettings.Items.Add(this.tabItem("Genel", this.pageSettingsGeneral));
                tabControlSettings.Items.Add(this.tabItem("Fiyatlar", this.pageSettingsPrice));

                tabControlMain.Items.Add(this.tabItem("Fotoğraflar", this.pageProducts));
                tabControlMain.Items.Add(this.tabItem("Fotoğrafçılar", this.pageProductOwners));
                tabControlMain.Items.Add(this.tabItem("Etkinlikler", this.pageEvents));
                tabControlMain.Items.Add(this.tabItem("Siparişler", tabControlOrders));
                tabControlMain.Items.Add(this.tabItem("Müşteriler", this.pageCustomers));
                tabControlMain.Items.Add(this.tabItem("Ayarlar", tabControlSettings));

                this.controlMain = tabControlMain;
            }
            private TabControl tabControlMain() {
                TabControl tabControl = new TabControl();
                tabControl.Background = Brushes.Transparent;
                tabControl.SelectionChanged += (o, e) => {
                    switch (tabControl.SelectedIndex) {
                        case 3: this.pageOrdersList.initPage(); break;
                        case 4: this.pageCustomers.initPage(); break;
                        case 5: 
                            this.pageSettingsGeneral.initPage(); 
                            this.pageSettingsPrice.initPage(); 
                            break;
                    }
                };
                return tabControl;
            }
            private TabControl tabControlSettings() {
                TabControl tabControl = new TabControl();
                tabControl.Background = Brushes.Transparent;
                return tabControl;
            }
            private TabControl tabControlOrders() {
                TabControl tabControl = new TabControl();
                tabControl.Background = Brushes.Transparent;
                tabControl.SelectionChanged += (o, e) => {
                    switch (tabControl.SelectedIndex) {
                        case 0: this.pageOrdersList.initPage(); break;
                        case 1: this.pageOrdersOld.initPage(); break;
                    }
                };
                return tabControl;
            }
            private TabItem tabItem(string header, object content) {
                TabItem tabItem = new TabItem();
                tabItem.Header = header;
                tabItem.Width = 100;
                tabItem.Background = Brushes.Transparent;
                Frame frame = new Frame();
                frame.Navigate(content);
                tabItem.Content = frame;
                return tabItem;
            }
        }
    }
}
