using Cryptobitfolio.Business.Entities.Portfolio;
using Cryptobitfolio.Data.Interfaces;
using Cryptobitfolio.Data.Interfaces.Database;
using Cryptobitfolio.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Cryptobitfolio.Data.Tests
{
    public class ExchangeCoinRepositoryTests : IDisposable
    {
        private readonly IDatabaseRepository<ExchangeCoin> _repo;
        private List<ExchangeCoin> datas = new List<ExchangeCoin>();

        public ExchangeCoinRepositoryTests()
        {
            _repo = new ExchangeCoinRepository();

            // first clear out the table
            var deleted = _repo.DeleteAll().Result;

            // then add some data for testing
            datas.Add(
                new ExchangeCoin
                {
                    AverageBuy = 6700.13M,
                    CurrencyId = 1,
                    Exchange = Business.Entities.Exchange.Binance,
                    Quantity = 0.24M
                });
            datas.Add(
                new ExchangeCoin
                {
                    AverageBuy = 0.024M,
                    CurrencyId = 2,
                    Exchange = Business.Entities.Exchange.Binance,
                    Quantity = 7.25M
                });

            var addedEntites = _repo.AddAll(datas).Result;
        }

        [Fact]
        public void Add_Test()
        {
            var entity = datas[0];

            var addedEntity = _repo.Add(entity).Result;

            Assert.NotNull(addedEntity);
            Assert.True(addedEntity.Id > 0);
        }

        [Fact]
        public void AddAll_Test()
        {
            var entities = datas;

            var addedEntites = _repo.AddAll(entities).Result;
            var entityList = addedEntites.ToList();

            Assert.NotNull(addedEntites);
            Assert.True(entityList[0].Id > 0);
            Assert.True(entityList[1].Id > 0);
        }

        [Fact]
        public void GetMany_Test()
        {
            var entityList = _repo.Get().Result;

            Assert.NotNull(entityList);
            Assert.NotEmpty(entityList);
        }

        [Fact]
        public void GetOne_Test()
        {
            var id = 1;
            var entity = _repo.Get(id).Result;

            Assert.NotNull(entity);
            Assert.Equal(id, entity.Id);
        }

        [Fact]
        public void UpdateOne_Test()
        {
            var id = 1;
            var newProperty = 5757.15M;
            var entity = _repo.Get(id).Result;

            Assert.NotNull(entity);
            Assert.Equal(id, entity.Id);

            entity.AverageBuy = newProperty;

            var updatedEntity = _repo.Update(entity).Result;

            Assert.Equal(entity.AverageBuy, updatedEntity.AverageBuy);

            var entityFetch = _repo.Get(id).Result;

            Assert.NotNull(entityFetch);
            Assert.Equal(id, entityFetch.Id);
            Assert.Equal(newProperty, entityFetch.AverageBuy);
        }

        [Fact]
        public void UpdateAll_Test()
        {
            var newProperties = new List<decimal> { 5757.15M, 0.0197M };
            var entities = _repo.Get().Result;

            Assert.NotNull(entities);
            Assert.NotEmpty(entities);

            var entityList = entities.ToList();

            entityList[0].AverageBuy = newProperties[0];
            entityList[1].AverageBuy = newProperties[1];

            var updatedEntities = _repo.UpdateAll(entities).Result;
            var updatedList = updatedEntities.ToList();

            Assert.Equal(entityList[0].AverageBuy, updatedList[0].AverageBuy);
            Assert.Equal(entityList[1].AverageBuy, updatedList[1].AverageBuy);

            var entitiesFetch = _repo.Get().Result;
            var fetchList = entitiesFetch.ToList();

            Assert.NotNull(entitiesFetch);
            Assert.NotEmpty(entitiesFetch);
            Assert.Equal(newProperties[0], fetchList[0].AverageBuy);
            Assert.Equal(newProperties[1], fetchList[1].AverageBuy);
        }

        [Fact]
        public void Delete_Test()
        {
            var entityList = _repo.Get().Result;
            var entityToDelete = entityList.FirstOrDefault();

            var delete = _repo.Delete(entityToDelete).Result;

            var entityFetch = _repo.Get(entityToDelete.Id).Result;

            Assert.Null(entityFetch);
        }

        [Fact]
        public void DeleteAll_Test()
        {
            var entityList = _repo.Get().Result;

            Assert.NotEmpty(entityList);

            var delete = _repo.DeleteAll().Result;
            var entityListRecheck = _repo.Get().Result;

            Assert.True(delete);
            Assert.Empty(entityListRecheck);
        }

        public void Dispose()
        {
            // first clear out the table after running tests
            var deleted = _repo.DeleteAll().Result;
        }
    }
}
