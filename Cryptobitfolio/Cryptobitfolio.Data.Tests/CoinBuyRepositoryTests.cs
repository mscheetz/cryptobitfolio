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
                    Exchange = Business.Entities.Exchange.Binance,
                    Price = 5418.00M,
                    Quantity = 0.4M,
                    ClosedDate = DateTime.UtcNow
                });
            datas.Add(
                new CoinBuy
                {
                    Id = 0,
                    CoinId = 2,
                    CurrencyId = 2,
                    Exchange = Business.Entities.Exchange.Binance,
                    Price = 0.0240M,
                    Quantity = 14.2M,
                    ClosedDate = DateTime.UtcNow
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
            var entityList = _repo.Get(e => e.Price).Result;

            Assert.NotNull(entityList);
            Assert.NotEmpty(entityList);
        }

        [Fact]
        public void GetManySearchAndOrder_Test()
        {
            var entityList = _repo.Get(e => e.Exchange == Business.Entities.Exchange.Binance, e => e.Price).Result;

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
            var entity = _repo.GetOne(e => e.Exchange == Business.Entities.Exchange.Binance && e.CurrencyId == id).Result;

            Assert.NotNull(entity);
            Assert.Equal(id, entity.Id);
        }

        [Fact]
        public void UpdateOne_Test()
        {
            var id = 1;
            var newProperty = 3900.00M;
            var entity = _repo.GetOne(id).Result;

            Assert.NotNull(entity);
            Assert.Equal(id, entity.Id);

            entity.Price = newProperty;

            var updatedEntity = _repo.Update(entity).Result;

            Assert.Equal(entity.Price, updatedEntity.Price);

            var entityFetch = _repo.GetOne(id).Result;

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

            var entityList = entities.ToList();

            entityList[0].Price = newProperties[0];
            entityList[1].Price = newProperties[1];

            var updatedEntities = _repo.UpdateAll(entities).Result;
            var updatedList = updatedEntities.ToList();

            Assert.Equal(entityList[0].Price, updatedList[0].Price);
            Assert.Equal(entityList[1].Price, updatedList[1].Price);

            var entitiesFetch = _repo.Get().Result;
            var fetchList = entitiesFetch.ToList();

            Assert.NotNull(entitiesFetch);
            Assert.NotEmpty(entitiesFetch);
            Assert.Equal(newProperties[0], fetchList[0].Price);
            Assert.Equal(newProperties[1], fetchList[1].Price);
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
