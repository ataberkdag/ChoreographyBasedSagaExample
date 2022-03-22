using Shared.Base;

namespace Shared.Events
{
    public class StockNotReservedEvent : IDomainEvent
    {
        public long OrderId { get; set; }
        public string Message { get; set; }
    }
}
