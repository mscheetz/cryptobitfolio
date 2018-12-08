// -----------------------------------------------------------------------------
// <copyright file="IAlertBuilder" company="Matt Scheetz">
//     Copyright (c) Matt Scheetz All Rights Reserved
// </copyright>
// <author name="Matt Scheetz" date="12/7/2018 9:36:38 PM" />
// -----------------------------------------------------------------------------

namespace Cryptobitfolio.Business.Common
{
    #region Usings

    using Cryptobitfolio.Business.Contracts.Portfolio;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    #endregion Usings

    public interface IAlertBuilder
    {
        /// <summary>
        /// Add an alert
        /// </summary>
        /// <param name="alerter">Alert to add</param>
        /// <returns>Added alert</returns>
        Task<Alerter> Add(Alerter alerter);

        /// <summary>
        /// Get all alerts
        /// </summary>
        /// <returns>Collection of Alerters</returns>
        Task<IEnumerable<Alerter>> Get();

        /// <summary>
        /// Get alerts for a coin
        /// </summary>
        /// <param name="Symbol">Symbol of coin</param>
        /// <returns>Collection of Alerters</returns>
        Task<IEnumerable<Alerter>> Get(string Symbol);

        /// <summary>
        /// Update an alert
        /// </summary>
        /// <param name="alerter">Alert to update</param>
        /// <returns>Updated alert</returns>
        Task<Alerter> Update(Alerter alerter);

        /// <summary>
        /// Delete an alert
        /// </summary>
        /// <param name="alerter">Alert to delete</param>
        /// <returns>Boolean of deletion</returns>
        Task<bool> Delete(Alerter alerter);
    }
}