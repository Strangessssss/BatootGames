using BatootGames.Data.Configurations;
using BatootGames.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using GameModel = BatootGames.Entities.GameModel;

namespace BatootGames.Data
{
    public sealed class ApplicationDbContext: DbContext
    {
        private readonly IConfiguration _configuration;
        
        public ApplicationDbContext(IConfiguration configuration)
        {
            _configuration = configuration;
            Database.EnsureCreated();
            
        }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(_configuration["ServerConnectionString"]);
        }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new CommentConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new UserGameConfiguration());
            modelBuilder.ApplyConfiguration(new GameConfiguration());
        }
        
        public DbSet<User> Users { get; set; }
        public DbSet<GameModel> Games { get; set; }
        public DbSet<UserGame> UserGames { get; set; }
        public DbSet<Comment> Comments { get; set; }
    }
}