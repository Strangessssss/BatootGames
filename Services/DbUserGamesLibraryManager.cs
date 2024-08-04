using BatootGames.Data;
using BatootGames.Interfaces;
using Microsoft.EntityFrameworkCore;
using GameModel = BatootGames.Models.GameModel;

namespace BatootGames.Services;

public class DbUserGamesLibraryManager: IUserGamesLibraryManager
{

    private ApplicationDbContext _applicationDbContext;
    
    public DbUserGamesLibraryManager(ApplicationDbContext applicationDbContext)
    {
        _applicationDbContext = applicationDbContext;
    }
    
    public void Add(GameModel game)
    {
        _applicationDbContext.Add(game);
        _applicationDbContext.SaveChanges();
    }

    public void Rate(int id, GameModel game , float rating)
    {
        var existingEntity = _applicationDbContext.Games.Find(id);
        if (existingEntity != null)
        {
            _applicationDbContext.Entry(existingEntity).State = EntityState.Detached;
        }

        var gameCopy = new GameModel
        {
            Description = game.Description,
            Developer = game.Developer,
            Id = id,
            Image = game.Image,
            Name = game.Name,
            Rating = rating,
            ReleaseDate = game.ReleaseDate,
            Saved = true
        };

        Update(gameCopy);
    }

    public void Save()
    {
        _applicationDbContext.SaveChanges();
    }
    
    public void Update(GameModel gameModel)
    {
        _applicationDbContext.Update(gameModel);
        _applicationDbContext.SaveChanges();
    }
    
    public ICollection<GameModel> GetGames()
    {
        return _applicationDbContext.Games.ToList();
    }

    public GameModel? GetGameById(int id)
    {
        return _applicationDbContext.Games.FirstOrDefault(a => a.Id == id);
    }

    public void Remove(int id)
    {
        _applicationDbContext.Games.Remove(new()
        {
            Id = id
        });

        _applicationDbContext.SaveChanges();
    }

    public void Remove(GameModel game)
    {
        Remove(game.Id);
    }
}