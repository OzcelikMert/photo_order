   M S B A M L   `   `   `  ���� [  WPhoto Order Customer, Version=1.0.0.0, Culture=neutral, PublicKeyToken=a5e143e16566518b;"clr-namespace:Photo_Order_CustomerPhoto_Order_Customer  /    )Photo_Order_Customer.WindowProductBigList   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     4local"clr-namespace:Photo_Order_Customer     -     � Title$  Resim Slideri�� � WindowStartupLocation$ CenterScreen=� � WindowState$ 	Maximized=� � WindowStyle$
 None=����	#FF212121 � Icon$ /sic_267.ico��.��� -    �Name$ gridMain��.+���  �� Orientation$ 
Horizontal=�.+�� .+�� -   $ gridSelected�� K� ZIndex$ 1��.+��� -    ��Name$ imageSelected��$	��250��$	��250��$��Right=�$	��Top=�$��0,5,15,0q��� -   $ image��$��Uniform=��� $	��265��$��Center=�$��Right=�$��	10,0,10,0q�.+��� $��0,0,0,0q�.+��� -   	 ��Name$	 count��$��1��$��75��$��35��
 �� TextAlignment$
 Center=�   editTextBox#��[  �� -    ��Name$ addBasketButton��$��45��  addToBasket#��[ $��0,15,0,0q�.��� $	��250��.+���   langAddBasket#��[ $��Center=�$��Center=��� $��30��$2��,/assets/images/icons/buttons/add-to-cart.png��$
��Left=�$��Center=�$��0,0,0,0q��� -   $ removeBasketButton��$��45�� 
 delete#��[ $��0,15,0,0q�$y�	Collapsed=�.��� $	��250��.+���   langDeleteFromBasket#��[ $��Center=�$��Center=��� $��30��$2��,/assets/images/icons/buttons/remove-cart.png��$
��Left=�$��Center=�$��0,0,0,0q��� $ 
Horizontal=�$��Center=�$��0,10,0,0q�.+���   langSelectedProductCount#��[  �� TextAlignment$ Center=�  priceTextBlock#��[ $��12(�$��Center=����	#FFC5C5C5�� -	    ��Name$ 
totalCount��$��0��$ Center=�#��[ ���	#FFC5C5C5$��5,0,0,0q��� $ 
Horizontal=�$��0,23,0,0q�.+��� -
   $ before��$��35��$	��125��  language#��[ .��� $	��120��.+���   
langBefore#��[ $��Center=�$��Center=��� $��25��$1��+/assets/images/icons/buttons/left-arrow.png��$
��Left=�$��Center=�$��2,0,0,0q��� -   $ after��$��35��$	��125��#��[ $��15,0,0,0q�.��� $	��120��.+���  	 	langAfter#��[	 $��Center=�$��Center=��� $��25��$2��,/assets/images/icons/buttons/right-arrow.png��$��Right=�$��Center=�$��0,0,2,0q��� -   $��35��#��[ $��0,25,0,0q�.��� $	��250��.+���  
 langBackToThumbnails#��[
 $��Center=�$��Center=��� $��20��$1��+/assets/images/icons/buttons/back-arrow.png��$
��Left=�$��Center=�$��15,0,0,0q�