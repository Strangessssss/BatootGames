using BatootGames.Entities;

namespace BatootGames.Services.Results;

public class LogInSignUpResult
{
    public bool Success { get; set; }
    public string? Message { get; set; }
    public User? User { get; set; }
}