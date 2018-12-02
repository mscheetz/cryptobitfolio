using SQLite;
using System;

namespace Cryptobitfolio.Business.Entities.Trade
{
    public class ExchangeApi
    {
        [PrimaryKey]
        [NotNull]
        [AutoIncrement]
        public int Id { get; set; }
        public string ApiKeyName { get; set; }
        public Exchange Exchange { get; set; }
        public string ApiKey { get; set; }
        public string ApiSecret { get;set; }
        public string ApiExtra { get; set; }
        public DateTime Created { get; set; }
    }
}