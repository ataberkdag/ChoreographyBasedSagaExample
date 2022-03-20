using MediatR;
using System.Collections.Generic;

namespace Order.Application.Orders.Commands.CreateOrder
{
    public class CreateOrderCommand : IRequest
    {
        public string BuyerId { get; set; }
        public List<OrderItemDto> OrderItems { get; set; }
        public PaymentDto PaymentMethod { get; set; }
        public AddressDto Address { get; set; }
    }

    public class OrderItemDto
    {
        public long ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }

    public class PaymentDto
    {
        public string CardName { get; set; }
        public string CardNumber { get; set; }
        public string Expiration { get; set; }
        public string Cvv { get; set; }
    }

    public class AddressDto
    {
        public string Line { get; set; }
        public string Province { get; set; }
        public string District { get; set; }
    }
}
