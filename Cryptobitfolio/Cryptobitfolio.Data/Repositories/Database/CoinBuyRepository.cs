using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Cryptobitfolio.Business.Entities.Portfolio;
using Cryptobitfolio.Data.Interfaces;
using Cryptobitfolio.Data.Interfaces.Database;
using SQLite;

namespace Cryptobitfolio.Data.Repositories
{
    public class CoinBuyRepository : IDatabaseRepository<CoinBuy>
    {
        private SQLiteAsyncConnection db;

        public CoinBuyRepository()
        {
            var context = new SqliteContext();
            db = context.GetConnection<CoinBuy>();

            db.CreateTableAsync<CoinBuy>();
        }

        public async Task<IEnumerable<CoinBuy>> Get()
        {
            return await db.Table<CoinBuy>().ToListAsync();
        }

        public async Task<IEnumerable<CoinBuy>> Get(List<int> ids)
        {
            return await db.Table<CoinBuy>().Where(e => ids.Contains(e.CurrencyId)).ToListAsync();
        }

        public async Task<CoinBuy> Get(int Id)
        {
            CoinBuy entity;

            try
            {
                entity = await db.Table<CoinBuy>().Where(e => e.Id == Id).FirstAsync();
            }
            catch
            {
                entity = null;
            }

            return entity;
        }

        public async Task<CoinBuy> Add(CoinBuy entity)
        {
            entity.Id = await db.InsertAsync(entity);

            return entity;
        }

        public async Task<IEnumerable<CoinBuy>> AddAll(IEnumerable<CoinBuy> entityList)
        {
            await db.InsertAllAsync(entityList);

            return entityList;
        }

        public async Task<CoinBuy> Update(CoinBuy entity)
        {
            await db.UpdateAsync(entity);

            return entity;
        }

        public async Task<IEnumerable<CoinBuy>> UpdateAll(IEnumerable<CoinBuy> entityList)
        {
            await db.UpdateAllAsync(entityList);

            return entityList;
        }

        public async Task<bool> Delete(CoinBuy entity)
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
                await db.DeleteAllAsync<CoinBuy>();
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
            await db.ExecuteScalarAsync<ArbitragePath>($"UPDATE SQLITE_SEQUENCE SET SEQ=0 WHERE NAME='{typeof(CoinBuy).Name}'");
        }
    }
}