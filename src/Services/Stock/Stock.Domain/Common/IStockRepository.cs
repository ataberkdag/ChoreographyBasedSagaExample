using Shared.Base;
using System.Threading.Tasks;

namespace Stock.Domain.Common
{
    public interface IStockRepository : IRepository<Entities.Stock>
    {
        Task<bool> AnyWithQuantity(long productId, int quantity);

        Task<Domain.Entities.Stock> GetByProductId(long productId);
    }
}
