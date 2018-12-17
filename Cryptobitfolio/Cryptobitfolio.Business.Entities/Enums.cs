namespace Cryptobitfolio.Business.Entities
{
    public enum Exchange
    {
        None,
        Binance,
        Bittrex,
        CoinbasePro,
        CoinEx,
        KuCoin,
        Switcheo
    }

    public enum Side
    {
        Buy,
        Sell
    }

    public enum CoinType
    {
        Portfolio,
        WatchList
    }

    public enum TradeStatus
    {
        Open,
        Filled,
        Canceled,
    }

    public enum Direction
    {
        GTE,
        LTE
    }
}