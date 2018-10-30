using System;
using System.IO;
using SQLite;

namespace Cryptobitfolio.Data
{
    public class SqliteContext
    {
        private string dbPath;
        private SQLiteAsyncConnection db;

        public SqliteContext()
        {
            db = new SQLiteAsyncConnection(dbPath);
        }

        public SQLiteAsyncConnection GetConnection<T>()
        {
            return db;
        }

        private string SetDbPath()
        {
            return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "cryptobitfolio.db3");
        }
    }
}