using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Cryptobitfolio.Business.Entities;
using Cryptobitfolio.Business.Entities.Trade;
using Cryptobitfolio.Data.Interfaces;
using Cryptobitfolio.Data.Interfaces.Database;
using SQLite;

namespace Cryptobitfolio.Data.Repositories
{
    public class ExchangeUpdateRepository : IDatabaseRepository<ExchangeUpdate>
    {
        private SQLiteAsyncConnection db;

        public ExchangeUpdateRepository()
        {
            var context = new SqliteContext();
            db = context.GetConnection<ExchangeUpdate>();

            db.CreateTableAsync<ExchangeUpdate>();
        }

        public async Task<IEnumerable<ExchangeUpdate>> Get()
        {
            return await db.Table<ExchangeUpdate>().ToListAsync();
        }

        public async Task<IEnumerable<ExchangeUpdate>> Get(List<int> ids)
        {
            return await db.Table<ExchangeUpdate>().Where(e => ids.Contains(e.Id)).ToListAsync();
        }

        public async Task<ExchangeUpdate> Get(int id)
        {
            ExchangeUpdate entity;

            try
            {
                entity = await db.Table<ExchangeUpdate>().Where(c => c.Id == id).FirstAsync();
            }
            catch
            {
                entity = null;
            }

            return entity;
        }

        public async Task<ExchangeUpdate> Add(ExchangeUpdate entity)
        {
            try
            {
                await db.InsertAsync(entity);

                return entity;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<ExchangeUpdate>> AddAll(IEnumerable<ExchangeUpdate> entityList)
        {
            await db.InsertAllAsync(entityList);

            return entityList;
        }

        public async Task<ExchangeUpdate> Update(ExchangeUpdate entity)
        {
            await db.UpdateAsync(entity);

            return entity;
        }

        public async Task<IEnumerable<ExchangeUpdate>> UpdateAll(IEnumerable<ExchangeUpdate> entityList)
        {
            await db.UpdateAllAsync(entityList);

            return entityList;
        }

        public async Task<bool> Delete(ExchangeUpdate entity)
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
                await db.DeleteAllAsync<ExchangeUpdate>();
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
            await db.ExecuteScalarAsync<ExchangeUpdate>($"UPDATE SQLITE_SEQUENCE SET SEQ=0 WHERE NAME='{typeof(ExchangeUpdate).Name}'");
        }
    }
}