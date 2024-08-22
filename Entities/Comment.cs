namespace BatootGames.Entities;

public class Comment
{
    public int CommentId { get; set; }
    public string Content { get; set; }
    public int UserId { get; set; }
    public int GameId { get; set; }
    public User User { get; set; }
    public GameModel Game { get; set; }
    
    public string Username => User.Username;
}