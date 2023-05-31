using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Microsoft.Win32;

namespace Photo_Order.views.pages.product
{
    public partial class addGroup : Page {
        public addGroup() {
            InitializeComponent();
            this.setOwners();
            this.setEvents();
            if(config.Values.selectedItemId > 0){
                this.setInputs();
            } else {
                this.calendar.SelectedDate = DateTime.Now;
            }
        }
        OpenFileDialog chooseImage { get; set; }
        List<Owner> owners { get; set; }
        List<Events> events { get; set; }
        private void saveClick(object sender, RoutedEventArgs e) {
            AppLibrary.Values.logger.loggerFunction(() => {
                this.save.IsEnabled = false;
                views.tools.LoaderProgress loaderProgress = new views.tools.LoaderProgress((worker) => {
                    string priceTurkishLira = "0",
                            priceDollar = "0",
                            priceEuro = "0",
                            pricePound = "0";
                    long owner = 0,
                        eventId = 0;
                    DateTime createDate = DateTime.Now;
                    bool isSliderActive = true;
                    Dispatcher.Invoke(() => {
                        priceTurkishLira = this.priceTurkishLira.Text.ToString();
                        priceDollar = this.priceDollar.Text.ToString();
                        priceEuro = this.priceEuro.Text.ToString();
                        pricePound = this.pricePound.Text.ToString();
                        owner = ((Owner)this.owner.SelectedItem).Id;
                        eventId = ((Events)this.@event.SelectedItem).Id;
                        createDate = (this.calendar.SelectedDates.Count == 0) ? DateTime.Now : (DateTime)this.calendar.SelectedDate;
                        isSliderActive = (bool)this.isActiveSlider.IsChecked;
                    });
                    if (AppLibrary.Var.isNullOrEmpty(owner, eventId, priceTurkishLira, priceDollar, priceEuro, pricePound))
                    {
                        MessageBox.Show("Lütfen gerekli yerleri doldurunuz!", "Hatalı Giriş", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    else
                    {
                        owner = Convert.ToInt64(owner);
                        eventId = Convert.ToInt64(eventId);
                        List<string> images = new List<string>();
                        List<string> imagesHigh = new List<string>();
                        long productGroupId = config.Values.selectedItemId;
                        if (config.Values.selectedItemId == 0)
                        {
                            productGroupId = (new config.db.functions.Insert()).setProductGroups(owner, eventId, createDate, isSliderActive,
                                Convert.ToDouble(priceTurkishLira),
                                Convert.ToDouble(priceDollar),
                                Convert.ToDouble(priceEuro),
                                Convert.ToDouble(pricePound));

                            if (!(AppLibrary.Var.isNullOrEmpty(this.chooseImage) || AppLibrary.Var.isNullOrEmpty(this.chooseImage.FileName)))
                            {
                                foreach (var fileName in this.chooseImage.FileNames)
                                {
                                    if (fileName.IndexOf(config.Values.pathImageHigh) == -1)
                                    {
                                        MessageBox.Show(
                                            String.Format("{0}\nGecerli bir konum degil! Gecerli bir konum ornegi:\n{1}", fileName, config.Values.pathImageHigh),
                                            "Gecersiz Konum", MessageBoxButton.OK, MessageBoxImage.Warning);
                                        return;
                                    }
                                    imagesHigh.Add(fileName.Replace(config.Values.pathImageHigh, ""));
                                }
                                int index = 0;
                                foreach (var name in this.chooseImage.FileNames) {
                                    images.Add(AppLibrary.File.saveImage(config.Values.pathUploadsProductsFolder(productGroupId), name: name));
                                    index++;
                                    worker.ReportProgress(index);
                                }
                            }
                        }


                        if (config.Values.selectedItemId == 0)
                        {
                            // Add
                            (new config.db.functions.Insert()).setProducts(productGroupId, images.ToArray(), imagesHigh.ToArray(), isSliderActive);
                        }
                        else
                        {
                            // Edit
                            (new config.db.functions.Update()).setProductGroups(productGroupId,
                                owner,
                                eventId,
                                createDate,
                                isSliderActive,
                                Convert.ToDouble(priceTurkishLira),
                                Convert.ToDouble(priceDollar),
                                Convert.ToDouble(priceEuro),
                                Convert.ToDouble(pricePound)
                            );
                        }

                        MessageBox.Show("İşlem başarılı ürün kayıt edildi.", "Ürün Kaydedildi", MessageBoxButton.OK, MessageBoxImage.Information);
                        config.Values.refreshList = true;

                        Dispatcher.Invoke(() => {
                            config.Values.windowEdit.Close();
                        });
                    }
                }, () => {
                    this.save.IsEnabled = true;
                });
                if (!(AppLibrary.Var.isNullOrEmpty(this.chooseImage) || AppLibrary.Var.isNullOrEmpty(this.chooseImage.FileName))) {
                    loaderProgress.progressMax = this.chooseImage.FileNames.Length;
                }
                loaderProgress._loaderTextInfo = "Seçilen resim {0} Yüklenen resim {1}";
                loaderProgress.Run();
                this.save.IsEnabled = false;
            });
        }
        private void setInputs() {
            AppLibrary.Values.logger.loggerFunction(() => {
                this.stackPanelImage.Visibility = Visibility.Collapsed;
                var productGroups = (new config.db.functions.Select()).getProductGroups(config.Values.selectedItemId);
                foreach (var productGroup in productGroups) {
                    this.priceTurkishLira.Text = productGroup.priceTurkishLira.ToString();
                    this.priceDollar.Text = productGroup.priceDollar.ToString();
                    this.priceEuro.Text = productGroup.priceEuro.ToString();
                    this.pricePound.Text = productGroup.pricePound.ToString();
                    this.owner.SelectedIndex = this.owners.FindIndex((item) => item.Id == productGroup.ownerId);
                    this.@event.SelectedIndex = this.events.FindIndex((item) => item.Id == productGroup.eventId);
                    Debug.WriteLine(productGroup.createDate, DateTime.Parse(productGroup.createDate));
                    this.calendar.SelectedDate = DateTime.Parse(productGroup.createDate);
                    this.calendar.DisplayDate = DateTime.Parse(productGroup.createDate);
                    this.isActiveSlider.IsChecked = productGroup.isSliderActive;
                }
            });
        }
        private void chooseImageClick(object sender, RoutedEventArgs e) {
            AppLibrary.Values.logger.loggerFunction(() =>
            {
                this.chooseImage = new OpenFileDialog();
                if (config.Values.selectedItemId == 0)
                {
                    this.chooseImage.Multiselect = true;
                }
                this.chooseImage.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.webp;";
                if (this.chooseImage.ShowDialog() == true)
                {
                    if (this.chooseImage.FileNames.Length > 1) {
                        this.image.Visibility = Visibility.Collapsed;
                        this.imagesMain.Visibility = Visibility.Visible;
                        this.images.Children.Clear();
                        int count = 1;
                        foreach (var name in this.chooseImage.FileNames)
                        {
                            TextBlock textBlock = new TextBlock();
                            textBlock.Text = String.Format("{0}- {1}", count, name);
                            this.images.Children.Add(textBlock);
                            count++;
                        }
                    }
                    else {
                        this.image.Visibility = Visibility.Visible;
                        this.imagesMain.Visibility = Visibility.Collapsed;
                        AppLibrary.Image.setImage(this.image, this.chooseImage.FileName);
                    }
                }
            });
        }
        private void setOwners() {
            AppLibrary.Values.logger.loggerFunction(() =>
            {
                this.owners = new List<Owner>();
                foreach (var owner in (new config.db.functions.Select()).getProductOwners())
                {
                    this.owners.Add(new Owner { Id = owner.id, Name = owner.name });
                }
                this.owner.ItemsSource = owners;
                this.owner.SelectedIndex = 0;
            });
        }
        private void setEvents() {
            AppLibrary.Values.logger.loggerFunction(() =>
            {
                this.events = new List<Events>();
                this.events.Add(new Events { Id = 0, Name = "Yok" });
                foreach (var _event in (new config.db.functions.Select()).getEvents())
                {
                    this.events.Add(new Events { Id = _event.id, Name = _event.name });
                }
                this.@event.ItemsSource = events;
                this.@event.SelectedIndex = 0;
            });
        }
        class Owner {
            public long Id { get; set; }
            public string Name { get; set; }
        }
        class Events {
            public long Id { get; set; }
            public string Name { get; set; }
        }

        private void numberPreviewTextInput(object sender, TextCompositionEventArgs e) => AppLibrary.Element.numberValidationTextBox(sender, e);
    }
}
