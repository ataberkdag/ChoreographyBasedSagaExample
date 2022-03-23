namespace Shared
{
    public class RabbitMQConsts
    {
        public const string StockReservedEventQueueName = "stock_reserved_queue";
        public const string OrderStockNotReservedQueueName = "order_stock_not_reserved_queue";
        public const string StockOrderCreatedEventQueueName = "stock_order_created_queue";
        public const string OrderPaymentSucceededQueueName = "order_payment_succeeded_queue";
        public const string OrderPaymentFailedQueueName = "order_payment_failed_queue";
        public const string StockPaymentFailedQueueName = "stock_payment_failed_queue";
    }
}
