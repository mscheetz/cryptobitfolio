﻿using System;

namespace Cryptobitfolio.Business.Entities.Portfolio
{
    public class Watcher : EntityBase
    {
        public int CurrencyId { get; set; }
        public bool Enabled { get; set; }
        public DateTime DateAdded { get; set; }
        public decimal WatchPrice { get; set; }
        public Exchange Exchange { get; set; }
        public string Pair { get; set; }
        public DateTime? WatchHit { get; set; }
    }
}
