﻿using Cryptobitfolio.Business.Contracts.Portfolio;
using Cryptobitfolio.Business.Contracts.Trade;
using Cryptobitfolio.Business.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Cryptobitfolio.Business.Common
{
    public interface IExchangeBuilder
    {
        void LoadExchange(ExchangeApi exchangeApi);

        DateTime UpdatePortfolio();

        void BuildCoins();
        
        void BuildOrders();

        Task<IEnumerable<ExchangeApi>> GetExchangeApis();

        Task<ExchangeApi> SaveExchangeApi(ExchangeApi exchangeApi);

        Task<bool> DeleteExchangeApi(ExchangeApi exchangeApi);

        List<Coin> GetCoins();

        List<ExchangeOrder> GetOpenOrders();

        IEnumerable<ArbitrageLoop> GetInternalArbitrages(string symbol, decimal quantity, Exchange exchange);
    }
}
