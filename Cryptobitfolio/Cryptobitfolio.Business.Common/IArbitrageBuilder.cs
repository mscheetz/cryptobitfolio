// -----------------------------------------------------------------------------
// <copyright file="IArbitrageBuilder" company="Matt Scheetz">
//     Copyright (c) Matt Scheetz All Rights Reserved
// </copyright>
// <author name="Matt Scheetz" date="11/26/2018 2:07:58 PM" />
// -----------------------------------------------------------------------------

namespace Cryptobitfolio.Business.Common
{
    using Cryptobitfolio.Business.Contracts.Portfolio;
    using Cryptobitfolio.Data.Interfaces;
    #region Usings

    using System;
    using System.Collections.Generic;
    using System.Text;

    #endregion Usings

    public interface IArbitrageBuilder
    {
        /// <summary>
        /// Get internal arbitrage values for a currency
        /// </summary>
        /// <param name="symbol">Symbol of a currency</param>
        /// <param name="quantity">Starting quantity</param>
        /// <param name="hub">Current exchange hub</param>
        /// <returns>Collection of ArbitrageLoop objects</returns>
        IEnumerable<ArbitrageLoop> GetInternalArbitrage(string symbol,
                                                               decimal quantity,
                                                               IExchangeHubRepository hub);
    }
}