using Cryptobitfolio.Business.Entities.Trade;
using Cryptobitfolio.Data.Interfaces;
using Cryptobitfolio.Data.Interfaces.Database;
using Cryptobitfolio.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Cryptobitfolio.Data.Tests
{
    public class ExchangeOrderRepositoryTests : IDisposable
    {
        private readonly IExchangeOrderRepository _repo;
        private List<ExchangeOrder> datas = new List<ExchangeOrder>();

        public ExchangeOrderRepositoryTests()
        {
            _repo = new ExchangeOrderRepository();
            
            // first clear out the table
            var deleted = _repo.DeleteAll().Result;

            // then add some data for testing
            datas.Add(
                new ExchangeOrder
                {
                    PlaceDate = DateTime.UtcNow.AddDays(-4),
                    Exchange = Business.Entities.Exchange.Binance,
                    ClosedDate = null,
                    Price = 3489.23M,
                    Quantity = 0.25M,
                    Status = Business.Entities.TradeStatus.Open,
                    Side = Business.Entities.Side.Buy,
                    Pair = "BTCUSDT"
                });
            datas.Add(
                new ExchangeOrder
                {
                    PlaceDate = DateTime.UtcNow.AddDays(-10),
                    Exchange = Business.Entities.Exchange.Binance,
                    ClosedDate = DateTime.UtcNow.AddDays(-10),
                    Price = 0.021M,
                    Quantity = 4.5M,
                    Status = Business.Entities.TradeStatus.Filled,
                    Side = Business.Entities.Side.Buy,
                    Pair = "ETHBTC"
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
            var entityList = _repo.Get(e => e.PlaceDate).Result;

            Assert.NotNull(entityList);
            Assert.NotEmpty(entityList);
        }

        [Fact]
        public void GetManySearchAndOrder_Test()
        {
            var entityList = _repo.Get(e => e.Exchange == Business.Entities.Exchange.Binance, e => e.PlaceDate).Result;

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
            var entity = _repo.GetOne(e => e.Exchange == Business.Entities.Exchange.Binance && e.Id == id).Result;

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

            entity.ClosedDate = newProperty;

            var updatedEntity = _repo.Update(entity).Result;

            Assert.Equal(entity.ClosedDate, updatedEntity.ClosedDate);

            var entityFetch = _repo.GetOne(id).Result;

            Assert.NotNull(entityFetch);
            Assert.Equal(id, entityFetch.Id);
            Assert.Equal(newProperty, entityFetch.ClosedDate);
        }

        [Fact]
        public void UpdateAll_Test()
        {
            var newProperties = new List<DateTime> { DateTime.UtcNow.AddMonths(-5), DateTime.UtcNow.AddMonths(-1) };
            var entities = _repo.Get().Result;
            var entityList = entities.ToList();

            Assert.NotNull(entities);
            Assert.NotEmpty(entities);

            entityList[0].ClosedDate = newProperties[0];
            entityList[1].ClosedDate = newProperties[1];

            var updatedEntities = _repo.UpdateAll(entities).Result;
            var updatedList = updatedEntities.ToList();

            Assert.Equal(entityList[0].ClosedDate, updatedList[0].ClosedDate);
            Assert.Equal(entityList[1].ClosedDate, updatedList[1].ClosedDate);

            var entitiesFetch = _repo.Get().Result;
            var fetchList = entitiesFetch.ToList();

            Assert.NotNull(entitiesFetch);
            Assert.NotEmpty(entitiesFetch);
            Assert.Equal(newProperties[0], fetchList[0].ClosedDate);
            Assert.Equal(newProperties[1], fetchList[1].ClosedDate);
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
