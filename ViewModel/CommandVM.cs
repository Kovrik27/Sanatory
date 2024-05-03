using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Sanatory.ViewModel
{
    public class CommandVM : ICommand
    {
        Action action;

        public CommandVM(Action action)
        {
            this.action = action;
        }

        public event EventHandler? CanExecuteChanged;

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object? parameter)
        {
            action();
        }
    }

    public class CommandVM<T> : ICommand
    {
        Action<T> action;

        public CommandVM(Action<T> action)
        {
            this.action = action;
        }

        public event EventHandler? CanExecuteChanged;

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object? parameter)
        {
            action((T)parameter);
        }
    }
}
