   M S B A M L   `   `   `  ����F  BQuanLyCaPhe, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:QuanLyCaPhe.ViewQuanLyCaPhe.View  5   	   -    'QuanLyCaPhe.View.ScanQRCodeDiscountView   5   	   a ]DevExpress.Xpf.Core.v17.1, Version=17.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   0localclr-namespace:QuanLyCaPhe.View     5   	   -   5        $��scanQRCodeDiscountView��5   	     � WindowState$  Normal=�5   	    � WindowStartupLocation$ CenterScreen=�5	   	    � WindowStyle$
 None=�5
   	    � 
ResizeMode$ NoResize=�5   	    � Title$ ScanQRCodeDiscountView��5   	   $	��500��6(   $	��600��65      ScanQRCodeDiscountViewModel � DataContext# [  5   	   .��5      �  � RowDefinitions 5   
   �� 5      $
��auto �6   6,   �� 5      $	��400 �6   6+   �� 5      $
��auto �6   6,   .+�5   
   � $��0��6   .+�5      �� $��X��5      $��30��6   $��0 0 0 0q�5      $��40��5      $��14(�5      $��Center=�5      
������ 
���   �
������ $��Right=�5!      ��5      �� *CloseWebcamCommand+ �� UpdateSourceTrigger$ PropertyChanged=���5      ��  �� ElementName$ scanQRCodeDiscountView��5!   0   5"      � 5#   
   $��1��6   .+�5$      �� -   5        	 ��Name$	 videoPlayer��5$   -   $��UniformToFill=�6   $��40 40 40 40q�6B   5%      � 5'   
   $��2��6   .+�5(      � .+�5)      �� -   5        
 ��Name$
 comboBox��5)      $	��180��5*      $��30��5+      $��12(�5,      $��	20 30 0 0q�5-      $
��Left=�5.      $��Bottom=�5/       �� DisplayMemberPath$
 Name��50      ��51      �� *VideoDevices+ �� SelectedItem 52      �� *CurrentDevice+�� 54      $
��Left=�56      $��Start��57      $��Impact)�58      $��12(�59      $	��100��5:      $��25��5;      $��	30 25 0 0q�5<      ���	#FF307CD6���	#FF307CD6��54      �� *StartCamera+��55      �� $ scanQRCodeDiscountView��5>      �� 5@      $
��Left=�5B      $
��Stop��5C      $��Impact)�5D      $��12(�5E      $	��100��5F      $��25��5G      $��	20 25 0 0q�5H      ���	#FF307CD6���	#FF307CD6��5@      �� *
StopCamera+��5A      �� $ scanQRCodeDiscountView��5J      �� 5L      $
��Left=�5N      $��Check QR Code��5O      $��Impact)�5P      $��12(�5Q      $	��110��5R      $��25��5S      $��	20 25 0 0q�5T      ���	#FF307CD6���	#FF307CD6��5L      �� *CheckQRCodeDiscount+��5M      �� $ scanQRCodeDiscountView��5V      5W      5X      5Y      5Z      