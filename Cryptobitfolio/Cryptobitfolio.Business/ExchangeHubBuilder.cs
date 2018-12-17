// -----------------------------------------------------------------------------
// <copyright file="ExchangeHubBuilder" company="Matt Scheetz">
//     Copyright (c) Matt Scheetz All Rights Reserved
// </copyright>
// <author name="Matt Scheetz" date="12/11/2018 8:00:57 PM" />
// -----------------------------------------------------------------------------

namespace Cryptobitfolio.Business
{
    using Cryptobitfolio.Business.Common;
    using Cryptobitfolio.Business.Contracts.Portfolio;
    using Cryptobitfolio.Business.Contracts.Trade;
    using Cryptobitfolio.Business.Entities;
    using Cryptobitfolio.Data.Interfaces;
    using Cryptobitfolio.Data.Repositories;
    using ExchangeHub.Contracts;
    #region Usings

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Exchange = Entities.Exchange;
    using Side = Entities.Side;

    #endregion Usings

    public class ExchangeHubBuilder : IExchangeHubBuilder
    {
        #region Properties

        private IExchangeHubRepository _currentHub = null;
        private IExchangeApiBuilder _xchApiBldr;
        private string _currentExchange = string.Empty;
        private DateTime lastUpdated;
        private IEnumerable<ExchangeApi> _exchangeApis;
        //private List<ExchangeHub.ExchangeHub> exchangeHubs;
        private List<IExchangeHubRepository> _exchangeHubs;
        private List<string> currentHubMarkets;

        #endregion Properties

        /// <summary>
        /// Constructor
        /// </summary>
        public ExchangeHubBuilder(IExchangeApiBuilder exchangeApiBuilder)
        {
            this._xchApiBldr = exchangeApiBuilder;
            LoadBuilder().Wait();
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public ExchangeHubBuilder(IExchangeApiBuilder exchangeApiBuilder, bool test)
        {
            this._xchApiBldr = exchangeApiBuilder;
            LoadBuilder(test).Wait();
        }

        public async Task LoadBuilder(bool test = false)
        { 
            _exchangeApis = await _xchApiBldr.Get();
            _exchangeHubs = new List<IExchangeHubRepository>();

            if (!test)
            {
                foreach (var api in _exchangeApis)
                {
                    if (api.Exchange == Exchange.Binance)
                    {
                        _exchangeHubs.Add(new ExchangeHubRepository(ExchangeHub.Contracts.Exchange.Binance, api.ApiKey, api.ApiSecret));
                    }
                    else if (api.Exchange == Exchange.Bittrex)
                    {
                        _exchangeHubs.Add(new ExchangeHubRepository(ExchangeHub.Contracts.Exchange.Bittrex, api.ApiKey, api.ApiSecret));
                    }
                    else if (api.Exchange == Exchange.CoinbasePro)
                    {
                        _exchangeHubs.Add(new ExchangeHubRepository(ExchangeHub.Contracts.Exchange.CoinbasePro, api.ApiKey, api.ApiSecret, api.ApiExtra));
                    }
                    else if (api.Exchange == Exchange.KuCoin)
                    {
                        _exchangeHubs.Add(new ExchangeHubRepository(ExchangeHub.Contracts.Exchange.KuCoin, api.ApiKey, api.ApiSecret));
                    }
                }
                if (_currentHub == null)
                {
                    _currentHub = _exchangeHubs[0];
                    _currentExchange = _currentHub.GetExchange();
                }
            }
        }

        public bool LoadExchange(ExchangeApi exchangeApi)
        {
            var loadedHub = _exchangeHubs.Where(e => e.GetExchange().Equals(exchangeApi.Exchange.ToString()) && e.GetApiKey().Equals(exchangeApi.ApiKey)).FirstOrDefault();

            if (loadedHub == null)
            {
                if (exchangeApi.Exchange == Exchange.CoinbasePro)
                {
                    loadedHub = new ExchangeHubRepository((ExchangeHub.Contracts.Exchange)exchangeApi.Exchange, exchangeApi.ApiKey, exchangeApi.ApiSecret, exchangeApi.ApiExtra);
                }
                else if (exchangeApi.Exchange == Exchange.Switcheo)
                {
                    loadedHub = new ExchangeHubRepository((ExchangeHub.Contracts.Exchange)exchangeApi.Exchange, exchangeApi.WIF);
                }
                else
                {
                    loadedHub = new ExchangeHubRepository((ExchangeHub.Contracts.Exchange)exchangeApi.Exchange, exchangeApi.ApiKey, exchangeApi.ApiSecret);
                }
                _exchangeHubs.Add(loadedHub);
            }
            _currentExchange = loadedHub.GetExchange();
            _currentHub = loadedHub;

            return true;
            //var e = OnBuildExchangeCoins(loadedHub);
            //var o = OnBuildOrders(loadedHub);
        }

        public bool UnloadExchange(ExchangeApi exchangeApi)
        {
            var hub = _exchangeHubs.Where(e => e.GetExchange().Equals(exchangeApi.Exchange.ToString()) && e.GetApiKey().Equals(exchangeApi.ApiKey)).FirstOrDefault();
            var response = false;

            if(hub != null)
            {
                _exchangeHubs.Remove(hub);
                response = true;
            }

            return response;
        }

        public bool SetExchange(Exchange exchange)
        {
            var xchg = exchange.ToString();

            this._currentHub = _exchangeHubs.Where(e => e.GetExchange().Equals(xchg)).FirstOrDefault();
            this._currentExchange = xchg;

            return true;
        }

        public async Task<IEnumerable<string>> GetMarkets()
        {
            var markets = new List<string>();
            foreach(var hub in _exchangeHubs)
            {
                var hubMarkets = await hub.GetMarkets();
                markets.AddRange(hubMarkets);
            }

            return markets;
        }

        public async Task<Dictionary<Exchange, IEnumerable<string>>> GetAllMarkets()
        {
            var markets = new Dictionary<Exchange, IEnumerable<string>>();
            foreach (var hub in _exchangeHubs)
            {
                var exchange = hub.GetExchange();
                Exchange thisExchange;
                Enum.TryParse(exchange, out thisExchange);
                var hubMarkets = await hub.GetMarkets();
                markets.Add(thisExchange, hubMarkets);
            }

            return markets;
        }

        public async Task<IEnumerable<string>> GetExchangeMarkets()
        {
            var markets = await _currentHub.GetMarkets();

            return markets;
        }

        /// <summary>
        /// Get all markets for a given coin
        /// </summary>
        /// <param name="symbol">Symbol of currency</param>
        /// <returns>Collection of trading pairs</returns>
        public async Task<IEnumerable<string>> GetMarketsForACoin(string symbol)
        {
            var pairs = await GetMarkets();

            pairs = pairs.Where(p => p.StartsWith(symbol));

            return pairs;
        }

        public async Task<IEnumerable<string>> GetExchangeMarkets(Exchange exchange)
        {
            var hub = _exchangeHubs.Where(e => e.GetExchange() == exchange.ToString()).FirstOrDefault();

            var markets = await hub.GetMarkets();

            return markets;
        }

        public async Task<IEnumerable<ExchangeCoin>> GetBalances()
        {
            var balances = new Dictionary<Exchange, IEnumerable<Balance>>();
            
            foreach(var hub in _exchangeHubs)
            {
                var xchBal = await hub.GetBalances();
                var exchange = IHelper.StringToExchange(hub.GetExchange());
                balances.Add(exchange, xchBal);
            }

            return BalanceDictionaryToExchangeCoins(balances);
        }

        public async Task<IEnumerable<ExchangeCoin>> GetBalances(string symbol)
        {
            var balances = new Dictionary<Exchange, IEnumerable<Balance>>();

            foreach (var hub in _exchangeHubs)
            {
                var xchBal = await hub.GetBalances();
                var symbolBalance = xchBal.Where(b => b.Symbol.Equals(symbol));

                var exchange = IHelper.StringToExchange(hub.GetExchange());
                balances.Add(exchange, symbolBalance);
            }

            return BalanceDictionaryToExchangeCoins(balances);
        }

        public async Task<IEnumerable<ExchangeCoin>> GetBalances(List<string> symbols)
        {
            var balances = new Dictionary<Exchange, IEnumerable<Balance>>();

            foreach (var hub in _exchangeHubs)
            {
                var xchBal = await hub.GetBalances();
                var symbolBalance = xchBal.Where(b => symbols.Contains(b.Symbol));

                var exchange = IHelper.StringToExchange(hub.GetExchange());
                balances.Add(exchange, symbolBalance);
            }

            return BalanceDictionaryToExchangeCoins(balances);
        }

        public async Task<IEnumerable<HistoricalPrice>> GetStats(List<string> pairs)
        {
            var hpList = new List<HistoricalPrice>();

            foreach (var hub in _exchangeHubs)
            {
                var exchange = hub.GetExchange();
                var tickers = new List<Ticker>();

                foreach (var pair in pairs)
                {
                    var ticker = await hub.GetStats(pair);

                    tickers.Add(ticker);
                }

                hpList.AddRange(TickerCollectionToHistoricalPrices(tickers, IHelper.StringToExchange(exchange)));
            }

            return hpList;
        }

        public async Task<IEnumerable<ExchangeCoin>> GetExchangeBalances()
        {
            var balances = await _currentHub.GetBalances();
            var exchange = IHelper.StringToExchange(_currentExchange);

            return BalanceCollectionToExchangeCoins(balances, exchange);
        }

        public async Task<IEnumerable<ExchangeCoin>> GetExchangeBalances(Exchange exchange)
        {
            var hub = _exchangeHubs.Where(e => e.GetExchange() == exchange.ToString()).FirstOrDefault();

            var balances = await hub.GetBalances();

            return BalanceCollectionToExchangeCoins(balances, exchange);
        }

        public async Task<IEnumerable<CoinBuy>> GetOrders(string pair)
        {
            var coinBuyList = new List<CoinBuy>();
            foreach (var hub in _exchangeHubs)
            {
                var xchgOrders = await hub.GetOrders(pair);
                var xchg = hub.GetExchange();
                var coinBuys = OrderResponseCollectionToCoinBuys(xchgOrders, IHelper.StringToExchange(xchg));
                coinBuyList.AddRange(coinBuys);
            }

            return coinBuyList;
        }

        public async Task<IEnumerable<CoinBuy>> GetExchangeOrders(string pair)
        {
            var orders = await _currentHub.GetOrders(pair);

            var coinBuys = OrderResponseCollectionToCoinBuys(orders, IHelper.StringToExchange(_currentExchange));

            return coinBuys;
        }

        public async Task<IEnumerable<CoinBuy>> GetExchangeOrders(string pair, Exchange exchange)
        {
            var hub = _exchangeHubs.Where(e => e.GetExchange() == exchange.ToString()).FirstOrDefault();

            var orders = await hub.GetOrders(pair);

            return OrderResponseCollectionToCoinBuys(orders, exchange);
        }

        /// <summary>
        /// Get orders for a collection of trading pairs
        /// </summary>
        /// <param name="pairs">Trading pairs</param>
        /// <returns>Collection of OrderResponses</returns>
        public async Task<IEnumerable<CoinBuy>> GetExchangeOrders(IEnumerable<string> pairs, Exchange exchange)
        {
            var hub = _exchangeHubs.Where(e => e.GetExchange() == exchange.ToString()).FirstOrDefault();

            var orders = new List<OrderResponse>();

            foreach (var pair in pairs)
            {
                var pairOrders = await hub.GetOrders(pair);

                orders.AddRange(pairOrders);
            }

            return OrderResponseCollectionToCoinBuys(orders, exchange);
        }

        public async Task<IEnumerable<ExchangeOrder>> GetOpenOrders(string pair)
        {
            var orders = new List<ExchangeOrder>();
            foreach (var hub in _exchangeHubs)
            {
                var xchgOrders = await hub.GetOpenOrders(pair);
                orders.AddRange(OrderResponseCollectionToExchangeOrders(xchgOrders, IHelper.StringToExchange(hub.GetExchange())));
            }

            return orders;
        }

        public async Task<IEnumerable<ExchangeOrder>> GetExchangeOpenOrders(string pair)
        {
            var orders = await _currentHub.GetOpenOrders(pair);
            var exchangeOrders = OrderResponseCollectionToExchangeOrders(orders, IHelper.StringToExchange(_currentExchange));

            return exchangeOrders;
        }

        public async Task<IEnumerable<ExchangeOrder>> GetExchangeOpenOrders(string pair, Exchange exchange)
        {
            var hub = _exchangeHubs.Where(e => e.GetExchange() == exchange.ToString()).FirstOrDefault();

            var orders = await hub.GetOpenOrders(pair);
            var exchangeOrders = OrderResponseCollectionToExchangeOrders(orders, IHelper.StringToExchange(hub.GetExchange()));

            return exchangeOrders;
        }

        public async Task<IEnumerable<ExchangeOrder>> GetExchangeOpenOrdersByPairs(IEnumerable<string> pairs, Exchange exchange)
        {
            var hub = _exchangeHubs.Where(e => e.GetExchange() == exchange.ToString()).FirstOrDefault();

            var exchangeOrders = new List<ExchangeOrder>();

            foreach (var pair in pairs)
            {
                var pairOrders = await hub.GetOpenOrders(pair);
                exchangeOrders.AddRange(OrderResponseCollectionToExchangeOrders(pairOrders, IHelper.StringToExchange(hub.GetExchange())));
            }

            return exchangeOrders;
        }

        private IEnumerable<ExchangeCoin> BalanceDictionaryToExchangeCoins(Dictionary<Exchange, IEnumerable<Balance>> balances)
        {
            var exCoinList = new List<ExchangeCoin>();

            foreach (var balance in balances)
            {
                var exCoins = BalanceCollectionToExchangeCoins(balance.Value, balance.Key);

                exCoinList.AddRange(exCoins);
            }

            return exCoinList;
        }

        private IEnumerable<ExchangeCoin> BalanceCollectionToExchangeCoins(IEnumerable<Balance> balances, Exchange exchange)
        {
            var exCoinList = new List<ExchangeCoin>();

            foreach (var balance in balances)
            {
                var exCoin = BalanceToExchangeCoin(balance, exchange);

                exCoinList.Add(exCoin);
            }

            return exCoinList;
        }

        private ExchangeCoin BalanceToExchangeCoin(Balance balance, Exchange exchange)
        {
            var exCoin = new ExchangeCoin
            {
                Exchange = exchange,
                Quantity = balance.Available + balance.Frozen,
                Symbol = balance.Symbol
            };

            return exCoin;
        }

        private IEnumerable<CoinBuy> OrderResponseCollectionToCoinBuys(IEnumerable<OrderResponse> orders, Exchange exchange)
        {
            var coinBuys = new List<CoinBuy>();

            foreach (var order in orders)
            {
                var coinBuy = OrderResponseToCoinBuy(order, exchange);
                coinBuys.Add(coinBuy);
            }

            return coinBuys;
        }

        /// <summary>
        /// Create a CoinBuy from an ExchangeHub OrderResponse
        /// </summary>
        /// <param name="orderResponse">OrderResponse to convert</param>
        /// <returns>new CoinBuy object</returns>
        private CoinBuy OrderResponseToCoinBuy(OrderResponse orderResponse, Exchange exchange, decimal quantity = 0)
        {
            var coinBuy = new CoinBuy
            {
                BTCPrice = orderResponse.Pair.EndsWith("BTC") ? orderResponse.Price : 0,
                Exchange = exchange,
                OrderId = orderResponse.OrderId,
                Pair = orderResponse.Pair,
                Price = orderResponse.Price,
                Quantity = quantity == 0 ? orderResponse.FilledQuantity : quantity,
                TransactionDate = orderResponse.TransactTime
            };

            return coinBuy;
        }

        private IEnumerable<ExchangeOrder> OrderResponseCollectionToExchangeOrders(IEnumerable<OrderResponse> orders, Exchange exchange)
        {
            var exchangeOrders = new List<ExchangeOrder>();

            foreach (var order in orders)
            {
                var exchangeOrder = OrderResponseToExchangeOrder(order, exchange);
                exchangeOrders.Add(exchangeOrder);
            }

            return exchangeOrders;
        }

        /// <summary>
        /// Convert an OrderResponse to an ExchangeOrder
        /// </summary>
        /// <param name="orderResponse">OrderResponse to convert</param>
        /// <returns>new ExchangeOrder object</returns>
        public ExchangeOrder OrderResponseToExchangeOrder(OrderResponse orderResponse, Exchange exchange)
        {
            var exchangeOrder = new ExchangeOrder
            {
                Exchange = exchange,
                FilledQuantity = orderResponse.FilledQuantity,
                OrderId = orderResponse.OrderId,
                Pair = orderResponse.Pair,
                PlaceDate = orderResponse.TransactTime,
                Price = orderResponse.Price,
                Quantity = orderResponse.OrderQuantity,
                Side = (Side)orderResponse.Side
            };

            return exchangeOrder;
        }

        private IEnumerable<HistoricalPrice> TickerCollectionToHistoricalPrices(IEnumerable<Ticker> tickers, Exchange exchange)
        {
            var hpList = new List<HistoricalPrice>();

            foreach(var ticker in tickers)
            {
                var hp = TickerToHistoricalPrice(ticker, exchange);

                hpList.Add(hp);
            }

            return hpList;
        }

        private HistoricalPrice TickerToHistoricalPrice(Ticker ticker, Exchange exchange)
        {
            var hp = new HistoricalPrice
            {
                Exchange = exchange,
                High = ticker.High,
                Low = ticker.Low,
                Pair = ticker.Pair,
                Price = ticker.LastPrice,
                Snapshot = DateTime.UtcNow
            };

            return hp;
        }
    }
}