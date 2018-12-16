// -----------------------------------------------------------------------------
// <copyright file="ICurrencyBuilder" company="Matt Scheetz">
//     Copyright (c) Matt Scheetz All Rights Reserved
// </copyright>
// <author name="Matt Scheetz" date="12/11/2018 7:19:32 PM" />
// -----------------------------------------------------------------------------

namespace Cryptobitfolio.Business.Common
{
    #region Usings

    using Cryptobitfolio.Business.Contracts.Portfolio;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    #endregion Usings

    public interface ICurrencyBuilder
    {
        Task<Currency> Add(Currency contract);

        Task<bool> Delete(Currency contract);

        Task<Currency> Update(Currency contract);

        Task<IEnumerable<Currency>> Get();

        Task<IEnumerable<Currency>> Get(string symbol);

        Task<IEnumerable<Currency>> Get(List<string> symbols);

        Task<IEnumerable<Currency>> GetLatest(List<string> symbols);
    }
}