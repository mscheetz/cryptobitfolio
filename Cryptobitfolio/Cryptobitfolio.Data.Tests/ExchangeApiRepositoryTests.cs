using Cryptobitfolio.Business.Entities.Trade;
using Cryptobitfolio.Data.Interfaces;
using Cryptobitfolio.Data.Repositories;
using System;
using System.Collections.Generic;
using Xunit;

namespace Cryptobitfolio.Data.Tests
{
    public class ExchangeApiRepositoryTests : IDisposable
    {
        private readonly IExchangeApiRepository _repo;
        private List<ExchangeApi> datas = new List<ExchangeApi>();

        public ExchangeApiRepositoryTests()
        {
            _repo = new ExchangeApiRepository();
            
            // first clear out the table
            var deleted = _repo.DeleteAll().Result;

            // then add some data for testing
            datas.Add(
                new ExchangeApi
                {
                    Id = 0,
                    ApiKey = "12345",
                    ApiKeyName = "Test Key 1",
                    ApiSecret = "a secret",
                    Created = DateTime.UtcNow,
                    Exchange = Business.Entities.Exchange.Binance
                });
            datas.Add(
                new ExchangeApi
                {
                    Id = 0,
                    ApiKey = "678910",
                    ApiKeyName = "Test Key 2",
                    ApiSecret = "a secret",
                    Created = DateTime.UtcNow,
                    Exchange = Business.Entities.Exchange.Bittrex
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
            Assert.True(entityList.Count > 0);
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
            var newProperty = "ABCDEF";
            var entity = _repo.Get(id).Result;

            Assert.NotNull(entity);
            Assert.Equal(id, entity.Id);

            entity.ApiKey = newProperty;

            var updatedEntity = _repo.Update(entity).Result;

            Assert.Equal(entity.ApiKey, updatedEntity.ApiKey);

            var entityFetch = _repo.Get(id).Result;

            Assert.NotNull(entityFetch);
            Assert.Equal(id, entityFetch.Id);
            Assert.Equal(newProperty, entityFetch.ApiKey);
        }

        [Fact]
        public void UpdateAll_Test()
        {
            var newProperties = new List<string> { "ABCDEF", "WXYZ" };
            var entities = _repo.Get().Result;

            Assert.NotNull(entities);
            Assert.NotEmpty(entities);

            entities[0].ApiKey = newProperties[0];
            entities[1].ApiKey = newProperties[1];

            var updatedEntities = _repo.UpdateAll(entities).Result;

            Assert.Equal(entities[0].ApiKey, updatedEntities[0].ApiKey);
            Assert.Equal(entities[1].ApiKey, updatedEntities[1].ApiKey);

            var entitiesFetch = _repo.Get().Result;

            Assert.NotNull(entitiesFetch);
            Assert.NotEmpty(entitiesFetch);
            Assert.Equal(newProperties[0], entitiesFetch[0].ApiKey);
            Assert.Equal(newProperties[1], entitiesFetch[1].ApiKey);
        }

        [Fact]
        public void Delete_Test()
        {
            var entityList = _repo.Get().Result;
            var entityToDelete = entityList[0];

            var delete = _repo.Delete(entityToDelete).Result;
            
            var entityFetch = _repo.Get(entityToDelete.Id).Result;

            Assert.Null(entityFetch);
        }

        [Fact]
        public void DeleteAll_Test()
        {
            var entityList = _repo.Get().Result;

            Assert.True(entityList.Count > 0);

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
