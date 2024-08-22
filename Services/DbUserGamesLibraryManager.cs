using BatootGames.Data;
using BatootGames.Entities;
using BatootGames.Interfaces;
using Microsoft.EntityFrameworkCore;
using GameModel = BatootGames.Entities.GameModel;

namespace BatootGames.Services;

public class DbUserGamesLibraryManager: IUserGamesLibraryManager
{

    private ApplicationDbContext _applicationDbContext;
    
    public DbUserGamesLibraryManager()
    {
        _applicationDbContext = DbContextProvider.Provide();
    }
    
    public void AddOrRemove(int gameId, int userId)
    {
        var existingUserGame = _applicationDbContext.UserGames
            .FirstOrDefault(ug => ug.GameId == gameId && ug.UserId == userId);

        var newUserGame = new UserGame
        {
            GameId = gameId,
            UserId = userId
        };
        if (existingUserGame == null)
        {
            _applicationDbContext.UserGames.Add(newUserGame);
        }
        else
        {
            _applicationDbContext.UserGames.Remove(existingUserGame);
        }
        
        _applicationDbContext.SaveChanges();
    }

    public void Add(int gameId, int userId)
    {
        var newUserGame = new UserGame
        {
            GameId = gameId,
            UserId = userId
        };
        _applicationDbContext.UserGames.Add(newUserGame);
    }

    public void Save()
    {
        _applicationDbContext.SaveChanges();
    }
    
    public ICollection<GameModel> GetGames()
    {
        return _applicationDbContext.Games.ToList();
    }

    public GameModel? GetGameById(int id)
    {
        return _applicationDbContext.Games.FirstOrDefault(a => a.GameId == id);
    }
    
    public ICollection<GameModel> GetGamesByUserId(int id)
    {
        var user = _applicationDbContext.Users
            .Include(u => u.UserGames) 
            .ThenInclude(ug => ug.Game)
            .FirstOrDefault(u => u.UserId == id);

        if (user == null)
        {
            return new List<GameModel>();
        }

        var gameModels = user.UserGames
            .Select(ug => new GameModel
            {
                GameId = ug.Game.GameId,
                Name = ug.Game.Name,
                Description = ug.Game.Description,
                Image = ug.Game.Image,
                ReleaseDate = ug.Game.ReleaseDate,
                Developer = ug.Game.Developer
            })
            .ToList();

        return gameModels;
    }
    
    public void Remove(int gameId, int userId)
    {
        var newUserGame = new UserGame
        {
            GameId = gameId,
            UserId = userId
        };
        
        _applicationDbContext.UserGames.Remove(newUserGame);
        _applicationDbContext.SaveChanges();
    }
    
    public void AddComment(Comment comment)
    {
        _applicationDbContext.Comments.Add(comment);
        _applicationDbContext.SaveChanges();
    }

    public IEnumerable<Comment> GetCommentsByGameId(int gameId)
    {
        return _applicationDbContext.Comments
            .Include(c => c.User)
            .Where(c => c.GameId == gameId)
            .ToList();
    }
}