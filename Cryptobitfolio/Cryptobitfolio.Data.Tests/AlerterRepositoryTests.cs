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
    public class AlerterRepositoryTests : IDisposable
    {
        private readonly IAlerterRepository _repo;
        private List<Alerter> datas = new List<Alerter>();

        public AlerterRepositoryTests()
        {
            _repo = new AlerterRepository();
            
            // first clear out the table
            var deleted = _repo.DeleteAll().Result;

            // then add some data for testing
            datas.Add(
                new Alerter
                {
                    Id = 0,
                    Created = DateTime.UtcNow.AddDays(-10),
                    CurrencyId = 1,
                    Enabled = true,
                    Exchange = Business.Entities.Exchange.Binance,
                    Hit = null,
                    Pair = "BTCUSDT",
                    Price = 3412.00M
                });
            datas.Add(
                new Alerter
                {
                    Id = 0,
                    Created = DateTime.UtcNow.AddDays(-4),
                    CurrencyId = 2,
                    Enabled = true,
                    Exchange = Business.Entities.Exchange.Binance,
                    Hit = null,
                    Pair = "ETHUSDT",
                    Price = 75.5M
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
        public void GetManySearch_Test()
        {
            var entityList = _repo.Get(e => e.Exchange == Business.Entities.Exchange.Binance).Result;

            Assert.NotNull(entityList);
            Assert.NotEmpty(entityList);
        }

        [Fact]
        public void GetManyOrder_Test()
        {
            var entityList = _repo.Get(e => e.Created).Result;

            Assert.NotNull(entityList);
            Assert.NotEmpty(entityList);
        }

        [Fact]
        public void GetManySearchAndOrder_Test()
        {
            var entityList = _repo.Get(e => e.Exchange == Business.Entities.Exchange.Binance, e => e.Created).Result;

            Assert.NotNull(entityList);
            Assert.NotEmpty(entityList);
        }

        [Fact]
        public void GetOne_Test()
        {
            var id = 1;
            var entity = _repo.GetOne(id).Result;

            Assert.NotNull(entity);
            Assert.Equal(id, entity.Id);
        }

        [Fact]
        public void GetOneSearch_Test()
        {
            var id = 1;
            var entity = _repo.GetOne(e => e.Exchange == Business.Entities.Exchange.Binance && e.Pair.Equals("BTCUSDT")).Result;

            Assert.NotNull(entity);
            Assert.Equal(id, entity.Id);
        }

        [Fact]
        public void UpdateOne_Test()
        {
            var id = 1;
            var newProperty = DateTime.UtcNow;
            var entity = _repo.GetOne(id).Result;

            Assert.NotNull(entity);
            Assert.Equal(id, entity.Id);

            entity.Hit = newProperty;

            var updatedEntity = _repo.Update(entity).Result;

            Assert.Equal(entity.Hit, updatedEntity.Hit);

            var entityFetch = _repo.GetOne(id).Result;

            Assert.NotNull(entityFetch);
            Assert.Equal(id, entityFetch.Id);
            Assert.Equal(newProperty, entityFetch.Hit);
        }

        [Fact]
        public void UpdateAll_Test()
        {
            var newProperties = new List<DateTime> { DateTime.UtcNow, DateTime.UtcNow };
            var entities = _repo.Get().Result;
            var entityList = entities.ToList();

            Assert.NotNull(entities);
            Assert.NotEmpty(entities);

            entityList[0].Hit = newProperties[0];
            entityList[1].Hit = newProperties[1];

            var updatedEntities = _repo.UpdateAll(entities).Result;
            var updatedList = updatedEntities.ToList();

            Assert.Equal(entityList[0].Hit, updatedList[0].Hit);
            Assert.Equal(entityList[1].Hit, updatedList[1].Hit);

            var entitiesFetch = _repo.Get().Result;
            var fetchList = entitiesFetch.ToList();

            Assert.NotNull(entitiesFetch);
            Assert.NotEmpty(entitiesFetch);
            Assert.Equal(newProperties[0], fetchList[0].Hit);
            Assert.Equal(newProperties[1], fetchList[1].Hit);
        }

        [Fact]
        public void Delete_Test()
        {
            var entityList = _repo.Get().Result;
            var entityToDelete = entityList.FirstOrDefault();

            var delete = _repo.Delete(entityToDelete).Result;

            var entityFetch = _repo.GetOne(entityToDelete.Id).Result;

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
