// -----------------------------------------------------------------------------
// <copyright file="ExchangeCoinBuilder" company="Matt Scheetz">
//     Copyright (c) Matt Scheetz All Rights Reserved
// </copyright>
// <author name="Matt Scheetz" date="12/11/2018 7:19:23 PM" />
// -----------------------------------------------------------------------------

namespace Cryptobitfolio.Business
{
    using Cryptobitfolio.Business.Common;
    using Cryptobitfolio.Business.Contracts.Portfolio;
    using Cryptobitfolio.Data.Interfaces.Database;
    #region Usings

    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    #endregion Usings

    public class ExchangeCoinBuilder : IExchangeCoinBuilder
    {
        #region Properties

        ICoinBuyBuilder _cbBldr;
        IExchangeCoinRepository _ecRepo;

        #endregion Properties

        public ExchangeCoinBuilder(ICoinBuyBuilder coinBuyBuilder, IExchangeCoinRepository repo)
        {
            this._cbBldr = coinBuyBuilder;
            this._ecRepo = repo;
        }

        public async Task<ExchangeCoin> Add(ExchangeCoin contract)
        {
            var entity = ContractToEntity(contract);
            entity = await _ecRepo.Add(entity);

            return EntityToContract(entity);
        }

        public async Task<bool> Delete(ExchangeCoin contract)
        {
            var entity = ContractToEntity(contract);
            var result = await _ecRepo.Delete(entity);

            return result;
        }

        public async Task<IEnumerable<ExchangeCoin>> Get()
        {
            var entities = await _ecRepo.Get();

            return EntitiesToContracts(entities);
        }

        public async Task<ExchangeCoin> Update(ExchangeCoin contract)
        {
            var entity = ContractToEntity(contract);
            entity = await _ecRepo.Update(entity);

            return EntityToContract(entity);
        }

        private IEnumerable<ExchangeCoin> EntitiesToContracts(IEnumerable<Entities.Portfolio.ExchangeCoin> entities)
        {
            var contracts = new List<ExchangeCoin>();

            foreach(var entity in entities)
            {
                var contract = EntityToContract(entity);

                contracts.Add(contract);
            }

            return contracts;
        }

        private ExchangeCoin EntityToContract(Entities.Portfolio.ExchangeCoin entity)
        {
            var contract = new ExchangeCoin
            {
                ExchangeCoinId = entity.Id,
                Exchange = entity.Exchange,
                Quantity = entity.Quantity,
                Symbol = entity.Symbol
            };

            return contract;
        }

        private IEnumerable<Entities.Portfolio.ExchangeCoin> ContractsToEntities(IEnumerable<ExchangeCoin> contracts)
        {
            var entities = new List<Entities.Portfolio.ExchangeCoin>();
            foreach(var contract in contracts)
            {
                var entity = ContractToEntity(contract);

                entities.Add(entity);
            }

            return entities;
        }

        private Entities.Portfolio.ExchangeCoin ContractToEntity(ExchangeCoin contract)
        {
            var entity = new Entities.Portfolio.ExchangeCoin
            {
                Id = contract.ExchangeCoinId,
                AverageBuy = contract.AverageBuy,
                Exchange = contract.Exchange,
                Quantity = contract.Quantity,
                Symbol = contract.Symbol
            };

            return entity;
        }
    }
}