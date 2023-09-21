using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace HomeWork1.AppCommon.MvvmComands
{
    public abstract class RelayComandAsyncBase : BaseNotifyPropertyChanged, IAsyncCommand
    {
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public abstract bool CanExecute(object? parameter);

        public abstract bool IsExecuting { get; protected set; }

        public abstract Task ExecuteAsync(object parameter);

        public async void Execute(object? parameter)
        {
            await ExecuteAsync(parameter);
        }

        protected void RaiseCanExecuteChanged()
        {
            CommandManager.InvalidateRequerySuggested();
        }
    }
}
