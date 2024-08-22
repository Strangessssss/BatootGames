using BatootGames.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BatootGames.Data.Configurations;

public class UserGameConfiguration: IEntityTypeConfiguration<UserGame>
{
    public void Configure(EntityTypeBuilder<UserGame> builder)
    {
        builder.HasKey(g => g.GameId);

        builder.HasOne(g => g.User)
            .WithMany(u => u.UserGames)
            .HasForeignKey(g => g.UserId);
        
        builder.HasOne(ug => ug.Game)
            .WithMany()
            .HasForeignKey(ug => ug.GameId);

    }
}