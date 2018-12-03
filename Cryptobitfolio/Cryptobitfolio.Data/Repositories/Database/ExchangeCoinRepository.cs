using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Cryptobitfolio.Business.Entities.Portfolio;
using Cryptobitfolio.Data.Interfaces;
using Cryptobitfolio.Data.Interfaces.Database;
using SQLite;

namespace Cryptobitfolio.Data.Repositories
{
    public class ExchangeCoinRepository : IDatabaseRepository<ExchangeCoin>
    {
        private SQLiteAsyncConnection db;

        public ExchangeCoinRepository()
        {
            var context = new SqliteContext();
            db = context.GetConnection<ExchangeCoin>();

            db.CreateTableAsync<ExchangeCoin>();
        }

        public async Task<IEnumerable<ExchangeCoin>> Get()
        {
            return await db.Table<ExchangeCoin>().ToListAsync();
        }

        public async Task<IEnumerable<ExchangeCoin>> Get(List<int> ids)
        {
            return await db.Table<ExchangeCoin>().Where(e => ids.Contains(e.CurrencyId)).ToListAsync();
        }

        public async Task<ExchangeCoin> Get(int Id)
        {
            ExchangeCoin entity;

            try
            {
                entity = await db.Table<ExchangeCoin>().Where(e => e.Id == Id).FirstAsync();
            }
            catch
            {
                entity = null;
            }

            return entity;
        }

        public async Task<ExchangeCoin> Add(ExchangeCoin entity)
        {
            entity.Id = await db.InsertAsync(entity);

            return entity;
        }

        public async Task<IEnumerable<ExchangeCoin>> AddAll(IEnumerable<ExchangeCoin> entityList)
        {
            await db.InsertAllAsync(entityList);

            return entityList;
        }

        public async Task<ExchangeCoin> Update(ExchangeCoin entity)
        {
            await db.UpdateAsync(entity);

            return entity;
        }

        public async Task<IEnumerable<ExchangeCoin>> UpdateAll(IEnumerable<ExchangeCoin> entityList)
        {
            await db.UpdateAllAsync(entityList);

            return entityList;
        }

        public async Task<bool> Delete(ExchangeCoin entity)
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
                await db.DeleteAllAsync<ExchangeCoin>();
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
            await db.ExecuteScalarAsync<ExchangeCoin>($"UPDATE SQLITE_SEQUENCE SET SEQ=0 WHERE NAME='{typeof(ExchangeCoin).Name}'");
        }
    }
}