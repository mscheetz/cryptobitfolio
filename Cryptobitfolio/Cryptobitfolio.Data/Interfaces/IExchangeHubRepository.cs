// -----------------------------------------------------------------------------
// <copyright file="IExchangeHubRepository" company="Matt Scheetz">
//     Copyright (c) Matt Scheetz All Rights Reserved
// </copyright>
// <author name="Matt Scheetz" date="11/26/2018 11:04:53 AM" />
// -----------------------------------------------------------------------------

namespace Cryptobitfolio.Data.Interfaces
{
    #region Usings

    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    #endregion Usings

    public interface IExchangeHubRepository
    {
        /// <summary>
        /// Get currently loaded exchange
        /// </summary>
        /// <returns>String of exchange name</returns>
        string GetExchange();

        /// <summary>
        /// Get all markets on an exchange
        /// </summary>
        /// <returns>Collection of trading pair strings</returns>
        Task<IEnumerable<string>> GetMarkets();

        /// <summary>
        /// Get latest prices for all markets on an exchange
        /// </summary>
        /// <returns>Collection of PairPrice objects</returns>
        Task<IEnumerable<ExchangeHub.Contracts.PairPrice>> GetLatestPrices();

        /// <summary>
        /// Get all balances for current account
        /// </summary>
        /// <returns>Collection of Balance objects</returns>
        Task<IEnumerable<ExchangeHub.Contracts.Balance>> GetBalances();

        /// <summary>
        /// Get all completed orders for a trading pair
        /// </summary>
        /// <param name="pair">Trading pair to find orders for</param>
        /// <returns>Collection of OrderResponses</returns>
        Task<IEnumerable<ExchangeHub.Contracts.OrderResponse>> GetOrders(string pair);

        /// <summary>
        /// Get all open orders for a trading pair
        /// </summary>
        /// <param name="pair">Trading pair to find orders for</param>
        /// <returns>Collection of OrderResponses</returns>
        Task<IEnumerable<ExchangeHub.Contracts.OrderResponse>> GetOpenOrders(string pair);
    }
}