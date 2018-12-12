// -----------------------------------------------------------------------------
// <copyright file="IExchangeCoinBuilder" company="Matt Scheetz">
//     Copyright (c) Matt Scheetz All Rights Reserved
// </copyright>
// <author name="Matt Scheetz" date="12/11/2018 7:19:32 PM" />
// -----------------------------------------------------------------------------

namespace Cryptobitfolio.Business.Common
{
    using Cryptobitfolio.Business.Contracts.Portfolio;
    #region Usings

    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    #endregion Usings

    public interface IExchangeCoinBuilder
    {
        Task<ExchangeCoin> Add(ExchangeCoin contract);

        Task<bool> Delete(ExchangeCoin contract);

        Task<IEnumerable<ExchangeCoin>> Get();

        Task<ExchangeCoin> Update(ExchangeCoin contract);
    }
}