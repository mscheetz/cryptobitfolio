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
    using Cryptobitfolio.Business.Entities;
    using Cryptobitfolio.Data.Interfaces.Database;
    using ExchangeHub.Contracts;
    #region Usings

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Exchange = Entities.Exchange;

    #endregion Usings

    public class ExchangeCoinBuilder : IExchangeCoinBuilder
    {
        #region Properties

        private ICoinBuyBuilder _cbBldr;
        private IExchangeOrderBuilder _eoBldr;
        private IExchangeHubBuilder _hubBldr;
        private IExchangeCoinRepository _ecRepo;

        #endregion Properties

        public ExchangeCoinBuilder(ICoinBuyBuilder coinBuyBuilder,
                                   IExchangeOrderBuilder eoBldr,
                                   IExchangeHubBuilder hubBuilder,
                                   IExchangeCoinRepository repo)
        {
            this._cbBldr = coinBuyBuilder;
            this._eoBldr = eoBldr;
            this._hubBldr = hubBuilder;
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

        public async Task<ExchangeCoin> Update(ExchangeCoin contract)
        {
            var entity = ContractToEntity(contract);
            entity = await _ecRepo.Update(entity);

            return EntityToContract(entity);
        }

        public async Task<IEnumerable<ExchangeCoin>> Get()
        {
            var entities = await _ecRepo.Get();

            return EntitiesToContracts(entities);
        }

        public async Task<IEnumerable<ExchangeCoin>> Get(string symbol)
        {
            var entities = await _ecRepo.Get(e => e.Symbol.Equals(symbol));

            return EntitiesToContracts(entities);
        }

        public async Task<IEnumerable<ExchangeCoin>> Get(Exchange exchange)
        {
            var entities = await _ecRepo.Get(e => e.Exchange == exchange);

            return EntitiesToContracts(entities);
        }

        public async Task<IEnumerable<ExchangeCoin>> Get(string symbol, Exchange exchange)
        {
            var entities = await _ecRepo.Get(e => e.Symbol.Equals(symbol) && e.Exchange == exchange);

            return EntitiesToContracts(entities);
        }

        public async Task<IEnumerable<ExchangeCoin>> GetLatest(string symbol)
        {
            var balances = await this._hubBldr.GetBalances(symbol);
            var exchangeBalance = BalanceDictionaryToExchangeCoins(balances);
            var exCoins = await this.Get(symbol);

            return await OnGetLastest(exchangeBalance.ToList(), exCoins.ToList());
        }

        public async Task<IEnumerable<ExchangeCoin>> GetLastest(Exchange exchange)
        {
            var balances = await this._hubBldr.GetExchangeBalances(exchange);
            var exchangeBalance = BalanceCollectionToExchangeCoins(balances, exchange);
            var exCoins = await this.Get(exchange);

            return await OnGetLastest(exchangeBalance.ToList(), exCoins.ToList());
        }

        private async Task<IEnumerable<ExchangeCoin>> OnGetLastest(List<ExchangeCoin> hubList, List<ExchangeCoin> dbList)
        {
            var addList = hubList.Where(e => !dbList.Any(d => d.Symbol.Equals(e.Symbol))).ToList();
            var updateList = dbList.Where(d => hubList.Any(e => e.Symbol.Equals(d.Symbol) && e.Quantity != d.Quantity)).Select(d => d.ExchangeCoinId).ToArray();
            var deleteList = dbList.Where(d => !hubList.Any(e => e.Symbol.Equals(d.Symbol))).Select(d => d.ExchangeCoinId).ToArray();

            if (addList.Count > 0)
            {
                foreach (var item in addList)
                {
                    await Add(item);
                }
            }

            if (updateList.Length > 0)
            {
                for (var i = 0; i < updateList.Length; i++)
                {
                    var item = dbList.Where(d => d.ExchangeCoinId == updateList[i]).FirstOrDefault();
                    item.Quantity = hubList.Where(e => e.Symbol.Equals(item.Symbol)).Select(e => e.Quantity).FirstOrDefault();
                    item = await Update(item);
                }
            }

            if (deleteList.Length > 0)
            {
                for (var i = 0; i < deleteList.Length; i++)
                {
                    var item = dbList.Where(d => d.ExchangeCoinId == deleteList[i]).FirstOrDefault();
                    await Delete(item);
                }
            }

            if (hubList != null && hubList.Count > 0)
            {
                var returner = await GetFullCoins(hubList);
                return returner;
            }
            else
            {
                var returner = await GetFullCoins(dbList);
                return returner;
            }

        }

        private async Task<IEnumerable<ExchangeCoin>> GetFullCoins(List<ExchangeCoin> exchangeCoins)
        {
            for (var i = 0; i < exchangeCoins.Count; i++)
            {
                exchangeCoins[i] = await GetFullCoin(exchangeCoins[i]);
            }

            return exchangeCoins;
        }

        private async Task<ExchangeCoin> GetFullCoin(ExchangeCoin exchangeCoin)
        {
            var coinBuys = await _cbBldr.GetLatest(exchangeCoin.Symbol, exchangeCoin.Quantity, exchangeCoin.Exchange);
            var orders = await _eoBldr.GetLatest(exchangeCoin.Symbol, exchangeCoin.Exchange);

            exchangeCoin.OpenOrderList = orders.ToList();
            exchangeCoin.CoinBuyList = coinBuys.ToList();

            return exchangeCoin;
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

        private IEnumerable<ExchangeCoin> BalanceDictionaryToExchangeCoins(Dictionary<Exchange, IEnumerable<Balance>> balances)
        {
            var exCoinList = new List<ExchangeCoin>();

            foreach (var balance in balances)
            {
                var exCoins = BalanceCollectionToExchangeCoins(balance.Value, balance.Key);

                exCoinList.AddRange(exCoins);
            }

            return exCoinList;
        }

        private IEnumerable<ExchangeCoin> BalanceCollectionToExchangeCoins(IEnumerable<Balance> balances, Exchange exchange)
        {
            var exCoinList = new List<ExchangeCoin>();

            foreach(var balance in balances)
            {
                var exCoin = BalanceToExchangeCoin(balance, exchange);

                exCoinList.Add(exCoin);
            }

            return exCoinList;
        }

        private ExchangeCoin BalanceToExchangeCoin(Balance balance, Exchange exchange)
        {
            var exCoin = new ExchangeCoin
            {
                Exchange = exchange,
                Quantity = balance.Available + balance.Frozen,
                Symbol = balance.Symbol
            };

            return exCoin;
        }
    }
}