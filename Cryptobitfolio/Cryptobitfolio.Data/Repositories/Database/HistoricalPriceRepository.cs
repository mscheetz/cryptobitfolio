// -----------------------------------------------------------------------------
// <copyright file="HistoricalPriceRepository" company="Matt Scheetz">
//     Copyright (c) Matt Scheetz All Rights Reserved
// </copyright>
// <author name="Matt Scheetz" date="12/3/2018 7:37:26 PM" />
// -----------------------------------------------------------------------------

namespace Cryptobitfolio.Data.Repositories
{
    #region usings

    using Cryptobitfolio.Business.Entities.Trade;
    using Cryptobitfolio.Data.Interfaces.Database;
    using Cryptobitfolio.Data.Repositories.Database;
    using SQLite;

    #endregion usings

    public class HistoricalPriceRepository : DatabaseRepositoryBase<HistoricalPrice>, IHistoricalPriceRepository
    {
        private SQLiteAsyncConnection db;

        public HistoricalPriceRepository() : this(new SqliteContext())
        {
        }

        public HistoricalPriceRepository(SqliteContext context) : base(context)
        {
        }
    }
}