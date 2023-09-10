using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWork1.AppCommon
{
    public interface INavigationService
    {
        Task ShowAsync(string windowKey, object parameter = null);

        Task<bool?> ShowDialogAsync(string windowKey, object parameter = null);
    }
}
