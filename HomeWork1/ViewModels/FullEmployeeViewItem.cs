using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWork1.ViewModels
{
    public sealed class FullEmployeeViewItem : EmployeeViewItem
    {
        private string _gender;
        private string _country;
        private string _city;
        private string _streetName;
        private string _streetAddress;
        private string _phone;
        private string _keySkill;
        private string _ccNumber;
        private string _username;
        private string _status;
        private string _plan;
        private string _paymentMethod;
        private string _term;        

        public string Gender
        {
            get => _gender;
            set => SetAndNotifieIfChanged(ref _gender, value);
        }

        public string Country
        {
            get => _country;
            set => SetAndNotifieIfChanged(ref _country, value);
        }

        public string City
        {
            get => _city;
            set => SetAndNotifieIfChanged(ref _city, value);
        }

        public string StreetName
        {
            get => _streetName;
            set => SetAndNotifieIfChanged(ref _streetName, value);
        }

        public string StreetAddress
        {
            get => _streetAddress;
            set => SetAndNotifieIfChanged(ref _streetAddress, value);
        }

        public string PhoneNumer
        {
            get => _phone;
            set => SetAndNotifieIfChanged(ref _phone, value);
        }

        public string KeySkill
        {
            get => _keySkill;
            set => SetAndNotifieIfChanged(ref _keySkill, value);
        }

        public string CcNumber
        {
            get => _ccNumber;
            set => SetAndNotifieIfChanged(ref _ccNumber, value);
        }

        public string Username
        {
            get => _username;
            set => SetAndNotifieIfChanged(ref _username, value);
        }

        public string Status
        {
            get => _status;
            set => SetAndNotifieIfChanged(ref _status, value);
        }

        public string Plan
        {
            get => _plan;
            set => SetAndNotifieIfChanged(ref _plan, value);
        }

        public string PaymentMethod
        {
            get => _paymentMethod;
            set => SetAndNotifieIfChanged(ref _paymentMethod, value);
        }

        public string Term
        {
            get => _term;
            set => SetAndNotifieIfChanged(ref _term, value);
        }
    }
}
