using System.Collections.ObjectModel;
using BatootGames.Data;
using BatootGames.Interfaces;
using BatootGames.Messages;
using BatootGames.Services;
using BatootGames.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Microsoft.Extensions.Configuration;
using GameModel = BatootGames.Models.GameModel;

namespace BatootGames.ViewModels;

public partial class GameStoreViewModel: ObservableObject, ILibraryViewModel, IViewModel
{
    private readonly DbUserGamesLibraryManager _dbUserGamesLibraryManager;

    private readonly ViewFactory _viewFactory = new();
    
    [ObservableProperty]
    private ObservableCollection<GameModel> _allGames = new();

    [ObservableProperty] private bool _myLib;
    [ObservableProperty]
    private string _myLibText = "MyLib";
    
    [ObservableProperty]
    private ObservableCollection<GameModel> _shownGames = new();

    [ObservableProperty] private string? _searchRequest;
    
    public GameStoreViewModel()
    {
        var configurationBuilder = new ConfigurationBuilder();
        configurationBuilder.AddJsonFile("config.json");

        var config = configurationBuilder.Build();
        var dbContext = new ApplicationDbContext(config);
        dbContext.Database.EnsureCreated();
        _dbUserGamesLibraryManager = new DbUserGamesLibraryManager(dbContext);
        GetAll();
        ShowAll();
        
        WeakReferenceMessenger.Default.Register<GameIdMessage>(this, ShowGamePage);
        WeakReferenceMessenger.Default.Register<RateMessage>(this, Rate);
        WeakReferenceMessenger.Default.Register<SaveDeleted>(this, Save );
    }

    private void Save(object recipient, SaveDeleted message)
    {
        _dbUserGamesLibraryManager.Save();
        ChoosePage();
    }

    private void Rate(object recipient, RateMessage message)
    {
        var receivedGame = _dbUserGamesLibraryManager.GetGameById(message.GameId);
        
        if (receivedGame == null)
            return;
        
        _dbUserGamesLibraryManager.Rate(receivedGame.Id, receivedGame, message.Rating);
        ChoosePage();
    }

    private void ShowGamePage(object recipient, GameIdMessage message)
    {
        var game = _dbUserGamesLibraryManager.GetGameById(message.Id);
        if (game != null)
        {
            var aboutGameViewModel = new AboutGameViewModel(game);
            var view = _viewFactory.CreateView<AboutGameView>(aboutGameViewModel);
            WeakReferenceMessenger.Default.Send(view);
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
            if (IsMyLibEmpty())
                return;
            MyLib = !MyLib;
            MyLibText = "All games";
            ShowSavedGames();
        }
        else
        {
            MyLibText = "MyLib";
            MyLib = !MyLib;
            ShowAll();
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
        ShownGames = new ObservableCollection<GameModel>();
        MyLibText = "All games";
        foreach (var game in AllGames)
        {
            if (game.Saved)
            {
                ShownGames.Add(game);
            }   
        }
    }

    private void ChoosePage()
    {
        if (MyLib)
            ShowSavedGames();
        else
            ShowAll();
    }

    private bool IsMyLibEmpty()
    {
        var count = 0;
        foreach (var game in _dbUserGamesLibraryManager.GetGames())
        {
            if (game.Saved)
                count++;
        }
        return count == 0;
    }
}