using Shared.Base;

namespace Shared.Events
{
    public class PaymentSucceededEvent : IDomainEvent
    {
        public long OrderId { get; set; }
        public string BuyerId { get; set; }
    }
}
