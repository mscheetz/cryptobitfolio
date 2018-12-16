// -----------------------------------------------------------------------------
// <copyright file="ExchangeApiBuilder" company="Matt Scheetz">
//     Copyright (c) Matt Scheetz All Rights Reserved
// </copyright>
// <author name="Matt Scheetz" date="12/7/2018 9:16:47 PM" />
// -----------------------------------------------------------------------------

namespace Cryptobitfolio.Business
{
    #region Usings

    using Cryptobitfolio.Business.Common;
    using Cryptobitfolio.Business.Contracts.Trade;
    using Cryptobitfolio.Business.Entities;
    using Cryptobitfolio.Data.Interfaces.Database;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    #endregion Usings

    public class ExchangeApiBuilder : IExchangeApiBuilder
    {
        #region Properties

        private readonly IExchangeApiRepository _exchangeApiRepo;

        #endregion Properties

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="exhangeApiRepo">Exchange Api repository</param>
        /// <param name="exchangeUpdateRepo">Exchange Update repository</param>
        /// <param name="arbitragePathRepo">Arbitrage Path repository</param>
        /// <param name="arbitrageBuilder">Arbitrage builder</param>
        /// <param name="cmcBuilder">CMC builder</param>
        public ExchangeApiBuilder(IExchangeApiRepository exhangeApiRepo)
        {
            _exchangeApiRepo = exhangeApiRepo;
        }

        #region ExchangeApi Methods

        /// <summary>
        /// Get all ExchangeApis
        /// </summary>
        /// <returns>Collection of ExchangeApis</returns>
        public async Task<IEnumerable<ExchangeApi>> Get()
        {
            var entities = await _exchangeApiRepo.Get();
            var contractList = new List<ExchangeApi>();

            foreach (var entity in entities)
            {
                var contract = ExchangeApiEntityToContract(entity);

                contractList.Add(contract);
            }

            return contractList;
        }

        /// <summary>
        /// Get all ExchangeApis for a given exchange
        /// </summary>
        /// <param name="exchange">Exchange to find</param>
        /// <returns>Collection of ExchangeApis</returns>
        public async Task<IEnumerable<ExchangeApi>> Get(Exchange exchange)
        {
            var entities = await _exchangeApiRepo.Get(e => e.Exchange == exchange);
            var contractList = new List<ExchangeApi>();

            foreach (var entity in entities)
            {
                var contract = ExchangeApiEntityToContract(entity);

                contractList.Add(contract);
            }

            return contractList;
        }

        /// <summary>
        /// Save exhange api to database
        /// </summary>
        /// <param name="exchangeApi">ExchangeApi to save</param>
        /// <returns>Updated ExchangeApi object</returns>
        public async Task<ExchangeApi> Add(ExchangeApi exchangeApi)
        {
            var entity = ExchangeApiContractToEntity(exchangeApi);

            entity = entity.Id == 0
                        ? await _exchangeApiRepo.Add(entity)
                        : await _exchangeApiRepo.Update(entity);

            exchangeApi.ExchangeApiId = entity.Id;

            return exchangeApi;
        }

        /// <summary>
        /// Delete an ExchangeApi
        /// </summary>
        /// <param name="exchangeApi">ExchangeApi to delete</param>
        /// <returns>Boolean value of deletion attempt</returns>
        public async Task<bool> Delete(ExchangeApi exchangeApi)
        {
            var entity = ExchangeApiContractToEntity(exchangeApi);

            try
            {
                await _exchangeApiRepo.Delete(entity);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        #endregion ExchangeApi Methods

        #region ExchangeApi converters

        private ExchangeApi ExchangeApiEntityToContract(Entities.Trade.ExchangeApi entity)
        {
            var contract = new ExchangeApi
            {
                ApiExtra = entity.ApiExtra,
                ApiKey = entity.ApiKey,
                ApiKeyName = entity.ApiKeyName,
                ApiSecret = entity.ApiSecret,
                Exchange = entity.Exchange,
                ExchangeApiId = entity.Id
            };

            return contract;
        }

        private Entities.Trade.ExchangeApi ExchangeApiContractToEntity(ExchangeApi contract)
        {
            var entity = new Entities.Trade.ExchangeApi
            {
                ApiExtra = contract.ApiExtra,
                ApiKey = contract.ApiKey,
                ApiKeyName = contract.ApiKeyName,
                ApiSecret = contract.ApiSecret,
                Exchange = contract.Exchange,
                Id = contract.ExchangeApiId
            };

            return entity;
        }

        #endregion ExchangeApi converters
    }
}