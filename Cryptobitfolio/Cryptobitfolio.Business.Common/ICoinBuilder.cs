// -----------------------------------------------------------------------------
// <copyright file="ICoinBuilder" company="Matt Scheetz">
//     Copyright (c) Matt Scheetz All Rights Reserved
// </copyright>
// <author name="Matt Scheetz" date="12/11/2018 7:19:32 PM" />
// -----------------------------------------------------------------------------

namespace Cryptobitfolio.Business.Common
{
    using Cryptobitfolio.Business.Contracts.Portfolio;
    #region Usings

    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    #endregion Usings

    public interface ICoinBuilder
    {
        Task<Coin> Add(Coin contract);

        Task<bool> Delete(Coin contract);

        Task<IEnumerable<Coin>> Get();

        Task<Coin> Update(Coin contract);
    }
}