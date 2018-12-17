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

        private readonly IAlertBuilder _alertBuilder;
        private readonly IWatchBuilder _watchBuilder;

        #endregion Properties

        #region Constructor/Destructor

        public WatchListViewModel(IAlertBuilder alertBuilder, IWatchBuilder watchBuilder)
        {
            _alertBuilder = alertBuilder;
            _watchBuilder = watchBuilder;
        }

        #endregion Constructor/Destructor
        
        #region Methods
        
        public async Task<IEnumerable<Business.Contracts.Portfolio.Alerter>> GetAlerts()
        {
            return await _alertBuilder.Get();
        }

        public async Task<IEnumerable<Business.Contracts.Portfolio.Alerter>> GetLatestAlerts()
        {
            return await _alertBuilder.GetLatest();
        }

        #endregion Methods
    }
}