using System;
using System.Collections.Generic;
using System.Text;

namespace Cryptobitfolio.Business.Contracts.Portfolio
{
    public class Coin
    {
        public int Id { get; set; }
        public Currency Currency { get; set; }
        public decimal Quantity { get; set; }
        public decimal AverageBuy { get; set; }
        public List<ExchangeCoin> CoinList { get; set; }
        public string QtyLabel => string.Format($"Qty: {Quantity}");
        public string AvgBuyLabel => string.Format($"Avg Buy: {AverageBuy}");
        public decimal CurrentPrice { get; set; }
        public string PercentDiff
        {
            get
            {
                var perc = AverageBuy == 0 ? 0 : (CurrentPrice / AverageBuy) - 1;
                var percString = String.Format("{0:P2}", perc);
                return percString.Replace(" ", "");
            }
        }
        public double Percent24Hr { get; set; }
        public double Percent7D { get; set; }
    }
}
