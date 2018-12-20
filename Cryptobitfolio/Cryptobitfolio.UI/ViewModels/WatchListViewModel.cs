// -----------------------------------------------------------------------------
// <copyright file="ExchangeViewModel" company="Matt Scheetz">
//     Copyright (c) Matt Scheetz All Rights Reserved
// </copyright>
// <author name="Matt Scheetz" date="11/19/2018 7:37:53 PM" />
// -----------------------------------------------------------------------------

namespace Cryptobitfolio.UI.ViewModels
{
    #region Usings

    using Cryptobitfolio.Business.Common;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    #endregion Usings

    public class WatchListViewModel
    {

        #region Properties

        private readonly IWatchBuilder _watchBuilder;

        #endregion Properties

        #region Constructor/Destructor

        public WatchListViewModel(IWatchBuilder watchBuilder)
        {
            _watchBuilder = watchBuilder;
        }

        #endregion Constructor/Destructor
        
        #region Methods
        
        public async Task<IEnumerable<Business.Contracts.Portfolio.Watcher>> GetWatchers()
        {
            return await _watchBuilder.Get();
        }

        public async Task<Business.Contracts.Portfolio.Watcher> AddWatcher(Business.Contracts.Portfolio.Watcher watcher)
        {
            return await _watchBuilder.Add(watcher);
        }

        public async Task<Business.Contracts.Portfolio.Watcher> UpdateWatcher(Business.Contracts.Portfolio.Watcher watcher)
        {
            return await _watchBuilder.Update(watcher);
        }

        public async Task<bool> DeleteWatcher(Business.Contracts.Portfolio.Watcher watcher)
        {
            return await _watchBuilder.Delete(watcher);
        }

        #endregion Methods
    }
}