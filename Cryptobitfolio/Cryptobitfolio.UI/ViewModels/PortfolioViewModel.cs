// -----------------------------------------------------------------------------
// <copyright file="PortfolioViewModel" company="Matt Scheetz">
//     Copyright (c) Matt Scheetz All Rights Reserved
// </copyright>
// <author name="Matt Scheetz" date="12/7/2018 8:33:50 PM" />
// -----------------------------------------------------------------------------

namespace Cryptobitfolio.UI.ViewModels
{
    using Cryptobitfolio.Business.Common;
    using Cryptobitfolio.UI.ViewModels.Base;
    #region Usings

    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    #endregion Usings

    public class PortfolioViewModel : ViewModelBase
    {
        #region Properties

        private IExchangeBuilder _exchangeBldr;

        #endregion Properties

        #region Constructor

        public PortfolioViewModel(IExchangeBuilder exchangeBldr)
        {
            this._exchangeBldr = exchangeBldr;
        }

        #endregion Constructor

        public async Task<List<Business.Contracts.Portfolio.Coin>> GetCoins()
        {
            return await _exchangeBldr.GetCoins();
        }
    }
}