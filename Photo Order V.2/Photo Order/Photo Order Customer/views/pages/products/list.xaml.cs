using AppComponents;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Photo_Order_Customer.views.pages.products
{
    /// <summary>
    /// Interaction logic for list.xaml
    /// </summary>
    public partial class list : Page
    {
        List<ComponentProducts> componentList = new List<ComponentProducts>();
        private List<long> productOwners { get; set; }
        private List<long> events { get; set; }
        private string[] date { get; set; }
        private List<config.db.functions.Select.ListProduct> listProducts = new List<config.db.functions.Select.ListProduct>();
        private int productMaxCount = 60;
        private int currentPageNumber = 1;
        public list() { 
            InitializeComponent();
        }
        public void initPageUseSelfData() {
            this.initPage(this.productOwners, this.events, this.date, true);
        }
        public void initPage(List<long> productOwners, List<long> events, string[] date, bool getFromLocal = false) {
            this.productOwners = productOwners;
            this.events = events;
            this.date = date;
            this.clearPage(getFromLocal);
            this.getProducts(getFromLocal);
            AppLibrary.GC.GCCollect();
        }
        public void clearPage(bool getFromLocal)
        {
            this.componentList.ForEach(item =>
            {
                item.img.Source = null;
                item.img.UpdateLayout();
            });
            this.productList.Children.Clear();
            this.pagination.Children.Clear();
            this.componentList.Clear();
            if (!getFromLocal) {
                this.currentPageNumber = 1;
                listProducts.Clear();
            }
        }
        public void initAddedIcon() {
            this.componentList.ForEach(component => {
                if (config.Values.listBasket.FindIndex(basket => basket.productId == component.id) > -1)
                {
                    component.addGridSelected();
                } else {
                    component.removeGridSelected();
                }
            });
        }
        private void getProducts(bool getFromLocal)
        {
            AppLibrary.Values.logger.loggerFunction( () =>
            {
                if (!getFromLocal) this.listProducts = (new config.db.functions.Select()).getProducts(productOwners: this.productOwners, events: this.events, date: this.date, orderBy: "asc");
                if (this.listProducts.Count == 0) {
                    return;
                }
                int maxPageNumber = Convert.ToInt32(Math.Ceiling((double)this.listProducts.Count / this.productMaxCount));
                int startIndex = (this.currentPageNumber - 1) * this.productMaxCount;
                int productCount = (maxPageNumber == this.currentPageNumber)
                    ? this.listProducts.Count - startIndex
                    : this.productMaxCount;
                var products = this.listProducts.GetRange(startIndex, productCount);
                WindowMain.showPreLoader(products.Count);
                createComponents(products);
                var paginationComponent = new ComponentPagination(
                    this.pagination,
                    maxPageNumber,
                    this.currentPageNumber,
                    (pageNumber) =>
                    {
                        if (pageNumber != this.currentPageNumber)
                        {
                            this.currentPageNumber = pageNumber;
                            this.initPageUseSelfData();
                        }
                    }
                );
                this.scroll.ScrollToTop();
            });
        }
        private void createComponents(List<config.db.functions.Select.ListProduct> products) {
            AppLibrary.Values.logger.loggerFunction(() => {
                Task.Factory.StartNew(() => {
                    for (int i = 0; i < products.Count; i++) {
                        var product = products[i];
                        product.priceTurkishLira = (product.priceTurkishLira == 0) ? AppLibrary.Values.settings.json.priceTurkishLira : product.priceTurkishLira;
                        product.priceDollar = (product.priceDollar == 0) ? AppLibrary.Values.settings.json.priceDollar : product.priceDollar;
                        product.priceEuro = (product.priceEuro == 0) ? AppLibrary.Values.settings.json.priceEuro : product.priceEuro;
                        product.pricePound = (product.pricePound == 0) ? AppLibrary.Values.settings.json.pricePound : product.pricePound;
                        this.Dispatcher.Invoke(() => {
                            this.componentList.Add(new ComponentProducts(
                                this.productList,
                                config.Values.listBasket.FindIndex((item) => item.productId == product.id) > -1,
                                product.id,
                                product.groupId,
                                product.image,
                                product.priceTurkishLira,
                                product.priceDollar,
                                product.priceEuro,
                                product.pricePound,
                                (id, count) => {
                                    if (config.Values.customerId > 0 && (new config.db.functions.Select()).getCustomer(customerId: config.Values.customerId) == 0)
                                    {
                                        MessageBox.Show(
                                            AppLibrary.App.getLanguageText("langAccountNotFound"),
                                            AppLibrary.App.getLanguageText("langSessionStatus"),
                                            MessageBoxButton.OK, MessageBoxImage.Error);
                                        return;
                                    }
                                    if (config.Values.customerId == 0)
                                    {
                                        config.Values.listBasket.Add(new config.db.functions.Select.ListBasket
                                        {
                                            id = 0,
                                            productGroupId = product.groupId,
                                            productId = id,
                                            count = count,
                                            createDate = (DateTime.Now).ToString("yyyy-MM-dd"),
                                            productImage = product.image,
                                            productImageHigh = product.imageHigh,
                                            productOwnerName = product.ownerName,
                                            priceTurkishLira = product.priceTurkishLira,
                                            pricePound = product.pricePound,
                                            priceDollar = product.priceDollar,
                                            priceEuro = product.priceEuro
                                        });
                                    }
                                    else
                                    {
                                        (new config.db.functions.Insert()).setBasket(id, config.Values.customerId, count);
                                    }
                                },
                                (id) =>
                                {
                                    WindowProductBigList windowProductBigList = new WindowProductBigList(this.listProducts, id);
                                    if (windowProductBigList.ShowDialog() == true)
                                    {
                                        initAddedIcon();
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
            });
        }
        public delegate void onAddBasket(long id, int count);
        public delegate void onImageClick(long id);

        class ComponentProducts
        {
            public long id { get; set; }
            public long groupId { get; set; }
            public Image img { get; set; }
            private Grid _gridMain { get; set; }
            private Grid _gridSelected { get; set; }
            private bool isSelected = false;

            public ComponentProducts(
                WrapPanel wrapPanel,
                bool isAddedBasket,
                long id,
                long groupId,
                string image,
                double priceTurkishLira,
                double priceDollar,
                double priceEuro,
                double pricePound,
                onAddBasket funcAddBasket,
                onImageClick onImageClick
            )
            {
                this.id = id;
                this.groupId = groupId;
                _gridMain = this.gridMain(onImageClick);
                _gridSelected = this.gridSelected();
                if (isAddedBasket) { addGridSelected(); }
                StackPanel stackPanelMain = this.stackPanelMain();
                this.img = this.image(image);
                stackPanelMain.Children.Add(this.img);
                _gridMain.Children.Add(stackPanelMain);
                wrapPanel.Children.Add(_gridMain);
            }
            public void addGridSelected() {
                if(!isSelected) _gridMain.Children.Add(_gridSelected);
                isSelected = true;
            }
            public void removeGridSelected() {
                if(isSelected) _gridMain.Children.Remove(_gridSelected);
                isSelected = false;
            }
            private Grid gridMain(onImageClick click)
            {
                Grid grid = new Grid();
                grid.Cursor = Cursors.Hand;
                grid.MouseDown += (o, e) =>
                {
                    click(this.id);
                };
                return grid;
            }
            private StackPanel stackPanelMain()
            {
                StackPanel stackPanel = new StackPanel();
                stackPanel.Background = new SolidColorBrush(Colors.WhiteSmoke);
                stackPanel.Margin = new Thickness(5, 5, 0, 0);
                return stackPanel;
            }
            private Image image(string imageName) {
                Image image = new Image();
                image.Width = 300;
                image.Height = 300;
                RenderOptions.SetBitmapScalingMode(image, BitmapScalingMode.Fant);
                image.Stretch = Stretch.Uniform;
                image.Cursor = Cursors.Hand;
                if (!AppLibrary.Var.isNullOrEmpty(imageName)) {
                    AppLibrary.Image.setImage(image, config.Values.pathUploadsProductsFolder(this.groupId) + imageName, true, 300);
                }
                return image;
            }
            private Grid gridSelected()
            {
                Grid grid = new Grid();
                grid.Style = AppLibrary.App.getStyle("gridSelected");
                Panel.SetZIndex(grid, 1);
                grid.Children.Add(this.imageSelected());
                return grid;
            }
            private Image imageSelected(string imageName = "success-tick.png")
            {
                Image image = new Image();
                if (!AppLibrary.Var.isNullOrEmpty(imageName))
                {
                    BitmapImage bi = new BitmapImage();
                    bi.BeginInit();
                    bi.UriSource = new Uri(config.Values.pathImagesFolder + imageName);
                    bi.EndInit();
                    image.Source = bi;
                }
                image.Height = 75;
                image.Width = 75;
                image.HorizontalAlignment = HorizontalAlignment.Right;
                image.VerticalAlignment = VerticalAlignment.Top;
                image.Margin = new Thickness(0, 10, 10, 0);
                return image;
            }
        }
    }
}
