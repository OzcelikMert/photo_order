   M S B A M L   `   `   `  ����F  BPhoto Order, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:Photo_OrderPhoto_Order  5   	   #    Photo_Order.WindowOrderDetail   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   +localclr-namespace:Photo_Order     5   	     � Title$  Siparis Detayi��5   	    � WindowStartupLocation$ CenterScreen=�6     � Icon$ /sic_267.ico��6E   .��5	      �� $��	5,10,5,10q�6    �� Orientation$ 
Horizontal=�6$   $	��Top=�6=   $��Center=�6U   .+�5
   
   �� .+�5      �� $ 
Horizontal=�6      customTable#��[  62   .+�5      �� $��Miktar��6   $��50��6*    �� TextAlignment$ Center=�65   �� 5      $��Fotograf��6   $��75��6,   $ Center=�67   �� 5      $��	Fotograci��6   $	��250��6-   $ Center=�69   �� 5      $��Fiyat��6   $	��100��6)   $
 Left=�65   �� 5      $��Ekleme Tarihi��6   $	��150��61   $ Center=�6=   �� 5      $��Duzenle��6   $	��100��6+   $ Center=�67   �� 5      $	��Sil��6   $	��100��6'   $ Center=�63   5      �� 5      $
��Auto=�6   $	��700��6>   .��5      �� -   5         ��Name$ orderProductList��5   0   $��	10,0,0,15q�6   .+�5      �� $ 
Horizontal=�6!   $��0,15,0,0q�6:   .+�5      �� $��3x��6$   $��Center=�6.   $��50��6I   $ Center=�6T   �� 5      $��60��6    $��60��6,   $
��Fill=�67   �� 5      $��Mert OZCELIK��6$   $��Center=�68   $	��250��6S   $ Center=�6_   �� 5      $	��100��6%   .+�5      �� $��50 TL��6(   $��Center=�65   $
 Left=�6P   �� 5      $��50 Dolar��6(   $��Center=�68   $
 Left=�6S   �� 5      $��50 Euro��6(   $��Center=�67   $
 Left=�6R   �� 5      $��50 Pound��6(   $��Center=�68   $
 Left=�6S   5      �� 5       $��
15/05/2022��6$   $��Center=�66   $	��150��6Q   $ Center=�6]   �� 5!      $��Duzenle��6!   $��30��63   $	��100��6?     edit#��[ 6K   �� 5"      $	��Sil��6!   $��30��6/   $	��100��6;   $��10,0,5,0q�6G    
 delete#��[ 6Y   5#      5$      5%      5&      �� 5'   
   $��25,0,5,0q�6   $	��350��6'   .+�5(      �� $��Toplam��6   $ Center=�6&     titleTextBlock#��[ 6=   �� 5)      -   5         ��Name$ priceTurkishLira��5)      $��50 TL��62   $
 Left=�6?     priceTextBlock#��[ 6T   �� 5*      -   5        $ priceDollar��5*      $��50 Dolar��6-   $
 Left=�6=   #��[ 6R   �� 5+      -   5        $ 	priceEuro��5+      $��50 Euro��6+   $
 Left=�6:   #��[ 6O   �� 5,      -   5        $ 
pricePound��5,      $��50 Pound��6,   $
 Left=�6<   #��[ 6Q   �� 5-      $ 
Horizontal=�6   $��Center=�62   $��0,10,0,0q�6O   .+�5.      �� $"��Seçilen Fotoğraf Sayısı:��6   $ Center=�6<   #��[ 6S   $��12(�6{   $��Center=�6�   �� 5/      -   5        $ 
totalCount��5/      $��0��60   $ Center=�69   #��[ 6P   $��5,0,0,0q�6x   50      �� 51      $��Ödeme Tipi��6   $
 Left=�6*   $��0,25,0,0q�6?     inputInfoTextBlock#��[ 6Q   �� 52      -   5         ��Name$ paymentType��52       �� SelectedValuePath$ Content��6,     editComboBox#��[ 6H   .@�53      �� $��Turkish Lira��6   �� 54      $��Dollar��6   �� 55      $
��Euro��6   �� 56      $��Pound��6   57      �� 58      -   5        $��
Ödeme Yap��58      $��35��6)   $��0,25,0,0q�6J     save#��[ 6\   59      5:      5;      