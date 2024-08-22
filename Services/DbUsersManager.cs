using BatootGames.Data;
using BatootGames.Entities;
using BatootGames.Interfaces;

namespace BatootGames.Services;

public class DbUsersManager: IUsersManager
{

    private ApplicationDbContext _applicationDbContext;
    
    public DbUsersManager()
    {
        _applicationDbContext = DbContextProvider.Provide();
    }

    public void Add(User user)
    {
        _applicationDbContext.Users.Add(user);
        _applicationDbContext.SaveChanges();
    }
    
    public ICollection<User> GetAllUsers()
    {
        return _applicationDbContext.Users.ToList();
    }

    public User? GetUserByLogin(string login)
    {
        return _applicationDbContext.Users.FirstOrDefault(u => u.Username == login);
    }

    public void Remove(int id)
    {
        _applicationDbContext.Users.Remove(new User { UserId = id });
    }

    public void Remove(User user)
    {
        Remove(user.UserId);
    }
}