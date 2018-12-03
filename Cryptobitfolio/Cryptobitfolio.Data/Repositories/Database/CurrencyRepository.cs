using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Cryptobitfolio.Business.Entities.Portfolio;
using Cryptobitfolio.Data.Interfaces;
using Cryptobitfolio.Data.Interfaces.Database;
using SQLite;

namespace Cryptobitfolio.Data.Repositories
{
    public class CurrencyRepository : IDatabaseRepository<Currency>
    {
        private SQLiteAsyncConnection db;

        public CurrencyRepository()
        {
            var context = new SqliteContext();
            db = context.GetConnection<Currency>();

            db.CreateTableAsync<Currency>();
        }

        public async Task<IEnumerable<Currency>> Get()
        {
            return await db.Table<Currency>().ToListAsync();
        }

        public async Task<IEnumerable<CoinBuy>> Get(List<int> ids)
        {
            return await db.Table<CoinBuy>().Where(e => ids.Contains(e.Id)).ToListAsync();
        }

        public async Task<Currency> Get(int Id)
        {
            Currency entity;

            try
            {
                entity = await db.Table<Currency>().Where(e => e.Id == Id).FirstAsync();
            }
            catch
            {
                entity = null;
            }

            return entity;
        }

        public async Task<Currency> Add(Currency entity)
        {
            entity.Id = await db.InsertAsync(entity);

            return entity;
        }

        public async Task<IEnumerable<Currency>> AddAll(IEnumerable<Currency> entityList)
        {
            await db.InsertAllAsync(entityList);

            return entityList;
        }

        public async Task<Currency> Update(Currency entity)
        {
            await db.UpdateAsync(entity);

            return entity;
        }

        public async Task<IEnumerable<Currency>> UpdateAll(IEnumerable<Currency> entityList)
        {
            await db.UpdateAllAsync(entityList);

            return entityList;
        }

        public async Task<bool> Delete(Currency entity)
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
                await db.DeleteAllAsync<Currency>();
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
            await db.ExecuteScalarAsync<Currency>($"UPDATE SQLITE_SEQUENCE SET SEQ=0 WHERE NAME='{typeof(Currency).Name}'");
        }
    }
}