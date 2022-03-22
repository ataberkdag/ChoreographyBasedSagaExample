using Stock.Domain.Common;
using Stock.Infrastructure.Persistence;
using Stock.Infrastructure.Repositories;
using System;
using System.Threading.Tasks;

namespace Stock.Infrastructure.UnitOfWork
{
    public class StockUnitOfWork : IStockUnitOfWork
    {
        private readonly EfCoreDbContext _context;
        private readonly IStockRepository _stockRepository;
        public StockUnitOfWork(EfCoreDbContext context)
        {
            this._context = context;
            this._stockRepository = new StockRepository(context);
        }

        public IStockRepository StockRepository => _stockRepository;

        public void SaveChanges()
        {
            this._context.SaveChanges();
        }

        public async Task SaveChangesAsync()
        {
            await this._context.SaveChangesAsync();
        }
    }
}
