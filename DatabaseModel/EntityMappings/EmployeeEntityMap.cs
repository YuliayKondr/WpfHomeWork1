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
    public sealed class EmployeeEntityMap : EntityMapBase<EmployeeEntity>
    {
        public EmployeeEntityMap() : base("employees")
        {            
        }

        protected override void ConfigureMap(EntityTypeBuilder<EmployeeEntity> b)
        {
            b.HasKey("Id");
            b.Property(x => x.Id).HasColumnName("id");
            b.Property(x => x.Uid).HasColumnName("uid");
            b.Property(x => x.Password).HasColumnName("password");
            b.Property(x => x.Name).HasColumnName("name");
            b.Property(x => x.LastName).HasColumnName("last_name");
            b.Property(x => x.Username).HasColumnName("username");
            b.Property(x => x.Email).HasColumnName("email");
            b.Property(x => x.Avatar).HasColumnName("avatar");
            b.Property(x => x.Gender).HasColumnName("gender");
            b.Property(x => x.PhoneNumer).HasColumnName("phone_number");
            b.Property(x => x.SocialInsuranceNumber).HasColumnName("social_insurance_number");
            b.Property(x => x.DateOfBirth).HasColumnName("birth_date");
            b.Property(x => x.SubscriptionId).HasColumnName("id_subscription");
            b.Property(x => x.AddressId).HasColumnName("id_address");
            b.Property(x => x.CreditCardJson).HasColumnName("credit_card");
            b.Property(x => x.EmploymentJson).HasColumnName("employment");
            b.Ignore(x => x.CreditCard);
            b.Ignore(x => x.Employment);

            b.HasOne(x => x.Subscription).WithOne(x => x.Employee).HasForeignKey<EmployeeEntity>(x => x.SubscriptionId);
            b.HasOne(x => x.Address).WithOne(x => x.Employee).HasForeignKey<EmployeeEntity>(x => x.AddressId);
        }
    }
}
