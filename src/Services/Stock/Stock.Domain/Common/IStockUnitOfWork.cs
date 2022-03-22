using Shared.Base;

namespace Stock.Domain.Common
{
    public interface IStockUnitOfWork : IUnitOfWork
    {
        IStockRepository StockRepository { get; }
    }
}
