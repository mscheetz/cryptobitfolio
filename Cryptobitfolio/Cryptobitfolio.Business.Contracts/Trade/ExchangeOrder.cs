using System;
using Cryptobitfolio.Business.Entities;

namespace Cryptobitfolio.Business.Contracts.Trade
{
    public class ExchangeOrder
    {
        public int ExchangeOrderId { get; set; }
        public string OrderId { get; set; }
        public string Pair { get; set; }
        public decimal Quantity { get; set; }
        public decimal FilledQuantity { get; set; }
        public decimal Price { get; set; }
        public Side Side { get; set; }
        public DateTime PlaceDate { get; set; }
        public Exchange Exchange { get; set; }
        public DateTime? ClosedDate { get; set; }
        public TradeStatus Status { get; set; }
    }
}