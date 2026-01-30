using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Vertex.App.ViewModels;

namespace Vertex.App.Views;

public partial class LoginView : Window
{

    public LoginView(LoginViewModel loginViewModel)
    {
        InitializeComponent();
        DataContext = loginViewModel;
    }


    private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
    {
        if (DataContext is LoginViewModel viewModel && sender is PasswordBox passwordBox)
        {
            viewModel.Password = passwordBox.Password;
            // Forzar reevaluación del comando
            CommandManager.InvalidateRequerySuggested();
        }
    }

}
