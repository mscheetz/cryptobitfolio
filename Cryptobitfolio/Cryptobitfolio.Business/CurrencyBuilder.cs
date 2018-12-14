// -----------------------------------------------------------------------------
// <copyright file="CurrencyBuilder" company="Matt Scheetz">
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

    public class CurrencyBuilder : ICurrencyBuilder
    {
        #region Properties

        private ICMCBuilder _cmcBldr;
        private ICurrencyRepository _currencyRepo;

        #endregion Properties

        public CurrencyBuilder(ICMCBuilder cmcBuilder, ICurrencyRepository repo)
        {
            this._cmcBldr = cmcBuilder;
            this._currencyRepo = repo;
        }

        public async Task<Currency> Add(Currency contract)
        {
            var entity = ContractToEntity(contract);
            entity = await _currencyRepo.Add(entity);

            return EntityToContract(entity);
        }

        public async Task<bool> Delete(Currency contract)
        {
            var entity = ContractToEntity(contract);
            var result = await _currencyRepo.Delete(entity);

            return result;
        }

        public async Task<IEnumerable<Currency>> Get()
        {
            var entities = await _currencyRepo.Get();

            return EntitiesToContracts(entities);
        }

        public async Task<IEnumerable<Currency>> Get(string symbol)
        {
            var entities = await _currencyRepo.Get(c => c.Symbol.Equals(symbol));

            return EntitiesToContracts(entities);
        }

        public async Task<Currency> Update(Currency contract)
        {
            var entity = ContractToEntity(contract);
            entity = await _currencyRepo.Update(entity);

            return EntityToContract(entity);
        }

        public async Task<IEnumerable<Currency>> GetLatest(List<string> symbols)
        {
            var cmcList = await _cmcBldr.GetCurrencies(symbols);

            return null;
        }

        private IEnumerable<Currency> EntitiesToContracts(IEnumerable<Entities.Portfolio.Currency> entities)
        {
            var contracts = new List<Currency>();

            foreach(var entity in entities)
            {
                var contract = EntityToContract(entity);

                contracts.Add(contract);
            }

            return contracts;
        }

        private Currency EntityToContract(Entities.Portfolio.Currency entity)
        {
            var contract = new Currency
            {
                CurrencyId = entity.Id,
                Image = entity.Image,
                Name = entity.Name,
                Symbol = entity.Symbol
            };

            return contract;
        }

        private IEnumerable<Entities.Portfolio.Currency> ContractsToEntities(IEnumerable<Currency> contracts)
        {
            var entities = new List<Entities.Portfolio.Currency>();
            foreach(var contract in contracts)
            {
                var entity = ContractToEntity(contract);

                entities.Add(entity);
            }

            return entities;
        }

        private Entities.Portfolio.Currency ContractToEntity(Currency contract)
        {
            var entity = new Entities.Portfolio.Currency
            {
                Id = contract.CurrencyId,
                Image = contract.Image,
                Name = contract.Name,
                Symbol = contract.Symbol
            };

            return entity;
        }
    }
}