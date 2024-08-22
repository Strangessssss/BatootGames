namespace BatootGames.Entities;

public class UserGame
{
    public int UserGameId { get; set; }
    
    public int UserId { get; set; }
    public int GameId { get; set; }
    
    public virtual User User { get; set; }
    public virtual GameModel Game { get; set; }
}