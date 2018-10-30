using Cryptobitfolio.Business.Entities;
using System.Collections.Generic;

namespace Cryptobitfolio.Business.Contracts.Portfolio
{
    public class ExchangeCoin
    {
        public int Id { get; set; }
        public decimal Quantity { get; set; }
        public decimal AverageBuy { get; set; }
        public List<CoinBuy> CoinBuyList { get; set; }
        public Exchange Exchange { get; set; }
    }
}
