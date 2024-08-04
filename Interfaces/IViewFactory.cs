using System.Windows.Controls;
using BatootGames.Services;

namespace BatootGames.Interfaces;

public interface IViewFactory
{
    public TV CreateView<TV>(IViewModel viewModel) where TV : UserControl, new();
}