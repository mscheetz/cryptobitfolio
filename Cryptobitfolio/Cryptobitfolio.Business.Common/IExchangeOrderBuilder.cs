// -----------------------------------------------------------------------------
// <copyright file="IExchangeOrderBuilder" company="Matt Scheetz">
//     Copyright (c) Matt Scheetz All Rights Reserved
// </copyright>
// <author name="Matt Scheetz" date="12/11/2018 7:19:32 PM" />
// -----------------------------------------------------------------------------

namespace Cryptobitfolio.Business.Common
{
    using Cryptobitfolio.Business.Contracts.Trade;
    #region Usings

    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    #endregion Usings

    public interface IExchangeOrderBuilder
    {
        Task<ExchangeOrder> Add(ExchangeOrder contract);

        Task<bool> Delete(ExchangeOrder contract);

        Task<IEnumerable<ExchangeOrder>> Get();

        Task<ExchangeOrder> Update(ExchangeOrder contract);
    }
}