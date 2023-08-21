using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseModel.Models
{
    public class EmployeeEntity
    {
        public int Id { get; protected set; }
        
        public string Uid { get; protected set; }
        
        public string Password { get; protected set; }

        public string Name { get; protected set; }
        
        public string LastName { get; protected set; }
        
        public string Username { get; protected set; }
        
        public string Email { get; protected set; }

        public string Avatar { get; protected set; }
        
        public string Gender { get; protected set; }
       
        public string PhoneNumer { get; protected set; }

        public string SocialInsuranceNumber { get; protected set; }
        
        public DateTime DateOfBirth { get; protected set; }

        public string EmploymentJson { get; protected set; }

        public string CreditCardJson { get; protected set; }

        public Employment Employment 
        {
            get => !string.IsNullOrEmpty(EmploymentJson) ? JsonConvert.DeserializeObject<Employment>(EmploymentJson) : new Employment();
            set => EmploymentJson = value != null ? JsonConvert.SerializeObject(value) : null;
        }

        public CreditCard CreditCard 
        {
            get => !string.IsNullOrEmpty(CreditCardJson) ? JsonConvert.DeserializeObject<CreditCard>(CreditCardJson) : new CreditCard();
            set => CreditCardJson = value != null ? JsonConvert.SerializeObject(value) : null;
        }

        public int? AddressId { get; protected set; }

        public int? SubscriptionId { get; protected set; }

        public AddressEntity Address { get; protected set; }

        public SubscriptionEntity Subscription { get; protected set; }
    }
}
