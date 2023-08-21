using DatabaseModel.Models;
using DatabaseModel.Repositories;
using Microsoft.EntityFrameworkCore;
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
        private readonly IRepository<EmployeeEntity> _repository;
        private readonly IRepository<AddressEntity> _addressRepository;
        private readonly IRepository<SubscriptionEntity> _subscriptionRepository;

        public int? EmployeeId { private get; set; }

        private EmployeeEntity? _currentEmployee;
        private AddressEntity? _currentAddress;
        private SubscriptionEntity? _currentSubscription;

        public EmployeeView(
            IRepository<EmployeeEntity> repository,
            IRepository<AddressEntity> addressRepository,
            IRepository<SubscriptionEntity> subscriptionRepository)
        {
            _repository = repository;
            _addressRepository = addressRepository;
            _subscriptionRepository = subscriptionRepository;

            InitializeComponent();            
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (EmployeeId.HasValue)
            {                
                await InitializeFromDatabaseAsync(EmployeeId.Value);
                

                if (_currentEmployee == null || _currentSubscription == null || _currentAddress== null)
                {
                    MessageBox.Show("Не всі компаненти знайдені в базі", "Попередження", MessageBoxButton.OK, MessageBoxImage.Warning);

                    return;
                }

                LastNameTextBlock.Text = _currentEmployee.LastName;
                NameTextBlock.Text = _currentEmployee.Name;
                EmailTextBlock.Text = _currentEmployee.Email;
                GenderTextBlock.Text = _currentEmployee.Gender;

                CountryTextBlock.Text = _currentAddress.Country;
                CityTextBlock.Text = _currentAddress.City;
                StreetNameTextBlock.Text = _currentAddress.StreetName;
                StreetAddressTextBlock.Text = _currentAddress.StreetAddress;
                PhoneNumerTextBlock.Text = _currentEmployee.PhoneNumer;

                TitleTextBlock.Text = _currentEmployee.Employment.Title;
                KeySkillTextBlock.Text = _currentEmployee.Employment.KeySkill;

                CcNumberTextBlock.Text = _currentEmployee.CreditCard.CcNumber;

                UsernameTextBlock.Text = _currentEmployee.Username;
                StatusTextBlock.Text = _currentSubscription.Status;
                PlanTextBlock.Text = _currentSubscription.Plan;
                PaymentMethodTextBlock.Text = _currentSubscription.PaymentMethod;
                TermTextBlock.Text = _currentSubscription.Term;

                Foto.Source = new BitmapImage(new Uri(_currentEmployee.Avatar));
            }
        }

        private void ButtonClickClose(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private async Task InitializeFromDatabaseAsync(int employeeId)
        {
            try
            {
                Task<EmployeeEntity> employeeEntityTask = GetEmployeeEntityAsync(employeeId);               

                await employeeEntityTask;

                _currentEmployee = employeeEntityTask.Result;
                _currentAddress = _currentEmployee.Address;
                _currentSubscription = _currentEmployee.Subscription;

            }
            catch (Exception ex)
            {
                //some do
            }
        }

        private Task<EmployeeEntity> GetEmployeeEntityAsync(int employeeId)
        {
            return _repository
                .Queryable()
                .AsNoTracking()
                .Include(x => x.Address)
                .Include(x => x.Subscription)
                .SingleOrDefaultAsync(x => x.Id == employeeId);
        }
    }
}
