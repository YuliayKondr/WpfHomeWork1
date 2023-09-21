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
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Windows;
using TransferObjects.Employee;
using HomeWork1.AppCommon;
using HomeWork1.AppCommon.MvvmComands;
using System.Collections.ObjectModel;
using HomeWork1.Views;

namespace HomeWork1.ViewModels
{
    public sealed class MainWindowViewModel : BaseNotifyPropertyChanged, IActivable
    {
        private readonly IEmployeeClient _employeeClient;
        private readonly IRepository<EmployeeEntity> _userEntityRepository;
        private readonly IRepository<AddressEntity> _addressRepository;
        private readonly IRepository<SubscriptionEntity> _subscriptionRepository;
        private readonly IUnitOfWork _unitOfWork1;
        private readonly IMapper _mapper;
        private readonly INavigationService _navigationService;

        private string _countInput;
        private ObservableCollection<EmployeeViewItem> _employeeViewItems;
        private EmployeeViewItem _selectedEmployeeItem;       

        public MainWindowViewModel(
            IEmployeeClient employeeClient,
            IUnitOfWork unitOfWork,
            IRepository<EmployeeEntity> userRepository,
            IRepository<AddressEntity> addressRepository,
            IRepository<SubscriptionEntity> subscriptionRepository,
            IMapper mapper,
            INavigationService navigationService)

        {
            _employeeClient = employeeClient;
            _unitOfWork1 = unitOfWork;
            _userEntityRepository = userRepository;
            _addressRepository = addressRepository;
            _subscriptionRepository = subscriptionRepository;
            _mapper = mapper;
            _navigationService = navigationService;

            LoadEmployeesCommand = new AsyncCommand(LoadEmployeesAsync);
            LoadFromApiCommand = new AsyncCommand(LoadFromApi);
            ShowInformCommand = new AsyncCommand(ShowInform);
            CreateEmployeeCommand = new AsyncCommand(CreateEmployee);
        }


        public IAsyncCommand LoadEmployeesCommand { get; private set; }

        public IAsyncCommand LoadFromApiCommand { get; private set; }

        public IAsyncCommand ShowInformCommand { get; private set; }

        public IAsyncCommand CreateEmployeeCommand { get; private set; }

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

        public string CountInput
        {
            get => _countInput;
            set => SetAndNotifieIfChanged(ref _countInput, value);
        }

        private async Task LoadEmployeesAsync(CancellationToken cancellationToken)
        {
            await Task.Delay(2000);
            EmployeeViewItems = await GetEmployeesAsync(cancellationToken);

            if(EmployeeViewItems.Any())
            {
                SelectedEmployeeItem = _employeeViewItems.First();
            }            
        }         

        private async Task LoadFromApi(CancellationToken cancellationToken)
        {
            if (!int.TryParse(CountInput, out int countEmployees) || countEmployees < 1 || countEmployees > 10)
            {
                MessageBox.Show("Введіть число від 1 до 10", "Кількість співробітників", MessageBoxButton.OK, MessageBoxImage.Warning);

                CountInput = string.Empty;

                return;
            }

            try
            {

                if (countEmployees == 1)
                {
                    EmployeeDto employee = await _employeeClient.GetEmployeeAsync();

                    EmployeeEntity userEntity = _mapper.Map<EmployeeEntity>(employee);

                    _userEntityRepository.Insert(userEntity);

                    await _unitOfWork1.SaveChangesAsync(cancellationToken);

                }
                else
                {
                    IReadOnlyCollection<EmployeeDto> employeesFromServer = await _employeeClient.GetEmployeesByCountAsync(countEmployees);

                    EmployeeEntity[] employeeEntities = employeesFromServer.Select(x => _mapper.Map<EmployeeEntity>(x)).ToArray();

                    Array.ForEach(employeeEntities, x => _userEntityRepository.Insert(x));

                    await _unitOfWork1.SaveChangesAsync(cancellationToken);
                }                

                EmployeeViewItems = await GetEmployeesAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async Task<ObservableCollection<EmployeeViewItem>> GetEmployeesAsync(CancellationToken cancellationToken)
        {
            IReadOnlyCollection<EmployeeEntity> employees = await _userEntityRepository.Queryable().ToArrayAsync(cancellationToken);

            return new ObservableCollection<EmployeeViewItem>(employees.Select(x => _mapper.Map<EmployeeViewItem>(x)));
        }

        private async Task ShowInform(CancellationToken cancellationToken)
        {

            if (SelectedEmployeeItem != null)
            {
                await _navigationService.ShowAsync(nameof(EmployeeView), new ShowEmployeeInfoParameter(SelectedEmployeeItem.Id));

                SelectedEmployeeItem = null;
            }
            else
            {
                MessageBox.Show("Не вибраний об'єкт", "Помилка", MessageBoxButton.OK, MessageBoxImage.Warning);
            }           
        }

        private async Task CreateEmployee(CancellationToken cancellationToken)
        {

            await _navigationService.ShowDialogAsync(nameof(EmployeeCreateView), null);

            await LoadEmployeesAsync(cancellationToken);
        }

        public Task ActivateAsync(object parameter)
        {
            return Task.CompletedTask;
            
        }
    }
}
