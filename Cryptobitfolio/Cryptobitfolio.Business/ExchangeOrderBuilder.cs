// -----------------------------------------------------------------------------
// <copyright file="ExchangeOrderBuilder" company="Matt Scheetz">
//     Copyright (c) Matt Scheetz All Rights Reserved
// </copyright>
// <author name="Matt Scheetz" date="12/11/2018 7:19:23 PM" />
// -----------------------------------------------------------------------------

namespace Cryptobitfolio.Business
{
    #region Usings

    using Cryptobitfolio.Business.Common;
    using Cryptobitfolio.Business.Contracts.Trade;
    using Cryptobitfolio.Business.Entities;
    using Cryptobitfolio.Data.Interfaces.Database;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    #endregion Usings

    public class ExchangeOrderBuilder : IExchangeOrderBuilder
    {
        #region Properties

        private IExchangeOrderRepository _eoRepo;
        private IExchangeHubBuilder _hubBldr;
        private List<ExchangeOrder> _orders;

        #endregion Properties

        public ExchangeOrderBuilder(IExchangeOrderRepository repo, IExchangeHubBuilder hubBldr)
        {
            this._eoRepo = repo;
            this._hubBldr = hubBldr;
        }

        public async Task<ExchangeOrder> Add(ExchangeOrder contract)
        {
            var entity = ContractToEntity(contract);
            entity = await _eoRepo.Add(entity);

            return EntityToContract(entity);
        }

        public async Task<bool> Delete(ExchangeOrder contract)
        {
            var entity = ContractToEntity(contract);
            var result = await _eoRepo.Delete(entity);

            return result;
        }

        public async Task<IEnumerable<ExchangeOrder>> Get()
        {
            var entities = await _eoRepo.Get();

            var contracts = EntitiesToContracts(entities);

            _orders = contracts.ToList();

            return contracts;
        }

        public async Task<IEnumerable<ExchangeOrder>> Get(string symbol, Exchange exchange)
        {
            var entities = await _eoRepo.Get(e => e.Pair.StartsWith(symbol) && e.Exchange == exchange);

            var contracts = EntitiesToContracts(entities);

            _orders = contracts.ToList();

            return contracts;
        }

        public async Task<IEnumerable<ExchangeOrder>> GetFromExchange(string symbol, Exchange exchange)
        {
            var pairs = await _hubBldr.GetMarketsForACoin(symbol);
            var orders = await _hubBldr.GetExchangeOpenOrdersByPairs(pairs, exchange);
            var exchangeOrderList = new List<ExchangeOrder>();

            foreach (var order in orders)
            {
                var exchangeOrder = OrderResponseToExchangeOrder(order, exchange);
                exchangeOrderList.Add(exchangeOrder);
            }

            return exchangeOrderList;
        }

        public async Task<IEnumerable<ExchangeOrder>> GetLatest(string symbol, Exchange exchange)
        {
            var _exchangeOrders = await GetFromExchange(symbol, exchange);

            var _orders = await Get(symbol, exchange);

            var exchangeList = _exchangeOrders.OrderByDescending(e => e.ClosedDate).ToList();
            var dbList = _orders.OrderByDescending(e => e.ClosedDate).ToList();

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
            if (clearDb == true && (exchangeList[0].ClosedDate == dbList[0].ClosedDate))
            {
                clearDb = false;
                updateDb = false;
            }
            if (clearDb)
            {
                foreach (var coinBuy in dbList)
                {
                    await Delete(coinBuy);
                }
            }
            if (updateDb)
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

        public async Task<ExchangeOrder> Update(ExchangeOrder contract)
        {
            var entity = ContractToEntity(contract);
            entity = await _eoRepo.Update(entity);

            return EntityToContract(entity);
        }

        private IEnumerable<ExchangeOrder> EntitiesToContracts(IEnumerable<Entities.Trade.ExchangeOrder> entities)
        {
            var contracts = new List<ExchangeOrder>();

            foreach(var entity in entities)
            {
                var contract = EntityToContract(entity);

                contracts.Add(contract);
            }

            return contracts;
        }

        private ExchangeOrder EntityToContract(Entities.Trade.ExchangeOrder entity)
        {
            var contract = new ExchangeOrder
            {
                ExchangeOrderId = entity.Id,
                ClosedDate = entity.ClosedDate,
                Exchange = entity.Exchange,
                FilledQuantity = entity.FilledQuantity,
                OrderId = entity.OrderId,
                Pair = entity.Pair,
                Price = entity.Price,
                PlaceDate = entity.PlaceDate,
                Quantity = entity.Quantity,
                Side = entity.Side,
                Status = entity.Status
            };

            return contract;
        }

        private IEnumerable<Entities.Trade.ExchangeOrder> ContractsToEntities(IEnumerable<ExchangeOrder> contracts)
        {
            var entities = new List<Entities.Trade.ExchangeOrder>();
            foreach(var contract in contracts)
            {
                var entity = ContractToEntity(contract);

                entities.Add(entity);
            }

            return entities;
        }

        private Entities.Trade.ExchangeOrder ContractToEntity(ExchangeOrder contract)
        {
            var entity = new Entities.Trade.ExchangeOrder
            {
                Id = contract.ExchangeOrderId,
                ClosedDate = contract.ClosedDate,
                Exchange = contract.Exchange,
                FilledQuantity = contract.FilledQuantity,
                OrderId = contract.OrderId,
                Pair = contract.Pair,
                Price = contract.Price,
                PlaceDate = contract.PlaceDate,
                Quantity = contract.Quantity,
                Side = contract.Side,
                Status = contract.Status
            };

            return entity;
        }

        /// <summary>
        /// Convert an OrderResponse to an ExchangeOrder
        /// </summary>
        /// <param name="orderResponse">OrderResponse to convert</param>
        /// <returns>new ExchangeOrder object</returns>
        public ExchangeOrder OrderResponseToExchangeOrder(ExchangeHub.Contracts.OrderResponse orderResponse, Exchange exchange)
        {
            var exchangeOrder = new ExchangeOrder
            {
                Exchange = exchange,
                FilledQuantity = orderResponse.FilledQuantity,
                OrderId = orderResponse.OrderId,
                Pair = orderResponse.Pair,
                PlaceDate = orderResponse.TransactTime,
                Price = orderResponse.Price,
                Quantity = orderResponse.OrderQuantity,
                Side = (Side)orderResponse.Side
            };

            return exchangeOrder;
        }
    }
}