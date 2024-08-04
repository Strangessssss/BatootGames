using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using BatootGames.Messages;
using BatootGames.Services;
using BatootGames.ViewModels;
using BatootGames.Views;
using CommunityToolkit.Mvvm.Messaging;
using ListView = System.Windows.Forms.ListView;

namespace BatootGames;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private ViewFactory _viewFactory = new ViewFactory();
    public MainWindow()
    {
        InitializeComponent();
        MainContentControl.Content = _viewFactory.CreateView<GamesStoreView>(new GameStoreViewModel());
        WeakReferenceMessenger.Default.Register<AboutGameView>(this, ShowViewModel);
        
        WeakReferenceMessenger.Default.Register<CloseMessage>(this, CloseGame);
    }

    private void CloseGame(object recipient, CloseMessage message)
    {
        MainContentControl.Content = _viewFactory.CreateView<GamesStoreView>(new GameStoreViewModel());
    }

    private void ShowViewModel(object recipient, AboutGameView message)
    {
        MainContentControl.Content = message;
    }
}