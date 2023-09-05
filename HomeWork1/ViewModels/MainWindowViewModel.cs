using AutoMapper;
using DatabaseModel.Models;
using DatabaseModel.Repositories;
using DatabaseModel;
using HomeWork1.Clients;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HomeWork1.EmployeeDirectory;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Windows;
using TransferObjects.Employee;
using System.ComponentModel;
using HomeWork1.AppCommon;
using HomeWork1.AppCommon.MvvmComands;
using System.Collections.ObjectModel;

namespace HomeWork1.ViewModels
{
    public class MainWindowViewModel : BaseNotifyPropertyChanged
    {     

        private ObservableCollection<EmployeeViewItem> _employeeViewItems;
        private EmployeeViewItem _selectedEmployeeItem;

        private readonly IEmployeeClient _employeeClient;
        private readonly IRepository<EmployeeEntity> _userEntityRepository;
        private readonly IRepository<AddressEntity> _addressRepository;
        private readonly IRepository<SubscriptionEntity> _subscriptionRepository;
        private readonly IUnitOfWork _unitOfWork1;
        private readonly IMapper _mapper;

        public MainWindowViewModel(
            IEmployeeClient employeeClient,
            IUnitOfWork unitOfWork,
            IRepository<EmployeeEntity> userRepository,
            IRepository<AddressEntity> addressRepository,
            IRepository<SubscriptionEntity> subscriptionRepository,
            IMapper mapper)

        {
            _employeeClient = employeeClient;
            _unitOfWork1 = unitOfWork;
            _userEntityRepository = userRepository;
            _addressRepository = addressRepository;
            _subscriptionRepository = subscriptionRepository;
            _mapper = mapper;

            LoadEmployeesCommand = new AsyncCommand(LoadEmployeesAsync);

        }

        public IAsyncCommand LoadEmployeesCommand { get; private set; }

        public ObservableCollection<EmployeeViewItem> EmployeeViewItems
        {
            get => _employeeViewItems;
            set => SetAndNotifieIfChanged(ref _employeeViewItems, value);
        }

        public EmployeeViewItem SelectedEmployeeItem
        {
            get => _selectedEmployeeItem;
            set => SetAndNotifieIfChanged(ref _selectedEmployeeItem, value);
        }

        private async Task LoadEmployeesAsync(CancellationToken cancellationToken)
        {
            IReadOnlyCollection<EmployeeEntity> employees = await _userEntityRepository.Queryable().ToArrayAsync(cancellationToken);

            _employeeViewItems = new ObservableCollection<EmployeeViewItem>(employees.Select(x => _mapper.Map<EmployeeViewItem>(x)));

            _selectedEmployeeItem = _employeeViewItems?.First();
        }

        //todo переробка на viewmodel
        /*private async void ShowInfoEnployee(object sender, RoutedEventArgs e)
        {
            if (ListEmployees.SelectedItem != null && ListEmployees.SelectedItem is EmployeeEntity)
            {
                EmployeeEntity selectedEmployee = (EmployeeEntity)ListEmployees.SelectedItem;

                EmployeeView employeeView = new EmployeeView(_userEntityRepository, _addressRepository, _subscriptionRepository);

                employeeView.EmployeeId = selectedEmployee.Id;

                employeeView.ShowDialog();

                ListEmployees.SelectedItem = null;
            }
            else
            {
                MessageBox.Show("Не вибраний об'єкт", "Помилка", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }      

        private async void Load_Click(object sender, RoutedEventArgs e)
        {
            if (!int.TryParse(CountInput.Text, out int countEmployees) || countEmployees < 1 || countEmployees > 10)
            {
                MessageBox.Show("Введіть число від 1 до 10", "Кількість співробітників", MessageBoxButton.OK, MessageBoxImage.Warning);

                CountInput.Text = string.Empty;

                return;
            }

            try
            {

                if (countEmployees == 1)
                {
                    EmployeeDto employee = await _employeeClient.GetEmployeeAsync();

                    EmployeeEntity userEntity = _mapper.Map<EmployeeEntity>(employee);

                    _userEntityRepository.Insert(userEntity);

                    await _unitOfWork1.SaveChangesAsync(CancellationToken.None);

                }
                else
                {
                    IReadOnlyCollection<EmployeeDto> employeesFromServer = await _employeeClient.GetEmployeesByCountAsync(countEmployees);

                    EmployeeEntity[] employeeEntities = employeesFromServer.Select(x => _mapper.Map<EmployeeEntity>(x)).ToArray();

                    Array.ForEach(employeeEntities, x => _userEntityRepository.Insert(x));

                    await _unitOfWork1.SaveChangesAsync(CancellationToken.None);
                }

                ListEmployees.ItemsSource = await GetEmployeesAsync();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }*/

    }
}
