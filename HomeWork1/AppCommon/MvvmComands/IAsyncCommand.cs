using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks;
using System.Windows.Input;

namespace HomeWork1.AppCommon.MvvmComands
{
    public interface IAsyncCommand : ICommand
    {
        Task ExecuteAsync(object? parameter);

        bool IsExecuting { get; }
    }
}
