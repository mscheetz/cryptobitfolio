using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Cryptobitfolio.Business.Entities.Trade;
using Cryptobitfolio.Data.Interfaces.Database;
using SQLite;

namespace Cryptobitfolio.Data.Repositories
{
    public class ExchangeOrderRepository : IDatabaseRepository<ExchangeOrder>
    {
        private SQLiteAsyncConnection db;

        public ExchangeOrderRepository()
        {
            var context = new SqliteContext();
            db = context.GetConnection<ExchangeOrder>();

            db.CreateTableAsync<ExchangeOrder>();
        }

        public async Task<IEnumerable<ExchangeOrder>> Get()
        {
            return await db.Table<ExchangeOrder>().ToListAsync();
        }

        public async Task<IEnumerable<ExchangeOrder>> Get(List<int> ids)
        {
            return await db.Table<ExchangeOrder>().Where(e => ids.Contains(e.Id)).ToListAsync();
        }

        public async Task<ExchangeOrder> Get(int Id)
        {
            ExchangeOrder entity;

            try
            {
                entity = await db.Table<ExchangeOrder>().Where(e => e.Id == Id).FirstAsync();
            }
            catch
            {
                entity = null;
            }

            return entity;
        }

        public async Task<ExchangeOrder> Add(ExchangeOrder entity)
        {
            entity.Id = await db.InsertAsync(entity);

            return entity;
        }

        public async Task<IEnumerable<ExchangeOrder>> AddAll(IEnumerable<ExchangeOrder> entityList)
        {
            await db.InsertAllAsync(entityList);

            return entityList;
        }

        public async Task<ExchangeOrder> Update(ExchangeOrder entity)
        {
            await db.UpdateAsync(entity);

            return entity;
        }

        public async Task<IEnumerable<ExchangeOrder>> UpdateAll(IEnumerable<ExchangeOrder> entityList)
        {
            await db.UpdateAllAsync(entityList);

            return entityList;
        }

        public async Task<bool> Delete(ExchangeOrder entity)
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
                await db.DeleteAllAsync<ExchangeOrder>();
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
            await db.ExecuteScalarAsync<ExchangeOrder>($"UPDATE SQLITE_SEQUENCE SET SEQ=0 WHERE NAME='{typeof(ExchangeOrder).Name}'");
        }
    }
}