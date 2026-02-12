using System;
using System.Windows;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Vertex.App.Views;

namespace Vertex.App
{
    public partial class App : Application
    {
        public static IServiceProvider ServiceProvider { get; private set; } = null!;
        public static IConfiguration AppConfiguration { get; private set; } = null!;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            try
            {
                //Cargar configuración
                AppConfiguration = new ConfigurationBuilder()
                    .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                    .Build();

                //Configurar servicios
                ServiceProvider = Bootstrapper.ConfigureServices(AppConfiguration);

                //Mostrar ventana inicial
                var loginView = ServiceProvider.GetRequiredService<LoginView>();
                MainWindow = loginView;
                loginView.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error inicializando la app: {ex.Message}", "Error",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                Current.Shutdown();
            }
        }
    }
}
