using System.Windows;

namespace Vertex.App.Views
{
    public partial class NotificationWindow : Window
    {
        public NotificationWindow(string message, string title)
        {
            InitializeComponent();
            Title = title;
            MessageText.Text = message;
        }

        private void Ok_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }
    }
}
