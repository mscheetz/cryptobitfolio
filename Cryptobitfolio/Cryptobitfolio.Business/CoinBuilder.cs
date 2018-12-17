// -----------------------------------------------------------------------------
// <copyright file="CoinBuilder" company="Matt Scheetz">
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
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    #endregion Usings

    public class CoinBuilder : ICoinBuilder
    {
        #region Properties

        private ICoinRepository _coinRepo;
        private ICurrencyBuilder _currencyBldr;
        private IExchangeCoinBuilder _xchgCoinBldr;

        #endregion Properties

        public CoinBuilder(ICoinRepository repo, ICurrencyBuilder currencyBuilder, IExchangeCoinBuilder exchangeCoinBuilder)
        {
            this._coinRepo = repo;
            this._currencyBldr = currencyBuilder;
            this._xchgCoinBldr = exchangeCoinBuilder;
        }

        public async Task<Coin> Add(Coin contract)
        {
            var entity = ContractToEntity(contract);
            entity = await _coinRepo.Add(entity);

            return EntityToContract(entity);
        }

        public async Task<bool> Delete(Coin contract)
        {
            var entity = ContractToEntity(contract);
            var result = await _coinRepo.Delete(entity);

            return result;
        }

        public async Task<Coin> Update(Coin contract)
        {
            var entity = ContractToEntity(contract);
            entity = await _coinRepo.Update(entity);

            return EntityToContract(entity);
        }

        public async Task<IEnumerable<Coin>> Get()
        {
            var entities = await _coinRepo.Get();

            return EntitiesToContracts(entities);
        }

        public async Task<IEnumerable<Coin>> GetLatest()
        {
            var coinEntities = await _coinRepo.Get();
            var symbols = coinEntities.Select(c => c.Symbol).ToList();
            var currencies = await _currencyBldr.GetLatest(symbols);
            var exchangeCoins = await _xchgCoinBldr.GetLatest(symbols);

            var contracts = EntitiesToContracts(coinEntities, currencies, exchangeCoins);

            return contracts;
        }

        public async Task<IEnumerable<Coin>> GetLatest(Business.Entities.Exchange exchange)
        {
            var coinEntities = await _coinRepo.Get();
            var symbols = coinEntities.Select(c => c.Symbol).ToList();
            var currencies = await _currencyBldr.GetLatest(symbols);
            var exchangeCoins = await _xchgCoinBldr.GetLatest(exchange);

            var contracts = EntitiesToContracts(coinEntities, currencies, exchangeCoins);

            return contracts;
        }

        private IEnumerable<Coin> EntitiesToContracts(IEnumerable<Entities.Portfolio.Coin> entities)
        {
            var contracts = new List<Coin>();

            foreach(var entity in entities)
            {
                var contract = EntityToContract(entity);

                contracts.Add(contract);
            }

            return contracts;
        }

        private Coin EntityToContract(Entities.Portfolio.Coin entity)
        {
            var contract = new Coin
            {
                CoinId = entity.Id,
                CurrencyId = entity.CurrencyId,
                Symbol = entity.Symbol
            };

            return contract;
        }

        private IEnumerable<Coin> EntitiesToContracts(IEnumerable<Entities.Portfolio.Coin> entities, IEnumerable<Currency> currencies, IEnumerable<ExchangeCoin> exchangeCoins)
        {
            var contracts = new List<Coin>();

            foreach (var entity in entities)
            {
                var currency = currencies.Where(c => c.Symbol.Equals(entity.Symbol)).FirstOrDefault();
                var exchangeCoinList = exchangeCoins.Where(e => e.Symbol.Equals(entity.Symbol)).ToList();
                var contract = EntityToContract(entity, currency, exchangeCoinList);

                contracts.Add(contract);
            }

            return contracts;
        }

        private Coin EntityToContract(Entities.Portfolio.Coin entity, Currency currency, List<ExchangeCoin> exchangeCoins)
        {
            var contract = new Coin(currency, entity.Id);
            contract.ExchangeCoinList = exchangeCoins;

            return contract;
        }

        private IEnumerable<Entities.Portfolio.Coin> ContractsToEntities(IEnumerable<Coin> contracts)
        {
            var entities = new List<Entities.Portfolio.Coin>();
            foreach(var contract in contracts)
            {
                var entity = ContractToEntity(contract);

                entities.Add(entity);
            }

            return entities;
        }

        private Entities.Portfolio.Coin ContractToEntity(Coin contract)
        {
            var entity = new Entities.Portfolio.Coin
            {
                Id = contract.CoinId,
                AverageBuy = contract.AverageBuy,
                Quantity = contract.Quantity,
                CurrencyId = contract.CurrencyId,
                Symbol = contract.Symbol
            };

            return entity;
        }
    }
}