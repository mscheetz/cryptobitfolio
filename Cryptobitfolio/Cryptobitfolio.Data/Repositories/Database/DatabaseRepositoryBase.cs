// -----------------------------------------------------------------------------
// <copyright file="DataRepositoryBase" company="Matt Scheetz">
//     Copyright (c) Matt Scheetz All Rights Reserved
// </copyright>
// <author name="Matt Scheetz" date="12/3/2018 7:29:29 PM" />
// -----------------------------------------------------------------------------

namespace Cryptobitfolio.Data.Repositories.Database
{
    #region Usings

    using Cryptobitfolio.Business.Entities;
    using SQLite;
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Threading.Tasks;

    #endregion Usings

    public class DatabaseRepositoryBase<T> where T : EntityBase, new()
    {
        private SQLiteAsyncConnection db;

        public DatabaseRepositoryBase(SqliteContext context)
        {
            db = context.GetConnection<T>();

            db.CreateTableAsync<T>();
        }

        public SQLiteAsyncConnection GetConnection()
        {
            return db;
        }

        public async Task<IEnumerable<T>> Get()
        {
            return await db.Table<T>().ToListAsync();
        }

        public async Task<IEnumerable<T>> Get(List<int> ids)
        {
            return await db.Table<T>().Where(e => ids.Contains(e.Id)).ToListAsync();
        }

        public async Task<IEnumerable<T>> Get(Expression<Func<T, bool>> predicate)
        {
            var query = db.Table<T>();

            query = query.Where(predicate);

            return await query.ToListAsync();
        }

        public async Task<IEnumerable<T>> Get<TValue>(Expression<Func<T, TValue>> orderBy)
        {
            return await OnGet<TValue>(null, orderBy);
        }

        public async Task<IEnumerable<T>> Get<TValue>(Expression<Func<T, bool>> predicate = null, Expression<Func<T, TValue>> orderBy = null)
        {
            return await OnGet<TValue>(predicate, orderBy);
        }

        private async Task<IEnumerable<T>> OnGet<TValue>(Expression<Func<T, bool>> predicate = null, Expression<Func<T, TValue>> orderBy = null)
        {
            var query = db.Table<T>();

            if (predicate != null)
                query = query.Where(predicate);

            if (orderBy != null)
                query = query.OrderBy<TValue>(orderBy);

            return await query.ToListAsync();
        }


        public async Task<T> GetOne(int Id)
        {
            T entity;

            try
            {
                entity = await db.Table<T>().Where(e => e.Id == Id).FirstAsync();
            }
            catch
            {
                entity = null;
            }

            return entity;
        }

        public async Task<T> GetOne(Expression<Func<T, bool>> predicate) =>
            await db.FindAsync<T>(predicate);

        public async Task<T> Add(T entity)
        {
            entity.Id = await db.InsertAsync(entity);

            return entity;
        }

        public async Task<IEnumerable<T>> AddAll(IEnumerable<T> entityList)
        {
            await db.InsertAllAsync(entityList);

            return entityList;
        }

        public async Task<T> Update(T entity)
        {
            await db.UpdateAsync(entity);

            return entity;
        }

        public async Task<IEnumerable<T>> UpdateAll(IEnumerable<T> entityList)
        {
            await db.UpdateAllAsync(entityList);

            return entityList;
        }

        public async Task<bool> Delete(T entity)
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
                await db.DeleteAllAsync<T>();
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
            await db.ExecuteScalarAsync<T>($"UPDATE SQLITE_SEQUENCE SET SEQ=0 WHERE NAME='{typeof(T).Name}'");
        }
    }
}