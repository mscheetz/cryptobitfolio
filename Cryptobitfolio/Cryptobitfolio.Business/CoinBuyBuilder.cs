// -----------------------------------------------------------------------------
// <copyright file="CoinBuyBuilder" company="Matt Scheetz">
//     Copyright (c) Matt Scheetz All Rights Reserved
// </copyright>
// <author name="Matt Scheetz" date="12/11/2018 7:33:13 PM" />
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

    public class CoinBuyBuilder : ICoinBuyBuilder
    {
        #region Properties

        private ICoinBuyRepository _cbRepo;

        #endregion Properties

        public CoinBuyBuilder(ICoinBuyRepository repo)
        {
            this._cbRepo = repo;
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

        public async Task<CoinBuy> Update(CoinBuy contract)
        {
            var entity = ContractToEntity(contract);
            entity = await _cbRepo.Update(entity);

            return EntityToContract(entity);
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
    }
}