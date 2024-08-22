using GameModel = BatootGames.Entities.GameModel;

namespace BatootGames.Interfaces;

public interface ILibraryViewModel
{
    public void ShowAll();
    public void SearchAndUpdate();
    public IEnumerable<GameModel> Search(string request);
}