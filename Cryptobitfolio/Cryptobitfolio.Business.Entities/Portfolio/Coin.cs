using SQLite;

namespace Cryptobitfolio.Business.Entities.Portfolio
{
    public class Coin : EntityBase
    {
        public int CurrencyId { get; set; }

        public decimal Quantity { get; set; }

        public decimal AverageBuy { get; set; }
    }
}
