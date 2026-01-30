using System;
using System.Windows;
using Microsoft.Extensions.Configuration;
using Vertex.App.ViewModels;
using Vertex.App.Views;
using Vertex.Services.ApiService;

namespace Vertex.App;

public partial class App : Application
{
    public static IConfiguration AppConfiguration { get; private set; } = null!;

    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

        try
        {
            AppConfiguration = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            string apiBaseUrl = AppConfiguration["ApiConfiguration:ApiBaseUrl"] ?? throw new InvalidOperationException("API base URL is not defined in configuration.");
            string timeoutString = AppConfiguration["ApiConfiguration:RequestTimeoutInSeconds"] ?? throw new InvalidOperationException("API request timeout is not defined in configuration.");

            if (!int.TryParse(timeoutString, out int requestTimeoutInSeconds))
                throw new FormatException("API request timeout is not a valid integer");

            // Configurar HttpClientProvider
            HttpClientProvider.Configure(apiBaseUrl, requestTimeoutInSeconds);

            var loginService = new LoginService();
            var loginViewModel = new LoginViewModel(loginService);
            var loginView = new LoginView(loginViewModel);

            this.MainWindow = loginView;
            loginView.Show();

        }
        catch (Exception ex)
        {
            MessageBox.Show(
                $"Error initializing application configuration: {ex.Message}",
                "Configuration Error",
                MessageBoxButton.OK,
                MessageBoxImage.Error
            );

            // Detener la aplicación si no se puede configurar
            Current.Shutdown();
        }
    }
}
