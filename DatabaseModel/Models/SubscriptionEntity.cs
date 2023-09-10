using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseModel.Models
{
    public class SubscriptionEntity
    {
        public SubscriptionEntity(string plan, string status, string paymentMethod, string term)
        {
            Plan = plan;
            Status = status;
            PaymentMethod = paymentMethod;
            Term = term;
        }

        public SubscriptionEntity()
        {
            
        }

        public int Id { get; protected set; }

        //public int EmployeeId { get; protected set; }

        public string Plan { get; protected set; }
        
        public string Status { get; protected set; }
        
        public string PaymentMethod { get; protected set; }
        
        public string Term { get; protected set; }

        public EmployeeEntity Employee { get; protected set; }
    }
}
