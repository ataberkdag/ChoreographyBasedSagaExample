using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public class RabbitMQConsts
    {
        public const string StockReservedEventQueueName = "stock_reserved_queue";
        public const string StockOrderCreatedEventQueueName = "stock_order_created_queue";
    }
}
