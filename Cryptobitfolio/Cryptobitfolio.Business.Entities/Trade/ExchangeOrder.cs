using SQLite;
using System;

namespace Cryptobitfolio.Business.Entities.Trade
{
    public class ExchangeOrder
    {
        [PrimaryKey]
        [NotNull]
        [AutoIncrement]
        public int Id { get; set; }
        public Exchange Exchange { get; set; }
        public Side TradeSide { get; set; }
        public string TradingPair { get; set; }
        public decimal Quantity { get; set; }
        public decimal Price { get; set; }
        public DateTime Created { get; set; }
        public TradeStatus Status { get; set; }
        public DateTime? Filled { get; set; }
    }
}