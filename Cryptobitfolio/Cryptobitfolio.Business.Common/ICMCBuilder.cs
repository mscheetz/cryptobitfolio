// -----------------------------------------------------------------------------
// <copyright file="ICMCBuilder" company="Matt Scheetz">
//     Copyright (c) Matt Scheetz All Rights Reserved
// </copyright>
// <author name="Matt Scheetz" date="11/30/2018 9:14:26 PM" />
// -----------------------------------------------------------------------------

namespace Cryptobitfolio.Business.Common
{
    #region Usings

    using System.Collections.Generic;
    using System.Threading.Tasks;

    #endregion Usings

    public interface ICMCBuilder
    {
        /// <summary>
        /// Get Currency data for symbols
        /// </summary>
        /// <param name="symbols">Collection of currency symbols</param>
        /// <returns>Collection of Currency objects</returns>
        Task<IEnumerable<Contracts.Portfolio.Currency>> GetCurrencies(List<string> symbols);
    }
}