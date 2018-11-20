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

        private readonly IExchangeBuilder _exchangeBuilder;

        #endregion Properties

        #region Constructor/Destructor

        public ExchangeViewModel(IExchangeBuilder exchangeBldr)
        {
            _exchangeBuilder = exchangeBldr;
        }

        #endregion Constructor/Destructor
        
        #region Methods

        public void LoadExchange(Business.Contracts.Trade.ExchangeApi exchangeApi)
        {
            _exchangeBuilder.LoadExchange(exchangeApi);
        }

        public DateTime UpdatePortfolio()
        {
            return _exchangeBuilder.UpdatePortfolio();
        }

        public async Task<IEnumerable<Business.Contracts.Trade.ExchangeApi>> GetExchangeApis()
        {
            return await _exchangeBuilder.GetExchangeApis();
        }

        public async Task<Business.Contracts.Trade.ExchangeApi> SaveExchangeApi(Business.Contracts.Trade.ExchangeApi exchangeApi)
        {
            return await _exchangeBuilder.SaveExchangeApi(exchangeApi);
        }

        public async Task<bool> DeleteExchangeApi(Business.Contracts.Trade.ExchangeApi exchangeApi)
        {
            return await _exchangeBuilder.DeleteExchangeApi(exchangeApi);
        }

        public IEnumerable<Business.Contracts.Portfolio.Coin> GetCoins()
        {
            return _exchangeBuilder.GetCoins();
        }

        public List<Business.Contracts.Trade.ExchangeOrder> GetOpenOrders()
        {
            return _exchangeBuilder.GetOpenOrders();
        }

        #endregion Methods
    }
}