using Cryptobitfolio.Business.Entities.Portfolio;
using Cryptobitfolio.Data.Interfaces;
using Cryptobitfolio.Data.Repositories;
using System;
using System.Collections.Generic;
using Xunit;

namespace Cryptobitfolio.Data.Tests
{
    public class CoinBuyRepositoryTests : IDisposable
    {
        private readonly ICoinBuyRepository _repo;
        private List<CoinBuy> datas = new List<CoinBuy>();

        public CoinBuyRepositoryTests()
        {
            _repo = new CoinBuyRepository();
            
            // first clear out the table
            var deleted = _repo.DeleteAll().Result;

            // then add some data for testing
            datas.Add(
                new CoinBuy
                {
                    Id = 0,
                    CoinId = 1,
                    CurrencyId = 1,
                    ExchangeName = Business.Entities.Exchange.Binance,
                    Price = 5418.00M,
                    Quantity = 0.4M,
                    TransactionDate = DateTime.UtcNow
                });
            datas.Add(
                new CoinBuy
                {
                    Id = 0,
                    CoinId = 2,
                    CurrencyId = 2,
                    ExchangeName = Business.Entities.Exchange.Binance,
                    Price = 0.0240M,
                    Quantity = 14.2M,
                    TransactionDate = DateTime.UtcNow
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
            var newProperty = 3900.00M;
            var entity = _repo.Get(id).Result;

            Assert.NotNull(entity);
            Assert.Equal(id, entity.Id);

            entity.Price = newProperty;

            var updatedEntity = _repo.Update(entity).Result;

            Assert.Equal(entity.Price, updatedEntity.Price);

            var entityFetch = _repo.Get(id).Result;

            Assert.NotNull(entityFetch);
            Assert.Equal(id, entityFetch.Id);
            Assert.Equal(newProperty, entityFetch.Price);
        }

        [Fact]
        public void UpdateAll_Test()
        {
            var newProperties = new List<decimal> { 3900.00M, 0.05M };
            var entities = _repo.Get().Result;

            Assert.NotNull(entities);
            Assert.NotEmpty(entities);

            entities[0].Price = newProperties[0];
            entities[1].Price = newProperties[1];

            var updatedEntities = _repo.UpdateAll(entities).Result;

            Assert.Equal(entities[0].Price, updatedEntities[0].Price);
            Assert.Equal(entities[1].Price, updatedEntities[1].Price);

            var entitiesFetch = _repo.Get().Result;

            Assert.NotNull(entitiesFetch);
            Assert.NotEmpty(entitiesFetch);
            Assert.Equal(newProperties[0], entitiesFetch[0].Price);
            Assert.Equal(newProperties[1], entitiesFetch[1].Price);
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
