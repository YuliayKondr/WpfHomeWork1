using DatabaseModel.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace DatabaseModel
{
    public class MySqlContext : DbContext, IDataContext
    {
        public MySqlContext(DbContextOptions options)
            :base(options)
        {            
        }

        public virtual DbSet<EmployeeEntity> Users { get; set; }       

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {            
            return base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(MySqlContext).Assembly);

            base.OnModelCreating(modelBuilder);
        }
    }
}
