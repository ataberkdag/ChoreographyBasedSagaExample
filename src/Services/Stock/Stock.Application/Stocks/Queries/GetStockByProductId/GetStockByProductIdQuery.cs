using MediatR;
using Stock.Application.Stocks.Queries.Models;

namespace Stock.Application.Stocks.Queries.GetStockByProductId
{
    public class GetStockByProductIdQuery : IRequest<StockDto>
    {
        public long ProductId { get; set; }
    }
}
