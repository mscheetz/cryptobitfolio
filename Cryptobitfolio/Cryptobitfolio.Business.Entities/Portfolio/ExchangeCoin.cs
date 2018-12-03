using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cryptobitfolio.Business.Entities.Portfolio
{
    public class ExchangeCoin
    {
        [PrimaryKey]
        [NotNull]
        [AutoIncrement]
        public int Id { get; set; }
        public int CurrencyId { get; set; }
        public decimal Quantity { get; set; }
        public decimal AverageBuy { get; set; }
        public Exchange Exchange { get; set; }
    }
}
