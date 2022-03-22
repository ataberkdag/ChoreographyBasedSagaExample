using MediatR;
using System.Collections.Generic;

namespace Stock.Application.Stocks.Commands.ReduceStock
{
    public class ReduceStockCommand : IRequest
    {
        public long OrderId { get; set; }
        public string BuyerId { get; set; }
        public PaymentDto Payment { get; set; }
        public List<OrderItemDto> OrderItems { get; set; } = new List<OrderItemDto>();
    }

    public class PaymentDto
    {
        public string CardName { get; set; }
        public string CardNumber { get; set; }
        public string Expiration { get; set; }
        public string Cvv { get; set; }
        public decimal TotalPrice { get; set; }
    }

    public class OrderItemDto
    {
        public long ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
