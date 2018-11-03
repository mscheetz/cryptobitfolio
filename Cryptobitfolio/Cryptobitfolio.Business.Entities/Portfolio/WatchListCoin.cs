using System;
using System.Collections.Generic;
using System.Text;

namespace Cryptobitfolio.Business.Entities.Portfolio
{
    public class WatchListCoin
    {
        public int WatchListId { get; set; }
        public int CurrencyId { get; set; }
        public DateTime DateAdded { get; set; }
        public decimal WatchPrice { get; set; }
        public Exchange Exchange { get; set; }
    }
}
