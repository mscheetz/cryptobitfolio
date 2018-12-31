// -----------------------------------------------------------------------------
// <copyright file="AlertBuilderTests" company="Matt Scheetz">
//     Copyright (c) Matt Scheetz All Rights Reserved
// </copyright>
// <author name="Matt Scheetz" date="12/30/2018 8:27:09 PM" />
// -----------------------------------------------------------------------------

namespace Business.Tests
{
    #region Usings

    using Cryptobitfolio.Business;
    using Cryptobitfolio.Business.Common;
    using Cryptobitfolio.Business.Entities.Portfolio;
    using Cryptobitfolio.Data.Interfaces.Database;
    using Cryptobitfolio.Tests.Common;
    using Moq;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Text;
    using System.Threading.Tasks;
    using Xunit;

    #endregion Usings

    public class AlertBuilderTests : IDisposable
    {
        #region Properties

        private TestObjects testObjs;
        private IAlertBuilder _bldr;
        private Mock<IAlerterRepository> alertRepoMock = new Mock<IAlerterRepository>();
        private Mock<IHistoricalPriceBuilder> histPriceBldrMock = new Mock<IHistoricalPriceBuilder>();

        #endregion Properties

        public  AlertBuilderTests()
        {
            testObjs = new TestObjects();
            alertRepoMock.Setup(m => m.Add(It.IsAny<Alerter>()))
                .Returns(Task.FromResult(testObjs.GetAlertEntities().First()));
            alertRepoMock.Setup(m => m.Delete(It.IsAny<Alerter>()))
                .Returns(Task.FromResult(true));
            alertRepoMock.Setup(m => m.Get())
                .Returns(Task.FromResult(testObjs.GetAlertEntities()));

            _bldr = new AlertBuilder(alertRepoMock.Object, histPriceBldrMock.Object);
        }

        public void Dispose()
        {
        }

        #region Tests

        [Fact]
        public void AddTest()
        {
            // Arrange
            var expected = testObjs.GetAlertContracts().First();
            var alerter = testObjs.GetAlertContracts().First();
            alerter.AlertId = 0;

            // Act
            var result = _bldr.Add(alerter).Result;

            // Assert
            Assert.Equal(expected.AlertId, result.AlertId);
        }

        [Fact]
        public void DeleteTest()
        {
            // Arrange
            var alerter = testObjs.GetAlertContracts().First();

            // Act
            var result = _bldr.Delete(alerter).Result;

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void UpdateTest()
        {
            // Arrange
            var expected = testObjs.GetAlertContracts().First();
            expected.Price = 3875.0M;
            var updatedEntity = testObjs.GetAlertEntities().First();
            updatedEntity.Price = 3875.0M;
            alertRepoMock.Setup(m => m.Update(It.IsAny<Alerter>()))
                .Returns(Task.FromResult(updatedEntity));

            _bldr = new AlertBuilder(alertRepoMock.Object, histPriceBldrMock.Object);

            // Act
            var result = _bldr.Update(expected).Result;

            // Assert
            Assert.Equal(expected.Price, result.Price);
        }

        [Fact]
        public void GetTest()
        {
            // Arrange
            var expected = testObjs.GetAlertContracts();

            // Act
            var result = _bldr.Get().Result;

            // Assert
            Assert.Equal(expected.Count(), result.Count());
        }

        [Fact]
        public void GetBySymbolTest()
        {
            // Arrange
            var symbol = "BTC";
            var expected = testObjs.GetAlertContracts().Where(e => e.Pair.StartsWith(symbol));
            alertRepoMock.Setup(m => m.Get(It.IsAny<Expression<Func<Alerter, bool>>>()))
                .Returns(Task.FromResult(testObjs.GetAlertEntities().Where(e => e.Pair.StartsWith(symbol))));

            _bldr = new AlertBuilder(alertRepoMock.Object, histPriceBldrMock.Object);

            // Act
            var result = _bldr.GetBySymbol(symbol).Result;

            // Assert
            Assert.Equal(expected.Count(), result.Count());
        }

        [Fact]
        public void GetByPairTest()
        {
            // Arrange
            var pair = "BTCUSDT";
            var expected = testObjs.GetAlertContracts().Where(e => e.Pair.Equals(pair));
            alertRepoMock.Setup(m => m.Get(It.IsAny<Expression<Func<Alerter, bool>>>()))
                .Returns(Task.FromResult(testObjs.GetAlertEntities().Where(e => e.Pair.Equals(pair))));

            _bldr = new AlertBuilder(alertRepoMock.Object, histPriceBldrMock.Object);

            // Act
            var result = _bldr.GetByPair(pair).Result;

            // Assert
            Assert.Equal(expected.Count(), result.Count());
        }

        [Fact]
        public void GetLatestTest()
        {
            ///////////
            /// TODO: Need to have alerter getlatest and watch getlatest 
            /// get latest prices from exchanges
            ///////////

            var expected = testObjs.GetAlertContracts();
            alertRepoMock.Setup(m => m.Get())
                .Returns(Task.FromResult(testObjs.GetAlertEntities()));
            alertRepoMock.Setup(m => m.Update(It.IsAny<Alerter>()))
                .Returns(Task.FromResult(It.IsAny<Alerter>()));
            histPriceBldrMock.Setup(m => m.GetLatest(It.IsAny<Dictionary<Cryptobitfolio.Business.Entities.Exchange, List<string>>>()))
                .Returns(Task.FromResult(testObjs.GetHistoricalPriceContracts()));

            _bldr = new AlertBuilder(alertRepoMock.Object, histPriceBldrMock.Object);

            // Act
            var result = _bldr.GetLatest().Result;

            // Assert
            Assert.Equal(expected.Count(), result.Count());
        }

        #endregion Tests
    }
}