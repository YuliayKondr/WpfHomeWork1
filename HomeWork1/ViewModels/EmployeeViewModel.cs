using AutoMapper;
using DatabaseModel.Models;
using DatabaseModel.Repositories;
using HomeWork1.AppCommon;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace HomeWork1.ViewModels
{
    public sealed class EmployeeViewModel : BaseNotifyPropertyChanged, IActivable
    {
        private readonly IRepository<EmployeeEntity> _repository;        
        private readonly IMapper _mapper;

        private ShowEmployeeInfoParameter _parameter;
        private FullEmployeeViewItem _employeeViewItem;

        public EmployeeViewModel(
            IRepository<EmployeeEntity> repository,
            IRepository<AddressEntity> addressRepository,
            IRepository<SubscriptionEntity> subscriptionRepository,
            IMapper mapper)
        {
            _repository = repository;           
            _mapper = mapper;
        }

        public int? EmployeeId { private get; set; }

        public FullEmployeeViewItem EmployeeViewItem
        {
            get => _employeeViewItem;
            set => SetAndNotifieIfChanged(ref _employeeViewItem, value);
        }

        public async Task ActivateAsync(object parameter)
        {
            if (parameter is ShowEmployeeInfoParameter)
            {
                _parameter = (ShowEmployeeInfoParameter)parameter;

                await InitializeFromDatabaseAsync(_parameter.EmployeeId);
            }           
        }

        public void SetParameter(object parameter)
        {
            if(parameter is ShowEmployeeInfoParameter info)
            {
                EmployeeId = info.EmployeeId;
            }
        }              

        private async Task InitializeFromDatabaseAsync(int employeeId)
        {
            try
            {
                Task<EmployeeEntity> employeeEntityTask = _repository.Queryable().AsNoTracking()
                    .Include(x => x.Address)
                    .Include(x => x.Subscription)                    
                    .SingleOrDefaultAsync(x => x.Id == employeeId);

                EmployeeViewItem = _mapper.Map<FullEmployeeViewItem>(await employeeEntityTask);                

            }
            catch (Exception ex)
            {
                //some do
            }
        }
    }
}
