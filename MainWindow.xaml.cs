using System.Windows;
using BatootGames.Entities;
using BatootGames.Messages;
using BatootGames.Services;
using BatootGames.ViewModels;
using BatootGames.Views;
using CommunityToolkit.Mvvm.Messaging;

namespace BatootGames;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private ViewFactory _viewFactory = new();
    private User? _user;
    private FrameworkElement? _currentView;
    private GamesStoreView? _gamesStoreView;
    public MainWindow()
    {
        InitializeComponent();
        MainContentControl.Content = _viewFactory.CreateView<UserLoginingView>(new UserLoginningViewModel());
        
        WeakReferenceMessenger.Default.Register<User>(this, Login);
        WeakReferenceMessenger.Default.Register<AboutGameView, string>(this, "openAbout", OpenGameAbout);
        WeakReferenceMessenger.Default.Register<CloseMessage>(this, CloseGame);
    }

    private void Login(object recipient, User message)
    {
        _user = message;
        _gamesStoreView =
            _viewFactory.CreateView<GamesStoreView>(new GameStoreViewModel(new DbUserGamesLibraryManager(), message));
        MainContentControl.Content = _gamesStoreView;
    }


    private void CloseGame(object recipient, CloseMessage message)
    {
        MainContentControl.Content = _gamesStoreView;
    }

    private void OpenGameAbout(object recipient, AboutGameView message)
    {
        MainContentControl.Content = message;
    }
}