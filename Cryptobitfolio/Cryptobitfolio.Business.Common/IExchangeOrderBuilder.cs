// -----------------------------------------------------------------------------
// <copyright file="IExchangeOrderBuilder" company="Matt Scheetz">
//     Copyright (c) Matt Scheetz All Rights Reserved
// </copyright>
// <author name="Matt Scheetz" date="12/11/2018 7:19:32 PM" />
// -----------------------------------------------------------------------------

namespace Cryptobitfolio.Business.Common
{
    #region Usings

    using Cryptobitfolio.Business.Contracts.Trade;
    using Cryptobitfolio.Business.Entities;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    #endregion Usings

    public interface IExchangeOrderBuilder
    {
        Task<ExchangeOrder> Add(ExchangeOrder contract);

        Task<bool> Delete(ExchangeOrder contract);

        Task<IEnumerable<ExchangeOrder>> Get();

        Task<IEnumerable<ExchangeOrder>> Get(string symbol, Exchange exchange);

        Task<ExchangeOrder> Update(ExchangeOrder contract);
        
        Task<IEnumerable<ExchangeOrder>> GetFromExchange(string symbol, Exchange exchange);

        Task<IEnumerable<ExchangeOrder>> GetLatest(string symbol, Exchange exchange);
    }
}