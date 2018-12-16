using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cryptobitfolio.Business.Contracts.Portfolio
{
    public class Coin : Currency
    {
        public Coin()
        {
        }

        public Coin(Currency currency)
        {
            this.SetCurrency(currency);
        }

        public Coin(Currency currency, int Id)
        {
            this.CoinId = Id;
            this.SetCurrency(currency);
        }

        public int CoinId { get; set; }
        public decimal Quantity => ExchangeCoinList.Count == 0 ? 0.0M : ExchangeCoinList.Sum(c => c.Quantity);
        public decimal AverageBuy => ExchangeCoinList.Count == 0 ? 0.0M : ExchangeCoinList.Average(c => c.AverageBuy);
        public string AverageBuyString => AverageBuy.ToString("0.00######");
        public List<ExchangeCoin> ExchangeCoinList { get; set; }
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
        public decimal High24Hr { get; set; }
        public decimal Low24Hr { get; set; }
        public double Percent7D { get; set; }

        public void SetCurrency(Currency currency)
        {
            this.CurrencyId = currency.CurrencyId;
            this.Symbol = currency.Symbol;
            this.Name = currency.Name;
            this.Image = currency.Image;
        }
    }
}
