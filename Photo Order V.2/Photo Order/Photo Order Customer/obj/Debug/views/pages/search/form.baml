   M S B A M L   `   `   `  ����[  WPhoto Order Customer, Version=1.0.0.0, Culture=neutral, PublicKeyToken=a5e143e16566518ba5clr-namespace:Photo_Order_Customer.views.pages.search'Photo_Order_Customer.views.pages.search  5      2    ,Photo_Order_Customer.views.pages.search.form   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      Glocal5clr-namespace:Photo_Order_Customer.views.pages.search     5      $	��900��5      $	��800��6(     N� 
Background
  ���� .��5      �� $��25,15,25,15q�6   .+�5	   
   �� $	��300��6   .+�5
      ��    langPhotographers#��[  6    �� TextAlignment$ Center=�6B     titleTextBlock#��[ 6Y   $��0,0,0,10q�6�   �� 5       ! langSelectPhotographerMessage#��[ 6     infoMessageTextBlock#��[ 6N   �� 5      $	��Top=�6   $
��Auto=�63   $��Disabled=�6X   .��5      �� -   5         ��Name$ productOwners��5   6    �� Orientation$ 
Horizontal=�6   .+�5      � $��10,0,0,0q�6    � Cursor$
 Hand��6-   .+�5      �  K� ZIndex$ 1��6     gridSelected#��[ 60   .+�5      �� $��75��6$   $��75��60   $��Center=�6;   $	��Top=�6X   $��0,25,0,0q�6p   5      �� 5      .+�5      �� $	��200��6$   $	��220��61   $
��Fill=�6=   �� 5      $��Mert Ozcelik��6(   $ Center=�6<   $��Bottom=�6S     inputInfoTextBlock#��[ 6n   5      5      5      5      5      �� 5   
   $��0,15,0,0q�6   $	��300��6'   .+�5      ��   
langEvents#��[ 6   $ Center=�6;   #��[ 6R   $��0,0,0,10q�6z   �� 5        langSelectActivityMessage#��[ 6   #��[ 6J   �� 5      $	��Top=�6   $
��Auto=�63   $��Disabled=�6X   .��5      �� -   5        $ events��5   6   $ 
Horizontal=�6   6H   5      5       �� 5!   
   $ 
Horizontal=�6   $��Center=�6.   $��0,15,0,0q�6L   .+�5"      �� .+�5#      � .+�5$      (� $
��Left=�6   $��55��69   .��5%      �� -   5         ��Name$ isActiveCalendar��5%   J     
langActive#��[ 6#   6�   5&      �� 5'      -   5        $��Right=�5'      $	��150��69   $��35��6E    	 language#��[	 6l   .��5(      � $	��150��6   $��35��6+   .+�5)      ��  
 	langClear#��[
 6(   $��Center=�6J   $��Center=�6e   �� 5*      $��20��6$   $/��)/assets/images/icons/buttons/cleaning.png��60   $��Right=�6c   $��Center=�6   $��0,0,15,0q�6�   5+      5,      5-      (� 5.      $	��250��6   $	��250��6'    (� Stretch$
 Fill=�63    (� StretchDirection$ UpOnly=�6B   .��5/      &   System.Windows.Controls.Calendar  -   5        	  Name$	 calendar��5/      
���   �
   SelectionMode$
 MultipleRange=�6D   6{   50      51      �� 52      -   5        $	��250��52        edit#��[ 68   $��Bottom=�6V   $��15,0,0,0q�6q   .��53      � $	��250��6   $��50��6#   .+�54      ��   
langSearch#��[ 6    $��Center=�6C   $��Center=�6^   �� 55      $��35��6   $-��'/assets/images/icons/buttons/search.png��6(   $��Right=�6Y   $��Center=�6u   $��0,0,15,0q�6�   56      57      58      59      5:      