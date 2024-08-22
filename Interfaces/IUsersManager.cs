using BatootGames.Entities;
using GameModel = BatootGames.Entities.GameModel;

namespace BatootGames.Interfaces;

public interface IUsersManager
{
    void Add(User user);
    User? GetUserByLogin(string login);
    void Remove(int id);
    void Remove(User user);
    public ICollection<User>? GetAllUsers();
}