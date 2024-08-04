using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms.VisualStyles;
using BatootGames.Messages;
using BatootGames.ViewModels;
using CommunityToolkit.Mvvm.Messaging;

namespace BatootGames.Views;

public partial class AboutGameView : UserControl
{
    public AboutGameView()
    {
        InitializeComponent();
    }

    private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
    {
        WeakReferenceMessenger.Default.Send(new CloseMessage());
    }
}