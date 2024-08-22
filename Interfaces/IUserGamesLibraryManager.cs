using BatootGames.Entities;
using GameModel = BatootGames.Entities.GameModel;

namespace BatootGames.Interfaces;

public interface IUserGamesLibraryManager
{
    void AddOrRemove(int gameId, int userId);
    void Add(int gameId, int userId);
    ICollection<GameModel> GetGames();
    GameModel? GetGameById(int id);
    void Remove(int gameId, int userId);
    void AddComment(Comment comment);
    IEnumerable<Comment> GetCommentsByGameId(int gameId);
}