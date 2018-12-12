// -----------------------------------------------------------------------------
// <copyright file="HistoricalPriceBuilder" company="Matt Scheetz">
//     Copyright (c) Matt Scheetz All Rights Reserved
// </copyright>
// <author name="Matt Scheetz" date="12/11/2018 7:19:23 PM" />
// -----------------------------------------------------------------------------

namespace Cryptobitfolio.Business
{
    using Cryptobitfolio.Business.Common;
    using Cryptobitfolio.Business.Contracts.Trade;
    using Cryptobitfolio.Data.Interfaces.Database;
    #region Usings

    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    #endregion Usings

    public class HistoricalPriceBuilder : IHistoricalPriceBuilder
    {
        #region Properties

        IHistoricalPriceRepository _hpRepo;

        #endregion Properties

        public HistoricalPriceBuilder(IHistoricalPriceRepository repo)
        {
            this._hpRepo = repo;
        }

        public async Task<HistoricalPrice> Add(HistoricalPrice contract)
        {
            var entity = ContractToEntity(contract);
            entity = await _hpRepo.Add(entity);

            return EntityToContract(entity);
        }

        public async Task<bool> Delete(HistoricalPrice contract)
        {
            var entity = ContractToEntity(contract);
            var result = await _hpRepo.Delete(entity);

            return result;
        }

        public async Task<IEnumerable<HistoricalPrice>> Get()
        {
            var entities = await _hpRepo.Get();

            return EntitiesToContracts(entities);
        }

        public async Task<IEnumerable<HistoricalPrice>> Get(string pair)
        {
            var entities = await _hpRepo.Get(h => h.Pair.Equals(pair));

            return EntitiesToContracts(entities);
        }

        public async Task<HistoricalPrice> Update(HistoricalPrice contract)
        {
            var entity = ContractToEntity(contract);
            entity = await _hpRepo.Update(entity);

            return EntityToContract(entity);
        }

        private IEnumerable<HistoricalPrice> EntitiesToContracts(IEnumerable<Entities.Trade.HistoricalPrice> entities)
        {
            var contracts = new List<HistoricalPrice>();

            foreach(var entity in entities)
            {
                var contract = EntityToContract(entity);

                contracts.Add(contract);
            }

            return contracts;
        }

        private HistoricalPrice EntityToContract(Entities.Trade.HistoricalPrice entity)
        {
            var contract = new HistoricalPrice
            {
                Exchange = entity.Exchange,
                Pair = entity.Pair,
                Price = entity.Price,
                Snapshot = entity.Snapshot
            };

            return contract;
        }

        private IEnumerable<Entities.Trade.HistoricalPrice> ContractsToEntities(IEnumerable<HistoricalPrice> contracts)
        {
            var entities = new List<Entities.Trade.HistoricalPrice>();
            foreach(var contract in contracts)
            {
                var entity = ContractToEntity(contract);

                entities.Add(entity);
            }

            return entities;
        }

        private Entities.Trade.HistoricalPrice ContractToEntity(HistoricalPrice contract)
        {
            var entity = new Entities.Trade.HistoricalPrice
            {
                Exchange = contract.Exchange,
                Pair = contract.Pair,
                Price = contract.Price,
                Snapshot = contract.Snapshot
            };

            return entity;
        }
    }
}