using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace HomeWork1.AppCommon
{
    public abstract class BaseNotifyPropertyChanged : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        public bool IsHavePropertyChangeListner => PropertyChanged != null;

        protected virtual void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        protected void SetAndNotifieIfChanged<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
        {
            if ((field?.Equals(value) ?? false) == false)
            {
                field = value;
                OnPropertyChanged(propertyName);
            }
        }
    }
}
