   M S B A M L   `   `   `  ����C  ?Roulette, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null#clr-namespace:RouletteRoulette  5   	       Roulette.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   (localclr-namespace:Roulette     5   	     � Title$  AliciaRouletteGrid��5   	   $	��900��6$   $
��1600��61    � WindowStyle$
 None=�6>    � 
ResizeMode$ 	CanResize=�6Q   
���   � � 	Resources 5
       �� MergedDictionaries �� Source�� 5   
    5      �� 5      $ Theme/RouletteTheme.xaml?�6%   %    5      .��5      �� 
���   � �� CornerRadius$ 10��5      ._�5   
   �  � ColumnDefinitions 5      �� 5      $��1* �6#   �� 5      $��2* �6#   �� 5      $��2* �6#   �� 5      $��2* �6#   �� 5      $��1* �6#   .+�5      � -   5         �Name$ 	MainGrid0��5      $��0��6&   $��10,131,10.2,100q�66   	 � RowDefinitions	 5      �� 5      $��2* �6$   �� 5       $
��0.5* �6$   �� 5!      $
��0.5* �6$   .+�5$      � $��0��6   $��0��6$   
��� � �.+�5%      �� $��0��6    $��55(�6)   5&      5'      � 5)      -   5        $ 	MainGrid1��5)      $��1��6&   $��10,100q�66   .+�5*      (� $��0��6   .��5+      A� -   5        $��
NumberGrid��5+   "   $��0��64   
 A� Rows$
 3��6A    A� Columns$ 4��6J   $��40��6V   $��45��6a   5-      5.      5/      � 51      -   5        $ 	MainGrid2��51      $��2��6&   $��10,100q�66   .+�52      (� $��0��6   .��53      A� -   5        $��NumberGrid2��53   "   $��0��65   $
 3��6B   $ 4��6K   $��40��6W   $��45��6b   55      56      57      � 59      -   5        $ 	MainGrid3��59      $��3��6&   $��10,100q�66   .+�5:      (� $��0��6   .��5;      A� -   5        $��NumberGrid3��5;   "   $��0��65   $
 3��6B   $ 4��6K   $��40��6W   $��45��6b   5=      5>      5?      � 5A      -   5        $ 	MainGrid4��5A      $��4��6&   $��10,100q�66   5B      5E      5F      5G      