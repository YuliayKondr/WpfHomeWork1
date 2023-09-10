using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWork1.AppCommon
{
    public interface IActivable
    {
        Task ActivateAsync(object parameter);
    }
}
