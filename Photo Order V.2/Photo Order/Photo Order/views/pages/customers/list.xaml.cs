using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Photo_Order.views.pages.customers {
    public partial class list : Page {
        List<ComponentCustomers> componentList = new List<ComponentCustomers>();
        private string searchText = "";
        public list() {
            InitializeComponent();
        }
        public void initPage() {
            this.clearPage();
            this.getCustomers();
            AppLibrary.GC.GCCollect();
        }
        private void clearPage() {
            this.componentList.Clear();
            this.customerList.Children.Clear();
        }
        private void getCustomers() {
            AppLibrary.Values.logger.loggerFunction(() =>
            {
                var customers = (new config.db.functions.Select()).getCustomers(name: this.searchText);
                if (customers.Count == 0)
                {
                    this.customerList.Children.Add(AppLibrary.Element.textBlockEmpty("Henüz eklenmiş bir müşteri yok", 
                        this.searchText, 
                        AppLibrary.App.getStyle("emptyTextBlock")
                    ));
                    return;
                }
                foreach (var customer in customers)
                {
                    this.componentList.Add(new ComponentCustomers(
                         this.customerList,
                         customer.id,
                         customer.room,
                         customer.name,
                         customer.email,
                         customer.createDate,
                         (id) => {
                             this.openWindowEdit(id);
                         },
                         (id) => {
                             if (MessageBox.Show(String.Format("'{0}' isimli müşteriyi silmek istediginize emin misiniz?\nSilinecek veriler:\n- Sepete eklenmiş ürünler\n- Verilmiş olan siparişler", customer.name), "Müşteri Silme", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                             {
                                 (new config.db.functions.Delete()).setBasket(customerId: id);
                                 (new config.db.functions.Delete()).setOrderProduct(customerId: id);
                                 (new config.db.functions.Delete()).setOrder(customerId: id);
                                 (new config.db.functions.Delete()).setCustomer(id);
                                 this.initPage();
                             }
                         }
                     ));
                }
            });
        }
        private void openWindowEdit(long itemId) {
            config.Values.selectedItemId = itemId;
            config.Values.refreshList = false;
            config.Values.windowEdit = new WindowEdit(WindowEdit.TypeEdit.Customer);
            config.Values.windowEdit.ShowDialog();
            if (config.Values.refreshList) {
                this.initPage();
            }
        }
        private void refreshList(object sender, RoutedEventArgs e) {
            this.initPage();
            MessageBox.Show("Liste başarı ile yenilendi.", "Liste Yenileme", MessageBoxButton.OK, MessageBoxImage.Information);
        }
        private void searchOrder(object sender, KeyEventArgs e) {
            string search = ((TextBox)sender).Text.ToString();
            if ((!AppLibrary.Var.isNullOrEmpty(search) && search.Length > 0) || (e.Key == Key.Back && AppLibrary.Var.isNullOrEmpty(search))) {
                this.searchText = search;
                this.initPage();
            }
        }
        public delegate void onChangePassword(long id);
        public delegate void onDelete(long id);
        class ComponentCustomers {
            public long id { get; set; }
            public ComponentCustomers(
                StackPanel stackPanel,
                long id,
                string room,
                string name,
                string email,
                string createDate,
                onChangePassword onChangePassword,
                onDelete onDelete
            ) {
                this.id = id;
                StackPanel stackPanelMain = this.stackPanelMain();
                stackPanelMain.Children.Add(this.room(room));
                stackPanelMain.Children.Add(this.name(name));
                stackPanelMain.Children.Add(this.email(email));
                stackPanelMain.Children.Add(this.createDate(createDate));
                stackPanelMain.Children.Add(this.changePassword(onChangePassword));
                stackPanelMain.Children.Add(this.delete(onDelete));
                stackPanel.Children.Add(stackPanelMain);
            }
            private StackPanel stackPanelMain() {
                StackPanel stackPanel = new StackPanel();
                stackPanel.Orientation = Orientation.Horizontal;
                stackPanel.Margin = new Thickness(0, 15, 0, 0);
                return stackPanel;
            }
            private TextBlock room(string room) {
                TextBlock textBlock = new TextBlock();
                textBlock.Text = room;
                textBlock.VerticalAlignment = VerticalAlignment.Center;
                textBlock.Width = 100;
                textBlock.TextAlignment = TextAlignment.Center;
                return textBlock;
            }
            private TextBlock name(string name) {
                TextBlock textBlock = new TextBlock();
                textBlock.Text = name;
                textBlock.VerticalAlignment = VerticalAlignment.Center;
                textBlock.Width = 250;
                textBlock.TextAlignment = TextAlignment.Center;
                return textBlock;
            }
            private TextBlock email(string email) {
                TextBlock textBlock = new TextBlock();
                textBlock.Text = email;
                textBlock.VerticalAlignment = VerticalAlignment.Center;
                textBlock.Width = 250;
                textBlock.TextWrapping = TextWrapping.Wrap;
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
            private Button changePassword(onChangePassword click) {
                Button button = new Button();
                button.Content = "Şifre Değiştir";
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
