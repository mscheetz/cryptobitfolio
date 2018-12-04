// -----------------------------------------------------------------------------
// <copyright file="IDatabaseRepository" company="Matt Scheetz">
//     Copyright (c) Matt Scheetz All Rights Reserved
// </copyright>
// <author name="Matt Scheetz" date="12/2/2018 7:15:49 PM" />
// -----------------------------------------------------------------------------

namespace Cryptobitfolio.Data.Interfaces.Database
{
    #region Usings

    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Text;
    using System.Threading.Tasks;

    #endregion Usings

    public interface IDatabaseRepositoryBase<T>
    {
        Task<IEnumerable<T>> Get();

        Task<IEnumerable<T>> Get(Expression<Func<T, bool>> predicate);

        Task<IEnumerable<T>> Get<TValue>(Expression<Func<T, TValue>> orderBy);

        Task<IEnumerable<T>> Get<TValue>(Expression<Func<T, bool>> predicate = null, Expression<Func<T, TValue>> orderBy = null);

        Task<T> GetOne(int Id);

        Task<T> GetOne(Expression<Func<T, bool>> predicate);

        Task<T> Add(T entity);

        Task<IEnumerable<T>> AddAll(IEnumerable<T> entityList);

        Task<T> Update(T entity);

        Task<IEnumerable<T>> UpdateAll(IEnumerable<T> entityList);

        Task<bool> Delete(T entity);

        Task<bool> DeleteAll();
    }
}