// -----------------------------------------------------------------------------
// <copyright file="CoinBuyBuilder" company="Matt Scheetz">
//     Copyright (c) Matt Scheetz All Rights Reserved
// </copyright>
// <author name="Matt Scheetz" date="12/11/2018 7:33:13 PM" />
// -----------------------------------------------------------------------------

namespace Cryptobitfolio.Business
{
    #region Usings

    using Cryptobitfolio.Business.Common;
    using Cryptobitfolio.Business.Contracts.Portfolio;
    using Cryptobitfolio.Business.Entities;
    using Cryptobitfolio.Data.Interfaces.Database;
    using ExchangeHub.Contracts;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Exchange = Entities.Exchange;

    #endregion Usings

    public class CoinBuyBuilder : ICoinBuyBuilder
    {
        #region Properties

        private ICoinBuyRepository _cbRepo;
        private IExchangeHubBuilder _hubBldr;

        #endregion Properties

        public CoinBuyBuilder(ICoinBuyRepository repo, IExchangeHubBuilder hubBldr)
        {
            this._cbRepo = repo;
            this._hubBldr = hubBldr;
        }

        public async Task<CoinBuy> Add(CoinBuy contract)
        {
            var entity = ContractToEntity(contract);
            entity = await _cbRepo.Add(entity);

            return EntityToContract(entity);
        }

        public async Task<bool> Delete(CoinBuy contract)
        {
            var entity = ContractToEntity(contract);
            var result = await _cbRepo.Delete(entity);

            return result;
        }

        public async Task<IEnumerable<CoinBuy>> Get()
        {
            var entities = await _cbRepo.Get();

            return EntitiesToContracts(entities);
        }

        public async Task<IEnumerable<CoinBuy>> Get(string symbol, Exchange exchange)
        {
            var entities = await _cbRepo.Get(c => c.Pair.StartsWith(symbol) && c.Exchange == exchange);

            return EntitiesToContracts(entities);
        }

        public async Task<CoinBuy> Update(CoinBuy contract)
        {
            var entity = ContractToEntity(contract);
            entity = await _cbRepo.Update(entity);

            return EntityToContract(entity);
        }

        public async Task<IEnumerable<CoinBuy>> GetLatest(string symbol, decimal quantity, Exchange exchange)
        {
            var relevantBuys = await this.GetRelevantCoinBuys(symbol, quantity, exchange);
            var coinBuys = await this.Get(symbol, exchange);

            var exchangeList = relevantBuys.OrderByDescending(b => b.TransactionDate).ToList();
            var dbList = coinBuys.OrderByDescending(b => b.TransactionDate).ToList();

            bool clearDb = false;
            bool updateDb = false;

            if (dbList != null || dbList.Count > 0)
            {
                clearDb = true;
                updateDb = true;
            }
            if (exchangeList == null || exchangeList.Count == 0)
            {
                clearDb = false;
                updateDb = false;
            }
            if(clearDb == true && (exchangeList[0].TransactionDate == dbList[0].TransactionDate))
            {
                clearDb = false;
                updateDb = false;
            }
            if(clearDb)
            {
                foreach(var coinBuy in coinBuys)
                {
                    await Delete(coinBuy);
                }
            }
            if(updateDb)
            {
                for (var i = 0; i < exchangeList.Count; i++)
                {
                    exchangeList[i] = await Add(exchangeList[i]);
                }
            }
            if (exchangeList != null && exchangeList.Count > 0)
            {
                return exchangeList;
            }
            else
            {
                return dbList;
            }
        }

        public async Task<IEnumerable<CoinBuy>> GetRelevantCoinBuys(string symbol, decimal quantity, Exchange exchange)
        {
            var pairs = await _hubBldr.GetMarketsForACoin(symbol);
            var orders = await _hubBldr.GetExchangeOrders(pairs, exchange);
            var coinBuyList = new List<CoinBuy>();

            foreach (var order in orders)
            {
                var quantityApplied = quantity >= order.FilledQuantity
                    ? order.FilledQuantity
                    : quantity;

                quantity -= order.FilledQuantity;

                var coinBuy = OrderResponseToCoinBuy(order, exchange, quantityApplied);
                coinBuyList.Add(coinBuy);

                if (quantity <= 0)
                {
                    break;
                }
            }

            return coinBuyList;
        }

        public async Task<IEnumerable<CoinBuy>> GetAllOrders(string symbol, Exchange exchange)
        {
            var pairs = await _hubBldr.GetMarketsForACoin(symbol);
            var orders = await _hubBldr.GetExchangeOrders(pairs, exchange);

            return OrderResponseCollectionToCoinBuys(orders, exchange);
        }

        private IEnumerable<CoinBuy> EntitiesToContracts(IEnumerable<Entities.Portfolio.CoinBuy> entities)
        {
            var contracts = new List<CoinBuy>();

            foreach (var entity in entities)
            {
                var contract = EntityToContract(entity);

                contracts.Add(contract);
            }

            return contracts;
        }

        private CoinBuy EntityToContract(Entities.Portfolio.CoinBuy entity)
        {
            var contract = new CoinBuy
            {
                CoinBuyId = entity.Id,
                Exchange = entity.Exchange,
                OrderId = entity.OrderId,
                Pair = entity.Pair,
                Price = entity.Price,
                Quantity = entity.Quantity,
                TransactionDate = entity.TransactionDate
            };

            return contract;
        }

        private IEnumerable<Entities.Portfolio.CoinBuy> ContractsToEntities(IEnumerable<CoinBuy> contracts)
        {
            var entities = new List<Entities.Portfolio.CoinBuy>();
            foreach (var contract in contracts)
            {
                var entity = ContractToEntity(contract);

                entities.Add(entity);
            }

            return entities;
        }

        private Entities.Portfolio.CoinBuy ContractToEntity(CoinBuy contract)
        {
            var entity = new Entities.Portfolio.CoinBuy
            {
                Id = contract.CoinBuyId,
                Exchange = contract.Exchange,
                OrderId = contract.OrderId,
                Pair = contract.Pair,
                Price = contract.Price,
                Quantity = contract.Quantity,
                TransactionDate = contract.TransactionDate
            };

            return entity;
        }

        private IEnumerable<CoinBuy> OrderResponseCollectionToCoinBuys(IEnumerable<OrderResponse> orders, Exchange exchange)
        {
            var coinBuys = new List<CoinBuy>();

            foreach(var order in orders)
            {
                var coinBuy = OrderResponseToCoinBuy(order, exchange);
                coinBuys.Add(coinBuy);
            }

            return coinBuys;
        }

        /// <summary>
        /// Create a CoinBuy from an ExchangeHub OrderResponse
        /// </summary>
        /// <param name="orderResponse">OrderResponse to convert</param>
        /// <returns>new CoinBuy object</returns>
        private CoinBuy OrderResponseToCoinBuy(OrderResponse orderResponse, Exchange exchange, decimal quantity = 0)
        {
            var coinBuy = new CoinBuy
            {
                BTCPrice = orderResponse.Pair.EndsWith("BTC") ? orderResponse.Price : 0,
                Exchange = exchange,
                OrderId = orderResponse.OrderId,
                Pair = orderResponse.Pair,
                Price = orderResponse.Price,
                Quantity = quantity == 0 ? orderResponse.FilledQuantity : quantity,
                TransactionDate = orderResponse.TransactTime
            };

            return coinBuy;
        }
    }
}