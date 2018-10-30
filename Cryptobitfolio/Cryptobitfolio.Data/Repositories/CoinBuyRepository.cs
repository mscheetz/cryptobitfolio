using System.Collections.Generic;
using System.Threading.Tasks;
using Cryptobitfolio.Business.Entities.Portfolio;
using Cryptobitfolio.Data.Interfaces;
using SQLite;

namespace Cryptobitfolio.Data.Repositories
{
    public class CoinBuyRepository : ICoinBuyRepository
    {
        private SQLiteAsyncConnection db;

        public CoinBuyRepository()
        {
            var context = new SqliteContext();
            db = context.GetConnection<CoinBuy>();

            db.CreateTableAsync<CoinBuy>();
        }

        public async Task<List<CoinBuy>> Get()
        {
            return await db.Table<CoinBuy>().ToListAsync();
        }

        public async Task<CoinBuy> Get(int Id)
        {
            return await db.Table<CoinBuy>().Where(c => c.Id == Id).FirstAsync();
        }

        public async Task<CoinBuy> Add(CoinBuy entity)
        {
            entity.Id = await db.InsertAsync(entity);

            return entity;
        }

        public async Task<List<CoinBuy>> AddAll(List<CoinBuy> entityList)
        {
            await db.InsertAllAsync(entityList);

            return entityList;
        }

        public async Task<CoinBuy> Update(CoinBuy entity)
        {
            await db.UpdateAsync(entity);

            return entity;
        }

        public async Task<List<CoinBuy>> UpdateAll(List<CoinBuy> entityList)
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