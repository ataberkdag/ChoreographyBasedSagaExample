using Order.Domain.Common;
using Order.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Infrastructure.Repositories
{
    public class OrderRepository : Repository<Domain.Entities.Order>, IOrderRepository
    {
        public OrderRepository(EfCoreDbContext dbContext) : base(dbContext)
        {

        }
    }
}
