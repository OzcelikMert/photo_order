using System.Windows;

namespace Photo_Order_Customer
{
    /// <summary>
    /// Interaction logic for WindowViewImage.xaml
    /// </summary>
    public partial class WindowViewImage : Window
    {
        private string pathImage { get; set; }
        public WindowViewImage(string pathImage, string highImage) {
            InitializeComponent();
            this.pathImage = pathImage;
            this.initWindow(highImage);
        }
        private void initWindow(string highImage) {
            AppLibrary.Values.logger.loggerFunction(() =>
            {
                AppLibrary.Image.setImage(this.image, this.pathImage);
                this.imageName.Text = highImage;
            });
        }

        private void close(object sender, RoutedEventArgs e) => this.Close();
    }
}
