   M S B A M L   `   `   `  ���� F  BPhoto Order, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullQ-clr-namespace:Photo_Order.views.pages.productPhoto_Order.views.pages.product  *    $Photo_Order.views.pages.product.list   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     ?local-clr-namespace:Photo_Order.views.pages.product     -     N�Name$  pageUserList�� N� 
Background
 ���� .��� $��	5,10,5,10q�.+��� .+�� $��0,0,0,10q�.+���  �� Orientation$ 
Horizontal=�$��30��$	��210��$
��Left=�$	��Top=�.+��� -    ��Name$ 
backButton��$	��100��$��Geri Gel��   refresh#��[  $��0,0,10,0q��� -   $	��100��$��+ Yeni Ekle��  	createNew#��[ �� $��Arama: ��$��Right=�$��Center=�$��	0,0,260,0q��� -    ��Name$ search��$��Right=�$	��Top=�  searchTextBox#��[ �� -    ��Name$ tableHeadProductGroups��$ 
Horizontal=�  customTable#��[ .+��� $��Resim��$��75�� �� TextAlignment$ Center=��� $��Oluşturma Tarihi��$	��150��$ Center=��� $��Fiyat��$	��100��$
 Left=��� $��Fotoğrafçı��$	��200��$ Center=��� $��Etkinlik��$	��200��$ Center=��� $��Düzenle��$	��100��$ Center=��� $��Detay��$	��100��$ Center=��� $	��Sil��$	��100��$ Center=��� -   $ tableHeadProducts��$ 
Horizontal=�#��[ .+��� $��Resim��$��75��$ Center=��� $��Fiyat��$	��100��$ Right=��� $��Fotoğrafçı��$	��200��$ Center=��� $��Etkinlik��$	��200��$ Center=��� $��Düzenle��$	��100��$ Center=��� $	��Sil��$	��100��$ Center=��� -    ��Name$ scroll��$
��Auto=�$	��700��.���� $��	10,0,0,15q�.+��� -   $ productList��.+��� $ 
Horizontal=�$��0,15,0,0q�.+��� $"��/assets/images/app/empty.jpg��$��60��$��60��$
��Fill=��� $��
15/05/2022��$��Center=�$	��150��$ Center=��� $��50��$��Center=�$	��100��$
 Left=��� $��Mert OZCELIK��$��Center=�$	��200��$ Center=��� $��Gunduz��$��Center=�$	��200��$ Center=��� $��Duzenle��$��30��$	��100��  edit#��[ �� $	��Sil��$��30��$	��100��$��10,0,0,0q� 
 delete#��[ �� -	   $ 
pagination��$ 
Horizontal=�$��Center=�$��0,15,0,0q�.+��� $��1��$��30��$��50��  addImage#��[ �� $��2��$��30��$��50��#��[ �� $��3��$��30��$��50��#��[  �� $��4��$��30��$��50��#��[ �� $	��255��$��30��$��50��#��[ 