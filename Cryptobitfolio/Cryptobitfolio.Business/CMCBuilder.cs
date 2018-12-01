// -----------------------------------------------------------------------------
// <copyright file="CMCBuilder" company="Matt Scheetz">
//     Copyright (c) Matt Scheetz All Rights Reserved
// </copyright>
// <author name="Matt Scheetz" date="11/30/2018 9:14:06 PM" />
// -----------------------------------------------------------------------------

namespace Cryptobitfolio.Business
{
    #region Usings

    using CoinMarketCap.Net.Contracts;
    using CoinMarketCap.Net.DataAccess.Interfaces;
    using Cryptobitfolio.Business.Common;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    #endregion Usings

    public class CMCBuilder : ICMCBuilder
    {
        #region Properties

        private ICoinMarketCapRepository cmcRepository;

        #endregion Properties

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="cmcRepo">CMC repository interface</param>
        public CMCBuilder(ICoinMarketCapRepository cmcRepo)
        {
            cmcRepository = cmcRepo;
        }

        /// <summary>
        /// Get Currency data for symbols
        /// </summary>
        /// <param name="symbols">Collection of currency symbols</param>
        /// <returns>Collection of Currency objects</returns>
        public async Task<IEnumerable<Contracts.Portfolio.Currency>> GetCurrencies(List<string> symbols)
        {
            var datas = await cmcRepository.GetMetadata(symbols);

            return CMCMetadataCollectionConverter(datas);
        }

        /// <summary>
        /// Convert a Dictionary of CMC meta data to a collection of Currency
        /// </summary>
        /// <param name="datas">Dictionary to convert</param>
        /// <returns>Collection of Currency</returns>
        private IEnumerable<Contracts.Portfolio.Currency> CMCMetadataCollectionConverter(Dictionary<string, Metadata> datas)
        {
            var currencyList = new List<Contracts.Portfolio.Currency>();

            foreach(var item in datas)
            {
                var currency = CMCMetadataConverter(item.Value);

                currencyList.Add(currency);
            }

            return currencyList;
        }

        /// <summary>
        /// Convert CMC Metadata to a Currency object
        /// </summary>
        /// <param name="data">Metadata to convert</param>
        /// <returns>new Currency object</returns>
        private Contracts.Portfolio.Currency CMCMetadataConverter(Metadata data)
        {
            var currency = new Contracts.Portfolio.Currency
            {
                Image = data.Logo,
                Name = data.Name,
                Symbol = data.Symbol
            };

            return currency;
        }
    }
}