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
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    #endregion Usings

    public class ApiViewModel
    {

        #region Properties

        private readonly ICoinBuilder _coinBuilder;
        private readonly IExchangeApiBuilder _exchangeApiBuilder;
        private readonly IExchangeHubBuilder _exchangeHubBuilder;

        #endregion Properties

        #region Constructor/Destructor

        public ApiViewModel(ICoinBuilder coinBuilder,
                                 IExchangeApiBuilder exchangeApiBuilder,
                                 IExchangeHubBuilder exchangeHubBuilder)
        {
            _coinBuilder = coinBuilder;
            _exchangeApiBuilder = exchangeApiBuilder;
            _exchangeHubBuilder = exchangeHubBuilder;
        }

        #endregion Constructor/Destructor
        
        #region Methods

        public void LoadExchange(Business.Contracts.Trade.ExchangeApi exchangeApi)
        {
            _exchangeHubBuilder.LoadExchange(exchangeApi);
        }

        public async Task<bool> UpdatePortfolio()
        {
            var coins = await _coinBuilder.GetLatest();
            return coins.Any() ? true : false;
        }

        public async Task<bool> UpdateExchange(Business.Entities.Exchange exchange)
        {
            var coins = await _coinBuilder.GetLatest(exchange);
            return coins.Any() ? true : false;
        }

        public async Task<IEnumerable<Business.Contracts.Trade.ExchangeApi>> GetExchangeApis()
        {
            return await _exchangeApiBuilder.Get();
        }

        public async Task<Business.Contracts.Trade.ExchangeApi> AddExchangeApi(Business.Contracts.Trade.ExchangeApi exchangeApi)
        {
            return await _exchangeApiBuilder.Add(exchangeApi);
        }

        public async Task<Business.Contracts.Trade.ExchangeApi> UpdateExchangeApi(Business.Contracts.Trade.ExchangeApi exchangeApi)
        {
            return await _exchangeApiBuilder.Add(exchangeApi);
        }

        public async Task<bool> DeleteExchangeApi(Business.Contracts.Trade.ExchangeApi exchangeApi)
        {
            var apiDelete = await _exchangeApiBuilder.Delete(exchangeApi);
            var hubDelete = _exchangeHubBuilder.UnloadExchange(exchangeApi);

            return apiDelete == hubDelete;
        }

        #endregion Methods
    }
}