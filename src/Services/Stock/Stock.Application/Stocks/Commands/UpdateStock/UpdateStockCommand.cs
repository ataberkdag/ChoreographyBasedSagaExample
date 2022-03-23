using MediatR;
using Stock.Application.Stocks.Commands.ReduceStock;
using System.Collections.Generic;

namespace Stock.Application.Stocks.Commands.UpdateStock
{
    public class UpdateStockCommand : IRequest
    {
        public long OrderId { get; set; }
        public string BuyerId { get; set; }
        public List<OrderItemDto> OrderItems { get; set; } = new List<OrderItemDto>();
    }
}
