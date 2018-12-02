// -----------------------------------------------------------------------------
// <copyright file="ICoinMarketCapRepository" company="Matt Scheetz">
//     Copyright (c) Matt Scheetz All Rights Reserved
// </copyright>
// <author name="Matt Scheetz" date="11/30/2018 9:07:15 PM" />
// -----------------------------------------------------------------------------

namespace Cryptobitfolio.Data.Interfaces
{
    using CoinMarketCap.Net.Contracts;
    #region Usings

    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    #endregion Usings

    public interface ICoinMarketCapRepository
    {
        Task<Dictionary<string, Metadata>> GetMetadatas(List<string> symbols);
    }
}