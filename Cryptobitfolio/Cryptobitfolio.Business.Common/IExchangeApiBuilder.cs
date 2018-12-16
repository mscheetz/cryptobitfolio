// -----------------------------------------------------------------------------
// <copyright file="IExchangeApiBuilder" company="Matt Scheetz">
//     Copyright (c) Matt Scheetz All Rights Reserved
// </copyright>
// <author name="Matt Scheetz" date="12/7/2018 9:16:58 PM" />
// -----------------------------------------------------------------------------

namespace Cryptobitfolio.Business.Common
{
    #region Usings

    using Cryptobitfolio.Business.Contracts.Trade;
    using Cryptobitfolio.Business.Entities;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    #endregion Usings

    public interface IExchangeApiBuilder
    {
        #region ExchangeApi Methods

        /// <summary>
        /// Get all ExchangeApis
        /// </summary>
        /// <returns>Collection of ExchangeApis</returns>
        Task<IEnumerable<ExchangeApi>> Get();

        /// <summary>
        /// Get all ExchangeApis for a given exchange
        /// </summary>
        /// <param name="exchange">Exchange to find</param>
        /// <returns>Collection of ExchangeApis</returns>
        Task<IEnumerable<ExchangeApi>> Get(Exchange exchange);

        /// <summary>
        /// Save exhange api to database
        /// </summary>
        /// <param name="exchangeApi">ExchangeApi to save</param>
        /// <returns>Updated ExchangeApi object</returns>
        Task<ExchangeApi> Add(ExchangeApi exchangeApi);

        /// <summary>
        /// Delete an ExchangeApi
        /// </summary>
        /// <param name="exchangeApi">ExchangeApi to delete</param>
        /// <returns>Boolean value of deletion attempt</returns>
        Task<bool> Delete(ExchangeApi exchangeApi);

        #endregion ExchangeApi Methods
    }
}