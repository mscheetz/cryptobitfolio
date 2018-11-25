using Cryptobitfolio.Business.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cryptobitfolio.Business
{
    public class ExchangeViewModel
    {
        private IExchangeBuilder _exchangeBuilder;

        public ExchangeViewModel(IExchangeBuilder exchangeBldr)
        {
            _exchangeBuilder = exchangeBldr;
        }

        
    }
}
