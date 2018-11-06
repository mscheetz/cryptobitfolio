using System;
using Cryptobitfolio.Business.Contracts.Portfolio;
using Cryptobitfolio.Business.Entities;

namespace Cryptobitfolio.Business.Contracts.Trade
{
    public class ExchangeTransaction
    {
        public string TransactionId { get; set; }
        public string Pair { get; set; }
        public Currency Currency { get; set; }
        public decimal Quantity { get; set; }
        public decimal Price { get; set; }
        public Side Side { get; set; }
        public DateTime FillDate { get; set; }
        public Exchange Exchange { get; set; }
    }
}