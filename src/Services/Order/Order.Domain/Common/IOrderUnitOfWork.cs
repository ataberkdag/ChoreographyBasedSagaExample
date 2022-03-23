using Shared.Base;

namespace Order.Domain.Common
{
    public interface IOrderUnitOfWork : IUnitOfWork
    {
        IOrderRepository OrderRepository { get; }
    }
}
