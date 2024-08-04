using GameModel = BatootGames.Models.GameModel;

namespace BatootGames.Interfaces;

public interface IUserGamesLibraryManager
{
    void Add(GameModel game);
    void Rate(int id, GameModel game , float rating);
    ICollection<GameModel> GetGames();
    GameModel? GetGameById(int id);
    void Remove(int id);
    void Remove(GameModel game);
}