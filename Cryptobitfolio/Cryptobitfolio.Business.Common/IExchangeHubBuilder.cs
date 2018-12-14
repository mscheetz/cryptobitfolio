// -----------------------------------------------------------------------------
// <copyright file="IExchangeHubBuilder" company="Matt Scheetz">
//     Copyright (c) Matt Scheetz All Rights Reserved
// </copyright>
// <author name="Matt Scheetz" date="12/11/2018 8:01:08 PM" />
// -----------------------------------------------------------------------------

namespace Cryptobitfolio.Business.Common
{
    using Cryptobitfolio.Business.Entities;
    using ExchangeHub.Contracts;
    #region Usings

    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;
    using Exchange = Entities.Exchange;

    #endregion Usings

    public interface IExchangeHubBuilder
    {
        Task<IEnumerable<string>> GetMarkets();

        Task<Dictionary<Exchange, IEnumerable<string>>> GetExchangeMarketDictionary();

        Task<IEnumerable<string>> GetExchangeMarkets();

        Task<IEnumerable<string>> GetMarketsForACoin(string symbol);

        Task<IEnumerable<string>> GetExchangeMarkets(Exchange exchange);

        Task<Dictionary<Exchange, IEnumerable<Balance>>> GetBalances();

        Task<Dictionary<Exchange, IEnumerable<Balance>>> GetBalances(string symbol);

        Task<IEnumerable<Balance>> GetExchangeBalances();

        Task<IEnumerable<Balance>> GetExchangeBalances(Exchange exchange);

        Task<IEnumerable<OrderResponse>> GetOrders(string pair);

        Task<IEnumerable<OrderResponse>> GetExchangeOrders(string pair);

        Task<IEnumerable<OrderResponse>> GetExchangeOrders(string pair, Exchange exchange);

        /// <summary>
        /// Get orders for a collection of trading pairs
        /// </summary>
        /// <param name="pairs">Trading pairs</param>
        /// <returns>Collection of OrderResponses</returns>
        Task<IEnumerable<OrderResponse>> GetExchangeOrders(IEnumerable<string> pairs, Exchange exchange);

        Task<IEnumerable<OrderResponse>> GetOpenOrders(string pair);

        Task<IEnumerable<OrderResponse>> GetExchangeOpenOrders(string pair);

        Task<IEnumerable<OrderResponse>> GetExchangeOpenOrders(string pair, Exchange exchange);

        Task<IEnumerable<OrderResponse>> GetExchangeOpenOrdersByPairs(IEnumerable<string> pairs, Exchange exchange);

    }
}