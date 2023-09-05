using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

namespace HomeWork1.AppCommon.MvvmComands
{
    public sealed class CancelAsyncCommand : RelayComandAsyncBase
    {
        private CancellationTokenSource _cts = new CancellationTokenSource();
        private bool _commandExecuting;

        public event EventHandler? CanExecuteChanged;
        public CancellationToken Token { get { return _cts.Token; } }
        public void NotifyCommandStarting()
        {
            _commandExecuting = true;
            if (!_cts.IsCancellationRequested)
                return;
            _cts = new CancellationTokenSource();
            RaiseCanExecuteChanged();
        }

        public void NotifyCommandFinished()
        {
            _commandExecuting = false;
            RaiseCanExecuteChanged();
        }

        public override bool CanExecute(object? parameter)
        {
            return _commandExecuting && _cts.IsCancellationRequested == false;
        }

        protected void RaiseCanExecuteChanged()
        {
            CommandManager.InvalidateRequerySuggested();
        }

        public override Task ExecuteAsync(object parameter)
        {
            _cts.Cancel();
            RaiseCanExecuteChanged();
            return Task.CompletedTask;
        }
    }
}
