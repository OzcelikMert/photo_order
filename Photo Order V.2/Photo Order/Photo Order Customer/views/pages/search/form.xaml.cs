using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Photo_Order_Customer.views.pages.search {
    public partial class form : Page {
        private List<long> selectedItems = new List<long>();
        private List<long> selectedItemsEvent = new List<long>();
        List<ComponentProductOwner> componentList = new List<ComponentProductOwner>();
        List<ComponentEvent> componentListEvent = new List<ComponentEvent>();
        bool isOpening = true;
        List<DateTime> selectedDates = new List<DateTime>();
        public form() {
            InitializeComponent();
            this.initPage();
        }
        public void initPage() {
            this.clearPage();
            this.getProductOwners();
            this.getEvents();
        }
        public void clearPage() {
            this.calendar.SelectedDate = DateTime.Now;
            this.isActiveCalendar.IsChecked = true;
            this.selectedItems.Clear();
            this.productOwners.Children.Clear();
            this.componentList.Clear();
        }
        private void getProductOwners() {
            AppLibrary.Values.logger.loggerFunction(() =>
            {
                var owners = (new config.db.functions.Select()).getProductOwners();
                foreach (var owner in owners)
                {
                    this.componentList.Add(new ComponentProductOwner(
                        this.productOwners,
                        owner.id,
                        owner.image,
                        owner.name,
                        (id, isSelected) => {
                            if (isSelected)
                            {
                                this.selectedItems.Add(id);
                            }
                            else
                            {
                                this.selectedItems.Remove(id);
                            }
                        }
                    ));
                }
            });
        }
        private void getEvents() {
            AppLibrary.Values.logger.loggerFunction(() =>
            {
                var events = (new config.db.functions.Select()).getEvents();
                foreach (var _event in events)
                {
                    this.componentListEvent.Add(new ComponentEvent(
                        this.events,
                        _event.id,
                        _event.image,
                        _event.name,
                        (id, isSelected) => {
                            if (isSelected)
                            {
                                this.selectedItemsEvent.Add(id);
                            }
                            else
                            {
                                this.selectedItemsEvent.Remove(id);
                            }
                        }
                    ));
                }
            });
        }
        private void calendarLoaded(object sender, RoutedEventArgs e) {
            this.calendar.DisplayDate = DateTime.Now;
        }
        private void changeActiveCalendar(object sender, RoutedEventArgs e) {
            if(this.isActiveCalendar.IsChecked == true) {
                this.calendar.IsEnabled = true;
            } else {
                this.calendar.IsEnabled = false;
            }
        }
        private void clearSelectedDates(object sender, RoutedEventArgs e) {
            selectedDates.Clear();
            this.calendar.SelectedDates.Clear();
        }
        private void selectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {
            if (isOpening) {
                isOpening = false;
                return;
            }
            if (this.calendar.SelectedDate.HasValue) {
                DateTime selectedDate = (DateTime)this.calendar.SelectedDate;
                if (selectedDates.IndexOf(selectedDate) > -1) {
                    selectedDates.Remove(selectedDate);
                } else {
                    selectedDates.Add(selectedDate);
                }
            }
            this.calendar.SelectedDates.Clear();
            foreach (var date in selectedDates.ToArray()) {
                isOpening = true;
                this.calendar.SelectedDates.Add(date);
                isOpening = false;
            }
        }
        private void searchProducts(object sender, RoutedEventArgs e) {
            List<string> date = new List<string>();
            foreach (var selectedDate in this.calendar.SelectedDates) {
                date.Add(AppLibrary.Var.toStringDateFormatDB(selectedDate));
            }
            if (date.Count == 0) {
                date.Add(AppLibrary.Var.toStringDateFormatDB(DateTime.Now));
            }
            config.Values.tabControl.pageProducts.initPage(this.selectedItems, this.selectedItemsEvent, ((this.calendar.IsEnabled) ? date.ToArray() : null));
            config.Values.tabControl.setPage(WindowMain.TabPages.Products);
        }
        public delegate void onSelect(long id, bool isSelected);
        class ComponentProductOwner {
            public long id { get; set; }
            public bool isSelected { get; set; }
            public ComponentProductOwner(
                StackPanel stackPanel,
                long id,
                string image,
                string name,
                onSelect onSelect
            ){
                this.id = id;
                this.isSelected = false;
                Grid gridSelected = this.gridSelected();
                Grid gridMain = this.gridMain(onSelect, gridSelected);
                StackPanel stackPanelInfo = new StackPanel();
                stackPanelInfo.Children.Add(this.image(image));
                stackPanelInfo.Children.Add(this.name(name));
                gridMain.Children.Add(stackPanelInfo);
                stackPanel.Children.Add(gridMain);
            }
            private Grid gridMain(onSelect func, Grid gridSelected){
                Grid grid = new Grid();
                grid.Height = 220;
                grid.Width = 220;
                grid.Cursor = Cursors.Hand;
                grid.Margin = new Thickness(10, 0, 0, 0);
                grid.MouseDown += (o, e) => {
                    if(this.isSelected) {
                        grid.Children.Remove(gridSelected);
                        this.isSelected = false;
                    }else {
                        grid.Children.Add(gridSelected);
                        this.isSelected = true;
                    }
                    func(this.id, this.isSelected);
                };
                return grid;
            }
            private StackPanel stackPanelInfo() {
                StackPanel stackPanel = new StackPanel();
                return stackPanel;
            }
            private Image image(string imageName) {
                Image image = new Image();
                image.Width = 220;
                image.Height = 200;
                image.Stretch = Stretch.Uniform;
                if (!AppLibrary.Var.isNullOrEmpty(imageName)) {
                    AppLibrary.Image.setImage(image, config.Values.pathUploadsProductOwnersFolder + imageName);
                }
                return image;
            }
            private TextBlock name(string name) {
                TextBlock textBlock = new TextBlock();
                textBlock.Text = name;
                textBlock.VerticalAlignment = VerticalAlignment.Bottom;
                textBlock.TextAlignment = TextAlignment.Center;
                textBlock.Style = AppLibrary.App.getStyle("inputInfoTextBlock");
                return textBlock;
            }
            private Grid gridSelected() {
                Grid grid = new Grid();
                grid.Style = AppLibrary.App.getStyle("gridSelected");
                Panel.SetZIndex(grid, 1);
                grid.Children.Add(this.imageSelected());
                return grid;
            }
            private Image imageSelected(string imageName = "success-tick.png") {
                Image image = new Image();
                if (!AppLibrary.Var.isNullOrEmpty(imageName)){
                    BitmapImage bi = new BitmapImage();
                    bi.BeginInit();
                    bi.UriSource = new Uri(config.Values.pathImagesFolder + imageName);
                    bi.EndInit();
                    image.Source = bi;
                }
                image.Height = 75;
                image.Width = 75;
                image.HorizontalAlignment = HorizontalAlignment.Center;
                image.VerticalAlignment = VerticalAlignment.Top;
                image.Margin = new Thickness(0, 25, 0, 0);
                return image;
            }
        }
        class ComponentEvent
        {
            public long id { get; set; }
            public bool isSelected { get; set; }
            public ComponentEvent(
                StackPanel stackPanel,
                long id,
                string image,
                string name,
                onSelect onSelect
            )
            {
                this.id = id;
                this.isSelected = false;
                Grid gridSelected = this.gridSelected();
                Grid gridMain = this.gridMain(onSelect, gridSelected);
                StackPanel stackPanelInfo = new StackPanel();
                stackPanelInfo.Children.Add(this.image(image));
                stackPanelInfo.Children.Add(this.name(name));
                gridMain.Children.Add(stackPanelInfo);
                stackPanel.Children.Add(gridMain);
            }
            private Grid gridMain(onSelect func, Grid gridSelected)
            {
                Grid grid = new Grid();
                grid.Height = 220;
                grid.Width = 220;
                grid.Cursor = Cursors.Hand;
                grid.Margin = new Thickness(10, 0, 0, 0);
                grid.MouseDown += (o, e) => {
                    if (this.isSelected)
                    {
                        grid.Children.Remove(gridSelected);
                        this.isSelected = false;
                    }
                    else
                    {
                        grid.Children.Add(gridSelected);
                        this.isSelected = true;
                    }
                    func(this.id, this.isSelected);
                };
                return grid;
            }
            private StackPanel stackPanelInfo()
            {
                StackPanel stackPanel = new StackPanel();
                return stackPanel;
            }
            private Image image(string imageName)
            {
                Image image = new Image();
                image.Width = 220;
                image.Height = 200;
                image.Stretch = Stretch.Uniform;
                if (!AppLibrary.Var.isNullOrEmpty(imageName)) {
                    AppLibrary.Image.setImage(image, config.Values.pathUploadsEventsFolder + imageName);
                }
                return image;
            }
            private TextBlock name(string name)
            {
                TextBlock textBlock = new TextBlock();
                textBlock.Text = name;
                textBlock.VerticalAlignment = VerticalAlignment.Bottom;
                textBlock.TextAlignment = TextAlignment.Center;
                textBlock.Style = AppLibrary.App.getStyle("inputInfoTextBlock");
                return textBlock;
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
                image.HorizontalAlignment = HorizontalAlignment.Center;
                image.VerticalAlignment = VerticalAlignment.Top;
                image.Margin = new Thickness(0, 25, 0, 0);
                return image;
            }
        }
    }
}
