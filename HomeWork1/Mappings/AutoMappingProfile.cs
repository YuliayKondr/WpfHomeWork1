using AutoMapper;
using DatabaseModel.Models;
using HomeWork1.ViewModels;
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

            CreateMap<EmployeeEntity, EmployeeViewItem>()
                .ForMember(x => x.Title, y => y.MapFrom(x => x.Employment.Title));

            CreateMap<EmployeeEntity, FullEmployeeViewItem>()
                .ForMember(x => x.Country, y => y.MapFrom(x => x.Address.Country))
                .ForMember(x => x.City, y => y.MapFrom(x => x.Address.City))
                .ForMember(x => x.StreetName, y => y.MapFrom(x => x.Address.StreetName))
                .ForMember(x => x.StreetAddress, y => y.MapFrom(x => x.Address.StreetAddress))
                .ForMember(x => x.Title, y => y.MapFrom(x => x.Employment.Title))
                .ForMember(x => x.KeySkill, y => y.MapFrom(x => x.Employment.KeySkill))
                .ForMember(x => x.CcNumber, y => y.MapFrom(x => x.CreditCard.CcNumber))
                .ForMember(x => x.Status, y => y.MapFrom(x => x.Subscription.Status))
                .ForMember(x => x.Plan, y => y.MapFrom(x => x.Subscription.Plan))
                .ForMember(x => x.Term, y => y.MapFrom(x => x.Subscription.Term))
                .ForMember(x => x.PaymentMethod, y => y.MapFrom(x => x.Subscription.PaymentMethod));

        }
    }
}
