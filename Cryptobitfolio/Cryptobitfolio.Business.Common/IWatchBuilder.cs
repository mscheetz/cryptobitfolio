// -----------------------------------------------------------------------------
// <copyright file="IWatchBuilder" company="Matt Scheetz">
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

    public interface IWatchBuilder
    {
        /// <summary>
        /// Add a watcher
        /// </summary>
        /// <param name="watcher">Watcher to add</param>
        /// <returns>Added watcher</returns>
        Task<Watcher> Add(Watcher watcher);

        /// <summary>
        /// Get all watchers
        /// </summary>
        /// <returns>Collection of Watchers</returns>
        Task<IEnumerable<Watcher>> Get();

        /// <summary>
        /// Get watchers for a coin
        /// </summary>
        /// <param name="Symbol">Symbol of coin</param>
        /// <returns>Collection of Watchers</returns>
        Task<IEnumerable<Watcher>> Get(string Symbol);

        /// <summary>
        /// Update a watcher
        /// </summary>
        /// <param name="alerter">Watcher to update</param>
        /// <returns>Updated watcher</returns>
        Task<Watcher> Update(Watcher alerter);

        /// <summary>
        /// Delete a watcher
        /// </summary>
        /// <param name="watcher">Watcher to delete</param>
        /// <returns>Boolean of deletion</returns>
        Task<bool> Delete(Watcher watcher);
    }
}