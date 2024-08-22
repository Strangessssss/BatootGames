using BatootGames.Entities;
using BatootGames.Interfaces;
using BatootGames.Services.Results;

namespace BatootGames.Services;

public class LoginService
{
    private readonly IUsersManager _userManager;

    public LoginService()
    {
        _userManager = new DbUsersManager();
        var users = _userManager.GetAllUsers();
        if (users != null)
            foreach (var user in users)
            {
                Console.WriteLine(user.Username);
            }
    }

    public LogInSignUpResult Login(string? login, string? password)
    {
        if (string.IsNullOrEmpty(login))
            return new LogInSignUpResult { Success = false, Message = "Login is not provided"};
        
        var user = _userManager.GetUserByLogin(login);
        if (user == null)
            return new LogInSignUpResult { Success = false, Message = "User not found"};

        if (string.IsNullOrEmpty(password))
            return new LogInSignUpResult { Success = false, Message = "Password is not provided"};

        if (Sha256PasswordHasher.HashPassword(password) == user.Password)
            return new LogInSignUpResult { Success = true, Message = "Success!", User = user};
        
        
        return new LogInSignUpResult { Success = false, Message = "Password is wrong"};
    }
    
    public LogInSignUpResult Register(string? login, string? password)
    {
        if (string.IsNullOrEmpty(login))
            return new LogInSignUpResult { Success = false, Message = "Login is not provided"};
        
        if (login.Length < 8)
        {
            return new LogInSignUpResult { Success = false, Message = "login is too short!"};
        }
        
        if (login.Length > 100)
        {
            return new LogInSignUpResult { Success = false, Message = "login is too long!"};
        }
        
        var user = _userManager.GetUserByLogin(login);
        if (user != null)
            return new LogInSignUpResult { Success = false, Message = "User already exists "};

        if (string.IsNullOrEmpty(password))
            return new LogInSignUpResult { Success = false, Message = "Password is not provided"};

        var passwordResult = PasswordChecker.CheckPasswordStrength(password);
        if (passwordResult.IsStrong)
        {
            _userManager.Add(new User()
            {
                Password = Sha256PasswordHasher.HashPassword(password),
                Username = login
            });
            return new LogInSignUpResult { Success = true, Message = "Success!" };
        }

        return new LogInSignUpResult { Success = false, Message = passwordResult.Message};
    }
}

