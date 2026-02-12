using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Vertex.App.ViewModels;
using Vertex.App.Views;
using Vertex.Core.Interfaces;
using Vertex.Services;
using Vertex.Services.ApiService;

namespace Vertex.App
{
    public static class Bootstrapper
    {
        public static IServiceProvider ConfigureServices(IConfiguration configuration)
        {
            var services = new ServiceCollection();

            // Configuración HttpClientProvider
            var apiBaseUrl = configuration["ApiConfiguration:ApiBaseUrl"]!;
            var timeoutSeconds = int.Parse(configuration["ApiConfiguration:RequestTimeoutInSeconds"]!);
            HttpClientProvider.Configure(apiBaseUrl, timeoutSeconds);

            //Servicios singleton
            services.AddSingleton<ISessionService, SessionService>();
            services.AddSingleton<ILoginService, LoginService>();

            //ViewModels
            services.AddTransient<LoginViewModel>();
            // Agrega otros ViewModels aquí
            // services.AddTransient<HomeViewModel>();

            //Views
            services.AddTransient<LoginView>();
            // Agrega otras Views aquí
            // services.AddTransient<HomeView>();

            return services.BuildServiceProvider();
        }
    }
}
