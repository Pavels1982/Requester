   M S B A M L   `   `   `  ���� D  @Requester, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullC&clr-namespace:Requester.CustomControlsRequester.CustomControls  %clr-namespace:Requester	Requester  7 clr-namespace:Requester.ServicesRequester.Services  M+clr-namespace:Requester.Services.ConvertersRequester.Services.Converters  $    Requester.Views.MainWindowView   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     V RMaterialDesignThemes.Wpf, Version=3.1.3.1513, Culture=neutral, PublicKeyToken=nullFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  ;customControls&clr-namespace:Requester.CustomControls  #localclr-namespace:Requester  /services clr-namespace:Requester.Services  <
converters+clr-namespace:Requester.Services.Converters  -   #��� ��  � WindowStartupLocation$  CenterScreen=�$��0q�$	��760��$	��240��   AppTitle � Title# [  $��560.524��$��522.33�� � 	Resources 6   0Requester.Services.Converters.IntToTimeConverter  IntToTimeConverter8   2Requester.Services.Converters.EnumBooleanConverter  EnumBooleanConverter��%   &	       &	          .��)  #MaterialDesignThemes.Wpf.DialogHost     
Identifier$
 main��.��� .+��  � RowDefinitions �� $
��auto ��� $��80 ��� $��* ��� $
��Auto �.+�(  "MaterialDesignThemes.Wpf.ColorZone  $��1��   Mode$ PrimaryDark=�.���� $��20q�$��24(�
�������#��[  �� $��1�� �� Orientation$ 
Horizontal=�$��Right=�$��0 12 15 12 q�.+�%  MaterialDesignThemes.Wpf.Badged  $��12q�  ControlzEx.BadgedEx   Badge �� * RequestManager.AbortedRequests+.����   RunRequestsTip � ToolTip# [ $	��100��  
RunRequest#��[ ���� *RunAbortedRequests+��   AbortRequestsTip# [ $	��120��  AbortRequests#��[ ���� *AbortedRequests+� $��2��$
��Auto��$
��Auto��.+��� $��Visible=�.���� ���� *RequestCollection+���� .R�9� 
�������$��2q�$	��0.5q�
���iii����� $��-15,-10,0,0q�.+�� $��78��	 � ColumnDefinitions	 �� $
��0.6* ��� $
��0.4* �.+�� $��0��.+��� $ Vertical=�.+��� 
�������$��2 2 0 0q����� 
 �� Path$
 Request.Url��� $��2q�$��55��$��10(� �� TextWrapping$
 Wrap=�" �� VerticalScrollBarVisibility$
 Auto=�  ServerResponse)  #MaterialDesignThemes.Wpf.HintAssist   Hint# [ ���� $
 Request.Response� �� Mode$ TwoWay=�.������  * &MaterialDesignOutlinedTextFieldTextBox �� BasedOn# [  �� 
TargetType" ��.���  �� Property � {� �� Value .  �� Triggers ��  �� Binding �� $
 Status� �� 	Converter# [  �� ConverterParameter$ Process�� �� Value True.a���  � {� .  .��� $��1��	 �� ��  �� �� .+��� $��2q�$��0��$��0��$ Vertical=�.+��� $��Center=�
�������$��10(� 	 ResponseDuration#��[	 �� $��Center=�$��16(����� $
 DurationRequestTime�# [ �� $��2q�$��1��$��0��$ Vertical=�.+��� $��Center=�
�������$��10(� 
 LastTimeResonse#��[
 �� $��Center=�$��16(����� $
 LastRequestTimeEnded�# [ �� $��0��$��1��$��2��$	��100��3  -MaterialDesignThemes.Wpf.ButtonProgressAssist   Value$ -1Q�   IsIndicatorVisible . ���� *DataContext.RunRequest+ �� RelativeSource ��  �� AncestorType$ ItemsControlL����� ����   MaterialDesignRaisedButton# [ " ��.���  �@ IsIndeterminate .  ��  � �� 	 Start# [ ��  �@�ToolTip  RunRequestTip# [  ��  �� $
 Status�# [ $ Process�� True.a���  �@ IsIndeterminate . ��  � ��  Stop# [ ��  �@�ToolTip  AbortRequestTip# [ .��� $��90��
�������	 �� $��0.35* ��� $��0.25* ��� $��0.25* ��� $��0.10* ����� " �.���  � {� .  ��  �� $
 Status�# [ $ Process�� True.a���  � {� .  .+��� $��0�� % !MaterialDesignFloatingHintTextBox#��[ $	 Url��$��8 -8 0 0q�$��44������ $
 Request.Url� �� UpdateSourceTrigger$ PropertyChanged=�.��4	   .Requester.CustomControls.MaterialNumericUpDown	    TimeOutDescription 	  Title# [ $��1�� 	  	Increment$ 1Q� 	  Maximum$	 230Q�  	  Minimum$  0Q�! 	  Value! �� $
 Request.TimeOut�$ TwoWay=�	    IntervalDescription# [ $��2��$��0��$ 1Q�$
 6000Q�$  0Q�! �� $
 Request.Interval�$ TwoWay=���   RemoveRequestTip# [ $��3��
���   �  MaterialDesignFlatButton#��[ ���� *DataContext.DeleteRequest+ �� $ ItemsControlL����� .��'
  !MaterialDesignThemes.Wpf.PackIcon
  " 
  Kind$" DeleteForever=�$��25��$��25��  $��3��$ PrimaryDark=�.���� $��20q�$��Copyright 2020����   CreateRequestTip# [ $��Right=�$��Bottom=�$��
15 0 25 35q� 0 ,MaterialDesignFloatingActionMiniAccentButton#��[ $��32(����� *AddNewRequest+.��
  $
" Plus=�$��22��$��22��