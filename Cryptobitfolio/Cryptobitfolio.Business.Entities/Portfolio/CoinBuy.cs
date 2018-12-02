using SQLite;
using System;

namespace Cryptobitfolio.Business.Entities.Portfolio
{
    public class CoinBuy
    {
        [PrimaryKey]
        [NotNull]
        [AutoIncrement]
        public int Id { get; set; }
        public int CurrencyId { get; set; }
        public int CoinId { get; set; }
        public decimal Quantity { get; set; }
        public decimal Price { get; set; }
        public Exchange ExchangeName { get; set; }
        public DateTime TransactionDate { get; set; }
    }
}