   M S B A M L   `   `   `  ����H  DChatAppServer, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null-clr-namespace:ChatAppServerChatAppServer  5   	       ChatAppServer.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   -localclr-namespace:ChatAppServer     5   	   -   5          � Title$  Server Service��5   	   $	��350��6    $	��627��6-   .��5	      �  � RowDefinitions 5
   
   �� 5      $��* �6   �� 5      $��2* �6    � ColumnDefinitions 5   
   �� 5      �� 5      .+�5   
   �� $��2��6   .��5      �� -   5        $	��txt��5      $
��Wrap=�6#   $��Khởi chạy...��67   5      �� 5   
   -   5        $��0��5      $��1��6   $��Start��6/   $��76��6?   $	��Top=�6a   $��Right=�6y   $��	0,15,26,0q�6�   �� 5   
   -   5        $��0��5      $��1��6   $��Close��6/   $��76��6?   $��Bottom=�6a   $��Right=�6|   $��	0,0,26,10q�6�   �� 5   
   -   5         ��Name$ listviewUser��5      $��1��6)   ��5      �� 5      .R�5      ��  �� Orientation$ 
Horizontal=�6!   .+�5      �� $��5 2 2 2q�68   ��6$   �� *Id+68   �� 5      $��5 2 2 2q�6<   ��6$   �� *Online+6<   �� 5      $��5 2 2 2q�68   ��6$   �� *Ip+68   5      5       5"      �� 5#   
   -   5        $ listviewLogFile��5#      $��1��6,   $��1��69   $��2��6I   ��5$      �� 5%      .R�5&      �� $ 
Horizontal=�6!   .+�5'      �� $��5 2 5 2q�6:   ��6$   �� *Name+6:   5(      5)      5+      �� 5-   
   -   5        $��1��5-      $��1��6   $��Delete��6/   $��75��6@   $��Bottom=�6`   $��Right=�6{   $��	0,0,10,10q�6�   5.      5/      