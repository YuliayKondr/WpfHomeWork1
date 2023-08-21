using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseModel.Repositories
{
    public interface IRepository<TEntity> where TEntity : class
    {
        void Delete(TEntity entity);

        void Insert(TEntity entity);

        IQueryable<TEntity> Queryable();

        void Update(TEntity entity);
    }
}
