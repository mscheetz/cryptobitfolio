using Cryptobitfolio.Business.Entities.Trade;
using Cryptobitfolio.Data.Interfaces;
using Cryptobitfolio.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Cryptobitfolio.Data.Tests
{
    public class ExchangeUpdateRepositoryTests : IDisposable
    {
        private readonly IExchangeUpdateRepository _repo;
        private List<ExchangeUpdate> datas = new List<ExchangeUpdate>();

        public ExchangeUpdateRepositoryTests()
        {
            _repo = new ExchangeUpdateRepository();
            
            // first clear out the table
            var deleted = _repo.DeleteAll().Result;

            // then add some data for testing
            datas.Add(
                new ExchangeUpdate
                {
                    Id = 0,
                    Exchange = Business.Entities.Exchange.Binance,
                    UpdateAt = DateTime.UtcNow
                });
            datas.Add(
                new ExchangeUpdate
                {
                    Id = 0,
                    Exchange = Business.Entities.Exchange.Bittrex,
                    UpdateAt = DateTime.UtcNow
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

            Assert.NotNull(addedEntites);
            Assert.True(addedEntites[0].Id > 0);
            Assert.True(addedEntites[1].Id > 0);
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
            var newProperty = DateTime.UtcNow.AddMonths(-5);
            var entity = _repo.Get(id).Result;

            Assert.NotNull(entity);
            Assert.Equal(id, entity.Id);

            entity.UpdateAt = newProperty;

            var updatedEntity = _repo.Update(entity).Result;

            Assert.Equal(entity.UpdateAt, updatedEntity.UpdateAt);

            var entityFetch = _repo.Get(id).Result;

            Assert.NotNull(entityFetch);
            Assert.Equal(id, entityFetch.Id);
            Assert.Equal(newProperty, entityFetch.UpdateAt);
        }

        [Fact]
        public void UpdateAll_Test()
        {
            var newProperties = new List<DateTime> { DateTime.UtcNow.AddMonths(-5), DateTime.UtcNow.AddMonths(-1) };
            var entities = _repo.Get().Result.ToList();

            Assert.NotNull(entities);
            Assert.NotEmpty(entities);

            entities[0].UpdateAt = newProperties[0];
            entities[1].UpdateAt = newProperties[1];

            var updatedEntities = _repo.UpdateAll(entities).Result;

            Assert.Equal(entities[0].UpdateAt, updatedEntities[0].UpdateAt);
            Assert.Equal(entities[1].UpdateAt, updatedEntities[1].UpdateAt);

            var entitiesFetch = _repo.Get().Result.ToList();

            Assert.NotNull(entitiesFetch);
            Assert.NotEmpty(entitiesFetch);
            Assert.Equal(newProperties[0], entitiesFetch[0].UpdateAt);
            Assert.Equal(newProperties[1], entitiesFetch[1].UpdateAt);
        }

        [Fact]
        public void Delete_Test()
        {
            var entityList = _repo.Get().Result.ToList();
            var entityToDelete = entityList[0];

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
