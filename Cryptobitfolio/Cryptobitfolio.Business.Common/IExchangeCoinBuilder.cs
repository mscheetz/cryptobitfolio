// -----------------------------------------------------------------------------
// <copyright file="IExchangeCoinBuilder" company="Matt Scheetz">
//     Copyright (c) Matt Scheetz All Rights Reserved
// </copyright>
// <author name="Matt Scheetz" date="12/11/2018 7:19:32 PM" />
// -----------------------------------------------------------------------------

namespace Cryptobitfolio.Business.Common
{
    #region Usings

    using Cryptobitfolio.Business.Contracts.Portfolio;
    using Cryptobitfolio.Business.Entities;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    #endregion Usings

    public interface IExchangeCoinBuilder
    {
        Task<ExchangeCoin> Add(ExchangeCoin contract);

        Task<bool> Delete(ExchangeCoin contract);

        Task<IEnumerable<ExchangeCoin>> Get();

        Task<ExchangeCoin> Update(ExchangeCoin contract);

        Task<IEnumerable<ExchangeCoin>> Get(string symbol);

        Task<IEnumerable<ExchangeCoin>> Get(List<string> symbols);

        Task<IEnumerable<ExchangeCoin>> Get(Exchange exchange);

        Task<IEnumerable<ExchangeCoin>> Get(string symbol, Exchange exchange);

        Task<IEnumerable<ExchangeCoin>> GetLatest(string symbol);

        Task<IEnumerable<ExchangeCoin>> GetLatest(List<string> symbols);

        Task<IEnumerable<ExchangeCoin>> GetLatest(Exchange exchange);
    }
}