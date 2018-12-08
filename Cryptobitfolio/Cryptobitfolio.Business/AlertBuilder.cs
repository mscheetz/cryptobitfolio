// -----------------------------------------------------------------------------
// <copyright file="AlertBuilder" company="Matt Scheetz">
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

    public class AlertBuilder : IAlertBuilder
    {
        #region Properties

        private IAlerterRepository _alertRepo;

        #endregion Properties

        public AlertBuilder(IAlerterRepository alerter)
        {
            _alertRepo = alerter;
        }

        /// <summary>
        /// Add an alert
        /// </summary>
        /// <param name="alerter">Alert to add</param>
        /// <returns>Added alert</returns>
        public async Task<Alerter> Add(Alerter alerter)
        {
            var entity = ContractToEntity(alerter);
            entity = await _alertRepo.Add(entity);

            alerter.AlertId = entity.Id;
            return alerter;
        }

        /// <summary>
        /// Get all alerts
        /// </summary>
        /// <returns>Collection of Alerters</returns>
        public async Task<IEnumerable<Alerter>> Get()
        {
            var entities = await _alertRepo.Get();

            return EntityCollectionToContracts(entities);
        }

        /// <summary>
        /// Get alerts for a coin
        /// </summary>
        /// <param name="Symbol">Symbol of coin</param>
        /// <returns>Collection of Alerters</returns>
        public async Task<IEnumerable<Alerter>> Get(string Symbol)
        {
            var entities = await _alertRepo.Get(a => a.Pair.StartsWith(Symbol));

            return EntityCollectionToContracts(entities);
        }

        /// <summary>
        /// Update an alert
        /// </summary>
        /// <param name="alerter">Alert to update</param>
        /// <returns>Updated alert</returns>
        public async Task<Alerter> Update(Alerter alerter)
        {
            var entity = ContractToEntity(alerter);
            entity = await _alertRepo.Update(entity);

            return EntityToContract(entity);
        }

        /// <summary>
        /// Delete an alert
        /// </summary>
        /// <param name="alerter">Alert to delete</param>
        /// <returns>Boolean of deletion</returns>
        public async Task<bool> Delete(Alerter alerter)
        {
            var entity = ContractToEntity(alerter);
            var result = await _alertRepo.Delete(entity);

            return result;
        }

        private IEnumerable<Alerter> EntityCollectionToContracts(IEnumerable<Entities.Portfolio.Alerter> entities)
        {
            var contractList = new List<Alerter>();

            foreach(var entity in entities)
            {
                var contract = EntityToContract(entity);

                contractList.Add(contract);
            }

            return contractList;
        }

        private Alerter EntityToContract(Entities.Portfolio.Alerter entity)
        {
            var contract = new Alerter
            {
                AlertId = entity.Id,
                Created = entity.Created,
                Enabled = entity.Enabled,
                Exchange = entity.Exchange,
                Hit = entity.Hit,
                Pair = entity.Pair,
                Price = entity.Price
            };

            return contract;
        }

        private Entities.Portfolio.Alerter ContractToEntity(Alerter contract)
        {
            var entity = new Entities.Portfolio.Alerter
            {
                Id = contract.Id,
                Created = contract.Created,
                Enabled = contract.Enabled,
                Exchange = contract.Exchange,
                Hit = contract.Hit,
                Pair = contract.Pair,
                Price = contract.Price
            };

            return entity;
        }
    }
}