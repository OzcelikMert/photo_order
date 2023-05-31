using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Photo_Order_Customer
{
    public partial class WindowMain : Window {
        public static views.tools.VirtualKeyboard virtualKeyboard = new views.tools.VirtualKeyboard();
        static Grid _preLoader;
        static ProgressBar _progressBar;
        public WindowMain() {
            InitializeComponent();
            _preLoader = this.preLoader;
            _progressBar = this.preLoaderProgress;
            AppLibrary.Values.logger.loggerFunction(() => {
                AppLibrary.Values.settings = new AppLibrary.Settings(config.Values.remotePaths.json.pathPublicDocument);
                config.Values.listBasket = (config.Values.customerId > 0)
                    ? (new config.db.functions.Select()).getBasket(config.Values.customerId)
                    : new List<config.db.functions.Select.ListBasket>();
                this.setTabControl();
                this.gridMain.Children.Add(views.tools.VirtualKeyboard.createKeyboard(virtualKeyboard));
                virtualKeyboard.hideKeyboard();
                if (!AppLibrary.Var.isNullOrEmpty(AppLibrary.Values.settings.json.backgroundLogo)) AppLibrary.Image.setImage(this.backgroundLogo, config.Values.pathUploadsBackgroundFolder + AppLibrary.Values.settings.json.backgroundLogo);
            });
        }
        public static void showPreLoader(int maxProgressValue) {
            _preLoader.Visibility = Visibility.Visible;
            _progressBar.Maximum = maxProgressValue;
            _progressBar.Minimum = 0;
            _progressBar.Value = _progressBar.Minimum;
        }
        public static void updateProgress(int value) {
            _progressBar.Value = value;
        }
        public static void hidePreLoader()
        {
            _preLoader.Visibility = Visibility.Collapsed;
        }
        private void setTabControl() {
            config.Values.tabControl = new ComponentTabControl();
            this.gridMain.Children.Add(config.Values.tabControl.control);
        }
        private void clickClose(object sender, RoutedEventArgs e) {
            this.Close();
        }
        private void windowClosing(object sender, CancelEventArgs e) {
            if (MessageBox.Show(
                AppLibrary.App.getLanguageText("langLogoutQuestion"),
                AppLibrary.App.getLanguageText("langLogout"), 
                MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes) {
                config.Values.customerId = 0;
            } else {
                e.Cancel = true;
            }
        }

        public enum TabPages {
            Language,
            Search,
            Products,
            Basket
        }
        public class ComponentTabControl {
            public TabControl control { get; set; }
            public TabItem itemSearch { get; set; }
            public TabItem itemProducts { get; set; }
            public TabItem itemBasket { get; set; }
            public views.pages.search.form pageSearch = new views.pages.search.form();
            public views.pages.products.list pageProducts = new views.pages.products.list();
            public views.pages.basket.list pageBasket = new views.pages.basket.list();
            private TextBlock basketCountElement { get; set; }
            public ComponentTabControl() {
                this.itemSearch = this.tabItemSearch();
                this.itemProducts = this.tabItemProducts();
                this.itemProducts.IsEnabled = false;
                this.itemBasket = this.tabItemBasket();
                TabControl tabControlMain = this.tabControlMain();
                tabControlMain.Items.Add(this.itemSearch);
                tabControlMain.Items.Add(this.itemProducts);
                tabControlMain.Items.Add(this.itemBasket);
                this.control = tabControlMain;
            }
            public void setPage(TabPages page) {
                switch (page) {
                    case TabPages.Search:
                        this.control.SelectedIndex = 0;
                        break;
                    case TabPages.Products:
                        this.control.SelectedIndex = 1;
                        this.itemProducts.IsEnabled = true;
                        break;
                    case TabPages.Basket:
                        this.control.SelectedIndex = 2;
                        break;
                }
            }
            public void setBasketCount()
            {
                int totalCount = 0;
                foreach (var item in config.Values.listBasket) {
                    totalCount += item.count;
                }
                this.basketCountElement.Text = String.Format("( {0} )", totalCount);
            }
            private TabControl tabControlMain() {
                TabControl tabControl = new TabControl();
                tabControl.Background = Brushes.Transparent;
                tabControl.Margin = new Thickness(0, 25, 0, 0);
                tabControl.SelectionChanged += (o, e) => {
                    switch (this.control.SelectedIndex) {
                        case 2: this.pageBasket.initPage(); break;
                    }
                };
                return tabControl;
            }
            private TabItem tabItemSearch() {
                // Content
                Grid grid = new Grid();
                grid.Width = 300;
                TextBlock textBlock = new TextBlock();
                textBlock.Text = AppLibrary.App.getLanguageText("langSearchAnotherPhotos");
                textBlock.VerticalAlignment = VerticalAlignment.Center;
                textBlock.HorizontalAlignment = HorizontalAlignment.Center;
                Image image = new Image();
                image.Height = 30;
                image.HorizontalAlignment = HorizontalAlignment.Left;
                image.VerticalAlignment = VerticalAlignment.Center;
                image.Margin = new Thickness(5, 0, 0, 0);
                image.Source = new BitmapImage(new Uri(@"/assets/images/icons/buttons/search.png", UriKind.Relative));
                grid.Children.Add(textBlock);
                grid.Children.Add(image);

                return this.tabItem(grid, pageSearch);
            }

            private TabItem tabItemProducts() {
                // Content
                Grid grid = new Grid();
                grid.Width = 300;
                TextBlock textBlock = new TextBlock();
                textBlock.Text = AppLibrary.App.getLanguageText("langPhotos");
                textBlock.VerticalAlignment = VerticalAlignment.Center;
                textBlock.HorizontalAlignment = HorizontalAlignment.Center;
                Image image = new Image();
                image.Height = 30;
                image.HorizontalAlignment = HorizontalAlignment.Left;
                image.VerticalAlignment = VerticalAlignment.Center;
                image.Margin = new Thickness(5, 0, 0, 0);
                image.Source = new BitmapImage(new Uri(@"/assets/images/icons/buttons/pictures.png", UriKind.Relative));
                grid.Children.Add(textBlock);
                grid.Children.Add(image);

                return this.tabItem(grid, pageProducts);
            }
            private TabItem tabItemBasket()
            {
                // Content
                Grid grid = new Grid();
                grid.Width = 300;
                TextBlock textBlock = new TextBlock();
                textBlock.Text = AppLibrary.App.getLanguageText("langBasket");
                textBlock.VerticalAlignment = VerticalAlignment.Center;
                textBlock.HorizontalAlignment = HorizontalAlignment.Center;
                Image image = new Image();
                image.Height = 30;
                image.HorizontalAlignment = HorizontalAlignment.Left;
                image.VerticalAlignment = VerticalAlignment.Center;
                image.Margin = new Thickness(5, 0, 0, 0);
                image.Source = new BitmapImage(new Uri(@"/assets/images/icons/buttons/cart.png", UriKind.Relative));
                basketCountElement = new TextBlock();
                basketCountElement.Text = "0";
                basketCountElement.FontSize = 18;
                basketCountElement.VerticalAlignment = VerticalAlignment.Center;
                basketCountElement.HorizontalAlignment = HorizontalAlignment.Right;
                basketCountElement.Margin = new Thickness(0, 0, 5, 0);
                this.setBasketCount();
                grid.Children.Add(textBlock);
                grid.Children.Add(image);
                grid.Children.Add(basketCountElement);

                return this.tabItem(grid, pageBasket);
            }

            private TabItem tabItem(object header, object content) {
                TabItem tabItem = new TabItem();
                tabItem.Header = header;
                tabItem.Width = 300;
                tabItem.Height = 45;
                tabItem.FontSize = 15;
                tabItem.Background = Brushes.Transparent;
                Frame frame = new Frame();
                frame.Navigate(content);
                tabItem.Content = frame;
                return tabItem;
            }
        }
        private void windowMouseMove(object sender, MouseEventArgs e)
        {
            WindowLanguage.sleepSeconds = 0;
        }
    }
}
