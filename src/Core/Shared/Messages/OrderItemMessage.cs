using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Messages
{
    public class OrderItemMessage
    {
        public long ProductId { get; set; }
        public int Quantity { get; set; }

        public OrderItemMessage(long productId, int quantity)
        {
            this.ProductId = productId;
            this.Quantity = quantity;
        }
    }
}
