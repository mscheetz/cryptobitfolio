using Cryptobitfolio.Business.Contracts.Portfolio;
using Cryptobitfolio.Tests.Common;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Business.Tests
{
    public class CoinBuilderTests : IDisposable
    {
        private TestObjects testObjs;

        public CoinBuilderTests()
        {
            testObjs = new TestObjects();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        [Fact]
        public void AddTest()
        {
            var coin = new Coin();
        }
    }
}
