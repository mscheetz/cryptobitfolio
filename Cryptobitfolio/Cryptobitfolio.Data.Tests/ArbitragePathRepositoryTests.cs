using Cryptobitfolio.Business.Entities.Portfolio;
using Cryptobitfolio.Data.Interfaces;
using Cryptobitfolio.Data.Repositories;
using System;
using System.Collections.Generic;
using Xunit;

namespace Cryptobitfolio.Data.Tests
{
    public class ArbitragePathRepositoryTests : IDisposable
    {
        private readonly IArbitragePathRepository _repo;
        private List<ArbitragePath> datas = new List<ArbitragePath>();

        public ArbitragePathRepositoryTests()
        {
            _repo = new ArbitragePathRepository();
            
            // first clear out the table
            var deleted = _repo.DeleteAll().Result;

            // then add some data for testing
            datas.Add(
                new ArbitragePath
                {
                    Id = 0,
                    Created = DateTime.UtcNow,
                    Path = "Pair 1,Pair 2,Pair 3"
                });
            datas.Add(
                new ArbitragePath
                {
                    Id = 0,
                    Created = DateTime.UtcNow,
                    Path = "Pair 1,Pair 3,Pair 4" 
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
            var newProperty = "a new path";
            var entity = _repo.Get(id).Result;

            Assert.NotNull(entity);
            Assert.Equal(id, entity.Id);

            entity.Path = newProperty;

            var updatedEntity = _repo.Update(entity).Result;

            Assert.Equal(entity.Path, updatedEntity.Path);

            var entityFetch = _repo.Get(id).Result;

            Assert.NotNull(entityFetch);
            Assert.Equal(id, entityFetch.Id);
            Assert.Equal(newProperty, entityFetch.Path);
        }

        [Fact]
        public void UpdateAll_Test()
        {
            var newProperties = new List<string> { "new path 1", "new path 2" };
            var entities = _repo.Get().Result;

            Assert.NotNull(entities);
            Assert.NotEmpty(entities);

            entities[0].Path = newProperties[0];
            entities[1].Path = newProperties[1];

            var updatedEntities = _repo.UpdateAll(entities).Result;

            Assert.Equal(entities[0].Path, updatedEntities[0].Path);
            Assert.Equal(entities[1].Path, updatedEntities[1].Path);

            var entitiesFetch = _repo.Get().Result;

            Assert.NotNull(entitiesFetch);
            Assert.NotEmpty(entitiesFetch);
            Assert.Equal(newProperties[0], entitiesFetch[0].Path);
            Assert.Equal(newProperties[1], entitiesFetch[1].Path);
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
