   M S B A M L   `   `   `  ����G  CPYJ_WatchDog, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null9!clr-namespace:PYJ_WatchDog.StylesPYJ_WatchDog.Styles  5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089   TextBox.Metro.Static.Border  Gray  �� Color " TextBox.Metro.MouseOver.Border 	 Black  TextBox.Metro.Focus.Border  MetroTextBox �� 
TargetType-   'System.Windows.Input.KeyboardNavigation �� Property �� Value  MetroToolBackground  MetroToolForeground �� 
TargetType ��Name J� SnapsToDevicePixels ��Name �� Triggers	 P� Property
 P� Value �� 
TargetName   MetroToolMouseOverBackground �� Triggers :� RoutedEvent �� TargetProperty �� From	 �� To g� Duration ]� 
Conditions �� Property �� Value 	 FlyoutTextBox�� 5      D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    :x,http://schemas.microsoft.com/winfx/2006/xaml     5      +local!clr-namespace:PYJ_WatchDog.Styles  5      %m  &	        &	      &	 4     &	 N     7   7  7  &		 "    7   7  7  �� 5      #  �  6:   �� 5      #  �  6=   �� 5      #  �  69   �� 5      " ��6!   .�5	   
   ��  �@  TabNavigation$
 None=�6=   �� 5
   
    �@�FocusVisualStyle 6-   Z� �� 5   
    �@J�	AllowDrop . 6&   �� 5   
    �@��PanningMode$ VerticalFirst=�65   �� 5   
    �@��IsFlicksEnabled .  63   �� 5   
    � ��# �  6'   �� 5   
    � ��# �  6'   �� 5   
    � ��$
 Left=�67   �� 5   
    � ��$ Center=�65   �� 5   
    � ��$ 1,1,1,1q�6,   �� 5   
    � ��8   6(   �� 5   
    � ��$ 0,0,0,0q�6#   �� 5   
    � �� 5      �� 5      " ��6"   .R�5      �� $ border��5      #��z��5      #��z��5      #��z��5       . ._�5      �� $ PART_ContentHost��5       |�.  $��Hidden=�5"      $��Hidden=�5#      5$       5%      P� 5&      	 � {�
 .  . �5'      �� $ border��6%    �@J�Opacity$
 0.56Q�6L   5(      P� 5)      	 �@J�IsMouseOver
 . . �5*      �� # �  6;    � ���� 5+      $ border��6%   8   � ��6P   �� 5,       � ��$ 1,1,1,1q�6@   5-      P� 5.      	 �@J�IsKeyboardFocused
 . . �5/      �� # �  6;    � ���� 50      $ border��6%   8   � ��6P   51      53      55       56   
   :� 57      $ TextBox.LostFocus��6   .[�58      �� .��59      �� .��5:      �� $0 *(Panel.Background).(SolidColorBrush.Color)�5;      $ 	#FF3F3F46X�5<      $ 	#FF333337X�5=      $ 0:0:0.4D�5>      5?      5@      5A      ]� 5B       5C      �� 5D      + �@��#IsInactiveSelectionHighlightEnabled . 6O   �� 5E       �@��IsSelectionActive .  6=   ./�5G      ��  �@��SelectionBrush# � /�63   5H      5J      �� 5L      " ��6"   .�5M   
   ��  �@  TabNavigation$
 None=�6=   �� 5N   
    �@�FocusVisualStyle 6-   Z� �� 5O   
    �@J�	AllowDrop . 6&   �� 5P   
    �@��PanningMode$ VerticalFirst=�65   �� 5Q   
    �@��IsFlicksEnabled .  63   �� 5R   
    � ��
 �   �6'   �� 5S   
    � ��
 �����6'   �� 5T   
    � ��$
 Left=�67   �� 5U   
    � ��$ Center=�65   �� 5V   
    � ��$ 1,1,1,1q�6,   �� 5W   
    � ��8   6(   �� 5Y   
    � ��$ 0,0,0,0q�6#   �� 5Z   
    � �� 5[      �� 5\      " ��6"   .R�5]      �� $ border��5^      #��z��5_      #��z��5`      #��z��5a       . ._�5c      �� $ PART_ContentHost��5d      |�.  $��Hidden=�5f      $��Hidden=�5g      5h       5i      P� 5j      	 � {�
 .  . �5k      �� $ border��6%    �@J�Opacity$
 0.56Q�6L   5l      P� 5m      	 �@J�IsMouseOver
 . . �5n      �� # �  6;    � ���� 5o      $ border��6%   8   � ��6P   �� 5p       � ��$ 1,1,1,1q�6@   5q      P� 5r      	 �@J�IsKeyboardFocused
 . . �5s      �� # �  6;    � ���� 5t      $ border��6%   8   � ��6P   5u      5w      5y      5z      5|      