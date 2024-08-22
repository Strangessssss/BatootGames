using BatootGames.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;

namespace BatootGames.ViewModels;

public partial class UserLoginningViewModel: ObservableObject, IViewModel
{
    private readonly LoginService _loginService;

    public UserLoginningViewModel()
    {
        _loginService = new LoginService();
    }
    
    [ObservableProperty] private string? _loginField;
    [ObservableProperty] private string? _passwordField;
    
    [ObservableProperty] private string? _errorField;
    [ObservableProperty] private string? _errorFieldColor;

    [RelayCommand]
    private void Login()
    {
        var result = _loginService.Login(LoginField, PasswordField);
        ErrorFieldColor = result.Success ? "Green": "Red";
        ErrorField = result.Message;

        if (result.User != null)
            WeakReferenceMessenger.Default.Send(result.User);
    }
    
    [RelayCommand]
    private void SignUp()
    {
        var result = _loginService.Register(LoginField, PasswordField);
        ErrorFieldColor = (result.Success ? "Green": "Red");
        ErrorField = result.Message;
        
    }
}