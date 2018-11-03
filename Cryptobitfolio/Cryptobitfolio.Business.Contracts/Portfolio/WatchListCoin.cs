﻿using Cryptobitfolio.Business.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cryptobitfolio.Business.Contracts.Portfolio
{
    public class WatchListCoin : Coin
    {
        public int WatchListId { get; set; }
        public DateTime DateAdded { get; set; }
        public decimal WatchPrice { get; set; }
        public Exchange Exchange { get; set; }
    }
}
