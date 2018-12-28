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
        public decimal High1Hr { get; set; }
        public decimal Low1Hr { get; set; }
        public decimal High24Hr { get; set; }
        public decimal Low24Hr { get; set; }
        public decimal High7Day { get; set; }
        public decimal Low7Day { get; set; }
        public HistoricalPrice[] HistoricalPrices { get; set; }
    }
}
