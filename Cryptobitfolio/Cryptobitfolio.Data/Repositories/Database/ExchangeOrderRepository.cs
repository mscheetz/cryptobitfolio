// -----------------------------------------------------------------------------
// <copyright file="ExchangeOrderRepository" company="Matt Scheetz">
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

    public class ExchangeOrderRepository : DatabaseRepositoryBase<ExchangeOrder>, IExchangeOrderRepository
    {
        private SQLiteAsyncConnection db;

        public ExchangeOrderRepository() : this(new SqliteContext())
        {            
        }

        public ExchangeOrderRepository(SqliteContext context): base(context)
        {
        }
    }
}