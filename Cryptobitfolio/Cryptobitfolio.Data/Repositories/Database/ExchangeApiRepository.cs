﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Cryptobitfolio.Business.Entities.Trade;
using Cryptobitfolio.Data.Interfaces;
using Cryptobitfolio.Data.Interfaces.Database;
using SQLite;

namespace Cryptobitfolio.Data.Repositories
{
    public class ExchangeApiRepository : IDatabaseRepository<ExchangeApi>
    {
        private SQLiteAsyncConnection db;

        public ExchangeApiRepository()
        {
            var context = new SqliteContext();
            db = context.GetConnection<ExchangeApi>();

            db.CreateTableAsync<ExchangeApi>();
        }

        public async Task<IEnumerable<ExchangeApi>> Get()
        {
            return await db.Table<ExchangeApi>().ToListAsync();
        }

        public async Task<IEnumerable<ExchangeApi>> Get(List<int> ids)
        {
            return await db.Table<ExchangeApi>().Where(e => ids.Contains(e.Id)).ToListAsync();
        }

        public async Task<ExchangeApi> Get(int Id)
        {
            ExchangeApi entity;

            try
            {
                entity = await db.Table<ExchangeApi>().Where(e => e.Id == Id).FirstAsync();
            }
            catch
            {
                entity = null;
            }

            return entity;
        }

        public async Task<ExchangeApi> Add(ExchangeApi entity)
        {
            entity.Id = await db.InsertAsync(entity);

            return entity;
        }

        public async Task<IEnumerable<ExchangeApi>> AddAll(IEnumerable<ExchangeApi> entityList)
        {
            await db.InsertAllAsync(entityList);

            return entityList;
        }

        public async Task<ExchangeApi> Update(ExchangeApi entity)
        {
            await db.UpdateAsync(entity);

            return entity;
        }

        public async Task<IEnumerable<ExchangeApi>> UpdateAll(IEnumerable<ExchangeApi> entityList)
        {
            await db.UpdateAllAsync(entityList);

            return entityList;
        }

        public async Task<bool> Delete(ExchangeApi entity)
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
                await db.DeleteAllAsync<ExchangeApi>();
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
            await db.ExecuteScalarAsync<ExchangeApi>($"UPDATE SQLITE_SEQUENCE SET SEQ=0 WHERE NAME='{typeof(ExchangeApi).Name}'");
        }
    }
}