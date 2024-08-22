using System.Text.RegularExpressions;
using BatootGames.Interfaces;
using BatootGames.Services.Results;

namespace BatootGames.Services;

public static class PasswordChecker
{
    public static PasswordCheckResult CheckPasswordStrength(string password)
    {
        if (string.IsNullOrEmpty(password))
            return new PasswordCheckResult { IsStrong = false, Message = "Password cannot be empty." };

        var result = new PasswordCheckResult { IsStrong = true };

        if (password.Length < 8)
        {
            result.IsStrong = false;
            result.Message += "Password must be at least 8 characters long. ";
        }

        if (password.Length > 100)
        {
            result.IsStrong = false;
            result.Message += "Password must be less than 100 characters long. ";
        }

        if (!Regex.IsMatch(password, @"[A-Z]"))
        {
            result.IsStrong = false;
            result.Message += "Password must contain at least one uppercase letter. ";
        }

        if (!Regex.IsMatch(password, @"[a-z]"))
        {
            result.IsStrong = false;
            result.Message += "Password must contain at least one lowercase letter. ";
        }

        if (!Regex.IsMatch(password, @"[0-9]"))
        {
            result.IsStrong = false;
            result.Message += "Password must contain at least one digit. ";
        }

        if (!Regex.IsMatch(password, @"[\W_]")) // \W matches any non-word character, including special characters
        {
            result.IsStrong = false;
            result.Message += "Password must contain at least one special character. ";
        }
        
        

        if (result.IsStrong)
        {
            result.Message = "Password is strong.";
        }

        return result;
    }
}