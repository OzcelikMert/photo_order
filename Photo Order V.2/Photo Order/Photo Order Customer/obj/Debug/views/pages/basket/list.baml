   M S B A M L   `   `   `  ����[  WPhoto Order Customer, Version=1.0.0.0, Culture=neutral, PublicKeyToken=a5e143e16566518ba5clr-namespace:Photo_Order_Customer.views.pages.basket'Photo_Order_Customer.views.pages.basket  5      2    ,Photo_Order_Customer.views.pages.basket.list   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      Glocal5clr-namespace:Photo_Order_Customer.views.pages.basket     5        N� 
Background
  ���� .��5      �� $	��Top=�6   $��Center=�6)   .+�5	   
   ��  &  "langSelectImageForBigImageInBasket#��[  6     infoMessageTextBlock#��[ 6O   �� 5
   
   $��	5,10,5,10q�6    �� Orientation$ 
Horizontal=�6(   .+�5      �� .+�5      �� $ 
Horizontal=�6     customTable#��[ 66   .+�5      ��   	langCount#��[ 6    $��50��6B    �� TextAlignment$ Center=�6M   �� 5        	langPhoto#��[ 6    $	��150��6B   $ Center=�6N   �� 5        langProductName#��[ 6    $	��250��6H   $ Center=�6T   �� 5        langPhotographer#��[ 6    $	��150��6I   $ Center=�6U   �� 5        	langPrice#��[ 6    $	��100��6B   $
 Left=�6N   �� 5        langAddedDate#��[ 6    $	��150��6F   $ Center=�6R   �� 5       	 langEdit#��[	 6    $	��100��6A   $ Center=�6M   �� 5       
 
langDelete#��[
 6    $	��100��6C   $ Center=�6O   5      �� 5      -   5         ��Name$ scroll��5   w   $
��Auto=�6   $
��Auto=�6B   $	��900��6g   .��5      �� -   5         ��Name$ 
basketList��5   !   .+�5      �� $ 
Horizontal=�6%   $��0,15,0,0q�6>   .+�5      �� $��1��6&   $��
0, 0, 5, 0q�6/   $��Center=�6C   $��50��6^    �� TextAlignment$ Center=�6i   $��3q�6�   
���**��$��16(�6�   �� 5      $	��150��6$   $	��150��61   $
��Fill=�6=   �� 5      $��Mert OZCELIK��6(   $��Center=�6<   $	��250��6W   $ Center=�6c   �� 5      $��Mert OZCELIK��6(   $��Center=�6<   $	��150��6W   $ Center=�6c   �� 5      $	��100��6)   .+�5   "   �� $��50 TL��6,   $��Center=�69   $
 Left=�6T   �� 5   "   $��50 Dolar��6,   $��Center=�6<   $
 Left=�6W   �� 5    "   $��50 Euro��6,   $��Center=�6;   $
 Left=�6V   �� 5!   "   $��50 Pound��6,   $��Center=�6<   $
 Left=�6W   5"      �� 5#      $��
15/05/2022��6(   $��Center=�6:   $	��150��6U   $ Center=�6a   �� 5$      $��35��6%   $	��120��61     edit#��[ 6=   .��5%   "   � $	��120��6'   .+�5&   &   �� #��[	 60   $��Center=�6Q   $��Center=�6l   �� 5'   &   $��20��6,   $+��%/assets/images/icons/buttons/edit.png��68   $
��Left=�6g   $��Center=�6�   $��5,0,0,0q�6�   5(   #   5)      �� 5*      $��35��6%   $	��120��61     save#��[ 6=   $y�	Collapsed=�6[   .��5+   "   � $	��120��6'   .+�5,   &   ��   
langAccept#��[ 60   $��Center=�6S   $��Center=�6n   �� 5-   &   $��20��6,   $,��&/assets/images/icons/buttons/check.png��68   $
��Left=�6h   $��Center=�6�   $��5,0,0,0q�6�   5.   #   5/      �� 50      $��35��6%   $	��120��61   $��10,0,0,0q�6=    
 delete#��[ 6O   .��51   "   � $	��120��6'   .+�52   &   �� #��[
 60   $��Center=�6S   $��Center=�6n   �� 53   &   $��20��6,   $0��*/assets/images/icons/buttons/trash-can.png��68   $
��Left=�6l   $��Center=�6�   $��5,0,0,0q�6�   54   #   55      56      57      58      59      �� 5:      $��25,0,0,0q�6   $	��350��6+   .+�5;      ��   	langTotal#��[ 6   $ Center=�6>     titleTextBlock#��[ 6U   �� 5<      -   5         ��Name$ priceTurkishLira��5<      $��50 TL��66   $
 Left=�6C     priceTextBlock#��[ 6X   �� 5=      -   5        $ priceDollar��5=      $��50 Dolar��61   $
 Left=�6A   #��[ 6V   �� 5>      -   5        $ 	priceEuro��5>      $��50 Euro��6/   $
 Left=�6>   #��[ 6S   �� 5?      -   5        $ 
pricePound��5?      $��50 Pound��60   $
 Left=�6@   #��[ 6U   �� 5@      $ 
Horizontal=�6   $��Center=�66   $��0,10,0,0q�6S   .+�5A      ��   langSelectedProductCount#��[ 6    $ Center=�6Q   #��[ 6h   $��12(�6�   $��Center=�6�   �� 5B      -   5        $ 
totalCount��5B       $��0��64   $ Center=�6=   #��[ 6T   $��5,0,0,0q�6|   5C      �� 5D      -   5         ��Name$ acceptBasketButton��5D      $��45��65   $��
0,25, 0, 0q�6A     addToBasket#��[ 6j   .��5E      � $	��250��6   .+�5F      ��   langAcceptBasket#��[ 6$   $��Center=�6M   $��Center=�6h   �� 5G      $��35��6    $,��&/assets/images/icons/buttons/check.png��6,   $
��Left=�6\   $��Center=�6w   $��15,0,0,0q�6�   5H      5I      �� 5J      -	   5        $ giveOrderButton��5J      $��45��62   $��
0,25, 0, 0q�6>   #��[ 6g   $y�	Collapsed=�6�   .��5K      � $	��250��6   .+�5L      ��   langGiveOrder#��[ 6$   $��Center=�6J   $��Center=�6e   �� 5M      $��35��6    $/��)/assets/images/icons/buttons/delivery.png��6,   $
��Left=�6_   $��Center=�6z   $��15,0,0,0q�6�   5N      5O      5P      5Q      5R      5S      