// -----------------------------------------------------------------------------
// <copyright file="IHistoricalPriceBuilder" company="Matt Scheetz">
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

    public interface IHistoricalPriceBuilder
    {
        Task<HistoricalPrice> Add(HistoricalPrice contract);

        Task<bool> Delete(HistoricalPrice contract);

        Task<IEnumerable<HistoricalPrice>> Get();

        Task<IEnumerable<HistoricalPrice>> Get(string pair);

        Task<HistoricalPrice> Update(HistoricalPrice contract);
    }
}