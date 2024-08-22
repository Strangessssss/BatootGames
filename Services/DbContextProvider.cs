using BatootGames.Data;
using BatootGames.Interfaces;
using Microsoft.Extensions.Configuration;

namespace BatootGames.Services;

public static class DbContextProvider
{
    public static ApplicationDbContext Provide()
    {
        var configurationBuilder = new ConfigurationBuilder();
        configurationBuilder.AddJsonFile("config.json");
        var config = configurationBuilder.Build();
        var dbContext = new ApplicationDbContext(config);
        return dbContext;
    }
}