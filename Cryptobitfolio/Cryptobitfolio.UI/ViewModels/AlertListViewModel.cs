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

    public class AlertListViewModel
    {

        #region Properties

        private readonly IAlertBuilder _alertBuilder;

        #endregion Properties

        #region Constructor/Destructor

        public AlertListViewModel(IAlertBuilder alertBuilder)
        {
            _alertBuilder = alertBuilder;
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

        public async Task<Business.Contracts.Portfolio.Alerter> AddAlert(Business.Contracts.Portfolio.Alerter alert)
        {
            return await _alertBuilder.Add(alert);
        }

        public async Task<Business.Contracts.Portfolio.Alerter> UpdateAlert(Business.Contracts.Portfolio.Alerter alert)
        {
            return await _alertBuilder.Update(alert);
        }

        public async Task<bool> DeleteAlert(Business.Contracts.Portfolio.Alerter alert)
        {
            return await _alertBuilder.Delete(alert);
        }

        #endregion Methods
    }
}