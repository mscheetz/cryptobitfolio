using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Cryptobitfolio.Business.Entities.Portfolio;
using Cryptobitfolio.Data.Interfaces;
using Cryptobitfolio.Data.Interfaces.Database;
using SQLite;

namespace Cryptobitfolio.Data.Repositories
{
    public class WatcherRepository : IDatabaseRepository<Watcher>
    {
        private SQLiteAsyncConnection db;

        public WatcherRepository()
        {
            var context = new SqliteContext();
            db = context.GetConnection<Watcher>();

            db.CreateTableAsync<Watcher>();
        }

        public async Task<IEnumerable<Watcher>> Get()
        {
            return await db.Table<Watcher>().ToListAsync();
        }

        public async Task<IEnumerable<Watcher>> Get(List<int> ids)
        {
            return await db.Table<Watcher>().Where(e => ids.Contains(e.CurrencyId)).ToListAsync();
        }

        public async Task<Watcher> Get(int Id)
        {
            Watcher entity;

            try
            {
                entity = await db.Table<Watcher>().Where(e => e.Id == Id).FirstAsync();
            }
            catch
            {
                entity = null;
            }

            return entity;
        }

        public async Task<Watcher> Add(Watcher entity)
        {
            entity.Id = await db.InsertAsync(entity);

            return entity;
        }

        public async Task<IEnumerable<Watcher>> AddAll(IEnumerable<Watcher> entityList)
        {
            await db.InsertAllAsync(entityList);

            return entityList;
        }

        public async Task<Watcher> Update(Watcher entity)
        {
            await db.UpdateAsync(entity);

            return entity;
        }

        public async Task<IEnumerable<Watcher>> UpdateAll(IEnumerable<Watcher> entityList)
        {
            await db.UpdateAllAsync(entityList);

            return entityList;
        }

        public async Task<bool> Delete(Watcher entity)
        {
            try
            {
                await db.DeleteAsync(entity);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return true;
        }

        public async Task<bool> DeleteAll()
        {
            try
            {
                await db.DeleteAllAsync<Watcher>();
                await ResetAutoIncrement();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return true;
        }

        private async Task ResetAutoIncrement()
        {
            await db.ExecuteScalarAsync<Watcher>($"UPDATE SQLITE_SEQUENCE SET SEQ=0 WHERE NAME='{typeof(Watcher).Name}'");
        }
    }
}