   M S B A M L   `   `   `  ���� Y  UPhotoOrder Updater, Version=1.0.0.0, Culture=neutral, PublicKeyToken=a5e143e16566518b7 clr-namespace:PhotoOrder_UpdaterPhotoOrder_Updater  %    PhotoOrder_Updater.WindowLoader   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     2local clr-namespace:PhotoOrder_Updater     -     � Title$  Güncelleme Ekranı��$	��300��$	��500�� � 
ResizeMode$ NoResize=� � WindowStyle$
 None=� � WindowStartupLocation$ CenterScreen=�
������  � Icon$ /sic_267.ico��.��� -    �Name$ mainGrid�� J� RenderTransformOrigin$ 0.5,0.5&�.+��� 
������ $��0q� �� CornerRadius$ 20�����	#FF303034� -   $ gridTop��$��Center=�$��Center=�$��54,31,54,167q�.+��� .+��� -    ��Name$ companyName��$��Photo Order��$��Center=�$��48(�$��Berlin Sans FB)�
��������� -   $ developerCompanyName��$��Created by MerkerSoft��$��Center=�$��Center=�$��15(�$��
Montserrat)�
�������� -   $ gridProgressBar��$��Bottom=�$��
10,0,10,10q�.+��� 	 �� Orientation$	 Vertical=�.+��� $	 
Horizontal=�.+�� $��Bottom=�$��Center=�.+��� -   $ totalSizeText��$�� ��$��Right=�$��15(�$��Segoe UI Black)�
��������� -   $ 
statusText��$*��$Güncelleştirmeler Kontrol Ediliyor��$��Right=�$��Bottom=�$��18(�$��Mongolian Baiti)�
��������� -	   $ downloadedSizeText��$�� ��$��Right=�$��15(�$��Segoe UI Black)�
��������� $��0q����	#FF680000���	#FF4B4B4B$ 0��$��15��$��0,5,0,0q�._�� -
   
 �Name$
 progressDownloadBar�� � Minimum$ 0Q� � IsIndeterminate . $��0q�
������ ���	#FF2C2CFF��Z� 