using Vertex.Core.Interfaces;
using Vertex.App.Views;

namespace Vertex.App.Services
{
    public class NotificationService : INotificationService
    {
        public Task ShowErrorAsync(string message)
        {
            var window = new NotificationWindow(message, "Error");
            window.ShowDialog();
            return Task.CompletedTask;
        }

        public Task ShowWarningAsync(string message)
        {
            var window = new NotificationWindow(message, "Warning");
            window.ShowDialog();
            return Task.CompletedTask;
        }

        public Task ShowInfoAsync(string message)
        {
            var window = new NotificationWindow(message, "Information");
            window.ShowDialog();
            return Task.CompletedTask;
        }

        public Task<bool> ShowConfirmationAsync(string message)
        {
            var window = new NotificationWindow(message, "Confirmation");
            return Task.FromResult(window.ShowDialog() == true);
        }
    }
}
