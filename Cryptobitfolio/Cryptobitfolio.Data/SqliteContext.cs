using System;
using System.IO;
using System.Threading.Tasks;
using SQLite;

namespace Cryptobitfolio.Data
{
    public class SqliteContext
    {
        private SQLiteAsyncConnection db;

        public SqliteContext()
        {
            db = new SQLiteAsyncConnection(GetDbPath());
        }

        public SQLiteAsyncConnection GetConnection()
        {
            return db;
        }

        public SQLiteAsyncConnection GetConnection<T>()
        {
            return db;
        }

        public string GetDbPath()
        {
            return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "cryptobitfolio.db3");
        }

        public bool ResetAutoincrement<T>()
        {
            try
            {
                var conn = new SQLiteConnection(GetDbPath());

                var cmd = conn.CreateCommand($"DELETE FROM SQLITE_SEQUENCE WHERE NAME='{typeof(T).Name}'");
                cmd.ExecuteNonQuery();
                conn.Close();

                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }

        public bool DropTable<T>()
        {
            try
            {
                var conn = new SQLiteConnection(GetDbPath());

                var cmd = conn.CreateCommand($"DELETE TABLE '{typeof(T).Name}'");
                cmd.ExecuteNonQuery();
                conn.Close();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool TableExists<T>()
        {
            try
            {
                var conn = new SQLiteConnection(GetDbPath());

                var tableName = conn.ExecuteScalar<string>($"SELECT name FROM exemple WHERE type = 'table' AND name = '{typeof(T).Name}'");

                if (!string.IsNullOrEmpty(tableName) && tableName.Equals(typeof(T).Name))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}