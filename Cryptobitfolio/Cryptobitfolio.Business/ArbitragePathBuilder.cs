// -----------------------------------------------------------------------------
// <copyright file="ArbitragePathBuilder" company="Matt Scheetz">
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

    public class ArbitragePathBuilder : IArbitragePathBuilder
    {
        #region Properties

        IArbitragePathRepository _apRepo;

        #endregion Properties

        public ArbitragePathBuilder(IArbitragePathRepository repo)
        {
            this._apRepo = repo;
        }

        public async Task<ArbitragePath> Add(ArbitragePath contract)
        {
            var entity = ContractToEntity(contract);
            entity = await _apRepo.Add(entity);

            return EntityToContract(entity);
        }

        public async Task<bool> Delete(ArbitragePath contract)
        {
            var entity = ContractToEntity(contract);
            var result = await _apRepo.Delete(entity);

            return result;
        }

        public async Task<IEnumerable<ArbitragePath>> Get()
        {
            var entities = await _apRepo.Get();

            return EntitiesToContracts(entities);
        }

        public async Task<ArbitragePath> Update(ArbitragePath contract)
        {
            var entity = ContractToEntity(contract);
            entity = await _apRepo.Update(entity);

            return EntityToContract(entity);
        }

        private IEnumerable<ArbitragePath> EntitiesToContracts(IEnumerable<Entities.Portfolio.ArbitragePath> entities)
        {
            var contracts = new List<ArbitragePath>();

            foreach(var entity in entities)
            {
                var contract = EntityToContract(entity);

                contracts.Add(contract);
            }

            return contracts;
        }

        private ArbitragePath EntityToContract(Entities.Portfolio.ArbitragePath entity)
        {
            var contract = new ArbitragePath
            {
                ArbitragePathId = entity.Id,
                Created = entity.Created,
                Path = entity.Path
            };

            return contract;
        }

        private IEnumerable<Entities.Portfolio.ArbitragePath> ContractsToEntities(IEnumerable<ArbitragePath> contracts)
        {
            var entities = new List<Entities.Portfolio.ArbitragePath>();
            foreach(var contract in contracts)
            {
                var entity = ContractToEntity(contract);

                entities.Add(entity);
            }

            return entities;
        }

        private Entities.Portfolio.ArbitragePath ContractToEntity(ArbitragePath contract)
        {
            var entity = new Entities.Portfolio.ArbitragePath
            {
                Id = contract.ArbitragePathId,
                Created = contract.Created,
                Path = contract.Path
            };

            return entity;
        }
    }
}