using BatootGames.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using GameModel = BatootGames.Models.GameModel;

namespace BatootGames.ViewModels;

public partial class AboutGameViewModel : ObservableObject, IViewModel
{
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

    public AboutGameViewModel(GameModel? game)
    {
        if (game == null) return;
        if (game.Name != null) _name = game.Name;
        if (game.Image != null) _image = game.Image;
        if (game.Description != null) _description = game.Description;
        _releaseDate = game.ReleaseDate;
        _rating = game.Rating;
        _saved = game.Saved;
        if (game.Developer != null) _developer = game.Developer;
    }
}