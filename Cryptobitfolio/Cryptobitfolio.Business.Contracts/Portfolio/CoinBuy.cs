using Cryptobitfolio.Business.Entities;
using System;

namespace Cryptobitfolio.Business.Contracts.Portfolio
{
    public class CoinBuy
    {
        public string Id { get; set; }
        public string Pair { get; set; }
        public decimal Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal BTCPrice { get; set; }
        public Exchange ExchangeName { get; set; }
        public DateTime TransactionDate { get; set; }
    }
}
