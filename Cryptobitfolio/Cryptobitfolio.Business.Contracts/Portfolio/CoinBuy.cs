using Cryptobitfolio.Business.Entities;
using System;

namespace Cryptobitfolio.Business.Contracts.Portfolio
{
    public class CoinBuy
    {
        public int CoinBuyId { get; set; }
        public string OrderId { get; set; }
        public string Pair { get; set; }
        public decimal Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal BTCPrice { get; set; }
        public Exchange Exchange { get; set; }
        public DateTime TransactionDate { get; set; }
    }
}
