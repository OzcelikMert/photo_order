using AppComponents;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Photo_Order.views.pages.orders
{
    public partial class list : Page {
        List<ComponentOrders> componentList = new List<ComponentOrders>();
        private string searchText = "";
        private List<config.db.functions.Select.ListOrder> listOfOrder = new List<config.db.functions.Select.ListOrder>();
        private int productMaxCount = 20;
        private int currentPageNumber = 1;
        public list() {
            InitializeComponent();
        }
        public void initPage(bool pagination = false) {
            this.clearPage(pagination);
            this.getOrders(pagination);
            AppLibrary.GC.GCCollect();
        }
        private void clearPage(bool pagination) {
            this.componentList.Clear();
            this.orderList.Children.Clear();
            this.pagination.Children.Clear();
            if (!pagination) {
                this.currentPageNumber = 1;
                listOfOrder.Clear();
            }
        }
        private void getOrders(bool pagination) {
            AppLibrary.Values.logger.loggerFunction(() =>
            {
                if (!pagination) {
                    this.listOfOrder = (new config.db.functions.Select()).getOrders(customerName: this.searchText);
                }
                if (listOfOrder.Count == 0)
                {
                    this.orderList.Children.Add(AppLibrary.Element.textBlockEmpty("Henüz verilmiş bir sipariş yok", 
                        this.searchText,
                        AppLibrary.App.getStyle("emptyTextBlock")));
                    return;
                }
                int listCount = listOfOrder.Count;
                int maxPageNumber = Convert.ToInt32(Math.Ceiling((double)listCount / this.productMaxCount));
                int startIndex = (this.currentPageNumber - 1) * this.productMaxCount;
                int productCount = (maxPageNumber == this.currentPageNumber)
                    ? listCount - startIndex
                    : this.productMaxCount;
                createComponentsOrders(listOfOrder.GetRange(startIndex, productCount));
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
        private void createComponentsOrders(List<config.db.functions.Select.ListOrder> orders = null)
        {
            AppLibrary.Values.logger.loggerFunction(() =>
            {
                foreach (var order in orders)
                {
                    this.componentList.Add(new ComponentOrders(
                         this.orderList,
                         order.id,
                         order.createDate,
                         order.customerName,
                         order.customerRoom,
                         order.customerEmail,
                         (id) =>
                         {
                             WindowOrderDetail windowOrderDetail = new WindowOrderDetail(id, order.customerId, order.customerRoom, order.customerName, order.customerEmail, order.createDate);
                             if (windowOrderDetail.ShowDialog() == true)
                             {
                                 this.initPage();
                             }
                         },
                         (id) =>
                         {
                             if (MessageBox.Show("Bu siparisi silmek istediginize emin misiniz? ", "Siparis Silme", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                             {
                                 (new config.db.functions.Delete()).setOrderProduct(orderId: id);
                                 (new config.db.functions.Delete()).setOrder(id);
                                 this.initPage();
                             }
                         }
                     ));
                }
            });
        }
        private void refreshList(object sender, RoutedEventArgs e) {
            this.initPage();
            MessageBox.Show("Liste başarı ile yenilendi.", "Liste Yenileme", MessageBoxButton.OK, MessageBoxImage.Information);
        }
        private void searchOrder(object sender, KeyEventArgs e) {
            string search = ((TextBox)sender).Text.ToString();
            if ((!AppLibrary.Var.isNullOrEmpty(search) && search.Length > 0) || (e.Key == Key.Back && AppLibrary.Var.isNullOrEmpty(search)))
            {
                this.searchText = search;
                this.initPage();
            }
        }
        public delegate void onDetail(long id);
        public delegate void onDelete(long id);
        class ComponentOrders {
            public long id { get; set; }
            public ComponentOrders(
                StackPanel stackPanel,
                long id,
                string createDate,
                string name,
                string room,
                string email,
                onDetail onDetail,
                onDelete onDelete
            )
            {
                this.id = id;
                StackPanel stackPanelMain = this.stackPanelMain();
                stackPanelMain.Children.Add(this._id(id));
                stackPanelMain.Children.Add(this.createDate(createDate));
                stackPanelMain.Children.Add(this.room(room));
                stackPanelMain.Children.Add(this.name(name));
                stackPanelMain.Children.Add(this.email(email));
                stackPanelMain.Children.Add(this.detail(onDetail));
                stackPanelMain.Children.Add(this.delete(onDelete));

                stackPanel.Children.Add(stackPanelMain);
            }
            private StackPanel stackPanelMain() {
                StackPanel stackPanel = new StackPanel();
                stackPanel.Orientation = Orientation.Horizontal;
                stackPanel.Margin = new Thickness(0, 15, 0, 0);
                return stackPanel;
            }
            private TextBlock _id(long id) {
                TextBlock textBlock = new TextBlock();
                textBlock.Text = id.ToString();
                textBlock.VerticalAlignment = VerticalAlignment.Center;
                textBlock.Width = 100;
                textBlock.TextAlignment = TextAlignment.Center;
                return textBlock;
            }
            private TextBlock email(string email)
            {
                TextBlock textBlock = new TextBlock();
                textBlock.Text = email;
                textBlock.VerticalAlignment = VerticalAlignment.Center;
                textBlock.Width = 200;
                textBlock.TextAlignment = TextAlignment.Center;
                return textBlock;
            }
            private TextBlock name(string name) {
                TextBlock textBlock = new TextBlock();
                textBlock.Text = name;
                textBlock.VerticalAlignment = VerticalAlignment.Center;
                textBlock.Width = 200;
                textBlock.TextAlignment = TextAlignment.Center;
                return textBlock;
            }
            private TextBlock room(string room) {
                TextBlock textBlock = new TextBlock();
                textBlock.Text = room;
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
            private Button detail(onDetail click) {
                Button button = new Button();
                button.Content = "Detay";
                button.Height = 30;
                button.Width = 100;
                button.Style = AppLibrary.App.getStyle("edit");
                button.Click += (o, e) => {
                    click(this.id);
                };
                return button;
            }
            private Button delete(onDelete click) {
                Button button = new Button();
                button.Content = "Sil";
                button.Height = 30;
                button.Width = 100;
                button.Style = AppLibrary.App.getStyle("delete");
                button.Margin = new Thickness(10, 0, 0, 0);
                button.Click += (o, e) => {
                    click(this.id);
                };
                return button;
            }
        }
    }
}
