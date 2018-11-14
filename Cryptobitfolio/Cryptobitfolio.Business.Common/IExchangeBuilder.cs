using Cryptobitfolio.Business.Contracts.Portfolio;
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

        void BuildCoins();
        
        void BuildOrders();

        IEnumerable<ExchangeApi> GetExchangeApis();

        Task<ExchangeApi> SaveExchangeApi(ExchangeApi exchangeApi);

        Task<bool> DeleteExchangeApi(ExchangeApi exchangeApi);

        List<Coin> GetCoins();

        List<ExchangeOrder> GetOpenOrders();
    }
}
