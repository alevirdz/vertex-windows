using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Vertex.Core.Models;

namespace Vertex.App.ViewModels
{
    public class PlantillaWindowViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<MenuItem> MenuItems { get; }

        private object? _currentView;
        public object? CurrentView
        {
            get => _currentView;
            set { _currentView = value; OnPropertyChanged(); }
        }

        public PlantillaWindowViewModel(Menu menu)
        {
            MenuItems = new ObservableCollection<MenuItem>(menu.Items);
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

        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propertyName = "") =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
