using Shared.Base;
using Shared.Messages;
using System.Collections.Generic;

namespace Shared.Events
{
    public class PaymentFailedEvent : IDomainEvent
    {
        public long OrderId { get; set; }
        public string BuyerId { get; set; }
        public string Message { get; set; }
        public List<OrderItemMessage> OrderItems { get; set; } = new List<OrderItemMessage>();
    }
}
