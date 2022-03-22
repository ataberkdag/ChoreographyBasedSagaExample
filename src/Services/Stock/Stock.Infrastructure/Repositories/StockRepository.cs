using Microsoft.EntityFrameworkCore;
using Stock.Domain.Common;
using Stock.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stock.Infrastructure.Repositories
{
    public class StockRepository : Repository<Domain.Entities.Stock>, IStockRepository
    {
        private readonly EfCoreDbContext _dbContext;
        public StockRepository(EfCoreDbContext dbContext) : base(dbContext)
        {
            this._dbContext = dbContext;
        }

        public async Task<bool> AnyWithQuantity(long productId, int quantity)
        {
            var result = await _dbContext.Stocks.AnyAsync(x => x.ProductId == productId && x.Quantity >= quantity);
            return result;
        }

        public async Task<Domain.Entities.Stock> GetByProductId(long productId)
        {
            return await _dbContext.Stocks.FirstOrDefaultAsync(x => x.ProductId == productId);
        }
    }
}
