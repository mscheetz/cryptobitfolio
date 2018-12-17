// -----------------------------------------------------------------------------
// <copyright file="AlertBuilder" company="Matt Scheetz">
//     Copyright (c) Matt Scheetz All Rights Reserved
// </copyright>
// <author name="Matt Scheetz" date="12/7/2018 9:36:28 PM" />
// -----------------------------------------------------------------------------

namespace Cryptobitfolio.Business
{
    #region Usings

    using Cryptobitfolio.Business.Common;
    using Cryptobitfolio.Business.Contracts.Portfolio;
    using Cryptobitfolio.Data.Interfaces.Database;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    #endregion Usings

    public class AlertBuilder : IAlertBuilder
    {
        #region Properties

        private IAlerterRepository _alertRepo;
        private IHistoricalPriceBuilder _hpBldr;

        #endregion Properties

        public AlertBuilder(IAlerterRepository alerter, IHistoricalPriceBuilder historicalPriceBuilder)
        {
            _alertRepo = alerter;
            this._hpBldr = historicalPriceBuilder;
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

        public async Task<IEnumerable<Alerter>> GetLatest()
        {
            var alerts = await this.Get();
            var alertList = alerts.ToList();
            var pairs = alerts.ToList().Where(a => a.Hit == null).Select(a => a.Pair).ToList();

            var hpList = await _hpBldr.GetLatest(pairs);
            var hitList = new List<Alerter>();
            var hitTime = DateTime.UtcNow;

            for (var i = 0; i < alertList.Count; i++)
            {
                var alert = alertList[i];
                var hit = false;
                var hp = hpList.Where(h => h.Exchange == alert.Exchange && h.Pair.Equals(alert.Pair)).FirstOrDefault();
                if(alert.Direction == Entities.Direction.GTE && alert.Price <= hp.High)
                {
                    hit = true;
                }
                if(alert.Direction == Entities.Direction.LTE && alert.Price >= hp.Low)
                {
                    hit = true;
                }
                if(hit)
                {
                    alert.Hit = hitTime;
                    hitList.Add(alert);
                }
            }

            for(var i = 0; i< hitList.Count; i++)
            {
                hitList[i] = await this.Update(hitList[i]);
            }

            return hitList;
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
                Direction = entity.Direction,
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
                Id = contract.AlertId,
                Created = contract.Created,
                Direction = contract.Direction,
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