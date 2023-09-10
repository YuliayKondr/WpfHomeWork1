using DatabaseModel;
using DatabaseModel.Models;
using DatabaseModel.Repositories;
using HomeWork1.AppCommon;
using HomeWork1.AppCommon.MvvmComands;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HomeWork1.ViewModels
{
    public class EmployeeCreateViewModel : BaseNotifyPropertyChanged, IActivable
    {
        private readonly IRepository<EmployeeEntity> _repository;
        private readonly IRepository<SubscriptionEntity> _subscriptionRepository;
        private readonly IUnitOfWork _unitOfWork1;

        private FullEmployeeViewItem _fullEmployeeViewItem;
        private bool _visible;
        private string _id;
        private IReadOnlyCollection<string> _genders;
        private IReadOnlyCollection<string> _plans;
        private IReadOnlyCollection<string> _terms;
        private IReadOnlyCollection<string> _paymentMethods;

        public EmployeeCreateViewModel(
            IRepository<EmployeeEntity> repository,
            IRepository<SubscriptionEntity> subscriptionRepository,
            IUnitOfWork unitOfWork)
        {
            _subscriptionRepository = subscriptionRepository;
            _unitOfWork1 = unitOfWork;
            _repository = repository;

            Employee = new FullEmployeeViewItem();
            Employee.Status = "Active";

            SaveNewEmployeeCommand = new AsyncCommand(SaveNewEmployeeAsync);
        }

        public IReadOnlyCollection<string> Genders
        {
            get => _genders;
            set => SetAndNotifieIfChanged(ref _genders, value);
        }

        public IReadOnlyCollection<string> Plans
        {
            get => _plans;
            set => SetAndNotifieIfChanged(ref _plans, value);
        }

        public IReadOnlyCollection<string> Terms
        {
            get => _terms;
            set => SetAndNotifieIfChanged(ref _terms, value);
        }

        public IReadOnlyCollection<string> PaymentMethods
        {
            get => _paymentMethods;
            set => SetAndNotifieIfChanged(ref _paymentMethods, value);
        }


        public FullEmployeeViewItem Employee
        {
            get => _fullEmployeeViewItem;
            set => SetAndNotifieIfChanged(ref _fullEmployeeViewItem, value);
        }

        public bool Visible
        {
            get => _visible;
            set => SetAndNotifieIfChanged(ref _visible, value);
            
        }

        public bool VisibleCommand
        {
            get => !Visible;
            set => SetAndNotifieIfChanged(ref _visible, value);

        }

        public string Id
        {
            get => _id;
            set => SetAndNotifieIfChanged(ref _id, value);
        }

        public IAsyncCommand SaveNewEmployeeCommand { get; }

        public async Task ActivateAsync(object parameter)
        { 
            Genders = await GetAllGendersAsync();
            PaymentMethods = await GetPaymentMethodAsync();
            Terms = await GetTermsAsync();
            Plans = await GetPlansAsync();
        }

        private async Task<IReadOnlyCollection<string>> GetAllGendersAsync()
        {
            string[] defaultGender = new[] { "Female", "Male" };            

            try
            {
                string[] employeeGenders = await _repository.Queryable().AsNoTracking()
                    .Where(x => !string.IsNullOrEmpty(x.Gender))
                    .GroupBy(x => x.Gender)
                    .Select(x => x.Key)
                    .ToArrayAsync();
                
                if(employeeGenders.Any())
                {
                    return employeeGenders;
                }

            }
            catch (Exception ex)
            {
                //some do
            }

            return defaultGender;
        }

        private async Task<IReadOnlyCollection<string>> GetPaymentMethodAsync()
        {
            string[] defaultPaymentMethods = new[] { "Cash", "Credit card" };

            try
            {
                string[] paymentMethods = await _subscriptionRepository.Queryable().AsNoTracking()
                    .Where(x => !string.IsNullOrEmpty(x.PaymentMethod))
                    .GroupBy(x => x.PaymentMethod)
                    .Select(x => x.Key)
                    .ToArrayAsync();

                if (paymentMethods.Any())
                {
                    return paymentMethods;
                }

            }
            catch (Exception ex)
            {
                //some do
            }

            return defaultPaymentMethods;
        }

        private async Task<IReadOnlyCollection<string>> GetPlansAsync()
        {
            string[] defaultPlans = new[] { "Standard", "Basic" };

            try
            {
                string[] paymentPlans = await _subscriptionRepository.Queryable().AsNoTracking()
                    .Where(x => !string.IsNullOrEmpty(x.Plan))
                    .GroupBy(x => x.Plan)
                    .Select(x => x.Key)
                    .ToArrayAsync();

                if (paymentPlans.Any())
                {
                    return paymentPlans;
                }

            }
            catch (Exception ex)
            {
                //some do
            }

            return defaultPlans;
        }

        private async Task<IReadOnlyCollection<string>> GetTermsAsync()
        {
            string[] defaultTerms = new[] { "Monthly", "Full subscription" };

            try
            {
                string[] terms = await _subscriptionRepository.Queryable().AsNoTracking()
                    .Where(x => !string.IsNullOrEmpty(x.Term))
                    .GroupBy(x => x.Term)
                    .Select(x => x.Key)
                    .ToArrayAsync();

                if (terms.Any())
                {
                    return terms;
                }

            }
            catch (Exception ex)
            {
                //some do
            }

            return defaultTerms;
        }

        private async Task SaveNewEmployeeAsync(CancellationToken cancellationToken)
        {
            IEnumerable<string> validations = Validation();

            if (validations?.Any() == true)
            {
                //show

                return;
            }

            Employee.Id = int.Parse(Id);

            await SaveAsync(cancellationToken);

            Visible = true;
        }

        private IEnumerable<string> Validation()
        {
            if(string.IsNullOrEmpty(Employee.LastName))
            {
                yield return "Не заповнена фамілія";
            }

            if (string.IsNullOrEmpty(Employee.Name))
            {
                yield return "Не заповнена ім'я";
            }

            if (string.IsNullOrEmpty(Employee.Gender))
            {
                yield return "Не заповнена стать";
            }

            if (string.IsNullOrEmpty(Employee.Email))
            {
                yield return "Не заповнена Email";
            }

            if (string.IsNullOrEmpty(Employee.Country))
            {
                yield return "Не заповнена Країна";
            }

            if (string.IsNullOrEmpty(Employee.City))
            {
                yield return "Не заповнена Місто";
            }

            if (string.IsNullOrEmpty(Employee.StreetName))
            {
                yield return "Не заповнена Назва вулиці";
            }

            if (string.IsNullOrEmpty(Employee.StreetAddress))
            {
                yield return "Не заповнена Адреса";
            }

            if (string.IsNullOrEmpty(Employee.PhoneNumer))
            {
                yield return "Не заповнена Телефон";
            }

            if (string.IsNullOrEmpty(Employee.Title) || string.IsNullOrEmpty(Employee.KeySkill))
            {
                yield return "Не заповнені Відомості про роботу";
            }

            if (string.IsNullOrEmpty(Employee.CcNumber))
            {
                yield return "Не заповнена Банківські картка";
            }

            if (string.IsNullOrEmpty(Employee.Avatar))
            {
                yield return "Відсутня аватарка";
            }

            if (string.IsNullOrEmpty(Employee.Username))
            {
                yield return "Не заповнен логин";
            }

            if (string.IsNullOrEmpty(Employee.Plan))
            {
                yield return "Не обран план";
            }

            if (string.IsNullOrEmpty(Employee.Term))
            {
                yield return "Не обран термін";
            }

            if (string.IsNullOrEmpty(Employee.PaymentMethod))
            {
                yield return "Не обран спосіб розразунку";
            }

            if(string.IsNullOrEmpty(Id) || !int.TryParse(_id, out int id) || id <= 0)
            {
                yield return "Не валидны1й Id";
            }
        }

        private async Task SaveAsync(CancellationToken cancellationToken)
        {
            EmployeeEntity employeeEntity = new EmployeeEntity(
                Guid.NewGuid().ToString(),
                 Employee.Username.ToLower(),
                Employee.Name,
                Employee.LastName,
                Employee.Username,
                Employee.Email,
                Employee.Avatar,
                Employee.Gender,
                Employee.PhoneNumer,
                string.Empty,
                new Employment() { Title = Employee.Title, KeySkill = Employee.KeySkill },
                new CreditCard() { CcNumber = Employee.CcNumber });

            employeeEntity.SetId(Employee.Id);

            employeeEntity.SetAddress(Employee.City, Employee.StreetName, Employee.StreetAddress, string.Empty, string.Empty, Employee.Country, null);

            employeeEntity.SetSubscription(Employee.Plan, Employee.Status, Employee.PaymentMethod, Employee.Term);

            _repository.Insert(employeeEntity);

            await _unitOfWork1.SaveChangesAsync(cancellationToken);
        }
    }
}
