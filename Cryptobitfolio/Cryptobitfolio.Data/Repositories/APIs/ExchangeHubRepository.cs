// -----------------------------------------------------------------------------
// <copyright file="ExchangeHubRepository" company="Matt Scheetz">
//     Copyright (c) Matt Scheetz All Rights Reserved
// </copyright>
// <author name="Matt Scheetz" date="11/26/2018 11:04:45 AM" />
// -----------------------------------------------------------------------------

namespace Cryptobitfolio.Data.Repositories
{
    #region Usings

    using Cryptobitfolio.Data.Interfaces;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    #endregion Usings

    public class ExchangeHubRepository : IExchangeHubRepository
    {
        #region Properties

        private string _exchange;
        private string _apiKey;
        private ExchangeHub.ExchangeHub _hub;

        #endregion Properties

        #region Constructor/Destructor

        public ExchangeHubRepository(ExchangeHub.Contracts.Exchange exchange, string WIF)
        {
            _exchange = exchange.ToString();
            _hub = new ExchangeHub.ExchangeHub(exchange, WIF);
            _hub.SetMarketsAsync();
        }

        public ExchangeHubRepository(ExchangeHub.Contracts.Exchange exchange, string apiKey, string apiSecret)
        {
            _apiKey = apiKey;
            _exchange = exchange.ToString();
            _hub = new ExchangeHub.ExchangeHub(exchange, apiKey, apiSecret);
            _hub.SetMarketsAsync();
        }

        public ExchangeHubRepository(ExchangeHub.Contracts.Exchange exchange, string apiKey, string apiSecret, string apiPassword)
        {
            _apiKey = apiKey;
            _exchange = exchange.ToString();
            _hub = new ExchangeHub.ExchangeHub(exchange, apiKey, apiSecret, apiPassword);
            _hub.SetMarketsAsync();
        }

        #endregion Constructor/Destructor

        /// <summary>
        /// Get currently loaded exchange
        /// </summary>
        /// <returns>String of exchange name</returns>
        public string GetExchange()
        {
            return _exchange;
        }

        /// <summary>
        /// Get api key for this connection
        /// </summary>
        /// <returns>String of Api Key</returns>
        public string GetApiKey()
        {
            return _apiKey;
        }

        /// <summary>
        /// Get all markets on an exchange
        /// </summary>
        /// <returns>Collection of trading pair strings</returns>
        public async Task<IEnumerable<string>> GetMarkets()
        {
            return await _hub.GetMarketsAsync();
        }

        /// <summary>
        /// Get latest prices for all markets on an exchange
        /// </summary>
        /// <returns>Collection of PairPrice objects</returns>
        public async Task<IEnumerable<ExchangeHub.Contracts.PairPrice>> GetLatestPrices()
        {
            return await _hub.GetLatestPricesAsync();
        }

        /// <summary>
        /// Get all balances for current account
        /// </summary>
        /// <returns>Collection of Balance objects</returns>
        public async Task<IEnumerable<ExchangeHub.Contracts.Balance>> GetBalances()
        {
            return await _hub.GetBalanceAsync();
        }

        /// <summary>
        /// Get all completed orders for a trading pair
        /// </summary>
        /// <param name="pair">Trading pair to find orders for</param>
        /// <returns>Collection of OrderResponses</returns>
        public async Task<IEnumerable<ExchangeHub.Contracts.OrderResponse>> GetOrders(string pair)
        {
            return await _hub.GetOrdersAsync(pair);
        }

        /// <summary>
        /// Get all open orders for a trading pair
        /// </summary>
        /// <param name="pair">Trading pair to find orders for</param>
        /// <returns>Collection of OrderResponses</returns>
        public async Task<IEnumerable<ExchangeHub.Contracts.OrderResponse>> GetOpenOrders(string pair)
        {
            return await _hub.GetOpenOrdersAsync(pair);
        }
    }
}