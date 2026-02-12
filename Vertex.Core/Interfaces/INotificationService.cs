namespace Vertex.Core.Interfaces
{
    public interface INotificationService
    {
        Task ShowErrorAsync(string message);
        Task ShowWarningAsync(string message);
        Task ShowInfoAsync(string message);
        Task<bool> ShowConfirmationAsync(string message);
    }
}
