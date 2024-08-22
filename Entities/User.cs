namespace BatootGames.Entities;

public class User
{
    public int UserId { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    
    public virtual ICollection<UserGame> UserGames { get; set; }
    public ICollection<Comment> Comments { get; set; }
}
