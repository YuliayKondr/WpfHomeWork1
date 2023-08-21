using System;
using System.Collections.Generic;
using System.Windows;
using TransferObjects.Employee;
using HomeWork1.EmployeeDirectory;
using HomeWork1.Clients;
using DatabaseModel;
using DatabaseModel.Repositories;
using DatabaseModel.Models;
using AutoMapper;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace HomeWork1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly IEmployeeClient _employeeClient;
        private readonly IRepository<EmployeeEntity> _userEntityRepository;
        private readonly IRepository<AddressEntity> _addressRepository;
        private readonly IRepository<SubscriptionEntity> _subscriptionRepository;
        private readonly IUnitOfWork _unitOfWork1;
        private readonly IMapper _mapper;

        public MainWindow(
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

            InitializeComponent();            
        }

        private async void ShowInfoEnployee(object sender, RoutedEventArgs e)
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

        private async Task<IReadOnlyCollection<EmployeeEntity>> GetEmployeesAsync()
        {
            return await _userEntityRepository.Queryable().ToArrayAsync();
        }

        private async void Load_Click(object sender, RoutedEventArgs e)
        {    
            if(!int.TryParse(CountInput.Text, out int countEmployees) || countEmployees < 1 || countEmployees > 10)
            {
                MessageBox.Show("Введіть число від 1 до 10", "Кількість співробітників", MessageBoxButton.OK, MessageBoxImage.Warning);

                CountInput.Text = string.Empty;

                return;
            }

            try
            {                

                if(countEmployees == 1)
                {
                    EmployeeDto employee = await _employeeClient.GetEmployeeAsync();                   

                    EmployeeEntity userEntity = _mapper.Map<EmployeeEntity> (employee);

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
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ListEmployees.ItemsSource = await GetEmployeesAsync();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
