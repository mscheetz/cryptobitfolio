﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Cryptobitfolio.Business.Entities.Portfolio;
using Cryptobitfolio.Data.Interfaces;
using Cryptobitfolio.Data.Interfaces.Database;
using SQLite;

namespace Cryptobitfolio.Data.Repositories
{
    public class ArbitragePathRepository : IDatabaseRepository<ArbitragePath>
    {
        private SQLiteAsyncConnection db;

        public ArbitragePathRepository()
        {
            var context = new SqliteContext();
            db = context.GetConnection<ArbitragePath>();

            db.CreateTableAsync<ArbitragePath>();
        }

        public async Task<IEnumerable<ArbitragePath>> Get()
        {
            return await db.Table<ArbitragePath>().ToListAsync();
        }

        public async Task<IEnumerable<ArbitragePath>> Get(List<int> ids)
        {
            return await db.Table<ArbitragePath>().Where(e => ids.Contains(e.Id)).ToListAsync();
        }

        public async Task<ArbitragePath> Get(int Id)
        {
            ArbitragePath entity;

            try
            {
                entity = await db.Table<ArbitragePath>().Where(e => e.Id == Id).FirstAsync();
            }
            catch
            {
                entity = null;
            }

            return entity;
        }

        public async Task<ArbitragePath> Add(ArbitragePath entity)
        {
            entity.Id = await db.InsertAsync(entity);

            return entity;
        }

        public async Task<IEnumerable<ArbitragePath>> AddAll(IEnumerable<ArbitragePath> entityList)
        {
            await db.InsertAllAsync(entityList);

            return entityList;
        }

        public async Task<ArbitragePath> Update(ArbitragePath entity)
        {
            await db.UpdateAsync(entity);

            return entity;
        }

        public async Task<IEnumerable<ArbitragePath>> UpdateAll(IEnumerable<ArbitragePath> entityList)
        {
            await db.UpdateAllAsync(entityList);

            return entityList;
        }

        public async Task<bool> Delete(ArbitragePath entity)
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
                await db.DeleteAllAsync<ArbitragePath>();
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
            await db.ExecuteScalarAsync<ArbitragePath>($"UPDATE SQLITE_SEQUENCE SET SEQ=0 WHERE NAME='{typeof(ArbitragePath).Name}'");
        }
    }
}