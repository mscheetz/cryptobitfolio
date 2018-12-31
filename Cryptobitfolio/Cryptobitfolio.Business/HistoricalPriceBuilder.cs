// -----------------------------------------------------------------------------
// <copyright file="HistoricalPriceBuilder" company="Matt Scheetz">
//     Copyright (c) Matt Scheetz All Rights Reserved
// </copyright>
// <author name="Matt Scheetz" date="12/11/2018 7:19:23 PM" />
// -----------------------------------------------------------------------------

namespace Cryptobitfolio.Business
{
    #region Usings

    using Cryptobitfolio.Business.Common;
    using Cryptobitfolio.Business.Contracts.Portfolio;
    using Cryptobitfolio.Business.Contracts.Trade;
    using Cryptobitfolio.Data.Interfaces.Database;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    #endregion Usings

    public class HistoricalPriceBuilder : IHistoricalPriceBuilder
    {
        #region Properties

        private IExchangeHubBuilder _hubBldr;
        private IHistoricalPriceRepository _hpRepo;

        #endregion Properties

        public HistoricalPriceBuilder(IExchangeHubBuilder exchangeHubBuilder, IHistoricalPriceRepository repo)
        {
            this._hubBldr = exchangeHubBuilder;
            this._hpRepo = repo;
        }

        public async Task<HistoricalPrice> Add(HistoricalPrice contract)
        {
            var entity = ContractToEntity(contract);
            entity = await _hpRepo.Add(entity);

            return EntityToContract(entity);
        }

        public async Task<IEnumerable<HistoricalPrice>> Add(IEnumerable<HistoricalPrice> contracts)
        {
            var entities = ContractsToEntities(contracts);
            entities = await _hpRepo.AddAll(entities);

            return EntitiesToContracts(entities);
        }

        public async Task<bool> Delete(HistoricalPrice contract)
        {
            var entity = ContractToEntity(contract);
            var result = await _hpRepo.Delete(entity);

            return result;
        }

        public async Task<HistoricalPrice> Update(HistoricalPrice contract)
        {
            var entity = ContractToEntity(contract);
            entity = await _hpRepo.Update(entity);

            return EntityToContract(entity);
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

        public async Task<IEnumerable<HistoricalPrice>> GetLatest(List<string> pairs)
        {
            return await OnGetLatest(pairs);
        }

        private async Task<IEnumerable<HistoricalPrice>> OnGetLatest(List<string> pairs)
        {
            var hps = await _hubBldr.GetStats(pairs);
            var hpList = hps.ToList();

            for (var i = 0; i < hpList.Count; i++)
            {
                hpList[i] = await this.Add(hpList[i]);
            }

            return hpList;
        }

        public async Task<IEnumerable<HistoricalPrice>> GetLatest(Dictionary<Entities.Exchange, List<string>> exchangePairs)
        {
            var hps = new List<HistoricalPrice>();
            foreach(var entry in exchangePairs)
            {
                var exchangeHps = await _hubBldr.GetStats(entry.Value, entry.Key);
                hps.AddRange(exchangeHps);
            }

            var hpList = await this.Add(hps);

            return hpList;
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
                HistoricalPriceId = entity.Id,
                Exchange = entity.Exchange,
                Pair = entity.Pair,
                Close = entity.Close,
                High = entity.High,
                Low = entity.Low,
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
                Id = contract.HistoricalPriceId,
                Exchange = contract.Exchange,
                Pair = contract.Pair,
                Close = contract.Close,
                High = contract.High,
                Low = contract.Low,
                Snapshot = contract.Snapshot
            };

            return entity;
        }
    }
}