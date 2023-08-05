using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using TransferObjects.Employee;

namespace HomeWork1.EmployeeDirectory
{
    /// <summary>
    /// Логика взаимодействия для EmployeeView.xaml
    /// </summary>
    public partial class EmployeeView : Window
    {
        public int? EmployeeId { private get; set; }

        private EmployeeDto? _currentEmployee;

        public EmployeeView()
        {
            InitializeComponent();            
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (EmployeeId.HasValue)
            {
                _currentEmployee = Employees.GetEmployeeById(EmployeeId.Value);

                if (_currentEmployee == null)
                {
                    MessageBox.Show("Співробітник не знайден", "Попередження", MessageBoxButton.OK, MessageBoxImage.Warning);

                    return;
                }

                LastNameTextBlock.Text = _currentEmployee.LastName;
                NameTextBlock.Text = _currentEmployee.Name;
                EmailTextBlock.Text = _currentEmployee.Email;
                GenderTextBlock.Text = _currentEmployee.Gender;

                CountryTextBlock.Text = _currentEmployee.Address.Country;
                CityTextBlock.Text = _currentEmployee.Address.City;
                StreetNameTextBlock.Text = _currentEmployee.Address.StreetName;
                StreetAddressTextBlock.Text = _currentEmployee.Address.StreetAddress;
                PhoneNumerTextBlock.Text = _currentEmployee.PhoneNumer;

                TitleTextBlock.Text = _currentEmployee.Employment.Title;
                KeySkillTextBlock.Text = _currentEmployee.Employment.KeySkill;

                CcNumberTextBlock.Text = _currentEmployee.CreditCard.CcNumber;

                UsernameTextBlock.Text = _currentEmployee.Username;
                StatusTextBlock.Text = _currentEmployee.Subscription.Status;
                PlanTextBlock.Text = _currentEmployee.Subscription.Plan;
                PaymentMethodTextBlock.Text = _currentEmployee.Subscription.PaymentMethod;
                TermTextBlock.Text = _currentEmployee.Subscription.Term;

                Foto.Source = new BitmapImage(new Uri(_currentEmployee.Avatar));
            }
        }

        private void ButtonClickClose(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
