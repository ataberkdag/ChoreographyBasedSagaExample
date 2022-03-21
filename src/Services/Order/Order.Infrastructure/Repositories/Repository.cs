using Order.Domain.Common;
using Order.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Infrastructure.Repositories
{
    public class Repository<T> : IRepository<T> where T : EntityBase
    {
        private readonly EfCoreDbContext _dbContext;

        public Repository(EfCoreDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public void Add(T item)
        {
            this._dbContext.Set<T>().Add(item);
        }
    }
}
