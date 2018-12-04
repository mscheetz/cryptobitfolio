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
    public class HistoricalPriceRepositoryTests : IDisposable
    {
        private readonly IHistoricalPriceRepository _repo;
        private List<HistoricalPrice> datas = new List<HistoricalPrice>();

        public HistoricalPriceRepositoryTests()
        {
            _repo = new HistoricalPriceRepository();
            
            // first clear out the table
            var deleted = _repo.DeleteAll().Result;

            // then add some data for testing
            datas.Add(
                new HistoricalPrice
                {
                    Id = 0,
                    CurrencyId = 1,
                    Exchange = Business.Entities.Exchange.Binance,
                    Pair = "BTCUSDT",
                    Price = 4210.00M,
                    Snapshot = DateTime.UtcNow.AddHours(-4),
                });
            datas.Add(
                new HistoricalPrice
                {
                    Id = 0,
                    CurrencyId = 1,
                    Exchange = Business.Entities.Exchange.Binance,
                    Pair = "BTCUSDT",
                    Price = 4100.00M,
                    Snapshot = DateTime.UtcNow.AddHours(-3),
                });
            datas.Add(
                new HistoricalPrice
                {
                    Id = 0,
                    CurrencyId = 1,
                    Exchange = Business.Entities.Exchange.Binance,
                    Pair = "BTCUSDT",
                    Price = 4050.00M,
                    Snapshot = DateTime.UtcNow.AddHours(-2),
                });
            datas.Add(
                new HistoricalPrice
                {
                    Id = 0,
                    CurrencyId = 1,
                    Exchange = Business.Entities.Exchange.Binance,
                    Pair = "BTCUSDT",
                    Price = 3950.00M,
                    Snapshot = DateTime.UtcNow.AddHours(-1),
                });
            datas.Add(
                new HistoricalPrice
                {
                    Id = 0,
                    CurrencyId = 1,
                    Exchange = Business.Entities.Exchange.Binance,
                    Pair = "BTCUSDT",
                    Price = 3850.00M,
                    Snapshot = DateTime.UtcNow,
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
            var entityList = _repo.Get(e => e.Snapshot).Result;

            Assert.NotNull(entityList);
            Assert.NotEmpty(entityList);
        }

        [Fact]
        public void GetManySearchAndOrder_Test()
        {
            var entityList = _repo.Get(e => e.Exchange == Business.Entities.Exchange.Binance, e => e.Snapshot).Result;

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

            entity.Snapshot = newProperty;

            var updatedEntity = _repo.Update(entity).Result;

            Assert.Equal(entity.Snapshot, updatedEntity.Snapshot);

            var entityFetch = _repo.GetOne(id).Result;

            Assert.NotNull(entityFetch);
            Assert.Equal(id, entityFetch.Id);
            Assert.Equal(newProperty, entityFetch.Snapshot);
        }

        [Fact]
        public void UpdateAll_Test()
        {
            var newProperties = new List<DateTime> { DateTime.UtcNow, DateTime.UtcNow };
            var entities = _repo.Get().Result;
            var entityList = entities.ToList();

            Assert.NotNull(entities);
            Assert.NotEmpty(entities);

            entityList[0].Snapshot = newProperties[0];
            entityList[1].Snapshot = newProperties[1];

            var updatedEntities = _repo.UpdateAll(entities).Result;
            var updatedList = updatedEntities.ToList();

            Assert.Equal(entityList[0].Snapshot, updatedList[0].Snapshot);
            Assert.Equal(entityList[1].Snapshot, updatedList[1].Snapshot);

            var entitiesFetch = _repo.Get().Result;
            var fetchList = entitiesFetch.ToList();

            Assert.NotNull(entitiesFetch);
            Assert.NotEmpty(entitiesFetch);
            Assert.Equal(newProperties[0], fetchList[0].Snapshot);
            Assert.Equal(newProperties[1], fetchList[1].Snapshot);
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
