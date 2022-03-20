using Shared.Base;
using Shared.Messages;
using System.Collections.Generic;

namespace Shared.Events
{
    public class OrderCreatedEvent : IDomainEvent
    {
        public long OrderId { get; set; }
        public string BuyerId { get; set; }
        public PaymentMessage Payment { get; set; }
        public List<OrderItemMessage> OrderItems { get; set; } = new List<OrderItemMessage>();

        public OrderCreatedEvent(long orderId, string buyerId, PaymentMessage payment)
        {
            this.OrderId = orderId;
            this.BuyerId = buyerId;
            this.Payment = payment;
        }

        public void AddOrderItem(OrderItemMessage orderItem)
        {
            this.OrderItems?.Add(orderItem);
        }
    }
}
