namespace Vertex.Core.Interfaces
{
    public interface INavigationService
    {
        void NavigateTo<TViewModel>(object? parameter = null);
        void Close(object viewModel);
    }
}
