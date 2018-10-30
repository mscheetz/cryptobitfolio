using System;
using Cryptobitfolio.Business.Entities;

namespace Cryptobitfolio.Business.Contracts.Trade
{
    public class ExchangeTransaction
    {
        public string transactionId { get; set; }
        public string pair { get; set; }
        public decimal quantity { get; set; }
        public decimal price { get; set; }
        public Side side { get; set; }
        public DateTime fillDate { get; set; }
        public Exchange exchange { get; set; }
    }
}