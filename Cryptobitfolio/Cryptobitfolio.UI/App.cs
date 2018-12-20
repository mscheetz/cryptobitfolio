using CommonServiceLocator;
using Cryptobitfolio.Business;
using Cryptobitfolio.Business.Common;
using Cryptobitfolio.Business.Contracts.Portfolio;
using Cryptobitfolio.Business.Contracts.Trade;
using Cryptobitfolio.Data.Interfaces;
using Cryptobitfolio.Data.Repositories;
using Cryptobitfolio.UI.Main;
using Cryptobitfolio.UI.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
//using Unity;
//using Unity.ServiceLocation;
using Xamarin.Forms;

namespace Cryptobitfolio.UI
{
    public class App : Application
    {
        private static List<Business.Contracts.Portfolio.Coin> coinList;
        private static List<Business.Contracts.Portfolio.WatchList> watchListCoins;
        private static List<ExchangeTransaction> transactions;

        public App()
        {
            //var unityContainer = new UnityContainer();

            //unityContainer.RegisterType<IExchangeApiRepository, ExchangeApiRepository>();
            //unityContainer.RegisterType<IExchangeBuilder, ExchangeBuilder>();
            //unityContainer.RegisterInstance(typeof(ExchangeViewModel));

            //var unityServiceLocator = new UnityServiceLocator(unityContainer);
            //ServiceLocator.SetLocatorProvider(() => unityServiceLocator);
            
            MainPage = new MainPage();
        }

        public List<Business.Contracts.Portfolio.Coin> GetCoinsStable()
        {
            if (coinList == null || coinList.Count == 0)
            {
                var coinBuyList = new List<CoinBuy>
                {
                    new CoinBuy { Exchange = Business.Entities.Exchange.Binance, Pair = "BTCUSDT", Price = 6500.00M, OrderQuantity = 0.0014M, TransactionDate = DateTime.UtcNow.AddMonths(-1) },
                    new CoinBuy { Exchange = Business.Entities.Exchange.Bittrex, Pair = "BTCUSDT", Price = 5600.00M, OrderQuantity = 0.0013M, TransactionDate = DateTime.UtcNow.AddMonths(-2) },
                    new CoinBuy { Exchange = Business.Entities.Exchange.Binance, Pair = "ETHUSDT", Price = 212.44M, OrderQuantity = 0.55M, TransactionDate = DateTime.UtcNow.AddMonths(-1) },
                    new CoinBuy { Exchange = Business.Entities.Exchange.Binance, Pair = "XLMBTC", Price = 0.00003400M, OrderQuantity = 175M, TransactionDate = DateTime.UtcNow.AddMonths(-1) },
                    new CoinBuy { Exchange = Business.Entities.Exchange.Binance, Pair = "XLMBTC", Price = 0.00002900M, OrderQuantity = 224M, TransactionDate = DateTime.UtcNow.AddMonths(-2) },
                    new CoinBuy { Exchange = Business.Entities.Exchange.Binance, Pair = "NEOBTC", Price = 0.0022M, OrderQuantity = 6.5M, TransactionDate = DateTime.UtcNow.AddMonths(-1) },
                    new CoinBuy { Exchange = Business.Entities.Exchange.Bittrex, Pair = "NEOBTC", Price = 0.0029M, OrderQuantity = 8.0M, TransactionDate = DateTime.UtcNow.AddMonths(-1) },
                    new CoinBuy { Exchange = Business.Entities.Exchange.Binance, Pair = "POLYBTC", Price = 0.00002978M, OrderQuantity = 500M, TransactionDate = DateTime.UtcNow.AddMonths(-1) },
                    new CoinBuy { Exchange = Business.Entities.Exchange.Binance, Pair = "POLYBTC", Price = 0.00003978M, OrderQuantity = 175M, TransactionDate = DateTime.UtcNow.AddMonths(-1) },
                    new CoinBuy { Exchange = Business.Entities.Exchange.Binance, Pair = "POLYBTC", Price = 0.0000362M, OrderQuantity = 227M, TransactionDate = DateTime.UtcNow.AddMonths(-1) },
                };

                var exchangeCoins = new List<Business.Contracts.Portfolio.ExchangeCoin>
                {
                    new ExchangeCoin
                    {
                        CoinBuyList = coinBuyList.Where(c => c.Pair.Equals("BTCUSDT") && c.Exchange == Business.Entities.Exchange.Binance).ToList(),
                        Exchange = Business.Entities.Exchange.Binance, Quantity = .0014M
                    },
                    new ExchangeCoin
                    {
                        CoinBuyList = coinBuyList.Where(c => c.Pair.Equals("BTCUSDT") && c.Exchange == Business.Entities.Exchange.Bittrex).ToList(),
                        Exchange = Business.Entities.Exchange.Bittrex, Quantity = .0013M
                    },
                    new ExchangeCoin
                    {
                        CoinBuyList = coinBuyList.Where(c => c.Pair.Equals("ETHUSDT") && c.Exchange == Business.Entities.Exchange.Binance).ToList(),
                        Exchange = Business.Entities.Exchange.Binance, Quantity = .55M
                    },
                    new ExchangeCoin
                    {
                        CoinBuyList = coinBuyList.Where(c => c.Pair.Equals("XLMBTC") && c.Exchange == Business.Entities.Exchange.Binance).ToList(),
                        Exchange = Business.Entities.Exchange.Binance, Quantity = 399
                    },
                    new ExchangeCoin
                    {
                        CoinBuyList = coinBuyList.Where(c => c.Pair.Equals("NEOBTC") && c.Exchange == Business.Entities.Exchange.Binance).ToList(),
                        Exchange = Business.Entities.Exchange.Binance, Quantity = 6.5M
                    },
                    new ExchangeCoin
                    {
                        CoinBuyList = coinBuyList.Where(c => c.Pair.Equals("NEOBTC") && c.Exchange == Business.Entities.Exchange.Bittrex).ToList(),
                        Exchange = Business.Entities.Exchange.Bittrex, Quantity = 8.0M
                    },
                    new ExchangeCoin
                    {
                        CoinBuyList = coinBuyList.Where(c => c.Pair.Equals("POLYBTC") && c.Exchange == Business.Entities.Exchange.Binance).ToList(),
                        Exchange = Business.Entities.Exchange.Binance, Quantity = 902M
                    }
                };



                var coins = new List<Business.Contracts.Portfolio.Coin>
                {

                    new Business.Contracts.Portfolio.Coin
                    {
                        CoinId =0, Name = "Bitcoin", Symbol = "BTC" , CurrentPrice = 6379.44M, Percent24Hr = 0.47, Percent7D = -0.5,
                        ExchangeCoinList = exchangeCoins.Where(e => e.CoinBuyList.Any(c => c.Pair.StartsWith("BTC"))).ToList()
                    },
                    new Business.Contracts.Portfolio.Coin
                    {
                        CoinId =1, Name = "Ethereum", Symbol = "ETH", CurrentPrice = 242.46M, Percent24Hr = 1.7, Percent7D = 2.5,
                        ExchangeCoinList = exchangeCoins.Where(e => e.CoinBuyList.Any(c => c.Pair.StartsWith("ETH"))).ToList()
                    },
                    new Business.Contracts.Portfolio.Coin
                    {
                        CoinId =2, Name = "Stellar", Symbol = "XLM", CurrentPrice = 0.00001042M, Percent24Hr = 0.00, Percent7D = -1.5,
                        ExchangeCoinList = exchangeCoins.Where(e => e.CoinBuyList.Any(c => c.Pair.StartsWith("XLM"))).ToList()
                    },
                    new Business.Contracts.Portfolio.Coin
                    {
                        CoinId =3, Name = "NEO", Symbol = "NEO", CurrentPrice = 0.0019M, Percent24Hr = -7.47, Percent7D = -0.5,
                        ExchangeCoinList = exchangeCoins.Where(e => e.CoinBuyList.Any(c => c.Pair.StartsWith("NEO"))).ToList()
                    },
                    new Business.Contracts.Portfolio.Coin
                    {
                        CoinId =4, Name = "Polymath", Symbol = "POLY", CurrentPrice = 0.00000570M, Percent24Hr = 20.47, Percent7D = 10.5,
                        ExchangeCoinList = exchangeCoins.Where(e => e.CoinBuyList.Any(c => c.Pair.StartsWith("POLY"))).ToList()
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
                    new CoinBuy { Exchange = Business.Entities.Exchange.Binance, Pair = "BTCUSDT", Price = 6500.00M, OrderQuantity = 0.0014M, TransactionDate = DateTime.UtcNow.AddMonths(-1) },
                    new CoinBuy { Exchange = Business.Entities.Exchange.Bittrex, Pair = "BTCUSDT", Price = 5600.00M, OrderQuantity = 0.0013M, TransactionDate = DateTime.UtcNow.AddMonths(-2) },
                    new CoinBuy { Exchange = Business.Entities.Exchange.Binance, Pair = "ETHUSDT", Price = 212.44M, OrderQuantity = 0.55M, TransactionDate = DateTime.UtcNow.AddMonths(-1) },
                    new CoinBuy { Exchange = Business.Entities.Exchange.Binance, Pair = "XLMBTC", Price = 0.00003400M, OrderQuantity = 175M, TransactionDate = DateTime.UtcNow.AddMonths(-1) },
                    new CoinBuy { Exchange = Business.Entities.Exchange.Binance, Pair = "XLMBTC", Price = 0.00002900M, OrderQuantity = 224M, TransactionDate = DateTime.UtcNow.AddMonths(-2) },
                    new CoinBuy { Exchange = Business.Entities.Exchange.Binance, Pair = "NEOBTC", Price = 0.0022M, OrderQuantity = 6.5M, TransactionDate = DateTime.UtcNow.AddMonths(-1) },
                    new CoinBuy { Exchange = Business.Entities.Exchange.Bittrex, Pair = "NEOBTC", Price = 0.0029M, OrderQuantity = 8.0M, TransactionDate = DateTime.UtcNow.AddMonths(-1) },
                    new CoinBuy { Exchange = Business.Entities.Exchange.Binance, Pair = "POLYBTC", Price = 0.00002978M, OrderQuantity = 500M, TransactionDate = DateTime.UtcNow.AddMonths(-1) },
                    new CoinBuy { Exchange = Business.Entities.Exchange.Binance, Pair = "POLYBTC", Price = 0.00003978M, OrderQuantity = 175M, TransactionDate = DateTime.UtcNow.AddMonths(-1) },
                    new CoinBuy { Exchange = Business.Entities.Exchange.Binance, Pair = "POLYBTC", Price = 0.0000362M, OrderQuantity = 227M, TransactionDate = DateTime.UtcNow.AddMonths(-1) },
                };

                var exchangeCoins = new List<Business.Contracts.Portfolio.ExchangeCoin>
                {
                    new ExchangeCoin
                    {
                        CoinBuyList = coinBuyList.Where(c => c.Pair.Equals("BTCUSDT") && c.Exchange == Business.Entities.Exchange.Binance).ToList(),
                        Exchange = Business.Entities.Exchange.Binance, Quantity = .0014M
                    },
                    new ExchangeCoin
                    {
                        CoinBuyList = coinBuyList.Where(c => c.Pair.Equals("BTCUSDT") && c.Exchange == Business.Entities.Exchange.Bittrex).ToList(),
                        Exchange = Business.Entities.Exchange.Bittrex, Quantity = .0013M
                    },
                    new ExchangeCoin
                    {
                        CoinBuyList = coinBuyList.Where(c => c.Pair.Equals("ETHUSDT") && c.Exchange == Business.Entities.Exchange.Binance).ToList(),
                        Exchange = Business.Entities.Exchange.Binance, Quantity = .55M
                    },
                    new ExchangeCoin
                    {
                        CoinBuyList = coinBuyList.Where(c => c.Pair.Equals("XLMBTC") && c.Exchange == Business.Entities.Exchange.Binance).ToList(),
                        Exchange = Business.Entities.Exchange.Binance, Quantity = 399
                    },
                    new ExchangeCoin
                    {
                        CoinBuyList = coinBuyList.Where(c => c.Pair.Equals("NEOBTC") && c.Exchange == Business.Entities.Exchange.Binance).ToList(),
                        Exchange = Business.Entities.Exchange.Binance, Quantity = 6.5M
                    },
                    new ExchangeCoin
                    {
                        CoinBuyList = coinBuyList.Where(c => c.Pair.Equals("NEOBTC") && c.Exchange == Business.Entities.Exchange.Bittrex).ToList(),
                        Exchange = Business.Entities.Exchange.Bittrex, Quantity = 8.0M
                    },
                    new ExchangeCoin
                    {
                        CoinBuyList = coinBuyList.Where(c => c.Pair.Equals("POLYBTC") && c.Exchange == Business.Entities.Exchange.Binance).ToList(),
                        Exchange = Business.Entities.Exchange.Binance, Quantity = 902M
                    }
                };

                var coins = new List<Business.Contracts.Portfolio.Coin>
                {
                    new Business.Contracts.Portfolio.Coin
                    {
                        CoinId =0, Name = "Bitcoin", Symbol = "BTC" , CurrentPrice = 6379.44M, Percent24Hr = 0.47, Percent7D = -0.5,
                        ExchangeCoinList = exchangeCoins.Where(e => e.CoinBuyList.Any(c => c.Pair.StartsWith("BTC"))).ToList()
                    },
                    new Business.Contracts.Portfolio.Coin
                    {
                        CoinId =1, Name = "Ethereum", Symbol = "ETH", CurrentPrice = 242.46M, Percent24Hr = 1.7, Percent7D = 2.5,
                        ExchangeCoinList = exchangeCoins.Where(e => e.CoinBuyList.Any(c => c.Pair.StartsWith("ETH"))).ToList()
                    },
                    new Business.Contracts.Portfolio.Coin
                    {
                        CoinId =2, Name = "Stellar", Symbol = "XLM", CurrentPrice = 0.00001042M, Percent24Hr = 0.00, Percent7D = -1.5,
                        ExchangeCoinList = exchangeCoins.Where(e => e.CoinBuyList.Any(c => c.Pair.StartsWith("XLM"))).ToList()
                    },
                    new Business.Contracts.Portfolio.Coin
                    {
                        CoinId =3, Name = "NEO", Symbol = "NEO", CurrentPrice = 0.0019M, Percent24Hr = -7.47, Percent7D = -0.5,
                        ExchangeCoinList = exchangeCoins.Where(e => e.CoinBuyList.Any(c => c.Pair.StartsWith("NEO"))).ToList()
                    },
                    new Business.Contracts.Portfolio.Coin
                    {
                        CoinId =4, Name = "Polymath", Symbol = "POLY", CurrentPrice = 0.00000570M, Percent24Hr = 20.47, Percent7D = 10.5,
                        ExchangeCoinList = exchangeCoins.Where(e => e.CoinBuyList.Any(c => c.Pair.StartsWith("POLY"))).ToList()
                    },
                };

                coinList = coins;
            }

            return coinList;
        }

        public async static Task<Business.Contracts.Portfolio.Coin> GetCoin(string symbol)
        {
            return GetCoins().Result.Where(c => c.Symbol.Equals(symbol)).FirstOrDefault();
        }

        public async static Task<Business.Contracts.Portfolio.Coin> GetCoin(int id)
        {
            return GetCoins().Result.Where(c => c.CoinId == id).FirstOrDefault();
        }

        public async static Task AddCoin(Business.Contracts.Portfolio.Coin coin)
        {
            coinList.Add(coin);
        }

        public async static Task<List<Business.Contracts.Portfolio.WatchList>> GetWatchListCoins()
        {
            if (watchListCoins == null || watchListCoins.Count == 0)
            {
                var coins = new List<Business.Contracts.Portfolio.WatchList>
                {
                    new Business.Contracts.Portfolio.WatchList { CurrencyId = 1, Name = "Bitcoin", Symbol = "BTC", Watchers = new Watcher[] { new Watcher { Created = DateTime.UtcNow.AddDays(-1), Enabled = true, Exchange = Business.Entities.Exchange.Binance, WatcherId = 1, Price = 6199.0M, Pair = "BTCUSDT" } }},
                    new Business.Contracts.Portfolio.WatchList { CurrencyId = 2, Name = "Ethereum", Symbol = "ETH", Watchers = new Watcher[] { new Watcher { Created = DateTime.UtcNow.AddDays(-1), Enabled = true, Exchange = Business.Entities.Exchange.Binance, WatcherId = 2, Price = 185.0M, Pair = "ETHUSDT" } }},
                    new Business.Contracts.Portfolio.WatchList { CurrencyId = 3, Name = "Stellar", Symbol = "XLM", Watchers = new Watcher[] { new Watcher { Created = DateTime.UtcNow.AddDays(-4), Enabled = true, Exchange = Business.Entities.Exchange.Binance, WatcherId = 3, Price = 0.00002900M, Pair = "XLMBTC", Hit = DateTime.UtcNow.AddHours(-20) } }},
                    new Business.Contracts.Portfolio.WatchList { CurrencyId = 4, Name = "NEO", Symbol = "NEO", Watchers = new Watcher[] { new Watcher { Created = DateTime.UtcNow.AddDays(-2), Enabled = true, Exchange = Business.Entities.Exchange.Binance, WatcherId = 4, Price = 0.001999M, Pair = "NEOBTC" } }},
                };

                watchListCoins = coins;
            }

            return watchListCoins;
        }

        public async static Task<Business.Contracts.Portfolio.WatchList> GetWatchListCoins(string symbol)
        {
            return GetWatchListCoins().Result.Where(w => w.Symbol.Equals(symbol)).FirstOrDefault();
        }

        public async static Task<Business.Contracts.Portfolio.WatchList> GetWatchListCoins(int id)
        {
            return GetWatchListCoins().Result.Where(w => w.CurrencyId == id).FirstOrDefault();
        }

        public async static Task AddWatchListCoin(Business.Contracts.Portfolio.WatchList coin)
        {
            watchListCoins.Add(coin);
        }

        public async static Task<List<ExchangeTransaction>> GetTransactions()
        {
            if (transactions == null || transactions.Count == 0)
            {
                var transactionList = new List<ExchangeTransaction>
                {
                    new ExchangeTransaction { Currency = new Currency{ CurrencyId = 1, Name = "Bitcoin", Symbol = "BTC" }, Exchange = Business.Entities.Exchange.Binance, FillDate = DateTime.UtcNow.AddDays(-2), Pair = "BTCUSDT", Price = 6700, Quantity = .01M, Side = Business.Entities.Side.Buy, TransactionId = "1" },
                    new ExchangeTransaction { Currency = new Currency{ CurrencyId = 1, Name = "Bitcoin", Symbol = "BTC" }, Exchange = Business.Entities.Exchange.Binance, FillDate = DateTime.UtcNow.AddDays(-1), Pair = "BTCUSDT", Price = 6500, Quantity = .01M, Side = Business.Entities.Side.Buy, TransactionId = "2" },
                    new ExchangeTransaction { Currency = new Currency{ CurrencyId = 1, Name = "Ethereum", Symbol = "ETH" }, Exchange = Business.Entities.Exchange.Binance, FillDate = DateTime.UtcNow.AddDays(-1), Pair = "ETHUSDT", Price = 199.0M, Quantity = 1, Side = Business.Entities.Side.Buy, TransactionId = "3" },
                    new ExchangeTransaction { Currency = new Currency{ CurrencyId = 1, Name = "Stellar", Symbol = "XLM" }, Exchange = Business.Entities.Exchange.Binance, FillDate = DateTime.UtcNow.AddDays(-1), Pair = "XLMBTC", Price = 0.00002570M, Quantity = 500M, Side = Business.Entities.Side.Buy, TransactionId = "4" },
                    new ExchangeTransaction { Currency = new Currency{ CurrencyId = 1, Name = "Stellar", Symbol = "XLM" }, Exchange = Business.Entities.Exchange.Binance, FillDate = DateTime.UtcNow.AddDays(-1), Pair = "XLMBTC", Price = 0.00001957M, Quantity = 1000M, Side = Business.Entities.Side.Buy, TransactionId = "5" },
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
            return GetTransactions().Result.Where(w => w.Currency.CurrencyId == id).ToList();
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