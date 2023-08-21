using AutoMapper;
using DatabaseModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransferObjects.Employee;

namespace HomeWork1.Mappings
{
    public sealed class AutoMappingProfile : Profile
    {
        public AutoMappingProfile()
        {
            CreateMap<AddressDto, AddressEntity>();

            CreateMap<EmployeeDto, EmployeeEntity>();

            CreateMap<SubscriptionDto, SubscriptionEntity>();

            CreateMap<CoordinatesDto, Coordinates>();

            CreateMap<CreditCardDto, CreditCard>();

            CreateMap<EmploymentDto, Employment>();
        }
    }
}
