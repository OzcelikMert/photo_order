   M S B A M L   `   `   `  ����Y  UPhotoOrder Updater, Version=1.0.0.0, Culture=neutral, PublicKeyToken=a5e143e16566518b7 clr-namespace:PhotoOrder_UpdaterPhotoOrder_Updater  5   	   %    PhotoOrder_Updater.WindowLoader   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   2local clr-namespace:PhotoOrder_Updater     5   	   -   5          � Title$  Güncelleme Ekranı��5   	   $	��300��6#   $	��500��60    � 
ResizeMode$ NoResize=�6<    � WindowStyle$
 None=�6R    � WindowStartupLocation$ CenterScreen=�6e   
������  � Icon$ /sic_267.ico��6�   .��5	      � -   5         �Name$ mainGrid��5	       J� RenderTransformOrigin$ 0.5,0.5&�6   .+�5
   
   �� 
������ $��0q�6+    �� CornerRadius$ 20��6?   ���	#FF3030346j   � 5   
   -   5        $ gridTop��5      $��Center=�6    $��Center=�6;   $��54,31,54,167q�6X   .+�5      �� .+�5      �� -   5         ��Name$ companyName��5      $��Photo Order��6-   $��Center=�6C   $��48(�6`   $��Berlin Sans FB)�6n   
�������6�   �� 5      -   5        $ developerCompanyName��5      $��Created by MerkerSoft��66   $��Center=�6V   $��Center=�6q   $��15(�6�   $��
Montserrat)�6�   
�������6�   5      5      � 5   
   -   5        $ gridProgressBar��5      $��Bottom=�6(   $��
10,0,10,10q�6C   .+�5      �� 	 �� Orientation$	 Vertical=�6   .+�5      �� $	 
Horizontal=�6   .+�5      � $��Bottom=�6   $��Center=�66   .+�5      �� -   5        $ totalSizeText��5       $�� ��67   $��Right=�6B   $��15(�6^   $��Segoe UI Black)�6l   
�������6�   �� 5      -   5        $ 
statusText��5       $*��$Güncelleştirmeler Kontrol Ediliyor��64   $��Right=�6a   $��Bottom=�6}   $��18(�6�   $��Mongolian Baiti)�6�   
�������6�   �� 5      -	   5        $ downloadedSizeText��5       $�� ��6<   $��Right=�6G   $��15(�6c   $��Segoe UI Black)�6q   
�������6�   5      5      �� 5      $��0q�6   ���	#FF680000���	#FF4B4B4B$ 0��6\   $��15��6m   $��0,5,0,0q�6y   ._�5      � -
   5        
 �Name$
 progressDownloadBar��5   "    � Minimum$ 0Q�6?    � IsIndeterminate . $��0q�6x   
������ ���	#FF2C2CFF��6b   Z� 6�   5      5      5      5      5       