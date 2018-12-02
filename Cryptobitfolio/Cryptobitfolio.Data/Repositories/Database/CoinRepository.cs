using System;
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
            Coin entity;

            try
            {
                entity = await db.Table<Coin>().Where(e => e.Id == Id).FirstAsync();
            }
            catch
            {
                entity = null;
            }

            return entity;
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

        public async Task<bool> Delete(Coin entity)
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
                await db.DeleteAllAsync<Coin>();
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
            await db.ExecuteScalarAsync<Coin>($"UPDATE SQLITE_SEQUENCE SET SEQ=0 WHERE NAME='{typeof(Coin).Name}'");
        }
    }
}