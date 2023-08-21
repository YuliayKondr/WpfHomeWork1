using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseModel
{
    public interface IDataContext
    {
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);

        DbSet<TEntity> Set<TEntity>()
            where TEntity : class;
    }
}
