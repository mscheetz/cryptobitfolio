using Cryptobitfolio.Business;
using Cryptobitfolio.Business.Common;
using Cryptobitfolio.Business.Contracts.Portfolio;
using Cryptobitfolio.Business.Contracts.Trade;
using Cryptobitfolio.Business.Entities.Trade;
using Cryptobitfolio.Data.Interfaces;
using Cryptobitfolio.Tests.Common;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Business.Tests
{
    public class ExchangeBuilderTests : IDisposable
    {
        private IExchangeBuilder exchangeBuilder;
        private TestObjects testObjs;

        public ExchangeBuilderTests()
        {
            testObjs = new TestObjects();
            var fileRepo = new FileRepository.FileRepository();
            var apiInfo = fileRepo.GetDataFromFile<Cryptobitfolio.Business.Entities.Trade.ExchangeApi>("config.json");
            var apiInfoList = new List<Cryptobitfolio.Business.Entities.Trade.ExchangeApi> { apiInfo };
            var exchangeApiRepoMock = new Mock<IExchangeApiRepository>();
            exchangeApiRepoMock.Setup(m => m.Get()).Returns(Task.FromResult(apiInfoList));
            var exchangeUpdateRepoMock = new Mock<IExchangeUpdateRepository>();
            var arbitrageRepoMock = new Mock<IArbitragePathRepository>();
            var arbitrageBldrMock = new Mock<IArbitrageBuilder>();
            var exchangeHubRepo = new Mock<IExchangeHubRepository>();
            exchangeHubRepo.Setup(m => m.GetBalances())
                .Returns(Task.FromResult(testObjs.GetExchangeHubBalances()));
            exchangeHubRepo.Setup(m => m.GetOrders(It.IsAny<string>()))
                .Returns(Task.FromResult(testObjs.GetExchangeHubOrders().Where(o => o.Pair.Equals("BTCUSDT"))));
            exchangeHubRepo.Setup(m => m.GetExchange())
                .Returns(testObjs.GetExchangeHubExchange());
            exchangeHubRepo.Setup(m => m.GetMarkets())
                .Returns(Task.FromResult(testObjs.GetExchangeHubMarkets()));
            exchangeBuilder = new ExchangeBuilder(exchangeApiRepoMock.Object, exchangeUpdateRepoMock.Object, arbitrageRepoMock.Object, arbitrageBldrMock.Object, exchangeHubRepo.Object);
        }

        public void Dispose()
        {
        }

        [Fact]
        public void GetMarketsForACoinTest()
        {
            var symbol = "XLM";
            var expected = new List<string> { "XLMBTC", "XLMETH", "XLMUSDT" };

            var pairs = exchangeBuilder.GetMarketsForACoin(symbol);

            Assert.NotNull(pairs);

            List<string> pairList = pairs.OrderBy(p => p).ToList();

            Assert.Equal(expected.Count(), pairList.Count());
            Assert.Equal(expected[0], pairList[0]);
        }

        [Fact]
        public void GetExchangCoinTest()
        {
            var balance = testObjs.GetExchangeHubBalances().Where(b => b.Symbol.Equals("BTC")).FirstOrDefault();
            var expected = new ExchangeCoin
            {
                Quantity = 0.025M,
                Symbol = "BTC",
                Exchange = Cryptobitfolio.Business.Entities.Exchange.Binance,
                CoinBuyList = new List<CoinBuy>(),
                Id = 0,
                OpenOrderList = new List<ExchangeOrder>()
            };
            var exchangeCoin = exchangeBuilder.GetExchangeCoin(balance);

            Assert.NotNull(exchangeCoin);
            Assert.Equal(expected.Exchange, exchangeCoin.Exchange);
            Assert.Equal(expected.Quantity, exchangeCoin.Quantity);
            Assert.Equal(expected.Symbol, exchangeCoin.Symbol);
        }

        [Fact]
        public void GetExchangeOrdersTest()
        {
            var pairs = new List<string> { "BTCUSDT" };

            var orders = exchangeBuilder.GetExchangeOrders(pairs).Result;
            List<ExchangeHub.Contracts.OrderResponse> ordersList = orders.ToList();

            Assert.NotNull(orders);
            Assert.Equal(2, ordersList.Count());
            Assert.Equal(pairs[0], ordersList[0].Pair);
        }

        [Fact]
        public void GetCoinBuyTest()
        {
            var orderResponse = testObjs.GetExchangeHubOrders().Where(o => o.Pair.Equals("BTCUSDT")).FirstOrDefault();
            var expected = new CoinBuy
            {
                ExchangeName = Cryptobitfolio.Business.Entities.Exchange.Binance,
                Id = orderResponse.OrderId,
                Pair = orderResponse.Pair,
                Price = orderResponse.Price,
                Quantity = orderResponse.FilledQuantity,
                TransactionDate = orderResponse.TransactTime
            };

            var coinBuy = exchangeBuilder.GetCoinBuy(orderResponse);

            Assert.Equal(expected.Id, coinBuy.Id);
            Assert.Equal(expected.Pair, coinBuy.Pair);
            Assert.Equal(expected.Price, coinBuy.Price);
            Assert.Equal(expected.TransactionDate, coinBuy.TransactionDate);
        }

        [Fact]
        public void GetRelevantBuysTest()
        {
            var symbol = "BTC";
            var quantity = 0.01M;
            var buys = exchangeBuilder.GetRelevantBuys(symbol, quantity);

            Assert.NotNull(buys);
        }

        

        [Fact]
        public void GetCoinsTest_NoCoins()
        {
            var coinList = exchangeBuilder.GetCoins();

            Assert.NotEmpty(coinList);
        }
    }
}
