using Cryptobitfolio.Business.Contracts.Trade;
using Cryptobitfolio.Business.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Cryptobitfolio.Business.Contracts.Portfolio
{
    public class ExchangeCoin
    {
        public int Id { get; set; }
        public string Symbol { get; set; }
        public decimal Quantity { get; set; }
        public decimal AverageBuy => CoinBuyList.Count == 0 ? 0.0M : CoinBuyList.Average(c => c.Price);
        public List<CoinBuy> CoinBuyList { get; set; }
        public List<ExchangeOrder> OpenOrderList { get; set; }
        public Exchange Exchange { get; set; }
    }
}
