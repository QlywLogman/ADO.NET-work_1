using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ADO.NET_work_1.Command
{
    public class RelayCommand : ICommand
    {
        public event EventHandler? CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        private readonly Action<object?> _execute;
        private readonly Predicate<object?> _canExecute;
        private Action addAuthor;

        public RelayCommand(Action<object?> execute, Predicate<object?>? canExecute = null)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute;
        }

        public RelayCommand(Action addAuthor)
        {
            this.addAuthor = addAuthor;
        }

        public bool CanExecute(object? parameter) => _canExecute is null || _canExecute.Invoke(parameter);


        public void Execute(object? parameter) => _execute.Invoke(parameter);
    }
}
