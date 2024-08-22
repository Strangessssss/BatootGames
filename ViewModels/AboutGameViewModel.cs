using System.Collections.ObjectModel;
using BatootGames.Entities;
using BatootGames.Interfaces;
using BatootGames.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GameModel = BatootGames.Entities.GameModel;

namespace BatootGames.ViewModels;

public partial class AboutGameViewModel : ObservableObject, IViewModel
{
    
    private readonly IUserGamesLibraryManager _userGamesLibraryManager;
    private int _userId;
    [ObservableProperty] private string _userName;
    private readonly GameModel? _game;
    
    [ObservableProperty]
    private string _name;
    [ObservableProperty]
    private string _image;
    [ObservableProperty]
    private string _description;
    [ObservableProperty]
    private DateTime _releaseDate;
    [ObservableProperty]
    private float _rating;
    [ObservableProperty]
    private bool _saved;
    [ObservableProperty]
    private string _developer;

    public AboutGameViewModel(GameModel? game, User user, IUserGamesLibraryManager userGamesLibraryManager)
    {
        _userId = user.UserId;
        _userName = user.Username;
        _game = game;
        _userGamesLibraryManager = userGamesLibraryManager;
        
        if (game == null) return;
        if (game.Name != null) _name = game.Name;
        if (game.Image != null) _image = game.Image;
        if (game.Description != null) _description = game.Description;
        _releaseDate = game.ReleaseDate;
        if (game.Developer != null) _developer = game.Developer;
        
        LoadComments(game.GameId);
    }
    
    [ObservableProperty]
    private string _newCommentContent;

    [ObservableProperty]
    private ObservableCollection<Comment> _comments = new();
    
    private void LoadComments(int gameId)
    {
        var comments = _userGamesLibraryManager.GetCommentsByGameId(gameId);
        Comments = new ObservableCollection<Comment>();
        foreach (var comment in comments)
        {
            Comments.Add(comment);
        }
    }
    
    [RelayCommand]
    private void AddComment()
    {
        if (string.IsNullOrWhiteSpace(NewCommentContent)) return;

        if (_game != null)
        {
            var comment = new Comment
            {
                Content = NewCommentContent,
                UserId = _userId,
                GameId =  _game.GameId
            };

            _userGamesLibraryManager.AddComment(comment);
            Comments.Add(comment);
        }

        NewCommentContent = string.Empty;
    }
    
}