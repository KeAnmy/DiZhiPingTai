   M S B A M L   `   `   `  ����O  Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem  5       ZYSK.DZPT.QueryUI+   %ZYSK.DZPT.QueryUI.UI.WndProjectImport   5      a ]DevExpress.Xpf.Core.v15.2, Version=15.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a?dxe5http://schemas.devexpress.com/winfx/2008/xaml/editors  j fDevExpress.Xpf.LayoutControl.v15.2, Version=15.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49aFdxlc;http://schemas.devexpress.com/winfx/2008/xaml/layoutcontrol  5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      ;dx2http://schemas.devexpress.com/winfx/2008/xaml/core  5      3System&clr-namespace:System;assembly=mscorlib  5      -   5          � Title$  工程数据入库��5      $	��745��6   $	��500��6!   .��5      �  � RowDefinitions 5	   
   �� 5
      $��40 �6   �� 5      $	��173 �6   �� 5      $��80 �6   �� 5      $	��141 �6   �� 5      $	��208 �6   �� 5      $��80 �6   .+�5   
   .  (DevExpress.Xpf.LayoutControl.LayoutGroup  -   5          Name$ lgroup1��5      $��0��6,   .+�5      ��  �� Orientation$ Vertical=�6   $��10q�60   .+�5      � $��20��6   .+�5      �� $0��**请选择要导入的理正数据库：  ��6    %  DevExpress.Xpf.Editors.TextEdit  5      -   5          Name$ txtFilePath��5   $   $
��200 ��69   &   DevExpress.Xpf.Core.SimpleButton  5      -   5          Name$ btnSelectFile��5   &   $��选择 ···��6>   $��3,0,0,0q�6O   6`   5      5      5      +  %DevExpress.Xpf.LayoutControl.GroupBox  5   
   -   5          Name$ gbox2��5      $��1��6'   4  .DevExpress.Xpf.Core.HeaderedContentControlBase   Header$ 工程信息��64   ,  &DevExpress.Xpf.Core.ContentControlBase   Content. 5      � 	 � Orientation$	 Vertical=�6   .+�5      � $	 
Horizontal=�6   $��5q�65   .+�5      �� $��*工程编号:   ��6    $�� 0,0,4,0q�62     5       -   5        $ txtPrjNo��5    #   $
��200 ��68   $
��Left=�6E   {�.  %  DevExpress.Xpf.Editors.BaseEdit
   	EditValue)  #DevExpress.Xpf.Core.ObjectConverter$
   6r   �� 5!      $)��#(编号：2位区域号+4位编号)��6    $��10,0,0,0q�68   5"      � 5#      $	 
Horizontal=�6   $��5q�65   .+�5$      �� $��*工程名称:   ��6    $�� 0,0,4,0q�62     5%      -   5        $ 
txtPrjName��5%   #   $
��352 ��69   $
��Left=�6F   5&      � 5'      $	 
Horizontal=�6   $��5q�65   .+�5(      �� $��*工程负责人: ��6      5)      -   5        $ txtPrjHeader��5)   #   $
��130 ��6;   $
��Left=�6H   )	  #DevExpress.Xpf.Editors.TextEditBase 	  Text$ 张三��6c   �� 5*      $��*建设单位:   ��6    $��22,0,2,0q�61     5+      -	   5        $ 
txtPrjUnit��5+   #   $
��130 ��69   $
��Left=�6F   $ 建设集团��6a   5,      � 5-      $	 
Horizontal=�6   $��5q�65   .+�5.      �� $��*建立时间:   ��6    $�� 0,0,4,0q�61   %
  DevExpress.Xpf.Editors.DateEdit
  5/      -
   5         
 Name$ 
txtPrjDate��5/   #   $
��130 ��67   $
��Left=�6D    
  DateTime$ 
2018-11-01��6_   �� 50      $��*档案编号:   ��6    $��22,0,4,0q�61     51      -   5        $ txtPrjFileNo��51   #   $
��130 ��6:   $
��Left=�6G   $ 
sy20180102��6b   52      53      54        55   
   -   5        $ gbox3��55      $��2��6'   $ 空间参考基准��64   {�.  . 56      � $	 
Horizontal=�6   $��5q�61   .+�57      �� $��坐标系统:   ��6   $�� 0,0,4,0q�6,   )  #DevExpress.Xpf.Editors.ComboBoxEdit  58      -   5          Name$ cboxCoordSys��58   #   $
��130 ��6:   $
��Left=�6G      Items. 59      -  'DevExpress.Xpf.Editors.ComboBoxEditItem  $��北京54坐标系��6+     5:      $��西安80坐标系��6+     5;      $��CGCS2000坐标系��6+   5<      �� 5=      $��高程系统:   ��6   $��22,0,4,0q�6,     5>      -   5        $ cboxElevationSys��5>   #   $
��130 ��6=   $
��Left=�6J   . 5?        $��黄海高程系统��6+     5@      $��国家高程基准��6+   5A      5B      5D        5E   
   -   5        $ gbox4��5E      $��3��6'   $ 场地信息��64   . 5F      � $	 Vertical=�6   .+�5G      � $	 
Horizontal=�6   $��5q�65   .+�5H      �� $��位置信息:   ��6!   $�� 0,0,4,0q�62     5I      -   5        $ txtLocationInfo��5I   #   $
��359 ��6>   $
��Left=�6K   5J      � 5K      $	 
Horizontal=�6   $��5q�65   .+�5L      �� $��地形信息:   ��6!   $�� 0,0,4,0q�62     5M      -   5        $ txtTerrainInfo��5M   #   $
��359 ��6=   $
��Left=�6J   5N      � 5O      $	 
Horizontal=�6   $��5q�65   .+�5P      �� $��地貌信息:   ��6    $�� 0,0,3,0q�61     5Q      -   5        $ txtDimaoInfo��5Q   #   $
��359 ��6;   $
��Left=�6H   5R      5S      5T        5V   
   -   5        $ gbox5��5V      $��4��6'   $ 地震效应评价��64   . 5W      � $	 Vertical=�6   .+�5X      � $	 
Horizontal=�6   $��5q�65   .+�5Y      �� $��抗震设防强度(级): ��6      5Z      -   5        $ 
txtDizhen1��5Z   #   $
��100 ��69   $
��Left=�6F   �� 5[      $%��基本地震加速度值(g):   ��6    $��21,0,0,0q�67     5\      -   5        $ 
txtDizhen2��5\   #   $
��100 ��68   $
��Left=�6E   5]      � 5^      $	 
Horizontal=�6   $��5q�65   .+�5_      �� $��设计地震分组(组): ��6      5`      -   5        $ 
txtDizhen3��5`   #   $
��100 ��68   $
��Left=�6E   �� 5a      $$��等效剪切波速(m/s):      ��6    $��20,0,0,0q�6:     5b      -   5        $ 
txtDizhen4��5b   #   $
��100 ��68   $
��Left=�6E   5c      � 5d      $	 
Horizontal=�6   $��5q�65   .+�5e      �� $��场地覆盖厚度(m): ��6      5f      -   5        $ 
txtDizhen5��5f   #   $
��100 ��68   $
��Left=�6E   �� 5g      $&�� 建筑物场地类别(类):     ��6    $��21,0,0,0q�68     5h      -   5        $ 
txtDizhen6��5h   #   $
��100 ��68   $
��Left=�6E   5i      � 5j      $	 
Horizontal=�6   $��5q�65   .+�5k      �� $��设计特征周期(s):   ��6      5l      -   5        $ 
txtDizhen7��5l   #   $
��100 ��68   $
��Left=�6E   �� 5m      $$��标准冻结深度(m):        ��6    $��22,0,0,0q�6:     5n      -   5        $ 
txtDizhen8��5n   #   $
��100 ��68   $
��Left=�6E   5o      � 5p      $	 
Horizontal=�6   $��5q�65   .+�5q      �� $��地下水情况介绍:  ��6    $�� 0,0,4,0q�63     5r      -   5        $ 
txtDizhen9��5r   #   $
��346 ��68   $
��Left=�6E   5s      5t      5u        5w   
   -   5        $ lgroup2��5w      $��5��6,   .+�5x      �� $ Vertical=�6   .+�5y      � $	 
Horizontal=�6   $��10q�65   .+�5z        -   5        $ btnModifyTuce��5z   &   $��测试��6=   6K     5{      -   5        $ btnImportProject��5{   &   $��导入��6@   $��	40,0,40,0q�6M   6a   ,  &DevExpress.Xpf.Editors.ProgressBarEdit  5|      -    5          Name$ 
pbarImport��5|   *   $	��150��6>      ContentDisplayMode$ Content=�6J   
 5}      X�5~      0Q�6/   5�        5�      -!   5        $ btnEixt��5�   &   $��退出��67   $��40,0,0,0q�6D   6V   5�      � 5�      $	 
Horizontal=�6   .+�5�      �� $��  注：*为必填项��6    $��0,0,0,5q�61   �� 5�      -"   5         ��Name$ 	txtErrMsg��5�       
���  �� �� TextAlignment$ Right=�6D   $��0,0,0,5q�6Z   $	��380��6l   5�      5�      5�      5�      5�      