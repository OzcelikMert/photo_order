using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Photo_Order.views.pages.events {
    public partial class list : Page {
        private List<ComponentEvents> componentList = new List<ComponentEvents>();
        private string searchText = "";
        public list() {
            InitializeComponent();
            this.initPage();
        }
        private void initPage() {
            this.clearPage();
            this.getEvents();
            AppLibrary.GC.GCCollect();
        }
        private void clearPage() {
            this.componentList.Clear();
            this.eventList.Children.Clear();
        }
        private void getEvents() {
            AppLibrary.Values.logger.loggerFunction(() =>
            {
                var events = (new config.db.functions.Select()).getEvents(name: this.searchText);
                if (events.Count == 0)
                {
                    this.eventList.Children.Add(AppLibrary.Element.textBlockEmpty("Henüz eklenmiş bir etkinlik yok", 
                        this.searchText, 
                        AppLibrary.App.getStyle("emptyTextBlock")));
                    return;
                }
                foreach (var _event in events)
                {
                    componentList.Add(new ComponentEvents(
                        this.eventList,
                        _event.id,
                        _event.image,
                        _event.createDate,
                        _event.name,
                        (sender, args) => {
                            this.openWindowEdit(_event.id);
                        },
                        (sender, args) => {
                            if (MessageBox.Show(String.Format("'{0}' isimli etkinligi silmek istediğinizden emin misiniz?", _event.name), "Etkinlik Silme", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                            {
                                (new config.db.functions.Delete()).setEvents(_event.id);
                                this.initPage();
                                AppLibrary.File.removeImage(config.Values.pathUploadsEventsFolder, _event.image);
                            }
                        }
                    ));
                }
            });
        }
        private void createNewClick(object sender, RoutedEventArgs e) {
            this.openWindowEdit(0);
        }
        private void searchEvent(object sender, KeyEventArgs e) {
            string eventName = ((TextBox)sender).Text.ToString();
            if ((!AppLibrary.Var.isNullOrEmpty(eventName) && eventName.Length > 2) || (e.Key == Key.Back && AppLibrary.Var.isNullOrEmpty(eventName))) {
                this.searchText = eventName;
                this.initPage();
            }
        }
        private void openWindowEdit(long itemId)
        {
            config.Values.selectedItemId = itemId;
            config.Values.refreshList = false;
            config.Values.windowEdit = new WindowEdit(WindowEdit.TypeEdit.Event);
            config.Values.windowEdit.ShowDialog();
            if (config.Values.refreshList) {
                this.initPage();
            }
        }

        class ComponentEvents {
            public long id { get; set; }
            public ComponentEvents(
                StackPanel stackPanel,
                long id,
                string image,
                string createDate,
                string name,
                RoutedEventHandler onEdit,
                RoutedEventHandler onDelete
            )
            {
                this.id = id;
                StackPanel stackPanelMain = this.stackPanelMain();
                stackPanelMain.Children.Add(this.image(image));
                stackPanelMain.Children.Add(this.name(name));
                stackPanelMain.Children.Add(this.createDate(createDate));
                stackPanelMain.Children.Add(this.edit(onEdit));
                stackPanelMain.Children.Add(this.delete(onDelete));

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
                if (!AppLibrary.Var.isNullOrEmpty(imageName)) {
                    AppLibrary.Image.setImage(image, config.Values.pathUploadsEventsFolder + imageName);
                }
                image.Height = 60;
                image.Width = 60;
                image.Stretch = Stretch.Uniform;
                return image;
            }
            private TextBlock name(string name) {
                TextBlock textBlock = new TextBlock();
                textBlock.Text = name;
                textBlock.VerticalAlignment = VerticalAlignment.Center;
                textBlock.Width = 250;
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
            private Button edit(RoutedEventHandler click) {
                Button button = new Button();
                button.Content = "Düzenle";
                button.Height = 30;
                button.Width = 100;
                button.Style = AppLibrary.App.getStyle("edit");
                button.Click += click;
                return button;
            }
            private Button delete(RoutedEventHandler click) {
                Button button = new Button();
                button.Content = "Sil";
                button.Height = 30;
                button.Width = 100;
                button.Style = AppLibrary.App.getStyle("delete");
                button.Margin = new Thickness(10, 0, 0, 0);
                button.Click += click;
                return button;
            }
        }
    }
}
