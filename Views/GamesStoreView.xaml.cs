using System.Windows.Controls;
using BatootGames.Interfaces;
using BatootGames.Messages;
using CommunityToolkit.Mvvm.Messaging;

namespace BatootGames.Views;

public partial class GamesStoreView : UserControl, IView
{
    public GamesStoreView()
    {
        InitializeComponent();
    }
}