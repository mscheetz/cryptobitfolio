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

    public class ExchangeViewModel
    {

        #region Properties

        private readonly IAlertBuilder _alertBuilder;
        private readonly ICoinBuilder _coinBuilder;
        private readonly IExchangeApiBuilder _exchangeApiBuilder;
        private readonly IExchangeHubBuilder _exchangeHubBuilder;
        private readonly IExchangeOrderBuilder _exchangeOrderBuilder;
        private readonly IWatchBuilder _watchBuilder;

        #endregion Properties

        #region Constructor/Destructor

        public ExchangeViewModel(IAlertBuilder alertBuilder,
                                 ICoinBuilder coinBuilder,
                                 IExchangeApiBuilder exchangeApiBuilder,
                                 IExchangeHubBuilder exchangeHubBuilder,
                                 IExchangeOrderBuilder exchangeOrderBldr, 
                                 IWatchBuilder watchBuilder)
        {
            _alertBuilder = alertBuilder;
            _coinBuilder = coinBuilder;
            _exchangeApiBuilder = exchangeApiBuilder;
            _exchangeHubBuilder = exchangeHubBuilder;
            _exchangeOrderBuilder = exchangeOrderBldr;
            _watchBuilder = watchBuilder;
        }

        #endregion Constructor/Destructor
        
        #region Methods

        public async Task<IEnumerable<Business.Contracts.Portfolio.Coin>> GetCoins()
        {
            return await _coinBuilder.Get();
        }

        public async Task<IEnumerable<Business.Contracts.Trade.ExchangeOrder>> GetOpenOrders(Business.Entities.Exchange exchange)
        {
            return await _exchangeOrderBuilder.GetLatest("", exchange);
        }

        #endregion Methods
    }
}