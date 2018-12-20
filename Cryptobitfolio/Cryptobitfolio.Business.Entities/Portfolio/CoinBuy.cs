using Cryptobitfolio.Business.Entities.Trade;
using System;

namespace Cryptobitfolio.Business.Entities.Portfolio
{
    public class CoinBuy : ExchangeOrder
    {
        public int CurrencyId { get; set; }
        public int CoinId { get; set; }
        public int CoinBuyId { get; set; }
        public decimal QuantityApplied { get; set; }
        public decimal BTCPrice { get; set; }
    }
}