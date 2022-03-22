using Shared.Base;

namespace Stock.Domain.Entities
{
    public class Stock : EntityBase
    {
        public long ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
