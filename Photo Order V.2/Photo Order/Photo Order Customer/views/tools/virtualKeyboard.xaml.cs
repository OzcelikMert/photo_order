using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Photo_Order_Customer.views.tools
{
    /// <summary>
    /// Interaction logic for virtualKeyboard.xaml
    /// </summary>
    public partial class VirtualKeyboard : Page
    {
        public VirtualKeyboard()
        {
            InitializeComponent();
        }

        public void showKeyboard() {
            this.Visibility = Visibility.Visible;
        }

        public void hideKeyboard() {
            this.Visibility = Visibility.Collapsed;
        }

        private void _btnAlphaKeyboard_Click(object sender, RoutedEventArgs e)
        {
            Keyboard.KeyboardState = Rife.Keyboard.KeyboardState.AlphaNumeric;
        }

        private void _btnNumericKeyboard_Click(object sender, RoutedEventArgs e)
        {
            Keyboard.KeyboardState = Rife.Keyboard.KeyboardState.Numeric;
        }

        private void closeKeyboard(object sender, RoutedEventArgs e) {
            this.hideKeyboard();
        }
        public void textboxFocus(object sender, RoutedEventArgs e) {
            var input = (Control)sender;
            if (input.IsFocused) {
                this.showKeyboard();
            }
        }
        public static Frame createKeyboard(object content) {
            Frame frame = new Frame();
            frame.HorizontalAlignment = HorizontalAlignment.Center;
            frame.VerticalAlignment = VerticalAlignment.Bottom;
            Panel.SetZIndex(frame, 9);
            frame.Navigate(content);
            return frame;
        }
    }
}
