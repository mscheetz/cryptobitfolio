using Cryptobitfolio.Business.Contracts.Trade;
using Cryptobitfolio.Business.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cryptobitfolio.Business.Contracts.Portfolio
{
    public class Watcher
    {
        public int WatcherId { get; set; }
        public bool Enabled { get; set; }
        public DateTime Created { get; set; }
        public decimal Price { get; set; }
        public Exchange Exchange { get; set; }
        public string Pair { get; set; }
        public DateTime? Hit { get; set; }
        public HistoricalPrice[] HistoricalPrices { get; set; }
    }
}
