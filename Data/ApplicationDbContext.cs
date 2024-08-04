using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using GameModel = BatootGames.Models.GameModel;

namespace BatootGames.Data
{
    public class ApplicationDbContext: DbContext
    {
        private readonly IConfiguration _configuration;

        public ApplicationDbContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(_configuration["ServerConnectionString"]);
        }
        
        public DbSet<GameModel> Games { get; set; }
    }
}