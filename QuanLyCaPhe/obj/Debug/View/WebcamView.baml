   M S B A M L   `   `   `  ����F  BQuanLyCaPhe, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:QuanLyCaPhe.ViewQuanLyCaPhe.View  5   	   !    QuanLyCaPhe.View.WebcamView   5   	   a ]DevExpress.Xpf.Core.v17.1, Version=17.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   0localclr-namespace:QuanLyCaPhe.View     5   	   -   5        $��
webcamView��5   	     � WindowState$  Normal=�5   	    � WindowStartupLocation$ CenterScreen=�5	   	    � WindowStyle$
 None=�5
   	    � 
ResizeMode$ NoResize=�5   	      WebcamViewModel � DataContext# [  5   	    � Title$ 
WebcamView��5   	   $	��500��6   $	��600��6)   .��5      �  � RowDefinitions 5   
   �� 5      $
��auto �6   6,   �� 5      $	��400 �6   6+   �� 5      $
��auto �6   6,   �� 5      $��50 �6   6*   .+�5   
   � $��0��6   .+�5      �� $��X��5      $��30��6   $��0 0 0 0q�5      $��40��5      $��14(�5      $��Center=�5      
������ 
���   �
������ $��Right=�5"      ��5      �� *CloseWebcamCommand+ �� UpdateSourceTrigger$ PropertyChanged=���5      ��  �� ElementName$ 
webcamView��5"   0   5#      � 5$   
   $��1��6   .+�5%      �� -   5        	 ��Name$	 videoPlayer��5%   -   $��UniformToFill=�6   $��40 40 40 40q�6B   5&      � 5(   
   $��2��6   .+�5)      � -   5        $��ProgressBar��5)      $	��350��6-   $��20��69   $��Center=�6E   $��Center=�6b   6�   �� 5*      $��Center=�5+   $   $��0 0 0 0q�5,   $   $��10(�5-   $   $��Center=�5.   $   ��5*      �� $ ProgressBar��
 �� Path$
 Value� �� StringFormat$ {0:0}%��5.   A   5/      � 50   
   $��3��6   .+�51      � .+�53      �� -   5         ��Name$ comboBox��53      $	��180��54      $��30��55      $��12(�56      $��	20 30 0 0q�57      $
��Left=�58      $��Bottom=�59       �� DisplayMemberPath$
 Name��5:      ��5;      �� *VideoDevices+ �� SelectedItem 5<      �� *CurrentDevice+�� 5>      $
��Left=�5@      $��Start��5A      $��Impact)�5B      $��14(�5C      $	��100��5D      $��25��5E      $��	30 20 0 0q�5F      ���	#FF307CD6���	#FF307CD6��5>      �� *StartCamera+��5?      �� $ 
webcamView��5H      �� 5J      $
��Left=�5L      $
��Stop��5M      $��Impact)�5N      $��14(�5O      $	��100��5P      $��25��5Q      $��	20 20 0 0q�5R      ���	#FF307CD6���	#FF307CD6��5J      �� *
StopCamera+��5K      �� $ 
webcamView��5T      �� 5V      $
��Left=�5X      $��Check Login��5Y      $��Impact)�5Z      $��14(�5[      $	��110��5\      $��25��5]      $��	20 20 0 0q�5^      ���	#FF307CD6���	#FF307CD6��5V      �� *
CheckLogin+��5W      �� $ 
webcamView��5`      5a      5b      5c      5d      