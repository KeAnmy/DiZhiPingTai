   M S B A M L   `   `   `  ����  ZYSK.DZPT.UI$    ZYSK.DZPT.UI.Main.WndDbConnect   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      a ]DevExpress.Xpf.Core.v15.2, Version=15.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a;dx2http://schemas.devexpress.com/winfx/2008/xaml/core  5      ?dxe5http://schemas.devexpress.com/winfx/2008/xaml/editors  5      Ddxnav8http://schemas.devexpress.com/winfx/2008/xaml/navigation  5        � Title$!  数据库连接参数设置��5      $	��335��6   $	��336��6$    � 
ResizeMode$ NoResize=�60   .��5	      �  � RowDefinitions 5
   
   �� 5      $��15 �6   �� 5      $��* �6   �� 5      $��* �6   �� 5      $��* �6   �� 5      $��* �6   �� 5      $��* �6   �� 5      $��* �6   �� 5      $��25 �6   �� 5      $��20 �6   .+�5   
   � $��0��6    � Orientation$ 
Horizontal=�6"   5      � 5   
   $��1��6   $ 
Horizontal=�6"   .+�5      �� $��数据库平台：��6   $	��100��6'    �� TextAlignment$ Right=�64   )  #DevExpress.Xpf.Editors.ComboBoxEdit  5      -   5          Name$ cmbDbPlatform��5      $	��200��66   +  %DevExpress.Xpf.Editors.LookUpEditBase   SelectedIndex$ 0��6B   {�.     Items. 5      -  'DevExpress.Xpf.Editors.ComboBoxEditItem  $��Oracle��6'     5      $��
PostgreSQL��6'     5       $��	SQLServer��6'   5!      5#      � 5%   
   $��2��6   $ 
Horizontal=�6#   .+�5&      �� $��主机名或IP：��6   $	��100��6'   $ Right=�64   %  DevExpress.Xpf.Editors.TextEdit  5'      -   5          Name$ 	txtHostIP��5'      $	��200��6.   {�.  6:   5(      � 5)   
   $��3��6   $ 
Horizontal=�6#   .+�5*      �� $��数据库实例名：��6   $	��100��6'   $ Right=�64     5+      -   5        $ txtDbInstance��5+      $	��200��62   5,      � 5-   
   $��4��6   $ 
Horizontal=�6#   .+�5.      �� $��数据库名：��6   $	��100��6%   $ Right=�62     5/      -   5        $ 	txtDbName��5/      $	��200��6/   50      � 51   
   $��5��6   $ 
Horizontal=�6#   .+�52      �� $��用户名：��6   $	��100��6$   $ Right=�61     53      -   5        $ txtDbUserName��53      $	��200��62   54      � 55   
   $��6��6   $ 
Horizontal=�6#   .+�56      �� $��	密码：��6   $	��100��6#   $ Right=�60   ,  &DevExpress.Xpf.Editors.PasswordBoxEdit  57      -   5        	  Name$	 txtDbPassword��57   #   $	��200��6:   58      � 59   
   $��7��6   $ 
Horizontal=�6#   $��Center=�6<   .+�5:      &   DevExpress.Xpf.Core.SimpleButton  -   5        
  Name$
 btnConnTest��5:      $��
测    试��64   6E   �� 5;      $��50��6     5<      -   5        $
 btnSave��5<      $��
保    存��6/   6@   5=      � 5>   
   $��8��6   $ 
Horizontal=�6"   $��Center=�6;   .+�5?      �� -	   5         ��Name$ txtTestResult��5?      5@      5E      5F      