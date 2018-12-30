using System;

namespace Cryptobitfolio.Business.Entities.Portfolio
{
    public class Watcher : EntityBase
    {
        public int CurrencyId { get; set; }
        public bool Enabled { get; set; }
        public DateTime DateAdded { get; set; }
        public decimal Price { get; set; }
        public Exchange Exchange { get; set; }
        public string Pair { get; set; }
        public decimal High1Hr { get; set; }
        public decimal Low1Hr { get; set; }
        public decimal High24Hr { get; set; }
        public decimal Low24Hr { get; set; }
        public decimal High7Day { get; set; }
        public decimal Low7Day { get; set; }
    }
}
