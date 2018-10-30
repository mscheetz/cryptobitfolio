namespace Cryptobitfolio.Business.Entities.Trade
{
    public class ExchangeTrade
    {
        public Exchange ExchangeName { get; set; }
        public Side TradeSide { get; set; }
        public string TradingPair { get; set; }
        public decimal Quantity { get; set; }
        public decimal Price { get; set; }
    }
}