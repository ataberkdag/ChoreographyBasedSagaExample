using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Messages
{
    public class PaymentMessage
    {
        public string CardName { get; set; }
        public string CardNumber { get; set; }
        public string Expiration { get; set; }
        public string Cvv { get; set; }
        public decimal TotalPrice { get; set; }

        public PaymentMessage(string cardName, string cardNumber, string expiration, string cvv, decimal totalPrice)
        {
            this.CardName = cardName;
            this.CardNumber = cardNumber;
            this.Expiration = expiration;
            this.Cvv = cvv;
            this.TotalPrice = totalPrice;
        }
    }
}
