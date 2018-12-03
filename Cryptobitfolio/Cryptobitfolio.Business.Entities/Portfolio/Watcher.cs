using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cryptobitfolio.Business.Entities.Portfolio
{
    public class Watcher
    {
        [PrimaryKey]
        [NotNull]
        [AutoIncrement]
        public int Id { get; set; }
        public int CurrencyId { get; set; }
        public bool Enabled { get; set; }
        public DateTime DateAdded { get; set; }
        public decimal WatchPrice { get; set; }
        public Exchange Exchange { get; set; }
        public string Pair { get; set; }
        public DateTime? WatchHit { get; set; }
    }
}
