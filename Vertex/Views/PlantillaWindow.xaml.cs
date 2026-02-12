using System.Windows;
using System.Windows.Controls;
using Vertex.App.Services;
using Vertex.App.ViewModels;
using Vertex.Core.Interfaces;
using Vertex.Core.Models;
using Vertex.Services;

namespace Vertex.App.Views;

public partial class PlantillaWindow : Window
{
    private PlantillaWindowViewModel ViewModel => (PlantillaWindowViewModel)DataContext;

    public PlantillaWindow(LoginData loginData, ILoginService loginService, INotificationService notificationService)
    {
        InitializeComponent();
        DataContext = new PlantillaWindowViewModel(loginData, loginService, notificationService);

        this.Title = $"Bienvenido, {loginData.User.Name}";
    }

    private void TreeView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
    {
        if (e.NewValue is Vertex.Core.Models.MenuItem item)
            ViewModel.Navigate(item);
    }

}
