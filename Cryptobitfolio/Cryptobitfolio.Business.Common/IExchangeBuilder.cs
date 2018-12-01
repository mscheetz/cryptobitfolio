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

        DateTime UpdatePortfolio();

        /// <summary>
        /// Build Coins in portfolio from exchange data
        /// </summary>
        void BuildCoinsFromExchanges();

        /// <summary>
        /// Create Coins for portfolio
        /// </summary>
        /// <param name="exchangeCoins">Exchange coins to add to Coins</param>
        /// <returns>Collection of Coins</returns>
        IEnumerable<Coin> CreateCoins(IEnumerable<ExchangeCoin> exchangeCoins);


        void BuildOrders();

        Task<IEnumerable<ExchangeApi>> GetExchangeApis();

        Task<ExchangeApi> SaveExchangeApi(ExchangeApi exchangeApi);

        Task<bool> DeleteExchangeApi(ExchangeApi exchangeApi);

        List<Coin> GetCoins();

        List<ExchangeOrder> GetOpenOrders();

        /// <summary>
        /// Get all ExchangeCoins for an exchange
        /// </summary>
        /// <returns>Collection of ExchangeCoins</returns>
        IEnumerable<ExchangeCoin> GetExchangeCoins();

        IEnumerable<ArbitrageLoop> GetInternalArbitrages(string symbol, decimal quantity, Exchange exchange);

        /// <summary>
        /// Create an exchange coin from a ExchangeHub Balance object
        /// </summary>
        /// <param name="balance">Balance object to convert</param>
        /// <returns>new ExchangeCoin object</returns>
        ExchangeCoin GetExchangeCoin(ExchangeHub.Contracts.Balance balance);

        /// <summary>
        /// Create a CoinBuy from an ExchangeHub OrderResponse
        /// </summary>
        /// <param name="orderResponse">OrderResponse to convert</param>
        /// <param name="quantityApplied">Quantity to apply to this order</param>
        /// <returns>new CoinBuy object</returns>
        CoinBuy GetCoinBuy(ExchangeHub.Contracts.OrderResponse orderResponse, decimal quantityApplied);

        ExchangeOrder GetExchangeOrder(ExchangeHub.Contracts.OrderResponse orderResponse);

        /// <summary>
        /// Get buys for a given coin that fulfill the a given quantity
        /// </summary>
        /// <param name="symbol">Symbol of coin</param>
        /// <param name="quantity">Quantity of coin</param>
        /// <returns>Collection of CoinBuy objects</returns>
        List<CoinBuy> GetRelevantBuys(string symbol, decimal quantity);

        /// <summary>
        /// Get all markets for a given coin
        /// </summary>
        /// <param name="symbol">Symbol of currency</param>
        /// <returns>Collection of trading pairs</returns>
        IEnumerable<string> GetMarketsForACoin(string symbol);

        /// <summary>
        /// Get all markets for the current hub
        /// </summary>
        /// <returns>Collection of Markets</returns>
        Task<IEnumerable<string>> GetMarkets();

        /// <summary>
        /// Get orders for a collection of trading pairs
        /// </summary>
        /// <param name="pairs">Trading pairs</param>
        /// <returns>Collection of OrderResponses</returns>
        Task<IEnumerable<ExchangeHub.Contracts.OrderResponse>> GetExchangeOrders(IEnumerable<string> pairs);

        /// <summary>
        /// Get Open orders for a currency symbol
        /// </summary>
        /// <param name="symbol">String of symbol</param>
        /// <returns>Collection of ExchangeOrders</returns>
        List<ExchangeOrder> GetOpenOrdersForASymbol(string symbol);
    }
}
