using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cryptobitfolio.Business.Entities.Portfolio
{
    public class ExchangeCoin : EntityBase
    {
        public int CurrencyId { get; set; }
        public string Symbol { get; set; }
        public decimal Quantity { get; set; }
        public decimal AverageBuy { get; set; }
        public Exchange Exchange { get; set; }
    }
}
