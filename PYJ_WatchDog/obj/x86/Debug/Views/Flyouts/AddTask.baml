   M S B A M L   `   `   `  ����G  CPYJ_WatchDog, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null9!clr-namespace:PYJ_WatchDog.CommonPYJ_WatchDog.Common  5      Q-clr-namespace:PYJ_WatchDog.ViewModels.FlyoutsPYJ_WatchDog.ViewModels.Flyouts  5      (    "PYJ_WatchDog.Views.Flyouts.AddTask   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      c _System.Windows.Interactivity, Version=4.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35Bi:http://schemas.microsoft.com/expression/2010/interactivity  5      U QMahApps.Metro, Version=1.5.0.23, Culture=neutral, PublicKeyToken=f4fb5a3c4d1e5b4f;controls,http://metro.mahapps.com/winfx/xaml/controls  5      .common!clr-namespace:PYJ_WatchDog.Common  5      P LPrism.Wpf, Version=6.3.0.0, Culture=neutral, PublicKeyToken=40ee6c3a2184dc59$prismhttp://prismlibrary.com/  5
      >
ViewModels-clr-namespace:PYJ_WatchDog.ViewModels.Flyouts  5      !  Prism.Mvvm.ViewModelLocator    AutoWireViewModel  .  � 	Resources 5         FlyoutLabel �� BasedOn �� 
TargetType �� Property �� Value  FlyoutTextBox  AccentedSquareButtonStyle��%,  '��       7   '�� �     7  '�� �    7  �� 5   
   8   " ��67   .�5      ��  � ��$ 30��6'   �� 5       � ��$
 Auto��6&   �� 5       � ��$	 100��6)   �� 5       � ��$ 14(�6)   5      �� 5   
   8   " ��69   .�5      ��  � ��
 �����6+   �� 5       � ��
 �   �6+   �� 5       � ��$ 30��6'   �� 5       � ��$
 Auto��6&   �� 5       � ��$	 100��6)   �� 5       � ��$ 0,2,0,0q�6(   �� 5       � ��$ 14(�6)   5      �� 5   
   8   " ��6E   .�5       ��  � ��$6 0pack://application:,,,/Styles/Fonts/#FontAwesome)�6+   �� 5!       �@�Cursor$
 Hand��6'   �� 5"       � ��$ 30��6'   �� 5#       � ��$	 100��6&   �� 5$       � ��$ 16(�6)   5%      5&      .  (System.Windows.Interactivity.Interaction   Triggers 5'      /  )System.Windows.Interactivity.EventTrigger  5(   
   .  (System.Windows.Interactivity.TriggerBase   Actions. 5)      -  'Prism.Interactivity.InvokeCommandAction     Command 6(   �� *LoadedCommand+5*      .��5,      � 	 � ColumnDefinitions	 5-   
   �� 5.      $��* �6   
 � RowDefinitions
 50   
   �� 51      $
��Auto �6   �� 52      $
��Auto �6   �� 53      $
��Auto �6   �� 54      �� 55      $
��Auto �6   .+�57   
   �� $��설정��6   � 58   
   $��1��6   	 59      �� 5:      $��2* �6#   �� 5;      $��3* �6#   
 5=      �� 5>      $
��Auto �6    �� 5?      $
��Auto �6    .+�5A      �� $��자동시작여부��6   �� 5B      $��1��6   $��TICK(밀리초)��6!   )  #MahApps.Metro.Controls.ToggleSwitch  5C      $��1��6$   $��Right=�6Q      	IsChecked 64   �� *IsAuto+6Q   *  $MahApps.Metro.Controls.NumericUpDown  5D      $��1��6%   $��1��62   $��15(�6Y   $��DemiBold"�6g      Maximum$ 20000Q�6}      Minimum$
 1000Q�6�      Value 6B   �� *Tick+6�   5E      �� 5F   
   $��프로그램 관리��6   $��2��6"   � 5G   
   $��3��6   	 5H      �� 5I      $��2* �6#   �� 5J      $��3* �6#   
 5L      �� 5M      $
��Auto �6    �� 5N      $
��Auto �6    �� 5O      $
��Auto �6    �� 5P      $��* �6    .+�5R      �� $��TASK명��6   �� 5S      $��1��65   $��DemiBold"�6E   ��6   �� *TaskInfo.Name+6E   �� 5T      $��1��6   $��설명��6!   �� 5U      $��1��65   $��1��6B   ��6   �� *TaskInfo.Desc+6B   �� 5V      $��2��6   $��파일경로��6!   � 5W      $��2��6   $��1��6    	 5X      �� 5Y      $��* �6'   �� 5Z      $
��Auto �6'   .+�5\      �� $��Stretch=�6=   ��6   �� *TaskInfo.FilePath+6=   �� 5]      $��1��6   $	����6)   $��40��6<   $��5 0 0 0q�6G   ��6X   �� *SearchCommand+5^      &   System.Windows.Controls.DataGrid  5_      $��3��6   $��2��6$      CanUserAddRows .     SelectionMode$ Single=�5a         HeadersVisibility$	 All=�5b         GridLinesVisibility$	 All=�5c         AutoGenerateColumns .  ��5e      �� *
TaskList+   RowHeaderTemplate 5f      �� 5g      .R�5h      �� ��6$   ��  �� RelativeSource ��  �� AncestorType$ DataGridRowL� �� Path$ Header�5i         Columns 5k      4	  .System.Windows.Controls.DataGridCheckBoxColumn	  5l      ,
  &System.Windows.Controls.DataGridColumn 
  Width5  /System.Windows.Controls.DataGridLengthConverter$
 Auto 5m       
  Header$ 선택��5o      1  +System.Windows.Controls.DataGridBoundColumn   Binding 5n      �� *	IsCheck+5o      0  *System.Windows.Controls.DataGridTextColumn  5p      $
 Auto 5q      $ 프로그램명��5s       5r      �� $
 Name�5s        5t      $
 Auto 5u      $ 설명��5w       5v      �� $
 Desc�5w        5x      $ * 5y      $ 경로��5{       5z      �� $ FilePath�5{      5}      5~      �� 5   
   $��4��6    �� Orientation$ 
Horizontal=�6"   $��Center=�6;   .+�5�      �� $��추가��6   $	��Add��6E   ��6"   �� *SettingCommand+6E   �� 5�      $��삭제��6   $��Delete��6E   ��6"   �� *SettingCommand+6E   �� 5�      $��저장��6   $
��Save��6E   ��6"   �� *SettingCommand+6E   �� 5�      $��취소��6   $
��Exit��6E   ��6"   �� *SettingCommand+6E   5�      5�      5�      