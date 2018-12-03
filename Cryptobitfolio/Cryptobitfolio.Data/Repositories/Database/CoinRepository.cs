using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Cryptobitfolio.Business.Entities.Portfolio;
using Cryptobitfolio.Data.Interfaces;
using Cryptobitfolio.Data.Interfaces.Database;
using SQLite;

namespace Cryptobitfolio.Data.Repositories
{
    public class CoinRepository : IDatabaseRepository<Coin>
    {
        private SQLiteAsyncConnection db;

        public CoinRepository()
        {
            var context = new SqliteContext();
            db = context.GetConnection<Coin>();

            db.CreateTableAsync<Coin>();
        }

        public async Task<IEnumerable<Coin>> Get()
        {
            return await db.Table<Coin>().ToListAsync();
        }

        public async Task<IEnumerable<Coin>> Get(List<int> ids)
        {
            return await db.Table<Coin>().Where(e => ids.Contains(e.CurrencyId)).ToListAsync();
        }

        public async Task<Coin> Get(int Id)
        {
            Coin entity;

            try
            {
                entity = await db.Table<Coin>().Where(e => e.Id == Id).FirstAsync();
            }
            catch
            {
                entity = null;
            }

            return entity;
        }

        public async Task<Coin> Add(Coin entity)
        {
            entity.Id = await db.InsertAsync(entity);

            return entity;
        }

        public async Task<IEnumerable<Coin>> AddAll(IEnumerable<Coin> entityList)
        {
            await db.InsertAllAsync(entityList);

            return entityList;
        }

        public async Task<Coin> Update(Coin entity)
        {
            await db.UpdateAsync(entity);

            return entity;
        }

        public async Task<IEnumerable<Coin>> UpdateAll(IEnumerable<Coin> entityList)
        {
            await db.UpdateAllAsync(entityList);

            return entityList;
        }

        public async Task<bool> Delete(Coin entity)
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
                await db.DeleteAllAsync<Coin>();
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
            await db.ExecuteScalarAsync<Coin>($"UPDATE SQLITE_SEQUENCE SET SEQ=0 WHERE NAME='{typeof(Coin).Name}'");
        }
    }
}