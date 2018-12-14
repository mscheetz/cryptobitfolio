// -----------------------------------------------------------------------------
// <copyright file="ICoinBuyBuilder" company="Matt Scheetz">
//     Copyright (c) Matt Scheetz All Rights Reserved
// </copyright>
// <author name="Matt Scheetz" date="12/11/2018 7:33:23 PM" />
// -----------------------------------------------------------------------------

namespace Cryptobitfolio.Business.Common
{
    using Cryptobitfolio.Business.Contracts.Portfolio;
    using Cryptobitfolio.Business.Entities;
    #region Usings

    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    #endregion Usings

    public interface ICoinBuyBuilder
    {
        Task<CoinBuy> Add(CoinBuy contract);

        Task<bool> Delete(CoinBuy contract);

        Task<IEnumerable<CoinBuy>> Get();

        Task<IEnumerable<CoinBuy>> Get(string symbol, Exchange exchange);

        Task<CoinBuy> Update(CoinBuy contract);

        Task<IEnumerable<CoinBuy>> GetLatest(string symbol, decimal quantity, Exchange exchange);

        Task<IEnumerable<CoinBuy>> GetRelevantCoinBuys(string symbol, decimal quantity, Exchange exchange);

        Task<IEnumerable<CoinBuy>> GetAllOrders(string symbol, Exchange exchange);
    }
}