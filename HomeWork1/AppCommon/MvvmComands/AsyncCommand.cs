using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace HomeWork1.AppCommon.MvvmComands
{
    public class AsyncCommand : RelayComandAsyncBase
    {
        private readonly Func<CancellationToken, Task> _command;
        private readonly CancelAsyncCommand _cancelCommand;
        private NotifyTaskCompletion _execution;

        public AsyncCommand(Func<CancellationToken, Task> command)
        {
            _command = command;
            _cancelCommand = new CancelAsyncCommand();
        }      

        public NotifyTaskCompletion Execution
        {
            get => _execution;
            private set => SetAndNotifieIfChanged(ref _execution, value);
        }

        public ICommand CancelCommand => _cancelCommand;

        public override async Task ExecuteAsync(object parameter)
        {
            _cancelCommand.NotifyCommandStarting();
            Execution = new NotifyTaskCompletion(_command(_cancelCommand.Token));

            RaiseCanExecuteChanged();
            await Execution.TaskCompletion;
            _cancelCommand.NotifyCommandFinished();
            RaiseCanExecuteChanged();
        }

        public override bool CanExecute(object? parameter)
        {
            return true;
        }

    }
}
