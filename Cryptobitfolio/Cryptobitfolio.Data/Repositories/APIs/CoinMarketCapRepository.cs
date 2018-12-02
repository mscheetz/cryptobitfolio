// -----------------------------------------------------------------------------
// <copyright file="CoinMarketCapRepository" company="Matt Scheetz">
//     Copyright (c) Matt Scheetz All Rights Reserved
// </copyright>
// <author name="Matt Scheetz" date="11/30/2018 9:07:09 PM" />
// -----------------------------------------------------------------------------

namespace Cryptobitfolio.Data.Repositories
{
    using CoinMarketCap.Net;
    using CoinMarketCap.Net.Contracts;
    using Cryptobitfolio.Data.Interfaces;
    #region Usings

    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    #endregion Usings

    public class CoinMarketCapRepository : ICoinMarketCapRepository
    {
        #region Properties

        private CMCClient cmc;
        private const string apiKey = "";

        #endregion Properties

        #region Constructor

        public CoinMarketCapRepository()
        {
            cmc = new CMCClient(apiKey);
        }

        #endregion Constructor

        public async Task<Dictionary<string, Metadata>> GetMetadatas(List<string> symbols)
        {
            return await cmc.GetMetadataAsync(symbols);
        }
    }
}