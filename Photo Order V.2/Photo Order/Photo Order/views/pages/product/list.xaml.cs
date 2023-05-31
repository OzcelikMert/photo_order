using AppComponents;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Photo_Order.views.pages.product
{
    public partial class list : Page
    {
        private string searchText = "";
        private List<ComponentProduct> componentList = new List<ComponentProduct>();
        private List<ComponentProductGroups> componentListGroup = new List<ComponentProductGroups>();
        private List<config.db.functions.Select.ListProduct> listOfProduct = new List<config.db.functions.Select.ListProduct>();
        private List<config.db.functions.Select.ListProductGroups> listOfProductGroup = new List<config.db.functions.Select.ListProductGroups>();
        private string searchTextProductGroups = "";
        private int productMaxCount = 10;
        private int currentPageNumber = 1;
        public static long selectedGroupId = 0;
        public list() {
            InitializeComponent();
            this.initPage();
        }
        public void initPage(bool pagination = false) {
            this.setTableHead();
            this.clearPage(pagination);
            this.getProducts(pagination);
            AppLibrary.GC.GCCollect();
        }
        private void clearPage(bool pagination) {
            componentList.Clear();
            componentListGroup.Clear();
            this.productList.Children.Clear();
            this.pagination.Children.Clear();
            if (!pagination) {
                this.currentPageNumber = 1;
                if (listOfProduct != null) listOfProduct.Clear();
                if (listOfProductGroup != null) listOfProductGroup.Clear();
            }
        }
        private void setTableHead() {
            if (selectedGroupId > 0) {
                backButton.Visibility = Visibility.Visible;
                tableHeadProducts.Visibility = Visibility.Visible;
                tableHeadProductGroups.Visibility = Visibility.Collapsed;
            } else {
                backButton.Visibility = Visibility.Collapsed;
                tableHeadProducts.Visibility = Visibility.Collapsed;
                tableHeadProductGroups.Visibility = Visibility.Visible;
            }
        }
        private void getProducts(bool pagination) {
            AppLibrary.Values.logger.loggerFunction(() => {
                if (!pagination) {
                    if (selectedGroupId > 0) this.listOfProduct = (new config.db.functions.Select()).getProducts(groupId: selectedGroupId, searchText: this.searchText);
                    else this.listOfProductGroup = (new config.db.functions.Select()).getProductGroups(searchText: this.searchText);
                }
                if (
                    (selectedGroupId == 0 && this.listOfProductGroup.Count == 0) ||
                    (selectedGroupId > 0 && this.listOfProduct.Count == 0)
                ) {
                    this.productList.Children.Add(AppLibrary.Element.textBlockEmpty("Henüz eklenmis bir fotograf yok", 
                        this.searchText,
                        AppLibrary.App.getStyle("emptyTextBlock")));
                    return;
                }
                int listCount = (selectedGroupId > 0) ? this.listOfProduct.Count : this.listOfProductGroup.Count;
                int maxPageNumber = Convert.ToInt32(Math.Ceiling((double)listCount / this.productMaxCount));
                int startIndex = (this.currentPageNumber - 1) * this.productMaxCount;
                int productCount = (maxPageNumber == this.currentPageNumber)
                    ? listCount - startIndex
                    : this.productMaxCount;
                if(selectedGroupId > 0) {
                    createComponentsProducts(this.listOfProduct.GetRange(startIndex, productCount));
                } else {
                    createComponentsProductGroup(this.listOfProductGroup.GetRange(startIndex, productCount));
                }
                var paginationComponent = new ComponentPagination(
                    this.pagination,
                    maxPageNumber,
                    this.currentPageNumber,
                    (pageNumber) => {
                        if (pageNumber != this.currentPageNumber)
                        {
                            this.currentPageNumber = pageNumber;
                            this.initPage(pagination: true);
                        }
                        
                    }
                );
                this.scroll.ScrollToTop();
            });
        }
        private void createComponentsProducts(List<config.db.functions.Select.ListProduct> products = null) {
            AppLibrary.Values.logger.loggerFunction(() =>
            {
                foreach (var product in products)
                {
                    product.priceTurkishLira = (product.priceTurkishLira == 0) ? AppLibrary.Values.settings.json.priceTurkishLira : product.priceTurkishLira;
                    product.priceDollar = (product.priceDollar == 0) ? AppLibrary.Values.settings.json.priceDollar : product.priceDollar;
                    product.priceEuro = (product.priceEuro == 0) ? AppLibrary.Values.settings.json.priceEuro : product.priceEuro;
                    product.pricePound = (product.pricePound == 0) ? AppLibrary.Values.settings.json.pricePound : product.pricePound;
                    componentList.Add(new ComponentProduct(
                        this.productList,
                        product.id,
                        product.groupId,
                        product.image,
                        product.ownerName,
                        product.eventName,
                        product.priceTurkishLira,
                        product.priceDollar,
                        product.priceEuro,
                        product.pricePound,
                        (id) => {
                            // edit
                            this.openWindowEdit(id);
                        },
                        (id, imageName) => {
                            // delete
                            if (MessageBox.Show("Bu ürünü silmek istediğinizden emin misiniz?", "Ürün Silme", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                            {
                                (new config.db.functions.Delete()).setProduct(id);
                                (new config.db.functions.Delete()).setOrderProduct(productId: id);
                                this.initPage();
                                AppLibrary.File.removeImage(config.Values.pathUploadsProductsFolder(selectedGroupId), imageName);
                            }
                        }
                    ));
                }
            });
        }
        private void createComponentsProductGroup(List<config.db.functions.Select.ListProductGroups> productGroups = null) {
            AppLibrary.Values.logger.loggerFunction(() => {
                foreach (var productGroup in productGroups) {
                    productGroup.priceTurkishLira = (productGroup.priceTurkishLira == 0) ? AppLibrary.Values.settings.json.priceTurkishLira : productGroup.priceTurkishLira;
                    productGroup.priceDollar = (productGroup.priceDollar == 0) ? AppLibrary.Values.settings.json.priceDollar : productGroup.priceDollar;
                    productGroup.priceEuro = (productGroup.priceEuro == 0) ? AppLibrary.Values.settings.json.priceEuro : productGroup.priceEuro;
                    productGroup.pricePound = (productGroup.pricePound == 0) ? AppLibrary.Values.settings.json.pricePound : productGroup.pricePound;
                    componentListGroup.Add(new ComponentProductGroups(
                        this.productList,
                        productGroup.id,
                        productGroup.createDate,
                        productGroup.ownerName,
                        productGroup.eventName,
                        productGroup.priceTurkishLira,
                        productGroup.priceDollar,
                        productGroup.priceEuro,
                        productGroup.pricePound,
                        (id) => {
                            // edit
                            this.openWindowEdit(id);
                        },
                        (id) => {
                            // detail
                            selectedGroupId = id;
                            this.searchTextProductGroups = this.searchText;
                            this.searchText = "";
                            this.search.Text = "";
                            initPage();
                        },
                        (id, imageName) => {
                            // delete
                            if (MessageBox.Show("Bu grubu silmek istediğinizden emin misiniz?\nGruba ait olan tüm resimler silinecektir!", "Grup Silme", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes) {
                                (new config.db.functions.Delete()).setProductGroups(id);
                                (new config.db.functions.Delete()).setProduct(groupId: id);
                                (new config.db.functions.Delete()).setOrderProduct(groupId: id);
                                this.initPage();
                                AppLibrary.File.removeFolder(config.Values.pathUploadsProductsFolder(id));
                            }
                        }
                    ));
                }
            });
        }
        private void createNewClick(object sender, RoutedEventArgs e) {
            this.openWindowEdit(0);
        }

        private void backClick(object sender, RoutedEventArgs e) {
            selectedGroupId = 0;
            this.currentPageNumber = 1;
            this.searchText = this.searchTextProductGroups;
            this.search.Text = this.searchTextProductGroups;
            this.initPage();
        }
        private void openWindowEdit(long itemId) {
            config.Values.selectedItemId = itemId;
            config.Values.refreshList = false;
            var typeEdit = (selectedGroupId > 0) ? WindowEdit.TypeEdit.Product : WindowEdit.TypeEdit.ProductGroup;
            config.Values.windowEdit = new WindowEdit(typeEdit);
            config.Values.windowEdit.ShowDialog();
            if (config.Values.refreshList) {
                this.initPage();
            }
        }
        private void searchProduct(object sender, KeyEventArgs e) {
            string search = ((TextBox)sender).Text.ToString();
            if ((!AppLibrary.Var.isNullOrEmpty(search) && search.Length > 2) || (e.Key == Key.Back && AppLibrary.Var.isNullOrEmpty(search))) {
                this.searchText = search;
                this.initPage();
            }
        }
        private delegate void onEdit(long id);
        private delegate void onDetail(long id);
        private delegate void onDelete(long id, string imageName = "");
        class ComponentProductGroups
        {
            public long id { get; set; }
            public ComponentProductGroups(
                StackPanel stackPanel,
                long id,
                string createDate,
                string owner,
                string _event,
                double priceTurkishLira,
                double priceDollar,
                double priceEuro,
                double pricePound,
                onEdit onEdit,
                onDetail onDetail,
                onDelete onDelete
            )
            {
                this.id = id;
                StackPanel stackPanelMain = this.stackPanelMain();
                stackPanelMain.Children.Add(this.image());
                stackPanelMain.Children.Add(this.createDate(createDate));

                StackPanel stackPanelPrice = this.stackPanelPrice();
                stackPanelPrice.Children.Add(this._priceTurkishLira(priceTurkishLira));
                stackPanelPrice.Children.Add(this._priceDollar(priceDollar));
                stackPanelPrice.Children.Add(this._priceEuro(priceEuro));
                stackPanelPrice.Children.Add(this._pricePound(pricePound));

                stackPanelMain.Children.Add(stackPanelPrice);
                stackPanelMain.Children.Add(this.owner(owner));
                stackPanelMain.Children.Add(this._event(_event));
                stackPanelMain.Children.Add(this.edit(onEdit));
                stackPanelMain.Children.Add(this.detail(onDetail));
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
            private Image image() {
                Image image = new Image();
                AppLibrary.Image.setImage(image, config.Values.pathImagesFolder + "folder.png");
                image.Height = 60;
                image.Width = 60;
                image.Stretch = Stretch.Uniform;
                return image;
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
            private StackPanel stackPanelPrice() {
                StackPanel stackPanel = new StackPanel();
                stackPanel.Width = 100;
                return stackPanel;
            }
            private TextBlock _priceTurkishLira(double price)
            {
                TextBlock textBlock = new TextBlock();
                textBlock.TextAlignment = TextAlignment.Left;
                textBlock.Text = String.Format("{0} {1}", AppLibrary.Var.toStringDecimalFormat(price), "TL"); ;
                return textBlock;
            }
            private TextBlock _priceDollar(double price)
            {
                TextBlock textBlock = new TextBlock();
                textBlock.TextAlignment = TextAlignment.Left;
                textBlock.Text = String.Format("{0} {1}", AppLibrary.Var.toStringDecimalFormat(price), "Dollar"); ;
                return textBlock;
            }
            private TextBlock _priceEuro(double price)
            {
                TextBlock textBlock = new TextBlock();
                textBlock.TextAlignment = TextAlignment.Left;
                textBlock.Text = String.Format("{0} {1}", AppLibrary.Var.toStringDecimalFormat(price), "Euro"); ;
                return textBlock;
            }
            private TextBlock _pricePound(double price)
            {
                TextBlock textBlock = new TextBlock();
                textBlock.TextAlignment = TextAlignment.Left;
                textBlock.Text = String.Format("{0} {1}", AppLibrary.Var.toStringDecimalFormat(price), "Pound"); ;
                return textBlock;
            }
            private TextBlock owner(string owner)
            {
                TextBlock textBlock = new TextBlock();
                textBlock.Text = owner;
                textBlock.VerticalAlignment = VerticalAlignment.Center;
                textBlock.Width = 200;
                textBlock.TextAlignment = TextAlignment.Center;
                return textBlock;
            }
            private TextBlock _event(string eventName)
            {
                TextBlock textBlock = new TextBlock();
                textBlock.Text = eventName;
                textBlock.VerticalAlignment = VerticalAlignment.Center;
                textBlock.Width = 200;
                textBlock.TextAlignment = TextAlignment.Center;
                return textBlock;
            }
            private Button edit(onEdit click)
            {
                Button button = new Button();
                button.Content = "Düzenle";
                button.Height = 30;
                button.Width = 100;
                button.Style = AppLibrary.App.getStyle("edit");
                button.Click += (o, e) => click(this.id);
                return button;
            }
            private Button detail(onDetail click) {
                Button button = new Button();
                button.Content = "Detay";
                button.Height = 30;
                button.Width = 100;
                button.Style = AppLibrary.App.getStyle("refresh");
                button.Margin = new Thickness(10, 0, 0, 0);
                button.Click += (o, e) => click(this.id);
                return button;
            }
            private Button delete(onDelete click)
            {
                Button button = new Button();
                button.Content = "Sil";
                button.Height = 30;
                button.Width = 100;
                button.Style = AppLibrary.App.getStyle("delete");
                button.Margin = new Thickness(10, 0, 0, 0);
                button.Click += (o, e) => click(this.id);
                return button;
            }
        }
        class ComponentProduct {
            public long id { get; set; }
            public long groupId { get; set; }
            public ComponentProduct(
                StackPanel stackPanel,
                long id,
                long groupId,
                string image,
                string owner,
                string _event,
                double priceTurkishLira,
                double priceDollar,
                double priceEuro,
                double pricePound,
                onEdit onEdit,
                onDelete onDelete
            ) {
                this.id = id;
                this.groupId = groupId;
                StackPanel stackPanelMain = this.stackPanelMain();
                stackPanelMain.Children.Add(this.image(image));

                StackPanel stackPanelPrice = this.stackPanelPrice();
                stackPanelPrice.Children.Add(this._priceTurkishLira(priceTurkishLira));
                stackPanelPrice.Children.Add(this._priceDollar(priceDollar));
                stackPanelPrice.Children.Add(this._priceEuro(priceEuro));
                stackPanelPrice.Children.Add(this._pricePound(pricePound));

                stackPanelMain.Children.Add(stackPanelPrice);
                stackPanelMain.Children.Add(this.owner(owner));
                stackPanelMain.Children.Add(this._event(_event));
                stackPanelMain.Children.Add(this.edit(onEdit));
                stackPanelMain.Children.Add(this.delete(onDelete, image));

                stackPanel.Children.Add(stackPanelMain);
            }
            private StackPanel stackPanelMain() {
                StackPanel stackPanel = new StackPanel();
                stackPanel.Orientation = Orientation.Horizontal;
                stackPanel.Margin = new Thickness(0, 15, 0, 0);
                return stackPanel;
            }
            private Image image(string imageName) {
                Image image = new Image();
                if(!AppLibrary.Var.isNullOrEmpty(imageName)) {
                    AppLibrary.Image.setImage(image, config.Values.pathUploadsProductsFolder(this.groupId) + imageName);
                }
                image.Height = 60;
                image.Width = 60;
                image.Stretch = Stretch.Uniform;
                return image;
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
                textBlock.TextAlignment = TextAlignment.Right;
                textBlock.Text = String.Format("{0} {1}", AppLibrary.Var.toStringDecimalFormat(price), "TL"); ;
                return textBlock;
            }
            private TextBlock _priceDollar(double price)
            {
                TextBlock textBlock = new TextBlock();
                textBlock.TextAlignment = TextAlignment.Right;
                textBlock.Text = String.Format("{0} {1}", AppLibrary.Var.toStringDecimalFormat(price), "Dollar"); ;
                return textBlock;
            }
            private TextBlock _priceEuro(double price)
            {
                TextBlock textBlock = new TextBlock();
                textBlock.TextAlignment = TextAlignment.Right;
                textBlock.Text = String.Format("{0} {1}", AppLibrary.Var.toStringDecimalFormat(price), "Euro"); ;
                return textBlock;
            }
            private TextBlock _pricePound(double price)
            {
                TextBlock textBlock = new TextBlock();
                textBlock.TextAlignment = TextAlignment.Right;
                textBlock.Text = String.Format("{0} {1}", AppLibrary.Var.toStringDecimalFormat(price), "Pound"); ;
                return textBlock;
            }
            private TextBlock owner(string owner) {
                TextBlock textBlock = new TextBlock();
                textBlock.Text = owner;
                textBlock.VerticalAlignment = VerticalAlignment.Center;
                textBlock.Width = 200;
                textBlock.TextAlignment = TextAlignment.Center;
                return textBlock;
            }
            private TextBlock _event(string eventName) {
                TextBlock textBlock = new TextBlock();
                textBlock.Text = eventName;
                textBlock.VerticalAlignment = VerticalAlignment.Center;
                textBlock.Width = 200;
                textBlock.TextAlignment = TextAlignment.Center;
                return textBlock;
            }
            private Button edit(onEdit click) {
                Button button = new Button();
                button.Content = "Düzenle";
                button.Height = 30;
                button.Width = 100;
                button.Style = AppLibrary.App.getStyle("edit");
                button.Click += (o, e) => click(this.id);
                return button;
            }
            private Button delete(onDelete click, string imageName) {
                Button button = new Button();
                button.Content = "Sil";
                button.Height = 30;
                button.Width = 100;
                button.Style = AppLibrary.App.getStyle("delete");
                button.Margin = new Thickness(10, 0, 0, 0);
                button.Click += (o, e) => click(this.id, imageName);
                return button;
            }
        }
    }
}
