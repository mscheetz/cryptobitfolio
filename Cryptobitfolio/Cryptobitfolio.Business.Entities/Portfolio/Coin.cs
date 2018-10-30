namespace Cryptobitfolio.Business.Entities.Portfolio
{
    public class Coin
    {
        public int Id { get; set; }
        public int CurrencyId { get; set; }
        public decimal Quantity { get; set; }
        public decimal AverageBuy { get; set; }
    }
}
