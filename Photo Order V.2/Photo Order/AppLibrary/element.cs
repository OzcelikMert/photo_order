using System;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace AppLibrary
{
    public class Element {
        public static void numberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
            if (e.Text == "." && textBox.Text.IndexOf(".") < 0)
            {
                e.Handled = false;
            }
        }

        public static void numberValidationTextBoxInt(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
        public static TextBlock textBlockEmpty(string emptyText, string searchText = "", Style style = null) {
            TextBlock textBlock = new TextBlock();
            textBlock.Style = style;
            textBlock.Text = (!Var.isNullOrEmpty(searchText)) ? String.Format("'{0}' ile ilgili hiçbir sonuç bulunamadı", searchText) : emptyText; ;
            return textBlock;
        }
    }
}
