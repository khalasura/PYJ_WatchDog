   M S B A M L   `   `   `  ����C  ?WatchDog, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null9!clr-namespace:PYJ_WatchDog.CommonPYJ_WatchDog.Common  5
      9!clr-namespace:PYJ_WatchDog.ModelsPYJ_WatchDog.Models  5      &     PYJ_WatchDog.Views.MonitorWindow   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      X TPrism.Unity.Wpf, Version=7.1.0.431, Culture=neutral, PublicKeyToken=40ee6c3a2184dc59R NPrism.Wpf, Version=7.1.0.431, Culture=neutral, PublicKeyToken=40ee6c3a2184dc59&prismhttp://prismlibrary.com/   5      V RMaterialDesignThemes.Wpf, Version=2.4.1.1101, Culture=neutral, PublicKeyToken=null;wpf1http://materialdesigninxaml.net/winfx/xaml/themes  5      :d2http://schemas.microsoft.com/expression/blend/2008  5      Dmc;http://schemas.openxmlformats.org/markup-compatibility/2006  5      c _System.Windows.Interactivity, Version=4.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35Bi:http://schemas.microsoft.com/expression/2010/interactivity  5      H	 DMahApps.Metro, Version=1.6.5.1, Culture=neutral, PublicKeyToken=null;controls,http://metro.mahapps.com/winfx/xaml/controls 	 5	      .common!clr-namespace:PYJ_WatchDog.Common 	 5
      -model!clr-namespace:PYJ_WatchDog.Models 	 5      !  Prism.Mvvm.ViewModelLocator    AutoWireViewModel  .  � 	Resources 5         AccentCircleButtonStyle + 'MaterialDesignFloatingActionLightButton �� BasedOn �� 
TargetType �� Property  AccentColorBrush �� Value  IdealForegroundColorBrush+  %MaterialDesignThemes.Wpf.ShadowAssist �� Triggers P� Property P� Value  AccentColorBrush2  ControlTextBrush��%�  &	        7  �� 5   
   8   " ��5      .�5      ��  � ��# �  6+   �� 5       � ��# �  6+   �� 5       � ��# �  6,   �� 5       �@ ShadowDepth$ Depth1=�6=   �� 5       � ��$ 30��6&   �� 5       � ��$ 30��6'   �� 5       � ��$ 5 0 5 0q�6'   �� 5       � ��$ Bottom=�62    5      P� 5       �@J�IsMouseOver . . �5      ��  � ��# �  64   �� 5        � ��# �  63   �� 5!       �@ ShadowDepth$ Depth2=�6E   5"      P� 5#       � {� .  . �5$      ��  �@ ShadowDepth$ Depth0=�6E   5%      5'      5(      .��5)      )  #MaterialDesignThemes.Wpf.DialogHost  	   
Identifier$	 
RootDialog��6   .��5*   
   � 
 � RowDefinitions
 5+      �� 5,      $
��Auto �6    �� 5-      $��* �6    .+�5/      ��  �� Orientation$ 
Horizontal=�6   $��6,1,1,1q�62   $
��Left=�6C   .+�50      �� #��[  6F    � ToolTip$ 전체 실행��6w   $��AllRun��6�   ��6   0  *MaterialDesignThemes.Wpf.PackIconExtension     Kind$ ArrowExpandAll=���6�   �� *MenuCommand+6�   �� 51      #��[  6H   $ 정체 정지��6y   $��AllStop��6�   ��6     $ ArrowCollapseAll=���6�   �� *MenuCommand+6�   �� 52      $��3��6   {�.  $��	10 0 10 0q�6;   �� 53      #��[  6S   $ 선택 실행��6�   $	��Run��6�   ��6     $! ArrowRightDropCircleOutline=���6�   �� *MenuCommand+6�   �� 54      #��[  6I   $ 선택 정지��6z   $
��Stop��6�   ��6     $ StopCircleOutline=���6�   �� *MenuCommand+6�   �� 55      $��3��6   {�.  $��	10 0 10 0q�6;   �� 56      #��[  6B   $ 	보이기��6s   $
��Show��6�   ��6     $ 
EyeOutline=���6�   �� *MenuCommand+6�   �� 57      #��[  6>   $ 	감추기��6o   $
��Hide��6�   ��6     $ EyeOff=���6}   �� *MenuCommand+6�   �� 58      $��3��6   {�.  $��	10 0 10 0q�6;   �� 59      #��[  6B   $ 위치설정��6s   $	��Pos��6�   ��6     $ 
CursorMove=���6�   �� *MenuCommand+6�   5;      �� 5<      $ 
Horizontal=�6   $��1 1 6 1q�62   $��Right=�6C   .+�5=      �� $��자동감시��6   $
��Bold"�6'   #���  69   d� 5>       $  MaterialDesignSwitchToggleButton#��[ 6   $��0 0 10 0q�6Y   $
��Left=�5?   <   $��Center=�6W    d� 	IsChecked 6   �� *IsAuto+6W   �� 5@      #��[  6@   $ 설정��6q   $��Setting��6�   ��6     $ Settings=���6~   �� *MenuCommand+6�   �� 5A      #��[  6C   $ 프로그램 추가��6t   $	��Add��6�   ��6     $ PlusOutline=���6�   �� *MenuCommand+6�   �� 5B      #��[  6=   $ 프로그램 삭제��6n   $��Remove��6�   ��6     $ Minus=���6�   �� *MenuCommand+6�   5C      &   System.Windows.Controls.DataGrid  5D      $��1��6   $��3,10,0,0q�6$   $��1q�5E         
IsReadOnly .    CanUserAddRows .     SelectionMode$ Single=�5H         AutoGenerateColumns .  -  'MaterialDesignThemes.Wpf.DataGridAssist   CellPadding$ 4q�5L         ColumnHeaderPadding$ 6q�60   $��13(�5M      ��5J      �� *
TaskList+ �� SelectedItem 5K      �� *	SelTask+   RowHeaderTemplate 5N      �� 5O      .R�5P      �� ��6$   ��  �� RelativeSource ��  �� AncestorType$ DataGridRowL� �� Path$ Header�5Q         Columns 5S      4  .System.Windows.Controls.DataGridTemplateColumn  5T      ,  &System.Windows.Controls.DataGridColumn   Header$ 상태��6-      Width5	  /System.Windows.Controls.DataGridLengthConverter$
 Auto	 69      CellTemplate 5U      �� 5V      .R�5W   "   #
  MaterialDesignThemes.Wpf.Card
   
  UniformCornerRadius$ 15Q�6+   $��4q�6D   $��25��6�   $��25��6�   ��6P   �� *IsRun+  RunStopColorConverter �� 	Converter# [ 5Y   #   5Z      5\      0  *System.Windows.Controls.DataGridTextColumn  5^      $
 Auto	 5_      $ 프로그램명��5a      1  +System.Windows.Controls.DataGridBoundColumn    Binding  5`      �� $
 Name�5a        5b      $
 Auto	 5c      $ 설명��5e        5d      �� $
 Desc�5e        5f      $
 Auto	 5g      $ 	메모리��5i        5h      �� $ 
MemorySize�5i        5j      $
 Auto	 5k      $ 응답��5m        5l      �� $ 
IsResponse�5m        5n      $
 Auto	 5o      $	 PID��5q        5p      �� $	 PID�5q        5r      !   
Visibility$! Visible=�5s      $
 Auto	 5t      $ X��5v        5u      �� $ X�5v        5w      $! Visible=�5x      $
 Auto	 5y      $ Y��5{        5z      �� $ Y�5{        5|      $! Hidden=�5}      $
 Auto	 5~      $ W��5�        5      �� $ Width�5�        5�      $! Hidden=�5�      $
 Auto	 5�      $ H��5�        5�      �� $ Height�5�        5�      $
 Auto	 5�      $ 시작시각��5�        5�      �� $ 	StartTime�" �� StringFormat$" {0:yyyy-MM-dd HH:mm}��5�        5�      $ *	 5�      $ 핸들��5�        5�      �� $ Handle�5�      5�      5�      5�      5�      