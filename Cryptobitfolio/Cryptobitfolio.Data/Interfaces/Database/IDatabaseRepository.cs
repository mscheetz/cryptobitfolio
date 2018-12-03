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
    using System.Text;
    using System.Threading.Tasks;

    #endregion Usings

    public interface IDatabaseRepository<T>
    {
        Task<IEnumerable<T>> Get();

        Task<IEnumerable<T>> Get(List<int> ids);

        Task<T> Get(int Id);

        Task<T> Add(T entity);

        Task<IEnumerable<T>> AddAll(IEnumerable<T> entityList);

        Task<T> Update(T entity);

        Task<IEnumerable<T>> UpdateAll(IEnumerable<T> entityList);

        Task<bool> Delete(T entity);

        Task<bool> DeleteAll();
    }
}