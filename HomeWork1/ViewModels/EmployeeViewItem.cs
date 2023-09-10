using HomeWork1.AppCommon;

namespace HomeWork1.ViewModels
{
    public class EmployeeViewItem : BaseNotifyPropertyChanged
    {
        private int _id;
        private string _avatar;
        private string _name;
        private string _email;
        private string _lastName;
        private string _title;

        public int Id
        {
            get => _id;
            set => SetAndNotifieIfChanged(ref _id, value);
        }

        public string Title 
        {
            get => _title;
            set => SetAndNotifieIfChanged(ref _title, value); 
        }

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
            get => _lastName;
            set => SetAndNotifieIfChanged(ref _lastName, value); 
        }

        public  string Email         
        {
            get => _email;
            set => SetAndNotifieIfChanged(ref _email, value);
        }

    }
}
