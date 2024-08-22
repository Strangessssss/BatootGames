using System.Collections.ObjectModel;
using BatootGames.Entities;
using BatootGames.Interfaces;
using BatootGames.Messages;
using BatootGames.Services;
using BatootGames.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using GameModel = BatootGames.Entities.GameModel;

namespace BatootGames.ViewModels;

public partial class GameStoreViewModel: ObservableObject, ILibraryViewModel, IViewModel
{
    private readonly DbUserGamesLibraryManager _dbUserGamesLibraryManager;

    private readonly User _user;

    private readonly ViewFactory _viewFactory = new();
    
    [ObservableProperty]
    private ObservableCollection<GameModel> _allGames = new();

    [ObservableProperty] private bool _myLib;
    [ObservableProperty]
    private string _myLibText = "MyLib";
    
    [ObservableProperty]
    private ObservableCollection<GameModel> _shownGames = new();

    [ObservableProperty] private string? _searchRequest;
    
    public GameStoreViewModel(DbUserGamesLibraryManager dbUserGamesLibraryManager, User user)
    {
        _dbUserGamesLibraryManager = dbUserGamesLibraryManager;
        _user = user;
        GetAll();
        ShowAll();
        
        WeakReferenceMessenger.Default.Register<GameIdMessage, string>(this, "openAbout", ShowGamePage);
        WeakReferenceMessenger.Default.Register<GameIdMessage, string>(this, "addGame", AddGame);
        WeakReferenceMessenger.Default.Register<SaveDeleted>(this, Save );
    }

    private void AddGame(object recipient, GameIdMessage message)
    {
        _dbUserGamesLibraryManager.AddOrRemove(message.Id, _user.UserId);
        if (MyLib)
            ShowSavedGames();
        else
            ShowAll();
    }

    private void Save(object recipient, SaveDeleted message)
    {
        _dbUserGamesLibraryManager.Save();
        ChoosePage();
    }

    private void ShowGamePage(object recipient, GameIdMessage message)
    {
        var game = _dbUserGamesLibraryManager.GetGameById(message.Id);
        if (game != null)
        {
            var aboutGameViewModel = new AboutGameViewModel(game, _user, new DbUserGamesLibraryManager());
            var view = _viewFactory.CreateView<AboutGameView>(aboutGameViewModel);
            WeakReferenceMessenger.Default.Send(view, "openAbout");
        }
    }

    [RelayCommand]
    public void SearchAndUpdate()
    {
        if (SearchRequest != null)
        {
            MyLib = false;
            MyLibText = "MyLib";
            var foundGames = Search(SearchRequest);
            ShownGames = (ObservableCollection<GameModel>)foundGames;
        }
    }
    
    [RelayCommand]
    private void MyLibOn()
    {
        if (!MyLib)
        {
            ShowSavedGames();
            MyLib = !MyLib;
        }
        else
        {
            ShowAll();
            MyLib = !MyLib;
        }
    }
    
    public IEnumerable<GameModel> Search(string request)
    {
        var foundGames = new ObservableCollection<GameModel>();
        var isRequestFilled = string.IsNullOrWhiteSpace(SearchRequest);
        foreach (var game in AllGames)
        {
            if (game.Name != null && (game.Name.ToLower().Contains(request.ToLower()) || isRequestFilled))
                foundGames.Add(game);
        }
        
        return foundGames;
    }

    public void ShowAll()
    {
        ShownGames = [];
        foreach (var game in AllGames)
        {
            ShownGames.Add(game);
        }
    }

    private void GetAll()
    {
        AllGames = [];
        foreach (var game in _dbUserGamesLibraryManager.GetGames())
        {
            AllGames.Add(game);
        }
    }

    private void ShowSavedGames()
    {
        ShownGames = [];
        var savedGames = _dbUserGamesLibraryManager.GetGamesByUserId(_user.UserId);
        foreach (var userGame in savedGames)
        {
            var game = _dbUserGamesLibraryManager.GetGameById(userGame.GameId);
            if (game != null)
                ShownGames.Add(game);
        }
    }

    private void ChoosePage()
    {
        if (MyLib)
            ShowSavedGames();
        else
            ShowAll();
    }
}