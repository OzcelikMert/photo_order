using System;
using System.Windows;
using System.Windows.Controls;

namespace Photo_Order
{
    /// <summary>
    /// Interaction logic for WindowEdit.xaml
    /// </summary>
    public partial class WindowEdit : Window
    {
        private TypeEdit typeEdit { get; set; }
        public WindowEdit(TypeEdit typeEdit) {
            InitializeComponent();
            if(config.Values.selectedItemId > 0) {
                this.Title = "Düzenle";
            }
            this.typeEdit = typeEdit;
            this.setPage();
        }
        private void setPage() {
            Frame frame = new Frame();
            frame.NavigationUIVisibility = System.Windows.Navigation.NavigationUIVisibility.Hidden;
            switch (typeEdit) {
                case TypeEdit.Product: frame.Navigate(new views.pages.product.add()); break;
                case TypeEdit.ProductGroup: frame.Navigate(new views.pages.product.addGroup()); break;
                case TypeEdit.ProductOwner: frame.Navigate(new views.pages.productOwner.add()); break;
                case TypeEdit.Event: frame.Navigate(new views.pages.events.add()); break;
                case TypeEdit.OrderProduct: frame.Navigate(new views.pages.orders.edit()); break;
                case TypeEdit.Customer: frame.Navigate(new views.pages.customers.edit()); break;
            }
            this.gridMain.Children.Clear();
            this.gridMain.Children.Add(frame);
        }
        public enum TypeEdit {
            Product,
            ProductGroup,
            ProductOwner,
            Event,
            OrderProduct,
            Customer
        }

        private void windowClosed(object sender, EventArgs e) {
            config.Values.selectedItemId = 0;
        }
    }
}
