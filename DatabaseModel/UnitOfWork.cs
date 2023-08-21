using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseModel
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IDataContext _dataContext;

        public UnitOfWork(IDataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public Task<int> SaveChangesAsync(CancellationToken cancellationToken) => _dataContext.SaveChangesAsync(cancellationToken);
    }
}
