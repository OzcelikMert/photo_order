using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Photo_Order
{
    /// <summary>
    /// Interaction logic for WindowOrderDetail.xaml
    /// </summary>
    public partial class WindowOrderDetail : Window {
        double totalPriceTurkishLira = 0,
               totalPriceDollar = 0,
               totalPriceEuro = 0,
               totalPricePound = 0,
               totalProductCount = 0;
        private long orderId { get; set; }
        private string date { get; set; }
        private long customerId { get; set; }
        private string customerRoom { get; set; }
        private string customerName { get; set; }
        private string customerEmail { get; set; }

        List<ComponentOrderProduct> componentList = new List<ComponentOrderProduct>();
        public WindowOrderDetail(long orderId, long customerId, string customerRoom, string customerName, string customerEmail, string date) {
            InitializeComponent();
            this.orderId = orderId;
            this.customerId = customerId;
            this.customerRoom = customerRoom;
            this.customerName = customerName;
            this.customerEmail = customerEmail;
            this.date = date;
            this.initPage();
        }
        private void initPage() {
            this.clearPage();
            this.getOrderProducts();
        }
        private void clearPage() {
            this.totalPriceTurkishLira = 0;
            this.totalPriceDollar = 0;
            this.totalPriceEuro = 0;
            this.totalPricePound = 0;
            this.paymentType.SelectedIndex = 0;
            this.orderProductList.Children.Clear();
            this.componentList.Clear();
        }
        private void paymentOrder(object sender, RoutedEventArgs e) {
            AppLibrary.Values.logger.loggerFunction(() =>
            {
                if (MessageBox.Show("Odeme Tipi: " + this.paymentType.SelectedValue + "\nOdeme yapmak istediginizden emin misiniz?", "Sepet Onaylama ve Siparis Verme", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes) {
                    string paymentType = this.paymentType.SelectedValue.ToString();
                    config.printer.HtmlToPdf htmlToPdf = new config.printer.HtmlToPdf(
                        this.customerRoom,
                        this.customerName,
                        this.customerEmail,
                        paymentType,
                        this.orderId,
                        AppLibrary.Var.toStringDateFormatLabel(Convert.ToDateTime(this.date)),
                        AppLibrary.Values.settings.json.companyName);
                    var listOrderProducts = new List<config.printer.HtmlToPdf.ListOrderProducts>();
                    var images = new List<string>();
                    foreach (var product in this.componentList) {

                        double price = 0;
                        switch (this.paymentType.SelectedIndex)
                        {
                            case 0: price = product.priceTurkishLira; break;
                            case 1: price = product.priceDollar; break;
                            case 2: price = product.priceEuro; break;
                            case 3: price = product.pricePound; break;
                        }
                        double totalPrice = price * product.count;
                        int findIndex = listOrderProducts.FindIndex(item => (item.productOwnerId == product.productOwnerId && item.price == price));
                        if (findIndex > -1) {
                            listOrderProducts[findIndex].totalPrice += totalPrice;
                            listOrderProducts[findIndex].totalCount += product.count;
                        }else {
                            listOrderProducts.Add(new config.printer.HtmlToPdf.ListOrderProducts
                            {
                                totalCount = product.count,
                                price = price,
                                totalPrice = totalPrice,
                                productOwnerName = product.productOwnerName,
                                productOwnerId = product.productOwnerId
                            });
                        }

                        for (int i = 0; i < product.count; i++) {
                            images.Add(product.productImageHigh);
                        }
                    }
                    listOrderProducts.Sort((x, y) => x.productOwnerId.CompareTo(y.productOwnerId));
                    htmlToPdf.setOrderProducts(listOrderProducts);
                    htmlToPdf.createPdf();
                    var print = new config.printer.Print();
                    if (!print.printPdf())
                    {
                        if (MessageBox.Show("Fiş çıkarılırken bir hata meydana geldi!\nFiş çıkmadan devam etmek istediğinizden emin misiniz?", "Yazdirma Hatasi", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No) {
                            return;
                        }
                    }
                    AppLibrary.File.copyImagesToDesktop(config.Values.pathImageHigh, config.Values.pathDesktopCopyImageFolder, String.Format("{0} - {1}", this.customerName, this.customerEmail), images);
                    (new config.db.functions.Update()).setOrder(this.orderId, true, paymentType);
                    MessageBox.Show("Odeme basari ile yapilmistir.", "Odeme Onaylandi", MessageBoxButton.OK, MessageBoxImage.Information);
                    this.DialogResult = true;
                    this.Close();
                }
            });
        }

        private void getOrderProducts() {
            AppLibrary.Values.logger.loggerFunction(() => {
                var orderProducts = (new config.db.functions.Select()).getOrderProduct(orderId: this.orderId);
                foreach (var orderProduct in orderProducts)
                {
                    this.componentList.Add(new ComponentOrderProduct(
                         this.orderProductList,
                         orderProduct.id,
                         orderProduct.count,
                         orderProduct.productId,
                         orderProduct.productGroupId,
                         orderProduct.productImage,
                         orderProduct.productImageHigh,
                         orderProduct.createDate,
                         orderProduct.productOwnerId,
                         orderProduct.productOwnerName,
                         orderProduct.priceTurkishLira,
                         orderProduct.priceDollar,
                         orderProduct.priceEuro,
                         orderProduct.pricePound,
                         (id) => {
                             this.openWindowEdit(id);
                         },
                         (id) => {
                             if (MessageBox.Show("Bu urunu siparisten silmek istediginize emin misiniz? ", "Siparis Duzenleme", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                             {
                                 (new config.db.functions.Delete()).setOrderProduct(id: id);
                                 this.initPage();
                             }
                         }
                     ));
                    this.totalPriceTurkishLira += orderProduct.priceTurkishLira * orderProduct.count;
                    this.totalPriceDollar += orderProduct.priceDollar * orderProduct.count;
                    this.totalPriceEuro += orderProduct.priceEuro * orderProduct.count;
                    this.totalPricePound += orderProduct.pricePound * orderProduct.count;
                    this.totalProductCount += orderProduct.count;
                }
                this.priceTurkishLira.Text = String.Format("{0} TL", AppLibrary.Var.toStringDecimalFormat(this.totalPriceTurkishLira));
                this.priceDollar.Text = String.Format("{0} Dolar", AppLibrary.Var.toStringDecimalFormat(this.totalPriceDollar));
                this.priceEuro.Text = String.Format("{0} Euro", AppLibrary.Var.toStringDecimalFormat(this.totalPriceEuro));
                this.pricePound.Text = String.Format("{0} Pound", AppLibrary.Var.toStringDecimalFormat(this.totalPricePound));
                this.totalCount.Text = this.totalProductCount.ToString();
            });
        }
        private void openWindowEdit(long itemId) {
            config.Values.selectedItemId = itemId;
            config.Values.refreshList = false;
            config.Values.windowEdit = new WindowEdit(WindowEdit.TypeEdit.OrderProduct);
            config.Values.windowEdit.ShowDialog();
            if (config.Values.refreshList) {
                this.initPage();
            }
        }
        public delegate void onDelete(long id);
        public delegate void onEdit(long id);
        class ComponentOrderProduct
        {
            public long id { get; set; }
            public int count { get; set; }
            public long productId { get; set; }
            public long productGroupId { get; set; }
            public string productImage { get; set; }
            public string productImageHigh { get; set; }
            public long productOwnerId { get; set; }
            public string productOwnerName { get; set; }
            public double priceTurkishLira { get; set; }
            public double priceDollar { get; set; }
            public double priceEuro { get; set; }
            public double pricePound { get; set; }
            public ComponentOrderProduct(
                StackPanel stackPanel,
                long id,
                int count,
                long productId,
                long productGroupId,
                string image,
                string imageHigh,
                string createDate,
                long ownerId,
                string owner,
                double priceTurkishLira,
                double priceDollar,
                double priceEuro,
                double pricePound,
                onEdit onEdit,
                onDelete onDelete
            )
            {
                this.id = id;
                this.count = count;
                this.productId = productId;
                this.productGroupId = productGroupId;
                this.priceTurkishLira = priceTurkishLira;
                this.priceDollar = priceDollar;
                this.priceEuro = priceEuro;
                this.pricePound = pricePound;
                this.productImage = image;
                this.productImageHigh = imageHigh;
                this.productOwnerName = owner;
                this.productOwnerId = ownerId;
                StackPanel stackPanelMain = this.stackPanelMain();
                stackPanelMain.Children.Add(this._count(count));
                stackPanelMain.Children.Add(this.image(image));
                stackPanelMain.Children.Add(this.owner(owner));

                StackPanel stackPanelPrice = this.stackPanelPrice();
                stackPanelPrice.Children.Add(this._priceTurkishLira(priceTurkishLira));
                stackPanelPrice.Children.Add(this._priceDollar(priceDollar));
                stackPanelPrice.Children.Add(this._priceEuro(priceEuro));
                stackPanelPrice.Children.Add(this._pricePound(pricePound));

                stackPanelMain.Children.Add(stackPanelPrice);
                stackPanelMain.Children.Add(this.createDate(createDate));
                stackPanelMain.Children.Add(this.edit(onEdit));
                stackPanelMain.Children.Add(this.delete(onDelete));

                stackPanel.Children.Add(stackPanelMain);
            }
            private StackPanel stackPanelMain()
            {
                StackPanel stackPanel = new StackPanel();
                stackPanel.Orientation = Orientation.Horizontal;
                stackPanel.Margin = new Thickness(0, 15, 0, 0);
                return stackPanel;
            }
            private TextBlock _count(int count)
            {
                TextBlock textBlock = new TextBlock();
                textBlock.Text = count + "x";
                textBlock.Margin = new Thickness(0, 0, 5, 0);
                textBlock.VerticalAlignment = VerticalAlignment.Center;
                textBlock.Width = 50;
                textBlock.TextAlignment = TextAlignment.Center;
                return textBlock;
            }
            private Image image(string imageName)
            {
                Image image = new Image();
                if (!AppLibrary.Var.isNullOrEmpty(imageName)) {
                    AppLibrary.Image.setImage(image, config.Values.pathUploadsProductsFolder(this.productGroupId) + imageName);
                }
                image.Height = 60;
                image.Width = 60;
                image.Stretch = Stretch.Uniform;
                return image;
            }
            private TextBlock owner(string owner)
            {
                TextBlock textBlock = new TextBlock();
                textBlock.Text = owner;
                textBlock.VerticalAlignment = VerticalAlignment.Center;
                textBlock.Width = 250;
                textBlock.TextAlignment = TextAlignment.Center;
                return textBlock;
            }
            private TextBlock createDate(string createDate)
            {
                TextBlock textBlock = new TextBlock();
                textBlock.Text = String.Format("{0:dd/MM/yyyy}", Convert.ToDateTime(createDate));
                textBlock.VerticalAlignment = VerticalAlignment.Center;
                textBlock.Width = 150;
                textBlock.TextAlignment = TextAlignment.Center;
                return textBlock;
            }
            private StackPanel stackPanelPrice()
            {
                StackPanel stackPanel = new StackPanel();
                stackPanel.Width = 100;
                return stackPanel;
            }
            private TextBlock _priceTurkishLira(double price)
            {
                TextBlock textBlock = new TextBlock();
                textBlock.TextAlignment = TextAlignment.Left;
                textBlock.Text = String.Format("{0} {1}", AppLibrary.Var.toStringDecimalFormat(price * this.count), "TL"); ;
                return textBlock;
            }
            private TextBlock _priceDollar(double price)
            {
                TextBlock textBlock = new TextBlock();
                textBlock.TextAlignment = TextAlignment.Left;
                textBlock.Text = String.Format("{0} {1}", AppLibrary.Var.toStringDecimalFormat(price * this.count), "Dollar"); ;
                return textBlock;
            }
            private TextBlock _priceEuro(double price)
            {
                TextBlock textBlock = new TextBlock();
                textBlock.TextAlignment = TextAlignment.Left;
                textBlock.Text = String.Format("{0} {1}", AppLibrary.Var.toStringDecimalFormat(price * this.count), "Euro"); ;
                return textBlock;
            }
            private TextBlock _pricePound(double price)
            {
                TextBlock textBlock = new TextBlock();
                textBlock.TextAlignment = TextAlignment.Left;
                textBlock.Text = String.Format("{0} {1}", AppLibrary.Var.toStringDecimalFormat(price * this.count), "Pound"); ;
                return textBlock;
            }
            private Button edit(onEdit func)
            {
                Button button = new Button();
                button.Content = "Duzenle";
                button.Height = 30;
                button.Width = 100;
                button.Style = AppLibrary.App.getStyle("edit");
                button.Click += (o, e) => {
                    func(this.id);
                };
                return button;
            }
            private Button delete(onDelete func)
            {
                Button button = new Button();
                button.Content = "Sil";
                button.Height = 30;
                button.Width = 100;
                button.Style = AppLibrary.App.getStyle("delete");
                button.Margin = new Thickness(10, 0, 5, 0);
                button.Click += (o, e) => {
                    func(this.id);
                };
                return button;
            }
        }
    }
}
