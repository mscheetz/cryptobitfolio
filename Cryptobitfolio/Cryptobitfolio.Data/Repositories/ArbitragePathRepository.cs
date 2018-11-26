using System.Collections.Generic;
using System.Threading.Tasks;
using Cryptobitfolio.Business.Entities.Portfolio;
using Cryptobitfolio.Data.Interfaces;
using SQLite;

namespace Cryptobitfolio.Data.Repositories
{
    public class ArbitragePathRepository : IArbitragePathRepository
    {
        private SQLiteAsyncConnection db;

        public ArbitragePathRepository()
        {
            var context = new SqliteContext();
            db = context.GetConnection<Coin>();

            db.CreateTableAsync<ArbitragePath>();
        }

        public async Task<List<ArbitragePath>> Get()
        {
            return await db.Table<ArbitragePath>().ToListAsync();
        }

        public async Task<ArbitragePath> Get(int Id)
        {
            return await db.Table<ArbitragePath>().Where(c => c.Id == Id).FirstAsync();
        }

        public async Task<ArbitragePath> Add(ArbitragePath entity)
        {
            entity.Id = await db.InsertAsync(entity);

            return entity;
        }

        public async Task<List<ArbitragePath>> AddAll(List<ArbitragePath> entityList)
        {
            await db.InsertAllAsync(entityList);

            return entityList;
        }

        public async Task<ArbitragePath> Update(ArbitragePath entity)
        {
            await db.UpdateAsync(entity);

            return entity;
        }

        public async Task<List<ArbitragePath>> UpdateAll(List<ArbitragePath> entityList)
        {
            await db.UpdateAllAsync(entityList);

            return entityList;
        }

        public async Task Delete(ArbitragePath entity)
        {
            await db.DeleteAsync(entity);
        }
    }
}