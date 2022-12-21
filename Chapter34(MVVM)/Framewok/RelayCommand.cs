using System;
using System.Windows.Input;

namespace Framewok
{
    public class RelayCommand : ICommand
    {
        public RelayCommand(Action execute, Func<bool>? canExecute = null)
        {
            _canExecute = canExecute;
            _execute = execute ?? throw Exceptions.Null(nameof(execute));
        }

        public event EventHandler? CanExecuteChanged;

        public void RaiseCanExecuteChanged() =>
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);

        public bool CanExecute(object? parameter) =>
            _canExecute?.Invoke() ?? true;

        public void Execute(object? parameter) => _execute();
 
        private Action _execute;
        private Func<bool>? _canExecute;
    }
}
