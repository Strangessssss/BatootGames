   M S B A M L   `   `   `  ����F  BBatootGames, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null;"clr-namespace:BatootGames.ServicesBatootGames.Services  5      &     BatootGames.Views.GamesStoreView   5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      7services"clr-namespace:BatootGames.Services     5      ���#1e173d.��5	      �   � ShowGridLines$  False��6    � RowDefinitions 5
   
   �� 5      $
��0.1* �6   �� 5      .+�5   
   � $��0��6   .+�5      ��  �� Orientation$ 
Horizontal=�6   $��10 0 0 0q�62   .+�5      �� $	��150��6   $
��Left=�6&   $��10q�6A   ��6M   �� *SearchRequest+6n   �� 5      $��50��6   
���� K�
�������$
��Left=�6K   $��0q�6f   $��Center=�6�   ��6z   �� *SearchAndUpdateCommand+.��6�   Search6�   �� 5      $��	150 0 0 0q�6   $��50��6,   
���� K�
�������$
��Left=�6^   $��0q�6y   $��Center=�6�   ��6�   �� *MyLibOnCommand+��6�   �� *	MyLibText+6�   5      5      � 5   
   $��1��6   .+�5      �� $��0q�6   
������ ��6/   �� *
ShownGames+��5      �� 5      .R�5      �� $ 
Horizontal=�6)   $��Stretch=�6B   .+�5   "   �� $	��100��6(   .��5   &   �� ��5   *   �� 5   .    �� 	UriSource 5   2   �� 5   6    �� Path$ Image�6>   5!   /   5#   '   5$   #   �� 5%   "   $	��370��6,   
���������68   �� *Name+6N   �� 5&   "   $��40 0 0 0q�6)   $��80��6;   $��50��6F   $��
Add/Delete��6q   ��6R   �� *
AddCommand+6q   �� 5'   "   $��40 0 0 0q�6)   $��50��6;   $��50��6F   $
��info��6s   ��6R   �� *AboutCommand+6s   5(      5)      5+      5,      5-      5.      