using Cryptobitfolio.Business.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cryptobitfolio.Business.Contracts.Portfolio
{
    public class WatchListCoin : Currency
    {
        public WatchListCoin()
        {
        }

        public WatchListCoin(Currency currency)
        {
            this.Id = currency.Id;
            this.Image = currency.Image;
            this.Name = currency.Name;
            this.Symbol = currency.Symbol;
        }

        public Watcher[] Watchers { get; set; }
    }
}
