using DatabaseModel.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseModel.EntityMappings
{
    public sealed class SubscriptionEntityMap : EntityMapBase<SubscriptionEntity>
    {
        public SubscriptionEntityMap() : base("subscription")
        {            
        }


        protected override void ConfigureMap(EntityTypeBuilder<SubscriptionEntity> b)
        {
            b.HasKey(x => x.Id);
            b.Property(x => x.Id).HasColumnName("id");
            //b.Property(x => x.EmployeeId).HasColumnName("id_employee");
            b.Property(x => x.Plan).HasColumnName("plan");
            b.Property(x => x.Status).HasColumnName("status");
            b.Property(x => x.PaymentMethod).HasColumnName("payment_method");
            b.Property(x => x.Term).HasColumnName("term");

            b.HasOne(x => x.Employee).WithOne(x => x.Subscription).HasForeignKey<EmployeeEntity>(x => x.SubscriptionId);
        }
    }
}
