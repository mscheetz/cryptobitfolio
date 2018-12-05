using Cryptobitfolio.Business.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cryptobitfolio.Business.Contracts.Portfolio
{
    public class AlertList : Currency
    {
        public AlertList()
        {
        }

        public AlertList(Currency currency)
        {
            this.CurrencyId = currency.CurrencyId;
            this.Image = currency.Image;
            this.Name = currency.Name;
            this.Symbol = currency.Symbol;
        }

        public Alerter[] Alerters { get; set; }
    }
}
