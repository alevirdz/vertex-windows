using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using Vertex.App.Views;
using Vertex.Core.Interfaces;
using Vertex.Core.Models;


namespace Vertex.App.ViewModels;

public class LoginViewModel : INotifyPropertyChanged
{
    private readonly ILoginService _loginService;

    public LoginViewModel(ILoginService loginService)
    {
        _loginService = loginService;
        LoginCommand = new AsyncAndWaitCommand(
            async _ => await ExecuteLoginAsync(),
            _ => !string.IsNullOrWhiteSpace(Email) && !string.IsNullOrWhiteSpace(Password)
        );
    }

    private string _email = string.Empty;
    public string Email
    {
        get => _email;
        set { _email = value; OnPropertyChanged(); }
    }

    private string _password = string.Empty;
    public string Password
    {
        get => _password;
        set { _password = value; OnPropertyChanged(); }
    }

    public ICommand LoginCommand { get; }

    private async Task ExecuteLoginAsync(object? parameter = null)
    {
        try
        {
            LoginResponse result = await _loginService.LoginAsync(Email, Password);
            if (result.Success && result.Data != null)
            {
                var plantilla = new PlantillaWindow(result.Data);
                plantilla.Show();

                //Cerramos ventana (Context) de login
                Application.Current.Windows.OfType<Window>().FirstOrDefault(w => w.DataContext == this)?.Close();
            }
            else
            {
                MessageBox.Show($"Login failed: {result.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        catch (System.Exception ex)
        {
            MessageBox.Show($"Error during login: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;
    private void OnPropertyChanged([CallerMemberName] string propertyName = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        // Esto fuerza a WPF a reevaluar CanExecute
        CommandManager.InvalidateRequerySuggested();
    }

}
