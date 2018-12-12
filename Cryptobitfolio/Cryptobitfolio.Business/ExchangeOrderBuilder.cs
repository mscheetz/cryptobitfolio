// -----------------------------------------------------------------------------
// <copyright file="ExchangeOrderBuilder" company="Matt Scheetz">
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

    public class ExchangeOrderBuilder : IExchangeOrderBuilder
    {
        #region Properties

        IExchangeOrderRepository _eoRepo;

        #endregion Properties

        public ExchangeOrderBuilder(IExchangeOrderRepository repo)
        {
            this._eoRepo = repo;
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

            return EntitiesToContracts(entities);
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
                ClosedDate = entity.Filled,
                Exchange = entity.Exchange,
                FilledQuantity = entity.FilledQuantity,
                OrderId = entity.OrderId,
                Pair = entity.Pair,
                Price = entity.Price,
                PlaceDate = entity.Created,
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
                Filled = contract.ClosedDate,
                Exchange = contract.Exchange,
                FilledQuantity = contract.FilledQuantity,
                OrderId = contract.OrderId,
                Pair = contract.Pair,
                Price = contract.Price,
                Created = contract.PlaceDate,
                Quantity = contract.Quantity,
                Side = contract.Side,
                Status = contract.Status
            };

            return entity;
        }
    }
}