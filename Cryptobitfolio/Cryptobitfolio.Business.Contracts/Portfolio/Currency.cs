using System;
using System.Collections.Generic;
using System.Text;

namespace Cryptobitfolio.Business.Contracts.Portfolio
{
    public class Currency
    {
        public int CurrencyId { get; set; }
        public string Symbol { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public string SymbolLabel => string.Format($"Symbol: {Symbol}");
        public string NameLabel => string.Format($"Name: {Name}");
    }
}
