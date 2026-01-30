using System.Windows;
using System.Windows.Controls;
using Vertex.App.ViewModels;
using Vertex.Core.Models;

namespace Vertex.App.Views;

public partial class PlantillaWindow : Window
{
    private PlantillaWindowViewModel ViewModel => (PlantillaWindowViewModel)DataContext;

    public PlantillaWindow(LoginData loginData)
    {
        InitializeComponent();
        DataContext = new PlantillaWindowViewModel(loginData.Menu);

        this.Title = $"Bienvenido, {loginData.User.Name}";
    }

    private void TreeView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
    {
        if (e.NewValue is Vertex.Core.Models.MenuItem item)
            ViewModel.Navigate(item);
    }

}
