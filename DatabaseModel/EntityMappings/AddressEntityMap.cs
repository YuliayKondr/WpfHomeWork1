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
    public sealed class AddressEntityMap : EntityMapBase<AddressEntity>
    {
        public AddressEntityMap() : base("address")
        {            
        }

        protected override void ConfigureMap(EntityTypeBuilder<AddressEntity> b)
        {
            b.HasKey(x => x.Id);
            b.Property(x => x.Id).HasColumnName("id");
            //b.Property(x => x.EmployeeId).HasColumnName("id_employee");
            b.Property(x => x.City).HasColumnName("city");
            b.Property(x => x.StreetName).HasColumnName("street_name");
            b.Property(x => x.StreetAddress).HasColumnName("street_address");
            b.Property(x => x.ZipCode).HasColumnName("zip_code");
            b.Property(x => x.State).HasColumnName("state");
            b.Property(x => x.Country).HasColumnName("country");
            b.Property(x => x.CoordinatesJson).HasColumnName("coordinates");
            b.Ignore(x => x.Coordinates);

            b.HasOne(x => x.Employee).WithOne(x => x.Address).HasForeignKey<EmployeeEntity>(x => x.AddressId);
        }
    }
}
