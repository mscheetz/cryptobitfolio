// -----------------------------------------------------------------------------
// <copyright file="IHistoricalPriceRepository" company="Matt Scheetz">
//     Copyright (c) Matt Scheetz All Rights Reserved
// </copyright>
// <author name="Matt Scheetz" date="12/3/2018 7:37:26 PM" />
// -----------------------------------------------------------------------------

namespace Cryptobitfolio.Data.Interfaces.Database
{
    #region Usings

    using Cryptobitfolio.Business.Entities.Trade;

    #endregion Usings

    public interface IHistoricalPriceRepository : IDatabaseRepositoryBase<HistoricalPrice>
    {
    }
}