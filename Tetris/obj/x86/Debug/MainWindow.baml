   M S B A M L   `   `   `  ����[  WSystem.Windows.Forms, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089Y@clr-namespace:System.Windows.Forms;assembly=System.Windows.FormsSystem.Windows.Forms  5   	   
 Tetris   Tetris.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   UwinForms@clr-namespace:System.Windows.Forms;assembly=System.Windows.Forms     5   	   -   5          � Title$  Tetris��5   	   $	��426��6   $	��360��6%    � 
ResizeMode$ NoResize=�61    � WindowStartupLocation$ CenterScreen=�6G    � WindowStyle$
 None=�6l   ���	#00000000 � AllowsTransparency .  � Icon$  /Tetris;component/icon.ico��6E   � 	Resources 5         Background1 �� EndPoint �� 
StartPoint	 � Color
 � Offset  Background2  BlueVioletBrushKey��%w  &	        &	 �     &	 8    �� 5   
   $ 1,0.5&�62   $ 0,0.5&�6C   .��5      � $	 	#FF1E3FA7��6   $
 0Q�6-   � 5	      $	 	#FF5478F2��6   $
 1Q�6-   5
      �� 5   
   $ 1,0.5&�62   $ 0,0.5&�6C   .��5      � $	 	#FF5E7ADF��6   $
 0Q�6-   � 5      $	 	#FFD4DFFF��6   $
 1Q�6-   5      ��5   
   	#FF4B26D2��6@   5      .��5      � .+�5   
   �� -   5        $��	BackImage��5      $	��426��6   $
��Fill=�6.   $	��360��6=   $-��'/Tetris;component/Images/background.png��6I   �� 5   
   -   5        $��canvas��5   M   $��360px��6   $
��Left=�6   $��	82,35,0,0q�6:   $
��None=�6[   $	��Top=�6j   $��180px��6�   $	��180��6�   6�   �� 5   
   -   5        $��button1��5   X   $��1��6   $��30��6   $
��Left=�6)   $��
29,278,0,0q�6D   $	��Top=�6g   $��30��6    �� 	ClickMode$ Hover=�6�   $��0q�6�   #��[ 6�   #��[ 6�   6  �� 5   
   -   5        $��button2��5   �   ���	#FFDADADA$��0q�6(   $ Hover=�6<   $��2��6N   $��30��6Z   $
��Left=�6f   $��
29,319,0,0q�6�   $	��Top=�6�   $��30��6�   �� 5   
   -   5        $��button3��5   �   ���	#FFDADADA$��0q�6(   $ Hover=�6<   $��3��6N   $��30��6Z   $
��Left=�6f   $��
29,360,0,0q�6�   $	��Top=�6�   $��30��6�   �� 5   
   -   5        $
��next��5   K   $��36��6   $
��Left=�6   $��
296,63,0,0q�67   $��UniformToFill=�6W   $	��Top=�6o   $��36��6�   �� 5   
   -   5        $��lines��5   K   $��36��6   $
��Left=�6   $��
18,232,0,0q�67   $	��Top=�6X   $��47��6p   ���	#FFFF9D00$��2q�6�   $��Center=�6�   $��Center=�6�   $
��Bold"�6�   $��16(�6�   ��5      �� 5       �� ImageSource$, &/Tetris;component/Images/linesBack.png��6   5      5      5      