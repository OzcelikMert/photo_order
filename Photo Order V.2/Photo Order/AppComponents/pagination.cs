using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace AppComponents
{
    public class ComponentPagination {
        public delegate void onPaginate(int pageNumber);
        public ComponentPagination(
                StackPanel stackPanel,
                int maxPageNumber,
                int currentPageNumber,
                onPaginate onPaginate
            )
        {
            stackPanel.Children.Add(pageButton(onPaginate, "<<", 1, (1 == currentPageNumber)));

            stackPanel.Children.Add(pageButton(onPaginate, "<", currentPageNumber - 1, (1 == currentPageNumber)));

            ComboBox comboBox = this.pageCountSelect(onPaginate);
            for (int i = 0; i < maxPageNumber; i++)
            {
                comboBox.Items.Add(this.pageCountSelectItem(i + 1));
            }
            comboBox.SelectedIndex = currentPageNumber - 1;
            stackPanel.Children.Add(comboBox);

            stackPanel.Children.Add(pageButton(onPaginate, ">", currentPageNumber + 1, (maxPageNumber == currentPageNumber)));

            stackPanel.Children.Add(pageButton(onPaginate, ">>", maxPageNumber, (maxPageNumber == currentPageNumber)));
        }

        private Button pageButton(onPaginate click, string buttonContent, int pageNumber, bool isCurrentPage) {
            Button button = new Button();
            button.Content = buttonContent;
            button.Height = 45;
            button.MinWidth = 75;
            button.Style = AppLibrary.App.getStyle("addImage");
            button.IsEnabled = !isCurrentPage;
            button.Click += (o, e) => {
                click(pageNumber);
            };
            return button;
        }
        private ComboBox pageCountSelect(onPaginate click)
        {
            ComboBox cb = new ComboBox();
            cb.Height = 45;
            cb.MinWidth = 150;
            cb.FontSize = 18;
            cb.HorizontalContentAlignment = System.Windows.HorizontalAlignment.Center;
            cb.VerticalContentAlignment = System.Windows.VerticalAlignment.Center;
            cb.SelectionChanged += (o, e) => {
                click(cb.SelectedIndex + 1);
            };
            return cb;
        }
        private ComboBoxItem pageCountSelectItem(int pageNumber)
        {
            ComboBoxItem cb = new ComboBoxItem();
            cb.Content = pageNumber;
            cb.Height = 45;
            cb.MinWidth = 150;
            return cb;
        }
    }
}
