// -----------------------------------------------------------------------------
// <copyright file="IExchangeHubBuilder" company="Matt Scheetz">
//     Copyright (c) Matt Scheetz All Rights Reserved
// </copyright>
// <author name="Matt Scheetz" date="12/11/2018 8:01:08 PM" />
// -----------------------------------------------------------------------------

namespace Cryptobitfolio.Business.Common
{
    using Cryptobitfolio.Business.Contracts.Portfolio;
    using Cryptobitfolio.Business.Contracts.Trade;
    #region Usings

    using ExchangeHub.Contracts;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Exchange = Entities.Exchange;

    #endregion Usings

    public interface IExchangeHubBuilder
    {
        bool SetExchange(Exchange exchange);

        Task<IEnumerable<string>> GetMarkets();

        Task<Dictionary<Exchange, IEnumerable<string>>> GetAllMarkets();

        Task<IEnumerable<string>> GetExchangeMarkets();

        Task<IEnumerable<string>> GetMarketsForACoin(string symbol);

        Task<IEnumerable<string>> GetExchangeMarkets(Exchange exchange);

        Task<IEnumerable<ExchangeCoin>> GetBalances();

        Task<IEnumerable<ExchangeCoin>> GetBalances(string symbol);

        Task<IEnumerable<ExchangeCoin>> GetBalances(List<string> symbols);

        Task<IEnumerable<ExchangeCoin>> GetExchangeBalances();

        Task<IEnumerable<ExchangeCoin>> GetExchangeBalances(Exchange exchange);

        Task<IEnumerable<CoinBuy>> GetOrders(string pair);

        Task<IEnumerable<CoinBuy>> GetExchangeOrders(string pair);

        Task<IEnumerable<CoinBuy>> GetExchangeOrders(string pair, Exchange exchange);

        /// <summary>
        /// Get orders for a collection of trading pairs
        /// </summary>
        /// <param name="pairs">Trading pairs</param>
        /// <returns>Collection of OrderResponses</returns>
        Task<IEnumerable<CoinBuy>> GetExchangeOrders(IEnumerable<string> pairs, Exchange exchange);

        Task<IEnumerable<ExchangeOrder>> GetOpenOrders(string pair);

        Task<IEnumerable<ExchangeOrder>> GetExchangeOpenOrders(string pair);

        Task<IEnumerable<ExchangeOrder>> GetExchangeOpenOrders(string pair, Exchange exchange);

        Task<IEnumerable<ExchangeOrder>> GetExchangeOpenOrdersByPairs(IEnumerable<string> pairs, Exchange exchange);

    }
}