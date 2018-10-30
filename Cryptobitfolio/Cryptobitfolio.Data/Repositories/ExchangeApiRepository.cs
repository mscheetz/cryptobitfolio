using System.Collections.Generic;
using System.Threading.Tasks;
using Cryptobitfolio.Business.Entities.Trade;
using Cryptobitfolio.Data.Interfaces;
using SQLite;

namespace Cryptobitfolio.Data.Repositories
{
    public class ExchangeApiRepository : IExchangeApiRepository
    {
        private SQLiteAsyncConnection db;

        public ExchangeApiRepository()
        {
            var context = new SqliteContext();
            db = context.GetConnection<ExchangeApi>();

            db.CreateTableAsync<ExchangeApi>();
        }

        public async Task<List<ExchangeApi>> Get()
        {
            return await db.Table<ExchangeApi>().ToListAsync();
        }

        public async Task<ExchangeApi> Get(int Id)
        {
            return await db.Table<ExchangeApi>().Where(c => c.Id == Id).FirstAsync();
        }

        public async Task<ExchangeApi> Get(string exchangeName)
        {
            return await db.Table<ExchangeApi>().Where(c => c.ExchangeName.Equals(exchangeName)).FirstAsync();
        }

        public async Task<ExchangeApi> Add(ExchangeApi entity)
        {
            entity.Id = await db.InsertAsync(entity);

            return entity;
        }

        public async Task<List<ExchangeApi>> AddAll(List<ExchangeApi> entityList)
        {
            await db.InsertAllAsync(entityList);

            return entityList;
        }

        public async Task<ExchangeApi> Update(ExchangeApi entity)
        {
            await db.UpdateAsync(entity);

            return entity;
        }

        public async Task<List<ExchangeApi>> UpdateAll(List<ExchangeApi> entityList)
        {
            await db.UpdateAllAsync(entityList);

            return entityList;
        }

        public async Task Delete(ExchangeApi entity)
        {
            await db.DeleteAsync(entity);
        }
    }
}