   M S B A M L   `   `   `  ���� F  BPhoto Order, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullQ-clr-namespace:Photo_Order.views.pages.productPhoto_Order.views.pages.product  .    (Photo_Order.views.pages.product.addGroup   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     ?local-clr-namespace:Photo_Order.views.pages.product       N� 
Background
  ���� .���� $��Center=�$��15,15,15,15q�$��Center=�.+��� -    ��Name$ stackPanelImage�� �� Orientation$ Vertical=�.+��� -    ��Name$ 
imagesMain��$	��500��$	��350��$
��Auto=�$y�	Collapsed=�.���� -   $ images���� -    ��Name$ image��$	��500��$	��350��$��/assets/images/empty.jpg��$��Uniform=�� $��0,5,0,0q�.+��� -   $��
Resim Ekle��$	��150��$��35��   addImage#��[  �� $ 
Horizontal=�$��0,15,0,0q�.+��� $	��250��.+��� .+��� $��Fiyat (TL)*��  inputInfoTextBlock#��[ �� -    ��Name$ priceTurkishLira��$��0�� �� 	MaxLength$ 10��  editTextBox#��[ �� .+��� $��Fiyat (Dolar)*��#��[ �� -   $ priceDollar��$��0��$ 10��#��[ �� .+��� $��Fiyat (Euro)*��#��[ �� -   $ 	priceEuro��$��0��$ 10��#��[ �� .+��� $��Fiyat (Pound)*��#��[ �� -	   $ 
pricePound��$��0��$ 10��#��[ �� $��0,10,0,0q�.+�(� $
��Left=�$	��100��.���� -
    ��Name$ isActiveSlider��$��Slider Aktif�� d� 	IsChecked$
 trueY��� $	��250��$��5,0,0,0q�.+��� .+��� $��Fotoğrafçı*��#��[ �� -   	 ��Name$	 owner��
 �� DisplayMemberPath$

 Name�� �� SelectedValuePath$ Id��  editComboBox#��[ �� .+��� $��Etkinlik��#��[ �� -   $	 event��$

 Name��$ Id��#��[ �� .+��� $��Tarih��#��[ &   System.Windows.Controls.Calendar  -     Name$ calendar��
���   �$
��Left=�   IsTodayHighlighted .  �� -    ��Name$
 save��$��Kaydet��$��Right=�$	��150��$��35��$��0,15,0,0q�  save#��[ 