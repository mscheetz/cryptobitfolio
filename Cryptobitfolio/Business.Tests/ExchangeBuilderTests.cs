using Cryptobitfolio.Business;
using Cryptobitfolio.Business.Common;
using Cryptobitfolio.Business.Contracts.Trade;
using Cryptobitfolio.Business.Entities.Trade;
using Cryptobitfolio.Data.Interfaces;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Business.Tests
{
    public class ExchangeBuilderTests : IDisposable
    {
        private IExchangeBuilder exchangeBuilder;

        public ExchangeBuilderTests()
        {
            var fileRepo = new FileRepository.FileRepository();
            var apiInfo = fileRepo.GetDataFromFile<Cryptobitfolio.Business.Entities.Trade.ExchangeApi>("config.json");
            var apiInfoList = new List<Cryptobitfolio.Business.Entities.Trade.ExchangeApi> { apiInfo };
            var exchangeApiRepoMock = new Mock<IExchangeApiRepository>();
            exchangeApiRepoMock.Setup(m => m.Get()).Returns(Task.FromResult(apiInfoList));
            var exchangeUpdateRepoMock = new Mock<IExchangeUpdateRepository>();
            var arbitrageRepoMock = new Mock<IArbitragePathRepository>();
            var arbitrageBldrMock = new Mock<IArbitrageBuilder>();
            exchangeBuilder = new ExchangeBuilder(exchangeApiRepoMock.Object, exchangeUpdateRepoMock.Object, arbitrageRepoMock.Object, arbitrageBldrMock.Object);
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        [Fact]
        public void GetCoinsTest_NoCoins()
        {
            var coinList = exchangeBuilder.GetCoins();

            Assert.NotEmpty(coinList);
        }
    }
}
