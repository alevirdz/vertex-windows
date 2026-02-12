using System;
using System.Linq;
using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using Vertex.Core.Interfaces;

namespace Vertex.App.Services
{
    public class NavigationService : INavigationService
    {
        private readonly IServiceProvider _serviceProvider;

        public NavigationService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public void NavigateTo<TViewModel>(object? parameter = null)
        {
            var viewModel = _serviceProvider.GetRequiredService<TViewModel>();

            var window = Application.Current.Windows
                .OfType<Window>()
                .FirstOrDefault(w => w.DataContext?.GetType() == typeof(TViewModel));

            window?.Show();
        }

        public void Close(object viewModel)
        {
            var window = Application.Current.Windows
                .OfType<Window>()
                .FirstOrDefault(w => w.DataContext == viewModel);

            window?.Close();
        }
    }
}
