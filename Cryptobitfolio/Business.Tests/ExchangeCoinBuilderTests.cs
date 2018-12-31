// -----------------------------------------------------------------------------
// <copyright file="ExchangeCoinBuilderTests" company="Matt Scheetz">
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
    
    public class ExchangeCoinBuilderTests : IDisposable
    {
        private TestObjects testObjs;
        private IExchangeCoinBuilder _bldr;
        private Mock<ICoinBuyBuilder> coinBuyBldrMock = new Mock<ICoinBuyBuilder>();
        private Mock<IExchangeOrderBuilder> exOrderBldrMock = new Mock<IExchangeOrderBuilder>();
        private Mock<IExchangeHubBuilder> exHubBldrMock = new Mock<IExchangeHubBuilder>();
        private Mock<IExchangeCoinRepository> exCoinRepoMock = new Mock<IExchangeCoinRepository>();

        public ExchangeCoinBuilderTests()
        {
            testObjs = new TestObjects();

            exCoinRepoMock.Setup(m => m.Add(It.IsAny<ExchangeCoin>()))
                .Returns(Task.FromResult(testObjs.GetExchangeCoinEntities().First()));
            exCoinRepoMock.Setup(m => m.Delete(It.IsAny<ExchangeCoin>()))
                .Returns(Task.FromResult(true));
            exCoinRepoMock.Setup(m => m.Get())
                .Returns(Task.FromResult(testObjs.GetExchangeCoinEntities()));

            _bldr = new ExchangeCoinBuilder(coinBuyBldrMock.Object, exOrderBldrMock.Object, exHubBldrMock.Object, exCoinRepoMock.Object);
        }

        public void Dispose()
        {
        }

        #region Tests

        [Fact]
        public void AddTest()
        {
            // Arrange
            var expected = testObjs.GetExchangeCoinContracts().First();
            var exchangeCoin = testObjs.GetExchangeCoinContracts().First();
            exchangeCoin.ExchangeCoinId = 0;

            // Act
            var result = _bldr.Add(exchangeCoin).Result;

            // Assert
            Assert.Equal(expected.ExchangeCoinId, result.ExchangeCoinId);
        }

        [Fact]
        public void DeleteTest()
        {
            // Arrange
            var exchangeCoin = testObjs.GetExchangeCoinContracts().First();

            // Act
            var result = _bldr.Delete(exchangeCoin).Result;

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void UpdateTest()
        {
            // Arrange
            var expected = testObjs.GetExchangeCoinContracts().First();
            expected.Quantity = 100;
            var updatedEntity = testObjs.GetExchangeCoinEntities().First();
            updatedEntity.Quantity = 100;
            exCoinRepoMock.Setup(m => m.Update(It.IsAny<ExchangeCoin>()))
                .Returns(Task.FromResult(updatedEntity));

            _bldr = new ExchangeCoinBuilder(coinBuyBldrMock.Object, exOrderBldrMock.Object, exHubBldrMock.Object, exCoinRepoMock.Object);

            // Act
            var result = _bldr.Update(expected).Result;

            // Assert
            Assert.Equal(expected.Quantity, result.Quantity);
        }
        
        [Fact]
        public void GetTest()
        {
            // Arrange
            var expected = testObjs.GetExchangeCoinContracts();

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
            var expected = testObjs.GetExchangeCoinContracts().Where(e => e.Symbol.Equals(symbol));
            exCoinRepoMock.Setup(m => m.Get(It.IsAny<Expression<Func<ExchangeCoin, bool>>>()))
                .Returns(Task.FromResult(testObjs.GetExchangeCoinEntities().Where(e => e.Symbol.Equals(symbol))));

            _bldr = new ExchangeCoinBuilder(coinBuyBldrMock.Object, exOrderBldrMock.Object, exHubBldrMock.Object, exCoinRepoMock.Object);

            // Act
            var result = _bldr.Get(symbol).Result;

            // Assert
            Assert.Equal(expected.Count(), result.Count());
        }

        [Fact]
        public void GetBySymbolsTest()
        {
            // Arrange
            var symbols = new List<string> { "BTC", "ETH" };
            var expected = testObjs.GetExchangeCoinContracts().Where(e => symbols.Contains(e.Symbol));
            exCoinRepoMock.Setup(m => m.Get(It.IsAny<Expression<Func<ExchangeCoin, bool>>>()))
                .Returns(Task.FromResult(testObjs.GetExchangeCoinEntities()));

            _bldr = new ExchangeCoinBuilder(coinBuyBldrMock.Object, exOrderBldrMock.Object, exHubBldrMock.Object, exCoinRepoMock.Object);

            // Act
            var result = _bldr.Get(symbols).Result;

            // Assert
            Assert.Equal(expected.Count(), result.Count());
        }

        [Fact]
        public void GetByExchangeTest()
        {
            // Arrange
            var exchange = Cryptobitfolio.Business.Entities.Exchange.CoinbasePro;
            var expected = testObjs.GetExchangeCoinContracts().Where(e => e.Exchange == exchange);
            exCoinRepoMock.Setup(m => m.Get(It.IsAny<Expression<Func<ExchangeCoin, bool>>>()))
                .Returns(Task.FromResult(testObjs.GetExchangeCoinEntities().Where(e => e.Exchange == exchange)));

            _bldr = new ExchangeCoinBuilder(coinBuyBldrMock.Object, exOrderBldrMock.Object, exHubBldrMock.Object, exCoinRepoMock.Object);

            // Act
            var result = _bldr.Get(exchange).Result;

            // Assert
            Assert.Equal(expected.Count(), result.Count());
        }

        [Fact]
        public void GetBySymbolAndExchangeTest()
        {
            // Arrange
            var symbol = "BTC";
            var exchange = Cryptobitfolio.Business.Entities.Exchange.Binance;
            var expected = testObjs.GetExchangeCoinContracts()
                .Where(e => e.Exchange == exchange && e.Symbol.Equals(symbol));
            exCoinRepoMock.Setup(m => m.Get(It.IsAny<Expression<Func<ExchangeCoin, bool>>>()))
                .Returns(Task.FromResult(testObjs.GetExchangeCoinEntities()
                .Where(e => e.Exchange == exchange && e.Symbol.Equals(symbol))));

            _bldr = new ExchangeCoinBuilder(coinBuyBldrMock.Object, exOrderBldrMock.Object, exHubBldrMock.Object, exCoinRepoMock.Object);

            // Act
            var result = _bldr.Get(exchange).Result;

            // Assert
            Assert.Equal(expected.Count(), result.Count());
        }

        [Fact]
        public void GetLatestBySymbolTest()
        {
            // Arrange
            var symbol = "BTC";
            var expectedQuantity = 100;
            var hubCoins = testObjs.GetExchangeCoinContracts().Where(e => e.Symbol.Equals(symbol)).ToList();
            hubCoins[0].Quantity = expectedQuantity;
            exHubBldrMock.Setup(m => m.GetBalances(It.IsAny<string>()))
                .Returns(Task.FromResult(hubCoins.AsEnumerable()));
            exCoinRepoMock.Setup(m => m.Get(It.IsAny<Expression<Func<ExchangeCoin, bool>>>()))
                .Returns(Task.FromResult(testObjs.GetExchangeCoinEntities().Where(e => e.Symbol.Equals(symbol))));
            var updatedEntity = testObjs.GetExchangeCoinEntities().First();
            updatedEntity.Quantity = expectedQuantity;
            exCoinRepoMock.Setup(m => m.Update(It.IsAny<ExchangeCoin>()))
                .Returns(Task.FromResult(updatedEntity));
            var coinBuys = testObjs.GetCoinBuyContracts().Where(c => c.Pair.StartsWith(symbol));
            var avgPrice = 0.0M;
            var count = 0;
            foreach(var cb in coinBuys)
            {
                avgPrice += cb.Price;
                count++;
            }
            avgPrice = avgPrice / count;
            coinBuyBldrMock.Setup(m => m.GetLatest(It.IsAny<string>(), It.IsAny<decimal>(), It.IsAny<Cryptobitfolio.Business.Entities.Exchange>()))
                .Returns(Task.FromResult(coinBuys));
            exOrderBldrMock.Setup(m => m.GetLatest(It.IsAny<string>(), It.IsAny<Cryptobitfolio.Business.Entities.Exchange>()))
                .Returns(Task.FromResult(It.IsAny<IEnumerable<Cryptobitfolio.Business.Contracts.Trade.ExchangeOrder>>()));

            _bldr = new ExchangeCoinBuilder(coinBuyBldrMock.Object, exOrderBldrMock.Object, exHubBldrMock.Object, exCoinRepoMock.Object);

            // Act
            var result = _bldr.GetLatest(symbol).Result;

            // Assert
            Assert.Equal(expectedQuantity, result.First().Quantity);
            Assert.Equal(avgPrice, result.First().AverageBuy);
        }

        [Fact]
        public void GetLatestByExchangeTest()
        {
            // Arrange
            var exchange = Cryptobitfolio.Business.Entities.Exchange.Binance;
            var expectedQuantity1 = 100;
            var expectedQuantity2 = 1000;
            var hubCoins = testObjs.GetExchangeCoinContracts().Where(e => e.Exchange == exchange).ToList();
            hubCoins[0].Quantity = expectedQuantity1;
            hubCoins[1].Quantity = expectedQuantity2;
            exHubBldrMock.Setup(m => m.GetExchangeBalances(It.IsAny<Cryptobitfolio.Business.Entities.Exchange>()))
                .Returns(Task.FromResult(hubCoins.AsEnumerable()));
            exCoinRepoMock.Setup(m => m.Get(It.IsAny<Expression<Func<ExchangeCoin, bool>>>()))
                .Returns(Task.FromResult(testObjs.GetExchangeCoinEntities().Where(e => e.Exchange == exchange)));
            var btcEntity = testObjs.GetExchangeCoinEntities().Where(e => e.Symbol.Equals("BTC")).First();
            var ethEntity = testObjs.GetExchangeCoinEntities().Where(e => e.Symbol.Equals("ETH")).First();
            btcEntity.Quantity = expectedQuantity1;
            ethEntity.Quantity = expectedQuantity2;
            exCoinRepoMock.SetupSequence(m => m.Update(It.IsAny<ExchangeCoin>()))
                .Returns(Task.FromResult(btcEntity))
                .Returns(Task.FromResult(ethEntity));
            var coinBuys = testObjs.GetCoinBuyContracts();
            coinBuyBldrMock.SetupSequence(m => m.GetLatest(It.IsAny<string>(), It.IsAny<decimal>(), It.IsAny<Cryptobitfolio.Business.Entities.Exchange>()))
                .Returns(Task.FromResult(coinBuys.Where(c => c.Pair.StartsWith("BTC"))))
                .Returns(Task.FromResult(coinBuys.Where(c => c.Pair.StartsWith("ETH"))));
            exOrderBldrMock.Setup(m => m.GetLatest(It.IsAny<string>(), It.IsAny<Cryptobitfolio.Business.Entities.Exchange>()))
                .Returns(Task.FromResult(It.IsAny<IEnumerable<Cryptobitfolio.Business.Contracts.Trade.ExchangeOrder>>()));

            _bldr = new ExchangeCoinBuilder(coinBuyBldrMock.Object, exOrderBldrMock.Object, exHubBldrMock.Object, exCoinRepoMock.Object);

            // Act
            var result = _bldr.GetLatest(exchange).Result.ToList();

            // Assert
            Assert.Equal(expectedQuantity1, result[0].Quantity);
            Assert.Equal(expectedQuantity2, result[1].Quantity);
        }

        #endregion Tests
    }
}
