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
        private volatile bool _isExecuting;

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

        public override bool IsExecuting
        {
            get { return _isExecuting; }
            protected set { SetAndNotifieIfChanged(ref _isExecuting, value); }

        }

        public ICommand CancelCommand => _cancelCommand;

        public override async Task ExecuteAsync(object parameter)
        {   
            _cancelCommand.NotifyCommandStarting();
            IsExecuting = _cancelCommand.IsExecuting;
            Execution = new NotifyTaskCompletion(_command(_cancelCommand.Token));

            RaiseCanExecuteChanged();
            await Execution.TaskCompletion;
            _cancelCommand.NotifyCommandFinished();
            IsExecuting = _cancelCommand.IsExecuting;
            RaiseCanExecuteChanged();
        }

        public override bool CanExecute(object? parameter)
        {
            return true;
        }

    }
}
