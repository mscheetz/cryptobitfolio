using System;
using Cryptobitfolio.Business.Entities;

namespace Cryptobitfolio.Business.Contracts.Trade
{
    public class ExchangeOrder
    {
        public string orderId { get; set; }
        public string pair { get; set; }
        public decimal quantity { get; set; }
        public decimal price { get; set; }
        public Side side { get; set; }
        public DateTime placeDate { get; set; }
        public Exchange exchange { get; set; }
    }
}