using System.Windows.Controls;
using BatootGames.Interfaces;
using BatootGames.ViewModels;
using BatootGames.Views;
using Microsoft.Extensions.DependencyInjection;

namespace BatootGames.Services;


public class ViewFactory : IViewFactory
{
    public TV CreateView<TV>(IViewModel viewModel) where TV : UserControl, new()
    {
        var newView = new TV
        {
            DataContext = viewModel
        };
        return newView;
    }
}