   M S B A M L   `   `   `  ���� [  WPhoto Order Customer, Version=1.0.0.0, Culture=neutral, PublicKeyToken=a5e143e16566518ba5clr-namespace:Photo_Order_Customer.views.pages.basket'Photo_Order_Customer.views.pages.basket  2    ,Photo_Order_Customer.views.pages.basket.list   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     Glocal5clr-namespace:Photo_Order_Customer.views.pages.basket       N� 
Background
  ���� .���� $	��Top=�$��Center=�.+���  &  "langSelectImageForBigImageInBasket#��[    infoMessageTextBlock#��[ �� $��	5,10,5,10q� �� Orientation$ 
Horizontal=�.+��� .+��� $ 
Horizontal=�  customTable#��[ .+���   	langCount#��[ $��50�� �� TextAlignment$ Center=���   	langPhoto#��[ $	��150��$ Center=���   langProductName#��[ $	��250��$ Center=���   langPhotographer#��[ $	��150��$ Center=���   	langPrice#��[ $	��100��$
 Left=���   langAddedDate#��[ $	��150��$ Center=���  	 langEdit#��[	 $	��100��$ Center=���  
 
langDelete#��[
 $	��100��$ Center=��� -    ��Name$ scroll��$
��Auto=�$
��Auto=�$	��900��.���� -    ��Name$ 
basketList��.+��� $ 
Horizontal=�$��0,15,0,0q�.+��� $��1��$��
0, 0, 5, 0q�$��Center=�$��50�� �� TextAlignment$ Center=�$��3q�
���**��$��16(��� $	��150��$	��150��$
��Fill=��� $��Mert OZCELIK��$��Center=�$	��250��$ Center=��� $��Mert OZCELIK��$��Center=�$	��150��$ Center=��� $	��100��.+��� $��50 TL��$��Center=�$
 Left=��� $��50 Dolar��$��Center=�$
 Left=��� $��50 Euro��$��Center=�$
 Left=��� $��50 Pound��$��Center=�$
 Left=��� $��
15/05/2022��$��Center=�$	��150��$ Center=��� $��35��$	��120��  edit#��[ .��� $	��120��.+��� #��[	 $��Center=�$��Center=��� $��20��$+��%/assets/images/icons/buttons/edit.png��$
��Left=�$��Center=�$��5,0,0,0q��� $��35��$	��120��  save#��[ $y�	Collapsed=�.��� $	��120��.+���   
langAccept#��[ $��Center=�$��Center=��� $��20��$,��&/assets/images/icons/buttons/check.png��$
��Left=�$��Center=�$��5,0,0,0q��� $��35��$	��120��$��10,0,0,0q� 
 delete#��[ .��� $	��120��.+��� #��[
 $��Center=�$��Center=��� $��20��$0��*/assets/images/icons/buttons/trash-can.png��$
��Left=�$��Center=�$��5,0,0,0q��� $��25,0,0,0q�$	��350��.+���   	langTotal#��[ $ Center=�  titleTextBlock#��[ �� -    ��Name$ priceTurkishLira��$��50 TL��$
 Left=�  priceTextBlock#��[ �� -   $ priceDollar��$��50 Dolar��$
 Left=�#��[ �� -   $ 	priceEuro��$��50 Euro��$
 Left=�#��[ �� -   $ 
pricePound��$��50 Pound��$
 Left=�#��[ �� $ 
Horizontal=�$��Center=�$��0,10,0,0q�.+���   langSelectedProductCount#��[ $ Center=�#��[ $��12(�$��Center=��� -   $ 
totalCount��$��0��$ Center=�#��[ $��5,0,0,0q��� -    ��Name$ acceptBasketButton��$��45��$��
0,25, 0, 0q�  addToBasket#��[ .��� $	��250��.+���   langAcceptBasket#��[ $��Center=�$��Center=��� $��35��$,��&/assets/images/icons/buttons/check.png��$
��Left=�$��Center=�$��15,0,0,0q��� -	   $ giveOrderButton��$��45��$��
0,25, 0, 0q�#��[ $y�	Collapsed=�.��� $	��250��.+���   langGiveOrder#��[ $��Center=�$��Center=��� $��35��$/��)/assets/images/icons/buttons/delivery.png��$
��Left=�$��Center=�$��15,0,0,0q�