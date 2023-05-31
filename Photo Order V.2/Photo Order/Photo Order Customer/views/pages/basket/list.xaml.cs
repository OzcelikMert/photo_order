using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Photo_Order_Customer.views.pages.basket {
    public partial class list : Page {
        double totalPriceTurkishLira = 0,
            totalPriceDollar = 0,
            totalPriceEuro = 0,
            totalPricePound = 0,
            totalProductCount = 0;
        List<ComponentBasket> componentList = new List<ComponentBasket>();
        public list() {
            InitializeComponent();
        }
        public void initPage() {
            this.clearPage();
            this.getBasket();
            this.setAcceptButton();
            AppLibrary.GC.GCCollect();
        }
        private void setAcceptButton() {
            if (config.Values.customerId == 0) {
                this.acceptBasketButton.Visibility = Visibility.Visible;
                this.giveOrderButton.Visibility = Visibility.Collapsed;
            } else {
                this.acceptBasketButton.Visibility = Visibility.Collapsed;
                this.giveOrderButton.Visibility = Visibility.Visible;
            }
        }
        public void clearPage() {
            this.componentList.ForEach(item => {
                item.img.Source = null;
                item.img.UpdateLayout();
            });
            this.basketList.Children.Clear();
            this.componentList.Clear();
        }
        private void acceptBasket(object sender, RoutedEventArgs e) {
            AppLibrary.Values.logger.loggerFunction(() =>
            {
                if (this.componentList.Count == 0) return;
                if (config.Values.customerId == 0) {
                    WindowLogin windowLogin = new WindowLogin(true);
                    if (windowLogin.ShowDialog() == true)
                    {
                        foreach (var component in this.componentList) {
                            (new config.db.functions.Insert()).setBasket(component.productId, config.Values.customerId, component.count);
                        }
                        config.Values.listBasket.Clear();
                        config.Values.listBasket = (new config.db.functions.Select()).getBasket(config.Values.customerId);
                        config.Values.tabControl.pageProducts.initAddedIcon();
                        this.initPage();
                    }
                    return;
                }
                if (MessageBox.Show(
                    AppLibrary.App.getLanguageText("langAcceptBasketQuestion"),
                    AppLibrary.App.getLanguageText("langAcceptBasket"),
                    MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    if ((new config.db.functions.Select()).getCustomer(customerId: config.Values.customerId) == 0)
                    {
                        MessageBox.Show(
                            AppLibrary.App.getLanguageText("langAccountNotFound"),
                            AppLibrary.App.getLanguageText("langSessionStatus"),
                            MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                    var oldOrder = (new config.db.functions.Select()).getOrders(customerId: config.Values.customerId);
                    long orderId = (oldOrder.Count > 0) ? oldOrder[0].id : (new config.db.functions.Insert()).setOrder(config.Values.customerId);
                    List<config.db.functions.Insert.OrderProduct> orderProducts = new List<config.db.functions.Insert.OrderProduct>();
                    foreach (var item in config.Values.listBasket) {
                        item.priceTurkishLira = (item.priceTurkishLira == 0) ? AppLibrary.Values.settings.json.priceTurkishLira : item.priceTurkishLira;
                        item.priceDollar = (item.priceDollar == 0) ? AppLibrary.Values.settings.json.priceDollar : item.priceDollar;
                        item.priceEuro = (item.priceEuro == 0) ? AppLibrary.Values.settings.json.priceEuro : item.priceEuro;
                        item.pricePound = (item.pricePound == 0) ? AppLibrary.Values.settings.json.pricePound : item.pricePound;
                        orderProducts.Add(new config.db.functions.Insert.OrderProduct {
                            orderId = orderId,
                            count = item.count,
                            productId = item.productId,
                            priceTurkishLira = item.priceTurkishLira,
                            priceDollar = item.priceDollar,
                            priceEuro = item.priceEuro,
                            pricePound = item.pricePound
                        });
                    }
                    (new config.db.functions.Insert()).setOrderProduct(orderProducts);
                    (new config.db.functions.Delete()).setBasket(customerId: config.Values.customerId);
                    config.Values.listBasket.Clear();
                    config.Values.tabControl.pageProducts.initPageUseSelfData();
                    this.initPage();
                    MessageBox.Show(
                        AppLibrary.App.getLanguageText("langAcceptedBasket"),
                        AppLibrary.App.getLanguageText("langAcceptBasket"),
                        MessageBoxButton.OK, MessageBoxImage.Information);
                }
            });
        }
        private void calculatePriceAndCount() {
            this.totalPriceTurkishLira = 0;
            this.totalPriceDollar = 0;
            this.totalPriceEuro = 0;
            this.totalPricePound = 0;
            this.totalProductCount = 0;
            foreach (var item in config.Values.listBasket) {
                item.priceTurkishLira = (item.priceTurkishLira == 0) ? AppLibrary.Values.settings.json.priceTurkishLira : item.priceTurkishLira;
                item.priceDollar = (item.priceDollar == 0) ? AppLibrary.Values.settings.json.priceDollar : item.priceDollar;
                item.priceEuro = (item.priceEuro == 0) ? AppLibrary.Values.settings.json.priceEuro : item.priceEuro;
                item.pricePound = (item.pricePound == 0) ? AppLibrary.Values.settings.json.pricePound : item.pricePound;

                this.totalPriceTurkishLira += item.priceTurkishLira * item.count;
                this.totalPriceDollar += item.priceDollar * item.count;
                this.totalPriceEuro += item.priceEuro * item.count;
                this.totalPricePound += item.pricePound * item.count;
                this.totalProductCount += item.count;
            }
            this.priceTurkishLira.Text = String.Format("{0} TL", AppLibrary.Var.toStringDecimalFormat(this.totalPriceTurkishLira));
            this.priceDollar.Text = String.Format("{0} Dollar", AppLibrary.Var.toStringDecimalFormat(this.totalPriceDollar));
            this.priceEuro.Text = String.Format("{0} Euro", AppLibrary.Var.toStringDecimalFormat(this.totalPriceEuro));
            this.pricePound.Text = String.Format("{0} Pound", AppLibrary.Var.toStringDecimalFormat(this.totalPricePound));
            this.totalCount.Text = this.totalProductCount.ToString();
            config.Values.tabControl.setBasketCount();
        }
        private void getBasket() {
            AppLibrary.Values.logger.loggerFunction(() => {
                WindowMain.showPreLoader(config.Values.listBasket.Count);
                Task.Run(() => {
                    for (int i = 0; i < config.Values.listBasket.Count; i++) {
                        var item = config.Values.listBasket[i];
                        item.priceTurkishLira = (item.priceTurkishLira == 0) ? AppLibrary.Values.settings.json.priceTurkishLira : item.priceTurkishLira;
                        item.priceDollar = (item.priceDollar == 0) ? AppLibrary.Values.settings.json.priceDollar : item.priceDollar;
                        item.priceEuro = (item.priceEuro == 0) ? AppLibrary.Values.settings.json.priceEuro : item.priceEuro;
                        item.pricePound = (item.pricePound == 0) ? AppLibrary.Values.settings.json.pricePound : item.pricePound;
                        this.Dispatcher.Invoke(() => {
                            this.componentList.Add(new ComponentBasket(
                                 this.basketList,
                                 item.id,
                                 item.productGroupId,
                                 item.count,
                                 item.productId,
                                 item.productImage,
                                 item.productImageHigh,
                                 item.createDate,
                                 item.productOwnerName,
                                 item.priceTurkishLira,
                                 item.priceDollar,
                                 item.priceEuro,
                                 item.pricePound,
                                 (id) => {
                                     var listProducts = new List<config.db.functions.Select.ListProduct>();
                                     config.Values.listBasket.ForEach(basket => {
                                         listProducts.Add(new config.db.functions.Select.ListProduct
                                         {
                                             id = basket.productId,
                                             groupId = basket.productGroupId,
                                             image = basket.productImage,
                                             imageHigh = basket.productImageHigh,
                                             createDate = basket.createDate,
                                             ownerName = basket.productOwnerName,
                                             ownerId = basket.productOwnerId,
                                             priceTurkishLira = basket.priceTurkishLira,
                                             priceDollar = basket.priceDollar,
                                             priceEuro = basket.priceEuro,
                                             pricePound = basket.pricePound
                                         });
                                     });
                                     WindowProductBigList windowProductBigList = new WindowProductBigList(listProducts, id);
                                     if (windowProductBigList.ShowDialog() == true)
                                     {
                                         config.Values.tabControl.pageProducts.initAddedIcon();
                                         this.initPage();
                                     }
                                 },
                                 (id, productId, oldCount, count) => {
                                     if (MessageBox.Show(
                                         AppLibrary.App.getLanguageText("langUpdateProductCount"),
                                         AppLibrary.App.getLanguageText("langUpdateBasket"),
                                         MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes) 
                                     {
                                         if (config.Values.customerId > 0) (new config.db.functions.Update()).setBasket(id, count);
                                         config.Values.listBasket.ForEach(basket => {
                                             if(basket.productId == productId) {
                                                basket.count = count;
                                             }
                                         });
                                         this.calculatePriceAndCount();
                                         return count;
                                     }
                                     return oldCount;
                                 },
                                 (id, productId) => {
                                     if (MessageBox.Show(
                                         AppLibrary.App.getLanguageText("langDeleteFromBasketQuestion"),
                                         AppLibrary.App.getLanguageText("langDeleteProduct"),
                                         MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes) 
                                     {
                                         if (config.Values.customerId > 0) (new config.db.functions.Delete()).setBasket(id);
                                         int findIndex = config.Values.listBasket.FindIndex(basket => basket.productId == productId);
                                         config.Values.listBasket.RemoveAt(findIndex);
                                         config.Values.tabControl.pageProducts.initAddedIcon();
                                         findIndex = this.componentList.FindIndex(component => component.productId == productId);
                                         this.basketList.Children.Remove(this.componentList[findIndex].stackPanelMain_);
                                         this.componentList.RemoveAt(findIndex);
                                         this.calculatePriceAndCount();
                                     }
                                 }
                             ));
                            this.scroll.ScrollToBottom();
                            WindowMain.updateProgress(i);
                        }, System.Windows.Threading.DispatcherPriority.ContextIdle);
                    }
                    this.Dispatcher.Invoke(() => {
                        this.scroll.ScrollToTop();
                        WindowMain.hidePreLoader();
                    });
                });
                this.calculatePriceAndCount();
            });
        }
        public delegate void onImageClick(long productId);
        public delegate void onDelete(long id, long productId);
        public delegate int onEdit(long id, long productId, int oldCount, int count);
        class ComponentBasket {
            public long id { get; set; }
            public long groupId { get; set; }
            public int count { get; set; }
            public long productId { get; set; }
            public double priceTurkishLira { get; set; }
            public double priceDollar { get; set; }
            public double priceEuro { get; set; }
            public double pricePound { get; set; }
            public bool isEditing = false;
            public Image img { get; set; }
            public StackPanel stackPanelMain_ { get; set; }
            public ComponentBasket(
                StackPanel stackPanel,
                long id,
                long gorupId,
                int count,
                long productId,
                string image,
                string imageHigh,
                string createDate,
                string owner,
                double priceTurkishLira,
                double priceDollar,
                double priceEuro,
                double pricePound,
                onImageClick onImageClick,
                onEdit onEdit,
                onDelete onDelete
            ) {
                this.id = id;
                this.groupId = gorupId;
                this.count = count;
                this.productId = productId;
                this.priceTurkishLira = priceTurkishLira;
                this.priceDollar = priceDollar;
                this.priceEuro = priceEuro;
                this.pricePound = pricePound;
                stackPanelMain_ = this.stackPanelMain(id);
                TextBox _textBoxcount = this.textBoxcount(count);
                stackPanelMain_.Children.Add(_textBoxcount);
                this.img = this.image(image, imageHigh, onImageClick);
                stackPanelMain_.Children.Add(this.img);
                stackPanelMain_.Children.Add(this.productName(Path.GetFileName(imageHigh)));
                stackPanelMain_.Children.Add(this.owner(owner));

                StackPanel stackPanelPrice = this.stackPanelPrice();
                stackPanelPrice.Children.Add(this._priceTurkishLira(priceTurkishLira));
                stackPanelPrice.Children.Add(this._priceDollar(priceDollar));
                stackPanelPrice.Children.Add(this._priceEuro(priceEuro));
                stackPanelPrice.Children.Add(this._pricePound(pricePound));

                stackPanelMain_.Children.Add(stackPanelPrice);
                stackPanelMain_.Children.Add(this.createDate(createDate));
                stackPanelMain_.Children.Add(this.edit(onEdit, _textBoxcount));
                stackPanelMain_.Children.Add(this.delete(onDelete));

                stackPanel.Children.Add(stackPanelMain_);
            }
            private StackPanel stackPanelMain(long id) {
                StackPanel stackPanel = new StackPanel();
                stackPanel.Orientation = Orientation.Horizontal;
                stackPanel.Margin = new Thickness(0, 15, 0, 0);
                stackPanel.Name = "basketItem_" + id.ToString();
                return stackPanel;
            }
            private TextBox textBoxcount(int count) {
                TextBox textBox = new TextBox();
                textBox.Text = count.ToString();
                textBox.Margin = new Thickness(0, 0, 5, 0);
                textBox.FontSize = 16;
                textBox.VerticalAlignment = VerticalAlignment.Center;
                textBox.Width = 50;
                textBox.TextAlignment = TextAlignment.Center;
                textBox.PreviewTextInput += AppLibrary.Element.numberValidationTextBoxInt;
                textBox.GotFocus += WindowMain.virtualKeyboard.textboxFocus;
                textBox.IsEnabled = false;
                textBox.BorderBrush = Brushes.Brown;
                textBox.BorderThickness = new Thickness(0);
                return textBox;
            }
            private Image image(string imageName, string imageHigh, onImageClick click) {
                Image image = new Image();
                if (!AppLibrary.Var.isNullOrEmpty(imageName)) {
                    AppLibrary.Image.setImage(image, config.Values.pathUploadsProductsFolder(this.groupId) + imageName, true, 150);
                }
                image.Height = 150;
                image.Width = 150;
                image.Stretch = Stretch.Uniform;
                image.Cursor = Cursors.Hand;
                image.MouseDown += (o, e) => {
                    click(this.productId);
                };
                return image;
            }
            private TextBlock productName(string photoName) {
                TextBlock textBlock = new TextBlock();
                textBlock.Text = photoName;
                textBlock.VerticalAlignment = VerticalAlignment.Center;
                textBlock.Width = 250;
                textBlock.TextWrapping = TextWrapping.Wrap;
                textBlock.TextAlignment = TextAlignment.Center;
                return textBlock;
            }
            private TextBlock owner(string owner) {
                TextBlock textBlock = new TextBlock();
                textBlock.Text = owner;
                textBlock.VerticalAlignment = VerticalAlignment.Center;
                textBlock.Width = 150;
                textBlock.TextAlignment = TextAlignment.Center;
                return textBlock;
            }
            private TextBlock createDate(string createDate) {
                TextBlock textBlock = new TextBlock();
                textBlock.Text = String.Format("{0:dd/MM/yyyy}", Convert.ToDateTime(createDate));
                textBlock.VerticalAlignment = VerticalAlignment.Center;
                textBlock.Width = 150;
                textBlock.TextAlignment = TextAlignment.Center;
                return textBlock;
            }
            private StackPanel stackPanelPrice() {
                StackPanel stackPanel = new StackPanel();
                stackPanel.Width = 100;
                stackPanel.VerticalAlignment = VerticalAlignment.Center;
                return stackPanel;
            }
            private TextBlock _priceTurkishLira(double price) {
                TextBlock textBlock = new TextBlock();
                textBlock.TextAlignment = TextAlignment.Left;
                textBlock.Text = String.Format("{0} {1}", AppLibrary.Var.toStringDecimalFormat(price * this.count), "TL"); ;
                return textBlock;
            }
            private TextBlock _priceDollar(double price) {
                TextBlock textBlock = new TextBlock();
                textBlock.TextAlignment = TextAlignment.Left;
                textBlock.Text = String.Format("{0} {1}", AppLibrary.Var.toStringDecimalFormat(price * this.count), "Dollar"); ;
                return textBlock;
            }
            private TextBlock _priceEuro(double price) {
                TextBlock textBlock = new TextBlock();
                textBlock.TextAlignment = TextAlignment.Left;
                textBlock.Text = String.Format("{0} {1}", AppLibrary.Var.toStringDecimalFormat(price * this.count), "Euro"); ;
                return textBlock;
            }
            private TextBlock _pricePound(double price) {
                TextBlock textBlock = new TextBlock();
                textBlock.TextAlignment = TextAlignment.Left;
                textBlock.Text = String.Format("{0} {1}", AppLibrary.Var.toStringDecimalFormat(price * this.count), "Pound"); ;
                return textBlock;
            }
            private Button edit(onEdit func, TextBox textBoxCount) {
                Button button = new Button();
                button.Height = 35;
                button.Width = 120;
                button.Style = AppLibrary.App.getStyle("edit");
                // Content
                Grid buttonGrid = new Grid();
                buttonGrid.Width = 120;
                TextBlock textBlock = new TextBlock();
                textBlock.Text = AppLibrary.App.getLanguageText("langEdit");
                textBlock.VerticalAlignment = VerticalAlignment.Center;
                textBlock.HorizontalAlignment = HorizontalAlignment.Center;
                Image image = new Image();
                image.Height = 20;
                image.HorizontalAlignment = HorizontalAlignment.Left;
                image.VerticalAlignment = VerticalAlignment.Center;
                image.Margin = new Thickness(5, 0, 0, 0);
                image.Source = new BitmapImage(new Uri(@"/assets/images/icons/buttons/edit.png", UriKind.Relative));
                buttonGrid.Children.Add(textBlock);
                buttonGrid.Children.Add(image);

                button.Click += (o, e) => {
                    this.isEditing = !this.isEditing;
                    if (this.isEditing) {
                        textBoxCount.IsEnabled = true;
                        textBoxCount.BorderThickness = new Thickness(2);
                        textBlock.Text = AppLibrary.App.getLanguageText("langAccept");
                        image.Source = new BitmapImage(new Uri(@"/assets/images/icons/buttons/check.png", UriKind.Relative));
                        button.Style = AppLibrary.App.getStyle("save");
                    } else {
                        textBoxCount.IsEnabled = false;
                        textBoxCount.BorderThickness = new Thickness(0);
                        textBlock.Text = AppLibrary.App.getLanguageText("langEdit");
                        image.Source = new BitmapImage(new Uri(@"/assets/images/icons/buttons/edit.png", UriKind.Relative));
                        button.Style = AppLibrary.App.getStyle("edit");
                        // Set Edit
                        int _count = (AppLibrary.Var.isNullOrEmpty(textBoxCount.Text)) ? 1 : Convert.ToInt32(textBoxCount.Text.ToString());
                        _count = (_count < 1) ? 1 : _count;
                        this.count = func(this.id, this.productId, this.count, _count);
                        textBoxCount.Text = this.count.ToString();
                    }
                };

                button.Content = buttonGrid;
                return button;
            }
            private Button delete(onDelete func) {
                Button button = new Button();
                button.Height = 35;
                button.Width = 120;
                button.Style = AppLibrary.App.getStyle("delete");
                button.Margin = new Thickness(10, 0, 0, 0);
                // Content
                Grid buttonGrid = new Grid();
                buttonGrid.Width = 120;
                TextBlock textBlock = new TextBlock();
                textBlock.Text = AppLibrary.App.getLanguageText("langDelete");
                textBlock.VerticalAlignment = VerticalAlignment.Center;
                textBlock.HorizontalAlignment = HorizontalAlignment.Center;
                Image image = new Image();
                image.Height = 20;
                image.HorizontalAlignment = HorizontalAlignment.Left;
                image.VerticalAlignment = VerticalAlignment.Center;
                image.Margin = new Thickness(5, 0, 0, 0);
                image.Source = new BitmapImage(new Uri(@"/assets/images/icons/buttons/trash-can.png", UriKind.Relative));
                buttonGrid.Children.Add(textBlock);
                buttonGrid.Children.Add(image);

                button.Click += (o, e) => {
                    func(this.id, this.productId);
                };

                button.Content = buttonGrid;
                return button;
            }
        }
    }
}
