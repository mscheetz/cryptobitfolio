using SQLite;
using System;

namespace Cryptobitfolio.Business.Entities.Trade
{
    public class ExchangeOrder : EntityBase
    {
        public string OrderId { get; set; }
        public string Pair { get; set; }
        public decimal Quantity { get; set; }
        public decimal FilledQuantity { get; set; }
        public decimal Price { get; set; }
        public Side Side { get; set; }
        public DateTime Created { get; set; }
        public Exchange Exchange { get; set; }
        public DateTime? Filled { get; set; }
        public TradeStatus Status { get; set; }
    }
}