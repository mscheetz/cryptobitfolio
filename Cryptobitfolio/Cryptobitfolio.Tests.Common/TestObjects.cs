using Cryptobitfolio.Business;
using System;
using System.Collections.Generic;
using System.Linq;

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
                    Exchange = Business.Entities.Exchange.Binance,
                    CoinBuyId = 1,
                    Pair = "BTCUSDT",
                    Price = 4100.00M,
                    Quantity = 0.005M
                },
                new Business.Contracts.Portfolio.CoinBuy
                {
                    Exchange = Business.Entities.Exchange.Binance,
                    CoinBuyId = 2,
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
                    CurrencyId = 1,
                    Image = "image.jpg",
                    Name = "Bitcoin",
                    Symbol = "BTC"
                },
                new Business.Contracts.Portfolio.Currency
                {
                    CurrencyId = 2,
                    Image = "image.jpg",
                    Name = "Ethereum",
                    Symbol = "ETH"
                },
                new Business.Contracts.Portfolio.Currency
                {
                    CurrencyId = 3,
                    Image = "image.jpg",
                    Name = "Stellar",
                    Symbol = "XLM"
                },
                new Business.Contracts.Portfolio.Currency
                {
                    CurrencyId = 4,
                    Image = "image.jpg",
                    Name = "Nano",
                    Symbol = "NANO"
                }
            };

            return currencies;
        }

        public IEnumerable<Business.Contracts.Trade.ExchangeApi> GetContractExchangeApis()
        {
            var apis = new List<Business.Contracts.Trade.ExchangeApi>();

            apis.Add(new Business.Contracts.Trade.ExchangeApi
            {
                ApiKey = "asdfasfd",
                ApiKeyName = "Binance Api 1",
                ApiSecret = "secret value",
                Exchange = Business.Entities.Exchange.Binance,
                ExchangeApiId = 1
            });
            apis.Add(new Business.Contracts.Trade.ExchangeApi
            {
                ApiKey = "asdfasfd",
                ApiKeyName = "Binance Api 2",
                ApiSecret = "secret value",
                Exchange = Business.Entities.Exchange.Binance,
                ExchangeApiId = 2
            });
            apis.Add(new Business.Contracts.Trade.ExchangeApi
            {
                ApiKey = "asdfasfd",
                ApiKeyName = "Bittrex Api 1",
                ApiSecret = "secret value",
                Exchange = Business.Entities.Exchange.Bittrex,
                ExchangeApiId = 3
            });
            apis.Add(new Business.Contracts.Trade.ExchangeApi
            {
                ApiExtra = "extra value",
                ApiKey = "asdfasfd",
                ApiKeyName = "Binance Api 1",
                ApiSecret = "secret value",
                Exchange = Business.Entities.Exchange.CoinbasePro,
                ExchangeApiId = 4
            });

            return apis;
        }

        public IEnumerable<Business.Contracts.Portfolio.Alerter> GetAlertContracts()
        {
            var alerts = new List<Business.Contracts.Portfolio.Alerter>
            {
                new Business.Contracts.Portfolio.Alerter
                {
                    AlertId = 1,
                    Created = DateTime.UtcNow,
                    Direction = Business.Entities.Direction.LTE,
                    Enabled = true,
                    Exchange = Business.Entities.Exchange.Binance,
                    HistoricalPrices = new Business.Contracts.Trade.HistoricalPrice[0],
                    Hit = null,
                    Pair = "BTCUSDT",
                    Price = 2875.4M
                },
                new Business.Contracts.Portfolio.Alerter
                {
                    AlertId = 2,
                    Created = DateTime.UtcNow.AddDays(-2),
                    Direction = Business.Entities.Direction.GTE,
                    Enabled = true,
                    Exchange = Business.Entities.Exchange.Binance,
                    HistoricalPrices = new Business.Contracts.Trade.HistoricalPrice[0],
                    Hit = DateTime.UtcNow.AddHours(-2),
                    Pair = "BTCUSDT",
                    Price = 3879.4M
                }
            };

            return alerts;
        }

        #endregion Contract Objects

        #region Entity Objects

        public IEnumerable<Business.Entities.Trade.ExchangeApi> GetEntityExchangeApis()
        {
            var apis = new List<Business.Entities.Trade.ExchangeApi>();

            apis.Add(new Business.Entities.Trade.ExchangeApi {
                ApiKey = "asdfasfd",
                ApiKeyName = "Binance Api 1",
                ApiSecret = "secret value",
                Created = DateTime.UtcNow,
                Exchange = Business.Entities.Exchange.Binance,
                Id = 1
            });
            apis.Add(new Business.Entities.Trade.ExchangeApi
            {
                ApiKey = "asdfasfd",
                ApiKeyName = "Binance Api 2",
                ApiSecret = "secret value",
                Created = DateTime.UtcNow,
                Exchange = Business.Entities.Exchange.Binance,
                Id = 2
            });
            apis.Add(new Business.Entities.Trade.ExchangeApi
            {
                ApiKey = "asdfasfd",
                ApiKeyName = "Bittrex Api 1",
                ApiSecret = "secret value",
                Created = DateTime.UtcNow,
                Exchange = Business.Entities.Exchange.Bittrex,
                Id = 3
            });
            apis.Add(new Business.Entities.Trade.ExchangeApi
            {
                ApiExtra = "extra value",
                ApiKey = "asdfasfd",
                ApiKeyName = "Binance Api 1",
                ApiSecret = "secret value",
                Created = DateTime.UtcNow,
                Exchange = Business.Entities.Exchange.CoinbasePro,
                Id = 4
            });

            return apis;
        }

        public IEnumerable<Business.Entities.Trade.ExchangeApi> GetEntityExchangeApis(Business.Entities.Exchange exchange)
        {
            return GetEntityExchangeApis().Where(e => e.Exchange == exchange);
        }

        public Business.Entities.Trade.ExchangeApi GetUpdatedExchangeApi()
        {
            return new Business.Entities.Trade.ExchangeApi
            {
                ApiKey = "asdfasfd",
                ApiKeyName = "Updated Binance Api 1",
                ApiSecret = "secret value",
                Created = DateTime.UtcNow,
                Exchange = Business.Entities.Exchange.Binance,
                Id = 1
            };
        }

        public IEnumerable<Business.Entities.Portfolio.Coin> GetCoinEntities()
        {
            var coins = new List<Business.Entities.Portfolio.Coin>
            {
                new Business.Entities.Portfolio.Coin
                {
                    AverageBuy = 4598.3M,
                    CurrencyId = 1,
                    Id = 1,
                    Quantity = 0.5M,
                    Symbol = "BTC"
                },
                new Business.Entities.Portfolio.Coin
                {
                    AverageBuy = 149.0M,
                    CurrencyId = 2,
                    Id = 2,
                    Quantity = 2.3M,
                    Symbol = "ETH"
                }
            };

            return coins;
        }

        public IEnumerable<Business.Entities.Portfolio.ExchangeCoin> GetExchangeCoinEntites()
        {

            var coins = new List<Business.Entities.Portfolio.ExchangeCoin>
            {
                new Business.Entities.Portfolio.ExchangeCoin
                {
                    AverageBuy = 4598.3M,
                    CurrencyId = 1,
                    Exchange = Business.Entities.Exchange.Binance,
                    Id = 1,
                    Quantity = 0.5M,
                    Symbol = "BTC"
                },
                new Business.Entities.Portfolio.ExchangeCoin
                {
                    AverageBuy = 149.0M,
                    CurrencyId = 2,
                    Exchange = Business.Entities.Exchange.Binance,
                    Id = 2,
                    Quantity = 2.3M,
                    Symbol = "ETH"
                }
            };
            
            return coins;
        }
        
        public IEnumerable<Business.Entities.Portfolio.Alerter> GetAlertEntities()
        {
            var alerts = new List<Business.Entities.Portfolio.Alerter>
            {
                new Business.Entities.Portfolio.Alerter
                {
                    Created = DateTime.UtcNow,
                    CurrencyId = 1,
                    Direction = Business.Entities.Direction.LTE,
                    Enabled = true,
                    Exchange = Business.Entities.Exchange.Binance,
                    Hit = null,
                    Id = 1,
                    Pair = "BTCUSDT",
                    Price = 3215.98M
                },
                new Business.Entities.Portfolio.Alerter
                {
                    Created = DateTime.UtcNow.AddDays(-10),
                    CurrencyId = 1,
                    Direction = Business.Entities.Direction.LTE,
                    Enabled = true,
                    Exchange = Business.Entities.Exchange.Binance,
                    Hit = DateTime.UtcNow.AddDays(-11),
                    Id = 2,
                    Pair = "BTCUSDT",
                    Price = 3356.78M
                }
            };

            return alerts;
        }
        
        public IEnumerable<Business.Entities.Portfolio.ArbitragePath> GetAribtragePathEntities()
        {
            var path = new List<Business.Entities.Portfolio.ArbitragePath>
            {
                new Business.Entities.Portfolio.ArbitragePath
                {
                    Created = DateTime.UtcNow,
                    Id = 1,
                    Path = "",
                },
                new Business.Entities.Portfolio.ArbitragePath
                {
                    Created = DateTime.UtcNow,
                    Id = 2,
                    Path = "",
                }
            };

            return path;
        }

        public IEnumerable<Business.Entities.Portfolio.CoinBuy> GetCoinBuyEntities()
        {
            var coins = new List<Business.Entities.Portfolio.CoinBuy>
            {
                new Business.Entities.Portfolio.CoinBuy
                {
                    BTCPrice = 3798.4M,
                    ClosedDate = DateTime.UtcNow,
                    CoinBuyId = 1,
                    CoinId = 1,
                    CurrencyId = 1,
                    Exchange = Business.Entities.Exchange.Binance,
                    FilledQuantity = 0.12M,
                    Id = 1,
                    OrderId = "0001",
                    Pair = "BTCUSDT",
                    PlaceDate = DateTime.UtcNow,
                    Price = 3798.4M,
                    Quantity = 0.12M,
                    QuantityApplied = 0.12M,
                    Side = Business.Entities.Side.Buy,
                    Status = Business.Entities.TradeStatus.Filled
                },
                new Business.Entities.Portfolio.CoinBuy
                {
                    BTCPrice = 3798.4M,
                    ClosedDate = DateTime.UtcNow,
                    CoinBuyId = 2,
                    CoinId = 1,
                    CurrencyId = 1,
                    Exchange = Business.Entities.Exchange.Binance,
                    FilledQuantity = 0.5M,
                    Id = 2,
                    OrderId = "0002",
                    Pair = "BTCUSDT",
                    PlaceDate = DateTime.UtcNow,
                    Price = 5767.34M,
                    Quantity = 0.5M,
                    QuantityApplied = 0.5M,
                    Side = Business.Entities.Side.Buy,
                    Status = Business.Entities.TradeStatus.Filled
                }
            };

            return coins;
        }

        public IEnumerable<Business.Entities.Portfolio.Currency> GetCurrencyEntities()
        {
            var currencies = new List<Business.Entities.Portfolio.Currency>
            {
                new Business.Entities.Portfolio.Currency
                {
                    Id = 1,
                    Image = "btc.png",
                    Name = "Bitcoin",
                    Symbol = "BTC"
                },
                new Business.Entities.Portfolio.Currency
                {
                    Id = 2,
                    Image = "eth.png",
                    Name = "Ethereum",
                    Symbol = "ETH"
                },
                new Business.Entities.Portfolio.Currency
                {
                    Id = 3,
                    Image = "xlm.png",
                    Name = "Stellar Lumens",
                    Symbol = "XLM"
                },
            };

            return currencies;
        }

        public IEnumerable<Business.Entities.Portfolio.Watcher> GetWatcherEntities()
        {
            var watchers = new List<Business.Entities.Portfolio.Watcher>
            {
                new Business.Entities.Portfolio.Watcher
                {
                    CurrencyId = 1,
                    DateAdded = DateTime.UtcNow,
                    Enabled = true,
                    Exchange = Business.Entities.Exchange.Binance,
                    Id = 1,
                    Pair = "BTCUSDT",
                    High1Hr = 3765.4M,
                    Low1Hr = 3749.4M,
                    High24Hr = 3848.7M,
                    Low24Hr = 3749.4M,
                    High7Day = 4287.4M,
                    Low7Day = 31459M
                }
            };

            return watchers;
        }

        public IEnumerable<Business.Entities.Trade.HistoricalPrice> GetHistoricalPriceEntities()
        {
            var prices = new List<Business.Entities.Trade.HistoricalPrice>
            {
                new Business.Entities.Trade.HistoricalPrice
                {
                    CurrencyId = 1,
                    Exchange = Business.Entities.Exchange.Binance,
                    High = 3765.4M,
                    Id = 1,
                    Low = 3749.4M,
                    Pair = "BTCUSDT",
                    Price = 3755.4M,
                    Snapshot = DateTime.UtcNow.AddHours(-1)
                },
                new Business.Entities.Trade.HistoricalPrice
                {
                    CurrencyId = 1,
                    Exchange = Business.Entities.Exchange.Binance,
                    High = 3765.4M,
                    Id = 2,
                    Low = 3749.4M,
                    Pair = "BTCUSDT",
                    Price = 3755.4M,
                    Snapshot = DateTime.UtcNow.AddHours(-2)
                }
            };

            return prices;
        }

        #endregion Entity Objects
    }
}
