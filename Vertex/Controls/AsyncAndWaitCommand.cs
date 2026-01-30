using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Vertex.App.ViewModels
{
    /// <summary>
    /// Comando que ejecuta un método async y espera su finalización.
    /// Ideal para usar en WPF con botones o controles de UI.
    /// </summary>
    public class AsyncAndWaitCommand : ICommand
    {
        private readonly Func<object?, Task> _execute;
        private readonly Func<object?, bool>? _canExecute;

        public AsyncAndWaitCommand(Func<object?, Task> execute, Func<object?, bool>? canExecute = null)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute;
        }

        public event EventHandler? CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }

        public bool CanExecute(object? parameter) => _canExecute?.Invoke(parameter) ?? true;

        public async void Execute(object? parameter)
        {
            try
            {
                await _execute(parameter);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error ejecutando comando: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }

}
