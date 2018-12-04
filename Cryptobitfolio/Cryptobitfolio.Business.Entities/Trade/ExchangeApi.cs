using SQLite;
using System;

namespace Cryptobitfolio.Business.Entities.Trade
{
    public class ExchangeApi : EntityBase
    {
        public string ApiKeyName { get; set; }
        public Exchange Exchange { get; set; }
        public string ApiKey { get; set; }
        public string ApiSecret { get;set; }
        public string ApiExtra { get; set; }
        public DateTime Created { get; set; }
    }
}