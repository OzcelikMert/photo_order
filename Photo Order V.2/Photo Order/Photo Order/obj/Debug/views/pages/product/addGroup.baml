   M S B A M L   `   `   `  ����F  BPhoto Order, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullQ-clr-namespace:Photo_Order.views.pages.productPhoto_Order.views.pages.product  5      .    (Photo_Order.views.pages.product.addGroup   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      ?local-clr-namespace:Photo_Order.views.pages.product     5        N� 
Background
  ���� .��5      �� $��Center=�6   $��15,15,15,15q�6.   $��Center=�6C   .+�5	   
   �� -   5         ��Name$ stackPanelImage��5	       �� Orientation$ Vertical=�6.   .+�5
      �� -   5         ��Name$ 
imagesMain��5
   n   $	��500��6   $	��350��6'   $
��Auto=�64   $y�	Collapsed=�6W   .��5      �� -   5        $ images��5      6/   5      �� 5      -   5         ��Name$ image��5   a   $	��500��6   $	��350��6    $��/assets/images/empty.jpg��6-   $��Uniform=�6O   6a   � 5      $��0,5,0,0q�6   .+�5      �� -   5        $��
Resim Ekle��5      $	��150��6/   $��35��6;      addImage#��[  6`   5      5      �� 5   
   $ 
Horizontal=�6   $��0,15,0,0q�6.   .+�5      �� $	��250��6   .+�5      �� .+�5      �� $��Fiyat (TL)*��6      inputInfoTextBlock#��[ 63   �� 5      -   5         ��Name$ priceTurkishLira��5   '   $��0��6    �� 	MaxLength$ 10��6k     editTextBox#��[ 6z   5      �� 5      .+�5      �� $��Fiyat (Dolar)*��6    #��[ 66   �� 5      -   5        $ priceDollar��5   '   $��0��6   $ 10��6f   #��[ 6u   5      �� 5      .+�5      �� $��Fiyat (Euro)*��6    #��[ 65   �� 5       -   5        $ 	priceEuro��5    '   $��0��6   $ 10��6d   #��[ 6s   5!      �� 5#      .+�5$      �� $��Fiyat (Pound)*��6    #��[ 66   �� 5%      -	   5        $ 
pricePound��5%   '   $��0��6   $ 10��6e   #��[ 6t   5&      �� 5(      $��0,10,0,0q�6   .+�5)      (� $
��Left=�6   $	��100��69   .��5*      �� -
   5         ��Name$ isActiveSlider��5*   :   $��Slider Aktif��6#    d� 	IsChecked$
 trueY�6R   5+      5,      5-      �� 5.      $	��250��6   $��5,0,0,0q�6%   .+�5/      �� .+�50      �� $��Fotoğrafçı*��6    #��[ 63   �� 51      -   5        	 ��Name$	 owner��51      
 �� DisplayMemberPath$

 Name��6.    �� SelectedValuePath$ Id��6G     editComboBox#��[ 6^   6�   52      �� 54      .+�55      �� $��Etkinlik��6    #��[ 60   �� 56      -   5        $	 event��56      $

 Name��6.   $ Id��6G   #��[ 6^   6�   57      �� 59      .+�5:      �� $��Tarih��6    #��[ 6-   &   System.Windows.Controls.Calendar  5;      -   5          Name$ calendar��5;      
���   �$
��Left=�6D      IsTodayHighlighted .  6_   5<      5=      5>      �� 5?   
   -   5         ��Name$
 save��5?   V   $��Kaydet��6   $��Right=�6"   $	��150��6>   $��35��6J   $��0,15,0,0q�6v     save#��[ 6�   5@      5A      