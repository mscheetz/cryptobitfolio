using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Cryptobitfolio.Business.Entities;
using Cryptobitfolio.Business.Entities.Trade;
using Cryptobitfolio.Data.Interfaces;
using SQLite;

namespace Cryptobitfolio.Data.Repositories
{
    public class ExchangeUpdateRepository : IExchangeUpdateRepository
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

        public async Task<IEnumerable<ExchangeUpdate>> Get(Exchange exchange)
        {
            return await db.Table<ExchangeUpdate>().Where(e => e.Exchange.Equals(exchange.ToString())).ToListAsync();
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

        public async Task<List<ExchangeUpdate>> AddAll(List<ExchangeUpdate> entityList)
        {
            await db.InsertAllAsync(entityList);

            return entityList;
        }

        public async Task<ExchangeUpdate> Update(ExchangeUpdate entity)
        {
            await db.UpdateAsync(entity);

            return entity;
        }

        public async Task<List<ExchangeUpdate>> UpdateAll(List<ExchangeUpdate> entityList)
        {
            await db.UpdateAllAsync(entityList);

            return entityList;
        }

        public async Task<int> Delete(ExchangeUpdate entity)
        {
            return await db.DeleteAsync(entity);
        }

        public async Task<int> DeleteAll()
        {
            return await db.DeleteAllAsync<ExchangeUpdate>();
        }
    }
}