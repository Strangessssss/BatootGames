using BatootGames.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BatootGames.Data.Configurations;

public class GameConfiguration: IEntityTypeConfiguration<GameModel>
{
    public void Configure(EntityTypeBuilder<GameModel> builder)
    {
        builder.HasKey(g => g.GameId);

        builder.Property(g => g.Name)
            .HasMaxLength(100);

        builder.Property(g => g.Image)
            .HasMaxLength(255);

        builder.Property(g => g.Description)
            .HasMaxLength(1000);

        builder.Property(g => g.ReleaseDate)
            .IsRequired();

        builder.Property(g => g.Developer)
            .HasMaxLength(100);

        builder.HasMany(g => g.Comments)
            .WithOne(c => c.Game)
            .HasForeignKey(c => c.GameId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}