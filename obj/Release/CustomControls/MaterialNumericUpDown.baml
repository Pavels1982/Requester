   M S B A M L   `   `   `  ���� D  @Requester, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullC&clr-namespace:Requester.CustomControlsRequester.CustomControls  4    .Requester.CustomControls.MaterialNumericUpDown   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     8local&clr-namespace:Requester.CustomControls     V RMaterialDesignThemes.Wpf, Version=3.1.3.1513, Culture=neutral, PublicKeyToken=nullFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes    � 	Resources   �� 
TargetType   RepeatButtonFlatStyle �� Property  FocusVisual �� Value  PrimaryHueMidBrush �� 
TargetType ��Name �� CornerRadius%  MaterialDesignThemes.Wpf.Ripple J� SnapsToDevicePixels �� Triggers	 P� Property
 P� Value �� 
TargetName ! MaterialDesignFlatButtonClick��%,  &	        7  �� " ��.���  �@�FocusVisualStyle8   ��  � ��
 ���� ��  � ��
 ���� ��  � ��# �  ��  � ��$ 0q���  � ��$ Center=���  � ��$ Center=���  � ��$ 	16 4 16 4q���  � ��$ 32����  � �� �� " ��.R�� .+��� $ border��#��z��$ 2��#��z��#��z��._�  #��z��#��z��|�.  #��z��#��z��#��z��# z  P� 	 �@J�IsMouseOver
 . . ��� $ border��# �   � ���� $ border��# �   � ��P� 	 � {�
 .  . ���  �@J�Opacity$
 0.23Q�.��� $��0 8 0 0 q�$	��150�� � RowDefinitions �� $��32 ��� $��25 � � ColumnDefinitions �� $
��Auto ��� $
��Auto ��� $
��Auto �.+��� 
�������$��0��$��0��$��3��$��Bottom=�$��Center=�$��9(�����  �� RelativeSource �� *FindAncestor+ �� AncestorType4   .Requester.CustomControls.MaterialNumericUpDown"   �� Path$ Title� �� Mode$ OneWay=��� -   $��
DownButton��$��2 0 2 2q�$��16��$��16��$��1��$��0��#��[  +  %MaterialDesignThemes.Wpf.RippleAssist   Feedback �	#DD000000$��0q�.��'  !MaterialDesignThemes.Wpf.PackIcon     Kind$ Minus=�
���iii� J� RenderTransformOrigin$ .3,.3&��� -   $��TextBox��$��2 0 2 2q�$��1��$��1��{�.   �� TextAlignment$ Center=�$��25������  �� *FindAncestor+"  $ Value�$ TwoWay=�.���� -   $��UpButton��$��2 0 2 2q�$��16��$��16��$��1��$��2��#��[   �	#DD000000$��0q�.��  $
 Plus=�
���iii�$ .3,.3&�