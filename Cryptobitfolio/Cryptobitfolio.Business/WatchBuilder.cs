// -----------------------------------------------------------------------------
// <copyright file="WatchBuilder" company="Matt Scheetz">
//     Copyright (c) Matt Scheetz All Rights Reserved
// </copyright>
// <author name="Matt Scheetz" date="12/7/2018 9:36:28 PM" />
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

    public class WatchBuilder : IWatchBuilder
    {
        #region Properties

        private IWatcherRepository _watcherRepo;

        #endregion Properties

        public WatchBuilder(IWatcherRepository watcher)
        {
            _watcherRepo = watcher;
        }

        /// <summary>
        /// Add a watcher
        /// </summary>
        /// <param name="watcher">Watcher to add</param>
        /// <returns>Added watcher</returns>
        public async Task<Watcher> Add(Watcher watcher)
        {
            var entity = ContractToEntity(watcher);
            entity = await _watcherRepo.Add(entity);

            watcher.WatcherId = entity.Id;
            return watcher;
        }

        /// <summary>
        /// Get all watchers
        /// </summary>
        /// <returns>Collection of Watchers</returns>
        public async Task<IEnumerable<Watcher>> Get()
        {
            var entities = await _watcherRepo.Get();

            return EntityCollectionToContracts(entities);
        }

        /// <summary>
        /// Get watchers for a coin
        /// </summary>
        /// <param name="Symbol">Symbol of coin</param>
        /// <returns>Collection of Watchers</returns>
        public async Task<IEnumerable<Watcher>> Get(string Symbol)
        {
            var entities = await _watcherRepo.Get(a => a.Pair.StartsWith(Symbol));

            return EntityCollectionToContracts(entities);
        }

        /// <summary>
        /// Update a watcher
        /// </summary>
        /// <param name="alerter">Watcher to update</param>
        /// <returns>Updated watcher</returns>
        public async Task<Watcher> Update(Watcher alerter)
        {
            var entity = ContractToEntity(alerter);
            entity = await _watcherRepo.Update(entity);

            return EntityToContract(entity);
        }

        /// <summary>
        /// Delete a watcher
        /// </summary>
        /// <param name="watcher">Watcher to delete</param>
        /// <returns>Boolean of deletion</returns>
        public async Task<bool> Delete(Watcher watcher)
        {
            var entity = ContractToEntity(watcher);
            var result = await _watcherRepo.Delete(entity);

            return result;
        }

        private IEnumerable<Watcher> EntityCollectionToContracts(IEnumerable<Entities.Portfolio.Watcher> entities)
        {
            var contractList = new List<Watcher>();

            foreach(var entity in entities)
            {
                var contract = EntityToContract(entity);

                contractList.Add(contract);
            }

            return contractList;
        }

        private Watcher EntityToContract(Entities.Portfolio.Watcher entity)
        {
            var contract = new Watcher
            {
                WatcherId = entity.Id,                
                Created = entity.DateAdded,
                Enabled = entity.Enabled,
                Exchange = entity.Exchange,
                Pair = entity.Pair,
                Price = entity.Price
            };

            return contract;
        }

        private Entities.Portfolio.Watcher ContractToEntity(Watcher contract)
        {
            var entity = new Entities.Portfolio.Watcher
            {
                Id = contract.WatcherId,
                DateAdded = contract.Created,
                Enabled = contract.Enabled,
                Exchange = contract.Exchange,
                Pair = contract.Pair,
                Price = contract.Price
            };

            return entity;
        }
    }
}