   M S B A M L   `   `   `  ���� F  BPhoto Order, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:Photo_OrderPhoto_Order  #    Photo_Order.WindowOrderDetail   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     +localclr-namespace:Photo_Order       � Title$  Siparis Detayi�� � WindowStartupLocation$ CenterScreen=� � Icon$ /sic_267.ico��.���� $��	5,10,5,10q� �� Orientation$ 
Horizontal=�$	��Top=�$��Center=�.+��� .+��� $ 
Horizontal=�   customTable#��[  .+��� $��Miktar��$��50�� �� TextAlignment$ Center=��� $��Fotograf��$��75��$ Center=��� $��	Fotograci��$	��250��$ Center=��� $��Fiyat��$	��100��$
 Left=��� $��Ekleme Tarihi��$	��150��$ Center=��� $��Duzenle��$	��100��$ Center=��� $	��Sil��$	��100��$ Center=��� $
��Auto=�$	��700��.���� -    ��Name$ orderProductList��$��	10,0,0,15q�.+��� $ 
Horizontal=�$��0,15,0,0q�.+��� $��3x��$��Center=�$��50��$ Center=��� $��60��$��60��$
��Fill=��� $��Mert OZCELIK��$��Center=�$	��250��$ Center=��� $	��100��.+��� $��50 TL��$��Center=�$
 Left=��� $��50 Dolar��$��Center=�$
 Left=��� $��50 Euro��$��Center=�$
 Left=��� $��50 Pound��$��Center=�$
 Left=��� $��
15/05/2022��$��Center=�$	��150��$ Center=��� $��Duzenle��$��30��$	��100��  edit#��[ �� $	��Sil��$��30��$	��100��$��10,0,5,0q� 
 delete#��[ �� $��25,0,5,0q�$	��350��.+��� $��Toplam��$ Center=�  titleTextBlock#��[ �� -    ��Name$ priceTurkishLira��$��50 TL��$
 Left=�  priceTextBlock#��[ �� -   $ priceDollar��$��50 Dolar��$
 Left=�#��[ �� -   $ 	priceEuro��$��50 Euro��$
 Left=�#��[ �� -   $ 
pricePound��$��50 Pound��$
 Left=�#��[ �� $ 
Horizontal=�$��Center=�$��0,10,0,0q�.+��� $"��Seçilen Fotoğraf Sayısı:��$ Center=�#��[ $��12(�$��Center=��� -   $ 
totalCount��$��0��$ Center=�#��[ $��5,0,0,0q��� $��Ödeme Tipi��$
 Left=�$��0,25,0,0q�  inputInfoTextBlock#��[ �� -    ��Name$ paymentType�� �� SelectedValuePath$ Content��  editComboBox#��[ .@��� $��Turkish Lira���� $��Dollar���� $
��Euro���� $��Pound���� -   $��
Ödeme Yap��$��35��$��0,25,0,0q�  save#��[ 