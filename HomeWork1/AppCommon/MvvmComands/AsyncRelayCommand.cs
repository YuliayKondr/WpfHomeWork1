using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

namespace HomeWork1.AppCommon.MvvmComands
{
    public class AsyncRelayCommand<TResult> : RelayComandAsyncBase
    {
        private readonly Func<CancellationToken, Task<TResult>> _command;
        private readonly CancelAsyncCommand _cancelCommand;
        private NotifyTaskRelayCompletion<TResult> _execution;

        public NotifyTaskRelayCompletion<TResult> Execution
        {
            get => _execution;
            private set => SetAndNotifieIfChanged(ref _execution, value);
        }
        public ICommand CancelCommand => _cancelCommand;

        public AsyncRelayCommand(Func<CancellationToken, Task<TResult>> command)
        {
            _command = command;
            _cancelCommand = new CancelAsyncCommand();
        }
        public override async Task ExecuteAsync(object parameter)
        {
            _cancelCommand.NotifyCommandStarting();
            Execution = new NotifyTaskRelayCompletion<TResult>(_command(_cancelCommand.Token));

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
