using Cryptobitfolio.Business;
using Cryptobitfolio.Business.Common;
using Cryptobitfolio.Business.Contracts.Portfolio;
using Cryptobitfolio.Business.Contracts.Trade;
using Cryptobitfolio.UI.Main;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Cryptobitfolio.UI
{
    public class App : Application
    {
        //private static IExchangeBuilder _exchangeBldr;
        private static List<Business.Contracts.Portfolio.Coin> coinList;
        private static List<WatchListCoin> watchListCoins;
        private static List<ExchangeTransaction> transactions;
        //private IExchangeBuilder _exchangeBldr;

        public App()//IExchangeBuilder exchangeBldr)
        {
            //_exchangeBldr = exchangeBldr;
            MainPage = new MainPage();
        }

        public List<Business.Contracts.Portfolio.Coin> GetCoinsStable()
        {
            if (coinList == null || coinList.Count == 0)
            {
                var coinBuyList = new List<CoinBuy>
                {
                    new CoinBuy { ExchangeName = Business.Entities.Exchange.Binance, Pair = "BTCUSDT", Price = 6500.00M, Quantity = 0.0014M, TransactionDate = DateTime.UtcNow.AddMonths(-1) },
                    new CoinBuy { ExchangeName = Business.Entities.Exchange.Bittrex, Pair = "BTCUSDT", Price = 5600.00M, Quantity = 0.0013M, TransactionDate = DateTime.UtcNow.AddMonths(-2) },
                    new CoinBuy { ExchangeName = Business.Entities.Exchange.Binance, Pair = "ETHUSDT", Price = 212.44M, Quantity = 0.55M, TransactionDate = DateTime.UtcNow.AddMonths(-1) },
                    new CoinBuy { ExchangeName = Business.Entities.Exchange.Binance, Pair = "XLMBTC", Price = 0.00003400M, Quantity = 175M, TransactionDate = DateTime.UtcNow.AddMonths(-1) },
                    new CoinBuy { ExchangeName = Business.Entities.Exchange.Binance, Pair = "XLMBTC", Price = 0.00002900M, Quantity = 224M, TransactionDate = DateTime.UtcNow.AddMonths(-2) },
                    new CoinBuy { ExchangeName = Business.Entities.Exchange.Binance, Pair = "NEOBTC", Price = 0.0022M, Quantity = 6.5M, TransactionDate = DateTime.UtcNow.AddMonths(-1) },
                    new CoinBuy { ExchangeName = Business.Entities.Exchange.Bittrex, Pair = "NEOBTC", Price = 0.0029M, Quantity = 8.0M, TransactionDate = DateTime.UtcNow.AddMonths(-1) },
                    new CoinBuy { ExchangeName = Business.Entities.Exchange.Binance, Pair = "POLYBTC", Price = 0.00002978M, Quantity = 500M, TransactionDate = DateTime.UtcNow.AddMonths(-1) },
                    new CoinBuy { ExchangeName = Business.Entities.Exchange.Binance, Pair = "POLYBTC", Price = 0.00003978M, Quantity = 175M, TransactionDate = DateTime.UtcNow.AddMonths(-1) },
                    new CoinBuy { ExchangeName = Business.Entities.Exchange.Binance, Pair = "POLYBTC", Price = 0.0000362M, Quantity = 227M, TransactionDate = DateTime.UtcNow.AddMonths(-1) },
                };

                var exchangeCoins = new List<Business.Contracts.Portfolio.ExchangeCoin>
                {
                    new ExchangeCoin
                    {
                        CoinBuyList = coinBuyList.Where(c => c.Pair.Equals("BTCUSDT") && c.ExchangeName == Business.Entities.Exchange.Binance).ToList(),
                        Exchange = Business.Entities.Exchange.Binance, Quantity = .0014M, Symbol = "BTC"
                    },
                    new ExchangeCoin
                    {
                        CoinBuyList = coinBuyList.Where(c => c.Pair.Equals("BTCUSDT") && c.ExchangeName == Business.Entities.Exchange.Bittrex).ToList(),
                        Exchange = Business.Entities.Exchange.Bittrex, Quantity = .0013M, Symbol = "BTC"
                    },
                    new ExchangeCoin
                    {
                        CoinBuyList = coinBuyList.Where(c => c.Pair.Equals("ETHUSDT") && c.ExchangeName == Business.Entities.Exchange.Binance).ToList(),
                        Exchange = Business.Entities.Exchange.Binance, Quantity = .55M, Symbol = "ETH"
                    },
                    new ExchangeCoin
                    {
                        CoinBuyList = coinBuyList.Where(c => c.Pair.Equals("XLMBTC") && c.ExchangeName == Business.Entities.Exchange.Binance).ToList(),
                        Exchange = Business.Entities.Exchange.Binance, Quantity = 399, Symbol = "XLM"
                    },
                    new ExchangeCoin
                    {
                        CoinBuyList = coinBuyList.Where(c => c.Pair.Equals("NEOBTC") && c.ExchangeName == Business.Entities.Exchange.Binance).ToList(),
                        Exchange = Business.Entities.Exchange.Binance, Quantity = 6.5M, Symbol = "NEO"
                    },
                    new ExchangeCoin
                    {
                        CoinBuyList = coinBuyList.Where(c => c.Pair.Equals("NEOBTC") && c.ExchangeName == Business.Entities.Exchange.Bittrex).ToList(),
                        Exchange = Business.Entities.Exchange.Bittrex, Quantity = 8.0M, Symbol = "NEO"
                    },
                    new ExchangeCoin
                    {
                        CoinBuyList = coinBuyList.Where(c => c.Pair.Equals("POLYBTC") && c.ExchangeName == Business.Entities.Exchange.Binance).ToList(),
                        Exchange = Business.Entities.Exchange.Binance, Quantity = 902M, Symbol = "POLY"
                    }
                };

                var coins = new List<Business.Contracts.Portfolio.Coin>
                {
                    new Business.Contracts.Portfolio.Coin
                    {
                        Id =0, Currency = new Currency{ Name = "Bitcoin", Symbol = "BTC" }, CurrentPrice = 6379.44M, Percent24Hr = 0.47, Percent7D = -0.5,
                        ExchangeCoinList = exchangeCoins.Where(e => e.Symbol.Equals("BTC")).ToList()
                    },
                    new Business.Contracts.Portfolio.Coin
                    {
                        Id =1, Currency = new Currency{ Name = "Ethereum", Symbol = "ETH"}, CurrentPrice = 242.46M, Percent24Hr = 1.7, Percent7D = 2.5,
                        ExchangeCoinList = exchangeCoins.Where(e => e.Symbol.Equals("ETH")).ToList()
                    },
                    new Business.Contracts.Portfolio.Coin
                    {
                        Id =2, Currency = new Currency{ Name = "Stellar", Symbol = "XLM"}, CurrentPrice = 0.00001042M, Percent24Hr = 0.00, Percent7D = -1.5,
                        ExchangeCoinList = exchangeCoins.Where(e => e.Symbol.Equals("XLM")).ToList()
                    },
                    new Business.Contracts.Portfolio.Coin
                    {
                        Id =3, Currency = new Currency{ Name = "NEO", Symbol = "NEO"}, CurrentPrice = 0.0019M, Percent24Hr = -7.47, Percent7D = -0.5,
                        ExchangeCoinList = exchangeCoins.Where(e => e.Symbol.Equals("NEO")).ToList()
                    },
                    new Business.Contracts.Portfolio.Coin
                    {
                        Id =4, Currency = new Currency{ Name = "Polymath", Symbol = "POLY"}, CurrentPrice = 0.00000570M, Percent24Hr = 20.47, Percent7D = 10.5,
                        ExchangeCoinList = exchangeCoins.Where(e => e.Symbol.Equals("POLY")).ToList()
                    },
                };

                coinList = coins;
            }

            return coinList;

        }

        public async static Task<List<Business.Contracts.Portfolio.Coin>> GetCoins()
        {
            if (coinList == null || coinList.Count == 0)
            {
                var coinBuyList = new List<CoinBuy>
                {
                    new CoinBuy { ExchangeName = Business.Entities.Exchange.Binance, Pair = "BTCUSDT", Price = 6500.00M, Quantity = 0.0014M, TransactionDate = DateTime.UtcNow.AddMonths(-1) },
                    new CoinBuy { ExchangeName = Business.Entities.Exchange.Bittrex, Pair = "BTCUSDT", Price = 5600.00M, Quantity = 0.0013M, TransactionDate = DateTime.UtcNow.AddMonths(-2) },
                    new CoinBuy { ExchangeName = Business.Entities.Exchange.Binance, Pair = "ETHUSDT", Price = 212.44M, Quantity = 0.55M, TransactionDate = DateTime.UtcNow.AddMonths(-1) },
                    new CoinBuy { ExchangeName = Business.Entities.Exchange.Binance, Pair = "XLMBTC", Price = 0.00003400M, Quantity = 175M, TransactionDate = DateTime.UtcNow.AddMonths(-1) },
                    new CoinBuy { ExchangeName = Business.Entities.Exchange.Binance, Pair = "XLMBTC", Price = 0.00002900M, Quantity = 224M, TransactionDate = DateTime.UtcNow.AddMonths(-2) },
                    new CoinBuy { ExchangeName = Business.Entities.Exchange.Binance, Pair = "NEOBTC", Price = 0.0022M, Quantity = 6.5M, TransactionDate = DateTime.UtcNow.AddMonths(-1) },
                    new CoinBuy { ExchangeName = Business.Entities.Exchange.Bittrex, Pair = "NEOBTC", Price = 0.0029M, Quantity = 8.0M, TransactionDate = DateTime.UtcNow.AddMonths(-1) },
                    new CoinBuy { ExchangeName = Business.Entities.Exchange.Binance, Pair = "POLYBTC", Price = 0.00002978M, Quantity = 500M, TransactionDate = DateTime.UtcNow.AddMonths(-1) },
                    new CoinBuy { ExchangeName = Business.Entities.Exchange.Binance, Pair = "POLYBTC", Price = 0.00003978M, Quantity = 175M, TransactionDate = DateTime.UtcNow.AddMonths(-1) },
                    new CoinBuy { ExchangeName = Business.Entities.Exchange.Binance, Pair = "POLYBTC", Price = 0.0000362M, Quantity = 227M, TransactionDate = DateTime.UtcNow.AddMonths(-1) },
                };

                var exchangeCoins = new List<Business.Contracts.Portfolio.ExchangeCoin>
                {
                    new ExchangeCoin
                    {
                        CoinBuyList = coinBuyList.Where(c => c.Pair.Equals("BTCUSDT") && c.ExchangeName == Business.Entities.Exchange.Binance).ToList(),
                        Exchange = Business.Entities.Exchange.Binance, Quantity = .0014M, Symbol = "BTC"
                    },
                    new ExchangeCoin
                    {
                        CoinBuyList = coinBuyList.Where(c => c.Pair.Equals("BTCUSDT") && c.ExchangeName == Business.Entities.Exchange.Bittrex).ToList(),
                        Exchange = Business.Entities.Exchange.Bittrex, Quantity = .0013M, Symbol = "BTC"
                    },
                    new ExchangeCoin
                    {
                        CoinBuyList = coinBuyList.Where(c => c.Pair.Equals("ETHUSDT") && c.ExchangeName == Business.Entities.Exchange.Binance).ToList(),
                        Exchange = Business.Entities.Exchange.Binance, Quantity = .55M, Symbol = "ETH"
                    },
                    new ExchangeCoin
                    {
                        CoinBuyList = coinBuyList.Where(c => c.Pair.Equals("XLMBTC") && c.ExchangeName == Business.Entities.Exchange.Binance).ToList(),
                        Exchange = Business.Entities.Exchange.Binance, Quantity = 399, Symbol = "XLM"
                    },
                    new ExchangeCoin
                    {
                        CoinBuyList = coinBuyList.Where(c => c.Pair.Equals("NEOBTC") && c.ExchangeName == Business.Entities.Exchange.Binance).ToList(),
                        Exchange = Business.Entities.Exchange.Binance, Quantity = 6.5M, Symbol = "NEO"
                    },
                    new ExchangeCoin
                    {
                        CoinBuyList = coinBuyList.Where(c => c.Pair.Equals("NEOBTC") && c.ExchangeName == Business.Entities.Exchange.Bittrex).ToList(),
                        Exchange = Business.Entities.Exchange.Bittrex, Quantity = 8.0M, Symbol = "NEO"
                    },
                    new ExchangeCoin
                    {
                        CoinBuyList = coinBuyList.Where(c => c.Pair.Equals("POLYBTC") && c.ExchangeName == Business.Entities.Exchange.Binance).ToList(),
                        Exchange = Business.Entities.Exchange.Binance, Quantity = 902M, Symbol = "POLY"
                    }
                };

                var coins = new List<Business.Contracts.Portfolio.Coin>
                {
                    new Business.Contracts.Portfolio.Coin
                    {
                        Id =0, Currency = new Currency{ Name = "Bitcoin", Symbol = "BTC" }, CurrentPrice = 6379.44M, Percent24Hr = 0.47, Percent7D = -0.5,
                        ExchangeCoinList = exchangeCoins.Where(e => e.Symbol.Equals("BTC")).ToList()
                    },
                    new Business.Contracts.Portfolio.Coin
                    {
                        Id =1, Currency = new Currency{ Name = "Ethereum", Symbol = "ETH"}, CurrentPrice = 242.46M, Percent24Hr = 1.7, Percent7D = 2.5,
                        ExchangeCoinList = exchangeCoins.Where(e => e.Symbol.Equals("ETH")).ToList()
                    },
                    new Business.Contracts.Portfolio.Coin
                    {
                        Id =2, Currency = new Currency{ Name = "Stellar", Symbol = "XLM"}, CurrentPrice = 0.00001042M, Percent24Hr = 0.00, Percent7D = -1.5,
                        ExchangeCoinList = exchangeCoins.Where(e => e.Symbol.Equals("XLM")).ToList()
                    },
                    new Business.Contracts.Portfolio.Coin
                    {
                        Id =3, Currency = new Currency{ Name = "NEO", Symbol = "NEO"}, CurrentPrice = 0.0019M, Percent24Hr = -7.47, Percent7D = -0.5,
                        ExchangeCoinList = exchangeCoins.Where(e => e.Symbol.Equals("NEO")).ToList()
                    },
                    new Business.Contracts.Portfolio.Coin
                    {
                        Id =4, Currency = new Currency{ Name = "Polymath", Symbol = "POLY"}, CurrentPrice = 0.00000570M, Percent24Hr = 20.47, Percent7D = 10.5,
                        ExchangeCoinList = exchangeCoins.Where(e => e.Symbol.Equals("POLY")).ToList()
                    },
                };

                coinList = coins;
            }

            return coinList;
        }

        public async static Task<Business.Contracts.Portfolio.Coin> GetCoin(string symbol)
        {
            return GetCoins().Result.Where(c => c.Currency.Symbol.Equals(symbol)).FirstOrDefault();
        }

        public async static Task<Business.Contracts.Portfolio.Coin> GetCoin(int id)
        {
            return GetCoins().Result.Where(c => c.Id == id).FirstOrDefault();
        }

        public async static Task AddCoin(Business.Contracts.Portfolio.Coin coin)
        {
            coinList.Add(coin);
        }

        public async static Task<List<WatchListCoin>> GetWatchListCoins()
        {
            if (watchListCoins == null || watchListCoins.Count == 0)
            {
                var coins = new List<WatchListCoin>
                {
                    new WatchListCoin { Id = 1, Name = "Bitcoin", Symbol = "BTC", Watchers = new Watcher[] { new Watcher { DateAdded = DateTime.UtcNow.AddDays(-1), Enabled = true, Exchange = Business.Entities.Exchange.Binance, WatchListId = 1, WatchPrice = 6199.0M, Pair = "BTCUSDT" } }},
                    new WatchListCoin { Id = 2, Name = "Ethereum", Symbol = "ETH", Watchers = new Watcher[] { new Watcher { DateAdded = DateTime.UtcNow.AddDays(-1), Enabled = true, Exchange = Business.Entities.Exchange.Binance, WatchListId = 2, WatchPrice = 185.0M, Pair = "ETHUSDT" } }},
                    new WatchListCoin { Id = 3, Name = "Stellar", Symbol = "XLM", Watchers = new Watcher[] { new Watcher { DateAdded = DateTime.UtcNow.AddDays(-4), Enabled = true, Exchange = Business.Entities.Exchange.Binance, WatchListId = 3, WatchPrice = 0.00002900M, Pair = "XLMBTC", WatchHit = DateTime.UtcNow.AddHours(-20) } }},
                    new WatchListCoin { Id = 4, Name = "NEO", Symbol = "NEO", Watchers = new Watcher[] { new Watcher { DateAdded = DateTime.UtcNow.AddDays(-2), Enabled = true, Exchange = Business.Entities.Exchange.Binance, WatchListId = 4, WatchPrice = 0.001999M, Pair = "NEOBTC" } }},
                };

                watchListCoins = coins;
            }

            return watchListCoins;
        }

        public async static Task<WatchListCoin> GetWatchListCoins(string symbol)
        {
            return GetWatchListCoins().Result.Where(w => w.Symbol.Equals(symbol)).FirstOrDefault();
        }

        public async static Task<WatchListCoin> GetWatchListCoins(int id)
        {
            return GetWatchListCoins().Result.Where(w => w.Id == id).FirstOrDefault();
        }

        public async static Task AddWatchListCoin(WatchListCoin coin)
        {
            watchListCoins.Add(coin);
        }

        public async static Task<List<ExchangeTransaction>> GetTransactions()
        {
            if (transactions == null || transactions.Count == 0)
            {
                var transactionList = new List<ExchangeTransaction>
                {
                    new ExchangeTransaction { Currency = new Currency{ Id = 1, Name = "Bitcoin", Symbol = "BTC" }, Exchange = Business.Entities.Exchange.Binance, FillDate = DateTime.UtcNow.AddDays(-2), Pair = "BTCUSDT", Price = 6700, Quantity = .01M, Side = Business.Entities.Side.buy, TransactionId = "1" },
                    new ExchangeTransaction { Currency = new Currency{ Id = 1, Name = "Bitcoin", Symbol = "BTC" }, Exchange = Business.Entities.Exchange.Binance, FillDate = DateTime.UtcNow.AddDays(-1), Pair = "BTCUSDT", Price = 6500, Quantity = .01M, Side = Business.Entities.Side.buy, TransactionId = "2" },
                    new ExchangeTransaction { Currency = new Currency{ Id = 1, Name = "Ethereum", Symbol = "ETH" }, Exchange = Business.Entities.Exchange.Binance, FillDate = DateTime.UtcNow.AddDays(-1), Pair = "ETHUSDT", Price = 199.0M, Quantity = 1, Side = Business.Entities.Side.buy, TransactionId = "3" },
                    new ExchangeTransaction { Currency = new Currency{ Id = 1, Name = "Stellar", Symbol = "XLM" }, Exchange = Business.Entities.Exchange.Binance, FillDate = DateTime.UtcNow.AddDays(-1), Pair = "XLMBTC", Price = 0.00002570M, Quantity = 500M, Side = Business.Entities.Side.buy, TransactionId = "4" },
                    new ExchangeTransaction { Currency = new Currency{ Id = 1, Name = "Stellar", Symbol = "XLM" }, Exchange = Business.Entities.Exchange.Binance, FillDate = DateTime.UtcNow.AddDays(-1), Pair = "XLMBTC", Price = 0.00001957M, Quantity = 1000M, Side = Business.Entities.Side.buy, TransactionId = "5" },
                };
                transactions = transactionList;
            }

            return transactions;
        }

        public async static Task<List<ExchangeTransaction>> GetTransactions(string symbol)
        {
            return GetTransactions().Result.Where(w => w.Currency.Symbol.Equals(symbol)).ToList();
        }

        public async static Task<List<ExchangeTransaction>> GetTransactions(int id)
        {
            return GetTransactions().Result.Where(w => w.Currency.Id == id).ToList();
        }
        //public static IExchangeBuilder ExchangeBuilder
        //{
        //    get
        //    {
        //        return _exchangeBldr;
        //    }
        //}

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}