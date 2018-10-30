using System.Collections.Generic;
using System.Threading.Tasks;
using Cryptobitfolio.Business.Entities.Portfolio;
using Cryptobitfolio.Data.Interfaces;
using SQLite;

namespace Cryptobitfolio.Data.Repositories
{
    public class CoinRepository : ICoinRepository
    {
        private SQLiteAsyncConnection db;

        public CoinRepository()
        {
            var context = new SqliteContext();
            db = context.GetConnection<Coin>();

            db.CreateTableAsync<Coin>();
        }

        public async Task<List<Coin>> Get()
        {
            return await db.Table<Coin>().ToListAsync();
        }

        public async Task<Coin> Get(int Id)
        {
            return await db.Table<Coin>().Where(c => c.Id == Id).FirstAsync();
        }

        public async Task<Coin> Add(Coin entity)
        {
            entity.Id = await db.InsertAsync(entity);

            return entity;
        }

        public async Task<List<Coin>> AddAll(List<Coin> entityList)
        {
            await db.InsertAllAsync(entityList);

            return entityList;
        }

        public async Task<Coin> Update(Coin entity)
        {
            await db.UpdateAsync(entity);

            return entity;
        }

        public async Task<List<Coin>> UpdateAll(List<Coin> entityList)
        {
            await db.UpdateAllAsync(entityList);

            return entityList;
        }

        public async Task Delete(Coin entity)
        {
            await db.DeleteAsync(entity);
        }
    }
}