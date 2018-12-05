// -----------------------------------------------------------------------------
// <copyright file="ArbitrageBuilder" company="Matt Scheetz">
//     Copyright (c) Matt Scheetz All Rights Reserved
// </copyright>
// <author name="Matt Scheetz" date="11/26/2018 11:33:59 AM" />
// -----------------------------------------------------------------------------

namespace Cryptobitfolio.Business
{
    using Cryptobitfolio.Business.Common;
    using Cryptobitfolio.Business.Contracts.Portfolio;
    using Cryptobitfolio.Business.Entities;
    using Cryptobitfolio.Data.Interfaces;
    using Cryptobitfolio.Data.Interfaces.Database;
    #region Usings

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    #endregion Usings

    public class ArbitrageBuilder : IArbitrageBuilder
    {

        private readonly IArbitragePathRepository _arbitragePathRepo;

        public ArbitrageBuilder(IArbitragePathRepository arbitragePathRepository)
        {
            this._arbitragePathRepo = arbitragePathRepository;
        }

        /// <summary>
        /// Get internal arbitrage values for a currency
        /// </summary>
        /// <param name="symbol">Symbol of a currency</param>
        /// <param name="quantity">Starting quantity</param>
        /// <param name="hub">Current exchange hub</param>
        /// <returns>Collection of ArbitrageLoop objects</returns>
        public IEnumerable<ArbitrageLoop> GetInternalArbitrage(string symbol, 
                                                               decimal quantity, 
                                                               IExchangeHubRepository hub)
        {
            var paths = GetPaths(symbol, quantity);
            var pathPairs = GetUniquePairs(paths);

            var arbitragePairs = GetPricePairs(hub, pathPairs);

            for (var i = 0; i < paths.Count(); i++)
            {
                paths[i].FinalQuantity = ArbitrageCalculator(arbitragePairs, paths[i].Path, paths[i].StartingQuantity);
            }

            return paths;
        }

        /// <summary>
        /// Get all paths for arbitrageing
        /// </summary>
        /// <param name="symbol">Symbol of currency</param>
        /// <param name="quantity">Starting quantity</param>
        /// <returns>Collection of ArbitrageLoop objects</returns>
        private List<ArbitrageLoop> GetPaths(string symbol, decimal quantity)
        {
            var path1 = new string[] { $"{symbol}BTC", $"BTCUSDT", $"{symbol}USDT" };
            var path2 = new string[] { $"{symbol}USDT", $"BTCUSDT", $"{symbol}BTC" };
            var path3 = new string[] { $"{symbol}ETH", $"ETHUSDT", $"{symbol}USDT" };
            var path4 = new string[] { $"{symbol}USDT", $"ETHUSDT", $"{symbol}ETH" };
            var path5 = new string[] { $"{symbol}BTC", $"BTCUSDT", $"ETHUSDT", $"{symbol}ETH" };
            var path6 = new string[] { $"{symbol}ETH", $"ETHUSDT", $"BTCUSDT", $"{symbol}BTC" };
            var paths = new List<ArbitrageLoop>();
            paths.Add(new ArbitrageLoop(path1, quantity));
            paths.Add(new ArbitrageLoop(path2, quantity));
            paths.Add(new ArbitrageLoop(path3, quantity));
            paths.Add(new ArbitrageLoop(path4, quantity));
            paths.Add(new ArbitrageLoop(path5, quantity));
            paths.Add(new ArbitrageLoop(path6, quantity));
            var arbitragePaths = _arbitragePathRepo.Get().Result.ToList();
            for (var i = 0; i < arbitragePaths.Count; i++)
            {
                paths.Add(new ArbitrageLoop(arbitragePaths[i].Path.Split(','), quantity));
            }

            return paths;
        }

        /// <summary>
        /// Get unique list of trading pairs
        /// </summary>
        /// <param name="arbitrages">Collection of ArbitrageLoops to pull pairs from</param>
        /// <returns>Array of trading pairs</returns>
        private string[] GetUniquePairs(List<ArbitrageLoop> arbitrages)
        {
            var paths = new List<string>();

            for (var i = 0; i < arbitrages.Count; i++)
            {
                paths = paths.Union(arbitrages[i].Path).ToList();
            }

            return paths.ToArray();
        }

        /// <summary>
        /// Get Prices for trading pairs
        /// </summary>
        /// <param name="hub">Current exchange hub</param>
        /// <param name="pairs">Unique array of trading pairs</param>
        /// <returns>Dictionary of trading pair and current price</returns>
        private Dictionary<string, decimal> GetPricePairs(IExchangeHubRepository hub, string[] pairs)
        {
            var tickers = hub.GetLatestPrices().Result;
            var arbitragePairs = new Dictionary<string, decimal>();
            for (var i = 0; i < pairs.Length; i++)
            {
                var price = tickers.Where(t => t.Pair.Equals(pairs[i])).Select(t => t.Price).FirstOrDefault();
                arbitragePairs.Add(pairs[i], price);
            }

            return arbitragePairs;
        }

        /// <summary>
        /// Calulate final quantity for an arbitrage path
        /// </summary>
        /// <param name="pricePairs">Dictionary of price pairs</param>
        /// <param name="path">Arbitrage path</param>
        /// <param name="quantity">Starting quantity</param>
        /// <returns>Decimal value of final quantity</returns>
        private decimal ArbitrageCalculator(Dictionary<string, decimal> pricePairs, string[] path, decimal quantity)
        {
            var currentQuantity = quantity;

            for (var i = 0; i < path.Length; i++)
            {
                var pairPrice = pricePairs[path[i]];
                currentQuantity = currentQuantity * pairPrice;
            }

            return currentQuantity;
        }
    }
}