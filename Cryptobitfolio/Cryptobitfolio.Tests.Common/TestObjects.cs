using Cryptobitfolio.Business;
using System;
using System.Collections.Generic;

namespace Cryptobitfolio.Tests.Common
{
    public class TestObjects
    {

        public TestObjects()
        {
        }

        #region ExchangeHub Objects

        public string GetExchangeHubExchange()
        {
            return "Binance";
        }

        public IEnumerable<ExchangeHub.Contracts.Balance> GetExchangeHubBalances()
        {
            var balances = new List<ExchangeHub.Contracts.Balance>();

            balances.Add(new ExchangeHub.Contracts.Balance
            {
                Available = 0.001M,
                Frozen = 0.024M,
                Symbol = "BTC"
            });
            balances.Add(new ExchangeHub.Contracts.Balance
            {
                Available = 2.5M,
                Frozen = 1.01M,
                Symbol = "ETH"
            });
            balances.Add(new ExchangeHub.Contracts.Balance
            {
                Available = 1000M,
                Frozen = 0M,
                Symbol = "XLM"
            });
            balances.Add(new ExchangeHub.Contracts.Balance
            {
                Available = 250M,
                Frozen = 0M,
                Symbol = "NANO"
            });
            balances.Add(new ExchangeHub.Contracts.Balance
            {
                Available = 10250M,
                Frozen = 0M,
                Symbol = "RVN"
            });

            return balances;
        }

        public IEnumerable<string> GetExchangeHubMarkets()
        {
            var markets = new List<string>();
            markets.Add("BTCUSDT");
            markets.Add("XLMBTC");
            markets.Add("NANOBTC");
            markets.Add("ETHBTC");
            markets.Add("XLMETH");
            markets.Add("NANOETH");
            markets.Add("ETHUSDT");
            markets.Add("XLMUSDT");

            return markets;
        }

        public IEnumerable<ExchangeHub.Contracts.OrderResponse> GetExchangeHubOrders()
        {
            var orders = new List<ExchangeHub.Contracts.OrderResponse>();

            orders.Add(new ExchangeHub.Contracts.OrderResponse
            {
                FilledQuantity = 0.005M,
                OrderId = "1",
                OrderQuantity = 0.005M,
                OrderStatus = ExchangeHub.Contracts.OrderStatus.Filled,
                Pair = "BTCUSDT",
                Price = 4100.00M,
                Side = ExchangeHub.Contracts.Side.Buy,
                StopPrice = 0.0M,
                TransactTime = DateTime.UtcNow.AddHours(-56)
            });
            orders.Add(new ExchangeHub.Contracts.OrderResponse
            {
                FilledQuantity = 0.011M,
                OrderId = "2",
                OrderQuantity = 0.011M,
                OrderStatus = ExchangeHub.Contracts.OrderStatus.Filled,
                Pair = "BTCUSDT",
                Price = 7423.00M,
                Side = ExchangeHub.Contracts.Side.Buy,
                StopPrice = 0.0M,
                TransactTime = DateTime.UtcNow.AddHours(-124)
            });
            orders.Add(new ExchangeHub.Contracts.OrderResponse
            {
                FilledQuantity = 500M,
                OrderId = "3",
                OrderQuantity = 500M,
                OrderStatus = ExchangeHub.Contracts.OrderStatus.Filled,
                Pair = "XLMBTC",
                Price = 0.00003445M,
                Side = ExchangeHub.Contracts.Side.Buy,
                StopPrice = 0.0M,
                TransactTime = DateTime.UtcNow.AddHours(-5)
            });
            orders.Add(new ExchangeHub.Contracts.OrderResponse
            {
                FilledQuantity = 3.51M,
                OrderId = "4",
                OrderQuantity = 3.51M,
                OrderStatus = ExchangeHub.Contracts.OrderStatus.Filled,
                Pair = "ETHUSDT",
                Price = 107.457M,
                Side = ExchangeHub.Contracts.Side.Buy,
                StopPrice = 0.0M,
                TransactTime = DateTime.UtcNow.AddHours(-22)
            });
            orders.Add(new ExchangeHub.Contracts.OrderResponse
            {
                FilledQuantity = 0.75M,
                OrderId = "19",
                OrderQuantity = 0.75M,
                OrderStatus = ExchangeHub.Contracts.OrderStatus.Filled,
                Pair = "ETHBTC",
                Price = 0.0451M,
                Side = ExchangeHub.Contracts.Side.Buy,
                StopPrice = 0.0M,
                TransactTime = DateTime.UtcNow.AddHours(-122)
            });
            orders.Add(new ExchangeHub.Contracts.OrderResponse
            {
                FilledQuantity = 250M,
                OrderId = "5",
                OrderQuantity = 250M,
                OrderStatus = ExchangeHub.Contracts.OrderStatus.Filled,
                Pair = "XLMBTC",
                Price = 0.00004478M,
                Side = ExchangeHub.Contracts.Side.Buy,
                StopPrice = 0.0M,
                TransactTime = DateTime.UtcNow.AddHours(-201)
            });
            orders.Add(new ExchangeHub.Contracts.OrderResponse
            {
                FilledQuantity = 250M,
                OrderId = "6",
                OrderQuantity = 250M,
                OrderStatus = ExchangeHub.Contracts.OrderStatus.Filled,
                Pair = "XLMBTC",
                Price = 0.00002278M,
                Side = ExchangeHub.Contracts.Side.Buy,
                StopPrice = 0.0M,
                TransactTime = DateTime.UtcNow.AddHours(-271)
            });
            orders.Add(new ExchangeHub.Contracts.OrderResponse
            {
                FilledQuantity = 50M,
                OrderId = "7",
                OrderQuantity = 50M,
                OrderStatus = ExchangeHub.Contracts.OrderStatus.Filled,
                Pair = "NANOBTC",
                Price = 0.0002278M,
                Side = ExchangeHub.Contracts.Side.Buy,
                StopPrice = 0.0M,
                TransactTime = DateTime.UtcNow.AddHours(-21)
            });
            orders.Add(new ExchangeHub.Contracts.OrderResponse
            {
                FilledQuantity = 50M,
                OrderId = "8",
                OrderQuantity = 50M,
                OrderStatus = ExchangeHub.Contracts.OrderStatus.Filled,
                Pair = "NANOBTC",
                Price = 0.0005545M,
                Side = ExchangeHub.Contracts.Side.Buy,
                StopPrice = 0.0M,
                TransactTime = DateTime.UtcNow.AddHours(-145)
            });
            orders.Add(new ExchangeHub.Contracts.OrderResponse
            {
                FilledQuantity = 50M,
                OrderId = "9",
                OrderQuantity = 50M,
                OrderStatus = ExchangeHub.Contracts.OrderStatus.Filled,
                Pair = "NANOBTC",
                Price = 0.0003978M,
                Side = ExchangeHub.Contracts.Side.Buy,
                StopPrice = 0.0M,
                TransactTime = DateTime.UtcNow.AddHours(-122)
            });

            return orders;
        }

        public IEnumerable<ExchangeHub.Contracts.OrderResponse> GetExchangeHubOpenOrders()
        {
            var orders = new List<ExchangeHub.Contracts.OrderResponse>();

            orders.Add(new ExchangeHub.Contracts.OrderResponse
            {
                FilledQuantity = 0,
                OrderId = "10",
                OrderQuantity = 0.15M,
                OrderStatus = ExchangeHub.Contracts.OrderStatus.Open,
                Pair = "BTCUSDT",
                Price = 6400.00M,
                Side = ExchangeHub.Contracts.Side.Buy,
                StopPrice = 0.0M,
                TransactTime = DateTime.UtcNow.AddHours(-56)
            });
            orders.Add(new ExchangeHub.Contracts.OrderResponse
            {
                FilledQuantity = 0.011M,
                OrderId = "11",
                OrderQuantity = 0.21M,
                OrderStatus = ExchangeHub.Contracts.OrderStatus.PartialFill,
                Pair = "BTCUSDT",
                Price = 5724.00M,
                Side = ExchangeHub.Contracts.Side.Buy,
                StopPrice = 0.0M,
                TransactTime = DateTime.UtcNow.AddHours(-124)
            });
            orders.Add(new ExchangeHub.Contracts.OrderResponse
            {
                FilledQuantity = 0,
                OrderId = "12",
                OrderQuantity = 500M,
                OrderStatus = ExchangeHub.Contracts.OrderStatus.Open,
                Pair = "XLMBTC",
                Price = 0.00002950M,
                Side = ExchangeHub.Contracts.Side.Buy,
                StopPrice = 0.0M,
                TransactTime = DateTime.UtcNow.AddHours(-5)
            });
            orders.Add(new ExchangeHub.Contracts.OrderResponse
            {
                FilledQuantity = 0.6M,
                OrderId = "13",
                OrderQuantity = 1.5M,
                OrderStatus = ExchangeHub.Contracts.OrderStatus.PartialFill,
                Pair = "ETHUSDT",
                Price = 215.7M,
                Side = ExchangeHub.Contracts.Side.Buy,
                StopPrice = 0.0M,
                TransactTime = DateTime.UtcNow.AddHours(-22)
            });
            orders.Add(new ExchangeHub.Contracts.OrderResponse
            {
                FilledQuantity = 0,
                OrderId = "14",
                OrderQuantity = 750M,
                OrderStatus = ExchangeHub.Contracts.OrderStatus.Open,
                Pair = "XLMBTC",
                Price = 0.00003248M,
                Side = ExchangeHub.Contracts.Side.Buy,
                StopPrice = 0.0M,
                TransactTime = DateTime.UtcNow.AddHours(-201)
            });
            orders.Add(new ExchangeHub.Contracts.OrderResponse
            {
                FilledQuantity = 0,
                OrderId = "15",
                OrderQuantity = 1000,
                OrderStatus = ExchangeHub.Contracts.OrderStatus.Open,
                Pair = "XLMBTC",
                Price = 0.00001997M,
                Side = ExchangeHub.Contracts.Side.Buy,
                StopPrice = 0.0M,
                TransactTime = DateTime.UtcNow.AddHours(-271)
            });
            orders.Add(new ExchangeHub.Contracts.OrderResponse
            {
                FilledQuantity = 0,
                OrderId = "16",
                OrderQuantity = 200,
                OrderStatus = ExchangeHub.Contracts.OrderStatus.Open,
                Pair = "NANOBTC",
                Price = 0.0001991M,
                Side = ExchangeHub.Contracts.Side.Buy,
                StopPrice = 0.0M,
                TransactTime = DateTime.UtcNow.AddHours(-21)
            });
            orders.Add(new ExchangeHub.Contracts.OrderResponse
            {
                FilledQuantity = 0,
                OrderId = "17",
                OrderQuantity = 3000,
                OrderStatus = ExchangeHub.Contracts.OrderStatus.Open,
                Pair = "NANOBTC",
                Price = 0.0001744M,
                Side = ExchangeHub.Contracts.Side.Buy,
                StopPrice = 0.0M,
                TransactTime = DateTime.UtcNow.AddHours(-145)
            });
            orders.Add(new ExchangeHub.Contracts.OrderResponse
            {
                FilledQuantity = 55,
                OrderId = "18",
                OrderQuantity = 125,
                OrderStatus = ExchangeHub.Contracts.OrderStatus.PartialFill,
                Pair = "NANOBTC",
                Price = 0.0004247M,
                Side = ExchangeHub.Contracts.Side.Buy,
                StopPrice = 0.0M,
                TransactTime = DateTime.UtcNow.AddHours(-122)
            });

            return orders;
        }

        #endregion ExchangeHub Objects

        #region CoinMarketCap Objects

        public Dictionary<string, CoinMarketCap.Net.Contracts.Metadata> GetCoinMarketCapMetaData()
        {
            var dictionary = new Dictionary<string, CoinMarketCap.Net.Contracts.Metadata>();
            dictionary.Add("BTC",
                new CoinMarketCap.Net.Contracts.Metadata
                {
                    Id = 1,
                    Logo = "link",
                    Name = "Bitcoin",
                    Slug = "bitcoin",
                    Symbol = "BTC"
                });
            dictionary.Add("ETH",
                new CoinMarketCap.Net.Contracts.Metadata
                {
                    Id = 2,
                    Logo = "link",
                    Name = "Ethereum",
                    Slug = "ethereum",
                    Symbol = "ETH"
                });
            dictionary.Add("XLM",
                new CoinMarketCap.Net.Contracts.Metadata
                {
                    Id = 3,
                    Logo = "link",
                    Name = "Stellar",
                    Slug = "stellar",
                    Symbol = "XLM"
                });
            dictionary.Add("NANO",
                new CoinMarketCap.Net.Contracts.Metadata
                {
                    Id = 4,
                    Logo = "link",
                    Name = "Nano",
                    Slug = "nano",
                    Symbol = "NANO"
                });

            return dictionary;
        }

        #endregion CoinMarketCap Objects

        #region Contract Objects

        public List<Business.Contracts.Portfolio.CoinBuy> GetContractCoinBuyList()
        {
            var list = new List<Business.Contracts.Portfolio.CoinBuy>
            {
                new Business.Contracts.Portfolio.CoinBuy
                {
                    ExchangeName = Business.Entities.Exchange.Binance,
                    Id = "1",
                    Pair = "BTCUSDT",
                    Price = 4100.00M,
                    Quantity = 0.005M
                },
                new Business.Contracts.Portfolio.CoinBuy
                {
                    ExchangeName = Business.Entities.Exchange.Binance,
                    Id = "2",
                    Pair = "BTCUSDT",
                    Price = 7423.00M,
                    Quantity = 0.005M
                }
            };

            return list;
        }

        public List<Business.Contracts.Trade.ExchangeOrder> GetContractExchangeOrderList()
        {
            var list = new List<Business.Contracts.Trade.ExchangeOrder>
            {
                new Business.Contracts.Trade.ExchangeOrder
                {
                    Exchange = Business.Entities.Exchange.Binance,
                    FilledQuantity = 0,
                    OrderId = "10",
                    Pair = "BTCUSTD",
                    Price = 6400.00M,
                    Side = Business.Entities.Side.Buy,
                    Quantity = 0.15M
                },
                new Business.Contracts.Trade.ExchangeOrder
                {
                    Exchange = Business.Entities.Exchange.Binance,
                    FilledQuantity = 0.011M,
                    OrderId = "11",
                    Pair = "BTCUSTD",
                    Price = 5724.00M,
                    Side = Business.Entities.Side.Buy,
                    Quantity = 0.21M
                }
            };

            return list;
        }

        public IEnumerable<Business.Contracts.Portfolio.Currency> GetCurrencies()
        {
            var currencies = new List<Business.Contracts.Portfolio.Currency>()
            {
                new Business.Contracts.Portfolio.Currency
                {
                    Id = 1,
                    Image = "image.jpg",
                    Name = "Bitcoin",
                    Symbol = "BTC"
                },
                new Business.Contracts.Portfolio.Currency
                {
                    Id = 2,
                    Image = "image.jpg",
                    Name = "Ethereum",
                    Symbol = "ETH"
                },
                new Business.Contracts.Portfolio.Currency
                {
                    Id = 3,
                    Image = "image.jpg",
                    Name = "Stellar",
                    Symbol = "XLM"
                },
                new Business.Contracts.Portfolio.Currency
                {
                    Id = 4,
                    Image = "image.jpg",
                    Name = "Nano",
                    Symbol = "NANO"
                }
            };

            return currencies;
        }

        #endregion Contract Objects
    }
}
