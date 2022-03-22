using Shared.Base;

namespace Order.Domain.Entities
{
    public class OrderItem : EntityBase
    {
        public long ProductId { get; set; }

        public long OrderId { get; set; }
        public decimal Price { get; set; }
        public virtual Order Order { get; set; }
        public int Quantity { get; set; }

        private OrderItem()
        {

        }

        private OrderItem(long productId, Order order, int quantity, decimal price)
        {
            this.Quantity = quantity;
            this.ProductId = productId;
            this.Order = order;
            this.Price = price;
        }

        public static OrderItem CreateOrderItem(long productId, Order order, int quantity, decimal price)
        {
            return new OrderItem(productId, order, quantity, price);
        }
    }
}
