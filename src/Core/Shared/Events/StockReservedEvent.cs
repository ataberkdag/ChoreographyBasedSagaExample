using Shared.Base;
using Shared.Messages;
using System.Collections.Generic;

namespace Shared.Events
{
    public class StockReservedEvent : IDomainEvent
    {
        public long OrderId { get; set; }
        public string BuyerId { get; set; }
        public PaymentMessage Payment { get; set; }
        public List<OrderItemMessage> OrderItems { get; set; } = new List<OrderItemMessage>();
    }
}
