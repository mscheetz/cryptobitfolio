namespace Cryptobitfolio.Business.Entities
{
    public enum Exchange
    {
        none,
        Binance,
        Bittrex,
        CoinbasePro,
        CoinEx,
        KuCoin,
        Switcheo
    }

    public enum Side
    {
        buy,
        sell
    }

    public enum CoinType
    {
        Portfolio,
        WatchList
    }
}