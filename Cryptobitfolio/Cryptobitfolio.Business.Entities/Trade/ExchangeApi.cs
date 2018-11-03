namespace Cryptobitfolio.Business.Entities.Trade
{
    public class ExchangeApi
    {
        public int Id { get; set; }
        public string ApiKeyName { get; set; }
        public Exchange ExchangeName { get; set; }
        public string ApiKey { get; set; }
        public string ApiSecret { get;set; }
        public string ApiExtra { get; set; }
    }
}