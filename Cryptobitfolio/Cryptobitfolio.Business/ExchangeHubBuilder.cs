// -----------------------------------------------------------------------------
// <copyright file="ExchangeHubBuilder" company="Matt Scheetz">
//     Copyright (c) Matt Scheetz All Rights Reserved
// </copyright>
// <author name="Matt Scheetz" date="12/11/2018 8:00:57 PM" />
// -----------------------------------------------------------------------------

namespace Cryptobitfolio.Business
{
    using Cryptobitfolio.Business.Common;
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
            _exchangeApis = await _xchApiBldr.GetExchangeApis();
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

        public void LoadExchange(ExchangeApi exchangeApi)
        {
            IExchangeHubRepository loadedHub = null;
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
            _currentHub = loadedHub;
            _exchangeHubs.Add(_currentHub);

            //var e = OnBuildExchangeCoins(loadedHub);
            //var o = OnBuildOrders(loadedHub);
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

        public async Task<Dictionary<Exchange, IEnumerable<string>>> GetExchangeMarketDictionary()
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

        public async Task<IEnumerable<string>> GetExchangeMarkets(Exchange exchange)
        {
            var hub = _exchangeHubs.Where(e => e.GetExchange() == exchange.ToString()).FirstOrDefault();

            var markets = await hub.GetMarkets();

            return markets;
        }

        public async Task<IEnumerable<Balance>> GetBalances()
        {
            var balances = new List<Balance>();
            foreach(var hub in _exchangeHubs)
            {
                var xchBal = await hub.GetBalances();
                balances.AddRange(xchBal);
            }

            return balances;
        }

        public async Task<IEnumerable<Balance>> GetExchangeBalances()
        {
            var balances = await _currentHub.GetBalances();

            return balances;
        }

        public async Task<IEnumerable<Balance>> GetExchangeBalances(Exchange exchange)
        {
            var hub = _exchangeHubs.Where(e => e.GetExchange() == exchange.ToString()).FirstOrDefault();

            var balances = await hub.GetBalances();

            return balances;
        }

        public async Task<IEnumerable<OrderResponse>> GetOrders(string pair)
        {
            var orders = new List<OrderResponse>();
            foreach (var hub in _exchangeHubs)
            {
                var xchgOrders = await _currentHub.GetOrders(pair);
                orders.AddRange(xchgOrders);
            }

            return orders;
        }

        public async Task<IEnumerable<OrderResponse>> GetExchangeOrders(string pair)
        {
            var orders = await _currentHub.GetOrders(pair);

            return orders;
        }

        public async Task<IEnumerable<OrderResponse>> GetExchangeOrders(string pair, Exchange exchange)
        {
            var hub = _exchangeHubs.Where(e => e.GetExchange() == exchange.ToString()).FirstOrDefault();

            var orders = await hub.GetOrders(pair);

            return orders;
        }

        public async Task<IEnumerable<OrderResponse>> GetOpenOrders(string pair)
        {
            var orders = new List<OrderResponse>();
            foreach (var hub in _exchangeHubs)
            {
                var xchgOrders = await _currentHub.GetOpenOrders(pair);
                orders.AddRange(xchgOrders);
            }

            return orders;
        }

        public async Task<IEnumerable<OrderResponse>> GetExchangeOpenOrders(string pair)
        {
            var orders = await _currentHub.GetOpenOrders(pair);

            return orders;
        }

        public async Task<IEnumerable<OrderResponse>> GetExchangeOpenOrders(string pair, Exchange exchange)
        {
            var hub = _exchangeHubs.Where(e => e.GetExchange() == exchange.ToString()).FirstOrDefault();

            var orders = await hub.GetOpenOrders(pair);

            return orders;
        }
    }
}