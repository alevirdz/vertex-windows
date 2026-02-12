using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using Vertex.App.Services;
using Vertex.Core.Interfaces;
using Vertex.Core.Models;
using Vertex.Services;

namespace Vertex.App.ViewModels
{
    public class PlantillaWindowViewModel : INotifyPropertyChanged
    {
        private readonly ILoginService _loginService;
        private readonly INotificationService _notificationService;

        public ObservableCollection<MenuItem> MenuItems { get; }

        private object? _currentView;
        public object? CurrentView
        {
            get => _currentView;
            set { _currentView = value; OnPropertyChanged(); }
        }

        private bool _isLoggingOut;
        public bool IsLoggingOut
        {
            get => _isLoggingOut;
            set { _isLoggingOut = value; OnPropertyChanged(); }
        }

        public ICommand LogoutCommand { get; }

        public PlantillaWindowViewModel(Menu menu, ILoginService loginService, INotificationService notificationService)
        {
            MenuItems = new ObservableCollection<MenuItem>(menu.Items);
            _loginService = loginService;
            _notificationService = notificationService;

            LogoutCommand = new AsyncAndWaitCommand(_ => ExecuteLogout());
        }

        public void Navigate(MenuItem item)
        {
            // Decide qué UserControl mostrar según item.Route
            switch (item.Route)
            {
                case "dashboard":
                    CurrentView = new Views.DashboardView();
                    break;
                //case "rooms":
                //    CurrentView = new Views.RoomsView();
                //    break;
                //default:
                //    CurrentView = new Views.DefaultView();
                //    break;
            }
        }

        private async Task ExecuteLogout()
        {
            if (IsLoggingOut) return;

            try
            {
                IsLoggingOut = true;
                await _loginService.LogoutAsync();
            }
            catch (Exception ex)
            {
                await _notificationService.ShowErrorAsync("No se pudo cerrar sesión: " + ex.Message);
            }
            finally
            {
                IsLoggingOut = false;
                Application.Current.Windows.OfType<Window>().FirstOrDefault(w => w.DataContext == this)?.Close();

                var login = new Views.LoginView(new LoginViewModel(_loginService, _notificationService));
                login.Show();
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propertyName = "") =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
