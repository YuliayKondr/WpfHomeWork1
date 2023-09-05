using HomeWork1.AppCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWork1.ViewModels
{
    public class EmployeeViewItem : BaseNotifyPropertyChanged
    {
        private string _avatar;
        private string _name;
        private string _email;
        private string _lastName;

        public string Title { get; set; }

        public string Avatar 
        {
            get => _avatar;
            set => SetAndNotifieIfChanged(ref _avatar, value);
        }

        public string Name 
        {
            get => _name;
            set => SetAndNotifieIfChanged(ref _name, value);
        }

        public string LastName 
        { 
            get => LastName;
            set => SetAndNotifieIfChanged(ref _lastName, value); 
        }

        public  string Email         
        {
            get => _email;
            set => SetAndNotifieIfChanged(ref _email, value);
        }

    }
}
