using Shared.Base;
using System;
using System.Collections.Generic;

namespace Order.Domain.Entities
{
    public class Order : EntityBase
    {
        public DateTime CreatedDate { get; set; }
        public string BuyerId { get; set; }
        public Address Address { get; set; }
        public OrderStatus Status { get; set; }
        public string Message { get; set; }
        public virtual ICollection<OrderItem> Items { get; set; } = new List<OrderItem>();

        private Order()
        {

        }

        private Order(string buyerId, Address address)
        {
            this.BuyerId = buyerId;
            this.CreatedDate = DateTime.Now;
            this.Status = OrderStatus.Waiting;
            this.Address = address;
        }

        public static Order CreateOrder(string buyerId, Address address)
        {
            return new Order(buyerId, address);
        }


        public void AddOrderItem(OrderItem orderItem)
        {
            this.Items.Add(orderItem);
        }

        public void SetMessage(string message)
        {
            this.Message = message;
        }
    }

    public enum OrderStatus
    {
        Waiting = 0,
        Success = 1,
        Failed = 2
    }
}
