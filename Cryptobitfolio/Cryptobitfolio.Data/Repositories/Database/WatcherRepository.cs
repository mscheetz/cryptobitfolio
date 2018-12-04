// -----------------------------------------------------------------------------
// <copyright file="WatcherRepository" company="Matt Scheetz">
//     Copyright (c) Matt Scheetz All Rights Reserved
// </copyright>
// <author name="Matt Scheetz" date="12/3/2018 7:37:26 PM" />
// -----------------------------------------------------------------------------

namespace Cryptobitfolio.Data.Repositories
{
    #region usings

    using Cryptobitfolio.Business.Entities.Portfolio;
    using Cryptobitfolio.Data.Interfaces.Database;
    using Cryptobitfolio.Data.Repositories.Database;
    using SQLite;

    #endregion usings

    public class WatcherRepository : DatabaseRepositoryBase<Watcher>, IWatcherRepository
    {
        private SQLiteAsyncConnection db;

        public WatcherRepository() : this(new SqliteContext())
        {
        }

        public WatcherRepository(SqliteContext context) : base(context)
        {
        }
    }
}