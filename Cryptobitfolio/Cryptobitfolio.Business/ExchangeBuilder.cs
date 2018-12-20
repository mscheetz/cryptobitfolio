using System;
using System.Collections.Generic;
using System.Linq;
using Cryptobitfolio.Data.Interfaces;
using Cryptobitfolio.Business.Contracts.Portfolio;
using Cryptobitfolio.Business.Entities;
using Cryptobitfolio.Business.Contracts.Trade;
using Cryptobitfolio.Business.Common;
using System.Threading.Tasks;
using Cryptobitfolio.Data.Repositories;
using Cryptobitfolio.Data.Interfaces.Database;

namespace Cryptobitfolio.Business
{
    public class ExchangeBuilder : IExchangeBuilder
    {
        private readonly IExchangeApiBuilder _exchangeApiBldr;
        private readonly IExchangeUpdateRepository _exchangeUpdateRepo;
        private readonly IArbitragePathRepository _arbitragePathRepo;
        private readonly IArbitrageBuilder _arbitrageBldr;
        private readonly ICMCBuilder _cmcBldr;
        //private ExchangeHub.ExchangeHub currentHub = null;
        private IExchangeHubRepository currentHub = null;
        private string currentExchange = string.Empty;
        private DateTime lastUpdated;
        private IEnumerable<ExchangeApi> _exchangeApis;
        //private List<ExchangeHub.ExchangeHub> exchangeHubs;
        private List<IExchangeHubRepository> exchangeHubs;
        private List<string> currentHubMarkets;
        private HashSet<string> coinSet;
        private List<Coin> coinList;
        private List<ExchangeCoin> exchangeCoinList;
        private List<ExchangeOrder> orderList;
        private List<ExchangeTransaction> transactionList;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="exchangeApiBldr">Exchange Api Builder</param>
        /// <param name="exchangeUpdateRepo">Exchange Update repository</param>
        /// <param name="arbitragePathRepo">Arbitrage Path repository</param>
        /// <param name="arbitrageBuilder">Arbitrage builder</param>
        /// <param name="cmcBuilder">CMC builder</param>
        public ExchangeBuilder(IExchangeApiBuilder exchangeApiBldr, 
                               IExchangeUpdateRepository exchangeUpdateRepo,
                               IArbitragePathRepository arbitragePathRepo,
                               IArbitrageBuilder arbitrageBuilder,
                               ICMCBuilder cmcBuilder)
        {
            _exchangeApiBldr = exchangeApiBldr;
            _exchangeUpdateRepo = exchangeUpdateRepo;
            _arbitragePathRepo = arbitragePathRepo;
            _arbitrageBldr = arbitrageBuilder;
            _cmcBldr = cmcBuilder;
            LoadBuilder().RunSynchronously();
        }

        /// <summary>
        /// Constructor for unit tests
        /// </summary>
        /// <param name="exchangeApiBldr">Exchange Api Builder</param>
        /// <param name="exchangeUpdateRepo">Exchange Update repository</param>
        /// <param name="arbitragePathRepo">Arbitrage Path repository</param>
        /// <param name="arbitrageBuilder">Arbitrage builder</param>
        /// <param name="exchangeHubRepo">Exchange Hub repository</param>
        /// <param name="cmcBuilder">CMC builder</param>
        public ExchangeBuilder(IExchangeApiBuilder exchangeApiBldr,
                               IExchangeUpdateRepository exchangeUpdateRepo,
                               IArbitragePathRepository arbitragePathRepo,
                               IArbitrageBuilder arbitrageBuilder,
                               IExchangeHubRepository exchangeHubRepo,
                               ICMCBuilder cmcBuilder)
        {
            _exchangeApiBldr = exchangeApiBldr;
            _exchangeUpdateRepo = exchangeUpdateRepo;
            _arbitragePathRepo = arbitragePathRepo;
            _arbitrageBldr = arbitrageBuilder;
            currentHub = exchangeHubRepo;
            _cmcBldr = cmcBuilder;
            currentExchange = "Binance";
            LoadBuilder(true).RunSynchronously();
        }

        /// <summary>
        /// Loads data for this builder
        /// </summary>
        /// <param name="test">Use test data?</param>
        public async Task LoadBuilder(bool test = false)
        {
            _exchangeApis = await _exchangeApiBldr.Get();
            exchangeHubs = new List<IExchangeHubRepository>();
            coinSet = new HashSet<string>();
            exchangeCoinList = new List<ExchangeCoin>();

            if (!test)
            {
                foreach (var api in _exchangeApis)
                {
                    if (api.Exchange == Exchange.Binance)
                    {
                        exchangeHubs.Add(new ExchangeHubRepository(ExchangeHub.Contracts.Exchange.Binance, api.ApiKey, api.ApiSecret));
                    }
                    else if (api.Exchange == Exchange.Bittrex)
                    {
                        exchangeHubs.Add(new ExchangeHubRepository(ExchangeHub.Contracts.Exchange.Bittrex, api.ApiKey, api.ApiSecret));
                    }
                    else if (api.Exchange == Exchange.CoinbasePro)
                    {
                        exchangeHubs.Add(new ExchangeHubRepository(ExchangeHub.Contracts.Exchange.CoinbasePro, api.ApiKey, api.ApiSecret, api.ApiExtra));
                    }
                    else if (api.Exchange == Exchange.KuCoin)
                    {
                        exchangeHubs.Add(new ExchangeHubRepository(ExchangeHub.Contracts.Exchange.KuCoin, api.ApiKey, api.ApiSecret));
                    }
                }
                if(currentHub == null)
                {
                    currentHub = exchangeHubs[0];
                    currentExchange = currentHub.GetExchange();
                }
            }
        }

        public void LoadExchange(ExchangeApi exchangeApi)
        {
            IExchangeHubRepository loadedHub = null;
            if (exchangeApi.Exchange == Exchange.CoinbasePro)
            {
                loadedHub = new ExchangeHubRepository((ExchangeHub.Contracts.Exchange)exchangeApi.Exchange, exchangeApi.ApiKey, exchangeApi.ApiSecret, exchangeApi.ApiExtra);
            }
            else if (exchangeApi.Exchange == Exchange.Switcheo)
            {
                loadedHub = new ExchangeHubRepository((ExchangeHub.Contracts.Exchange)exchangeApi.Exchange, exchangeApi.WIF);
            }
            else
            {
                loadedHub = new ExchangeHubRepository((ExchangeHub.Contracts.Exchange)exchangeApi.Exchange, exchangeApi.ApiKey, exchangeApi.ApiSecret);
            }
            currentHub = loadedHub;
            exchangeHubs.Add(loadedHub);

            var e = OnBuildExchangeCoins(loadedHub);
            var o = OnBuildOrders(loadedHub);
        }

        /// <summary>
        /// Returns a collection of Coins
        /// Or builds a collection of coins if one
        /// does not exist
        /// </summary>
        /// <returns>Collection of Coins</returns>
        public async Task<List<Coin>> GetCoins()
        {
            if (coinList == null || coinList.Count == 0)
            {
                await BuildCoinsFromExchanges();
            }
            return coinList;
        }

        /// <summary>
        /// Updates Portfolio with latest data from exchanges
        /// </summary>
        /// <returns>DateTime of update</returns>
        public async Task<DateTime> UpdatePortfolio()
        {
            await BuildCoinsFromExchanges();
            await BuildOrders();
            this.lastUpdated = DateTime.UtcNow;

            var exchanges = _exchangeApis.Select(e => e.Exchange).ToList();
            foreach(var exchange in exchanges)
            {
                var exchg = new Entities.Trade.ExchangeUpdate { Exchange = exchange, UpdateAt = lastUpdated };
                await _exchangeUpdateRepo.Add(exchg);
            }

            return lastUpdated;
        }

        public IEnumerable<IExchangeHubRepository> GetExchanges()
        {
            return exchangeHubs;
        }

        /// <summary>
        /// Build Coins in portfolio from exchange data
        /// </summary>
        public async Task BuildCoinsFromExchanges()
        {
            if(coinList == null || coinList.Count == 0)
                coinList = new List<Coin>();

            var exchangeCoins = new List<ExchangeCoin>();

            foreach(var hub in exchangeHubs)
            {
                exchangeCoins.AddRange(await OnBuildExchangeCoins(hub));
            }

            await CreateCoins(exchangeCoins);
        }

        /// <summary>
        /// Build Coins
        /// </summary>
        /// <param name="hub">ExchangeHub for current exchange</param>
        /// <returns>Collection of ExchangeCoins</returns>
        private async Task<IEnumerable<ExchangeCoin>> OnBuildExchangeCoins(IExchangeHubRepository hub)
        {
            currentHub = hub;
            var markets = await currentHub.GetMarkets();
            currentHubMarkets = markets.ToList();

            return await GetExchangeCoins();
        }

        /// <summary>
        /// Create Coins for portfolio
        /// </summary>
        /// <param name="exchangeCoins">Exchange coins to add to Coins</param>
        /// <returns>Collection of Coins</returns>
        public async Task<IEnumerable<Coin>> CreateCoins(IEnumerable<ExchangeCoin> exchangeCoins)
        {
            var currencies = await GetCurrencies(coinSet.ToList());

            foreach(var symbol in coinSet)
            {
                var coin = new Coin(currencies.Where(c => c.Symbol.Equals(symbol)).FirstOrDefault());

                coin.ExchangeCoinList = exchangeCoins.Where(e => e.Symbol.Equals(symbol)).ToList();

                coinList.Add(coin);
            }

            return coinList;
        }

        public async Task BuildOrders()
        {
            orderList = new List<ExchangeOrder>();
            foreach (var hub in exchangeHubs)
            {
                await OnBuildOrders(hub);
            }
        }

        private async Task OnBuildOrders(IExchangeHubRepository hub)
        {
            currentHub = hub;

            var markets = await currentHub.GetMarkets();

            currentHubMarkets = markets.ToList();

        }

        public List<ExchangeOrder> GetOpenOrders()
        {
            return orderList;
        }

        public IEnumerable<ArbitrageLoop> GetInternalArbitrages(string symbol, decimal quantity, Exchange exchange)
        {
            currentHub = exchangeHubs.Where(e => e.GetExchange().Equals(exchange.ToString())).FirstOrDefault();
            currentExchange = exchange.ToString();
            return _arbitrageBldr.GetInternalArbitrage(symbol, quantity, currentHub);
        }

        /// <summary>
        /// Get all ExchangeCoins for an exchange
        /// </summary>
        /// <returns>Collection of ExchangeCoins</returns>
        public async Task<IEnumerable<ExchangeCoin>> GetExchangeCoins()
        {
            //var coinList = new List<Coin>();
            var exCoinList = new List<ExchangeCoin>();

            var balances = await currentHub.GetBalances();
            
            var nonZeroBalances = balances.Where(b => b.Available + b.Frozen > 0);

            //var currencyList = GetCurrencies(balances.Select(b => b.Symbol).ToList());

            foreach(var bal in nonZeroBalances)
            {
                //var currency = currencyList.Where(c => c.Symbol.Equals(bal.Symbol)).FirstOrDefault();
                var coin = CreateExchangeCoin(bal);
                coin.CoinBuyList = await GetRelevantBuys(coin.Symbol, coin.Quantity);
                coin.OpenOrderList = await GetOpenOrdersForASymbol(coin.Symbol);

                exCoinList.Add(coin);
                coinSet.Add(bal.Symbol);
            }

            return exCoinList;
        }

        /// <summary>
        /// Get currencies
        /// </summary>
        /// <param name="symbols">Symbols to find</param>
        /// <returns>Collection of Currencies</returns>
        public async Task<List<Currency>> GetCurrencies(List<string> symbols)
        {
            var currencies = await _cmcBldr.GetCurrencies(symbols);

            return currencies.ToList();
        }

        /// <summary>
        /// Get buys for a given coin that fulfill the a given quantity
        /// </summary>
        /// <param name="symbol">Symbol of coin</param>
        /// <param name="quantity">Quantity of coin</param>
        /// <returns>Collection of CoinBuy objects</returns>
        public async Task<List<CoinBuy>> GetRelevantBuys(string symbol, decimal quantity)
        {
            var pairs = await GetMarketsForACoin(symbol);
            var orders = await GetExchangeOrders(pairs);
            orders = orders.OrderByDescending(o => o.TransactTime);
            var coinBuyList = new List<CoinBuy>();

            foreach (var order in orders)
            {
                var quantityApplied = quantity >= order.FilledQuantity
                    ? order.FilledQuantity
                    : quantity;

                quantity -= order.FilledQuantity;

                var coinBuy = GetCoinBuy(order, quantityApplied);
                coinBuyList.Add(coinBuy);

                if (quantity <= 0)
                {
                    break;
                }
            }

            return coinBuyList;
        }

        /// <summary>
        /// Get all markets for a given coin
        /// </summary>
        /// <param name="symbol">Symbol of currency</param>
        /// <returns>Collection of trading pairs</returns>
        public async Task<IEnumerable<string>> GetMarketsForACoin(string symbol)
        {
            var pairs = await GetMarkets();

            pairs = pairs.Where(p => p.StartsWith(symbol));

            return pairs;
        }

        /// <summary>
        /// Get all markets for the current hub
        /// </summary>
        /// <returns>Collection of Markets</returns>
        public async Task<IEnumerable<string>> GetMarkets()
        {
            return await currentHub.GetMarkets();
        }

        /// <summary>
        /// Get orders for a collection of trading pairs
        /// </summary>
        /// <param name="pairs">Trading pairs</param>
        /// <returns>Collection of OrderResponses</returns>
        public async Task<IEnumerable<ExchangeHub.Contracts.OrderResponse>> GetExchangeOrders(IEnumerable<string> pairs)
        {
            var orders = new List<ExchangeHub.Contracts.OrderResponse>();

            foreach (var pair in pairs)
            {
                var pairOrders = await currentHub.GetOrders(pair);

                orders.AddRange(pairOrders);
            }

            return orders;
        }

        private async Task<IEnumerable<ExchangeHub.Contracts.OrderResponse>> GetExchangeOpenOrdersByPairs(IEnumerable<string> pairs)
        {
            var orders = new List<ExchangeHub.Contracts.OrderResponse>();

            foreach (var pair in pairs)
            {
                var pairOrders = await currentHub.GetOpenOrders(pair);

                orders.AddRange(pairOrders);
            }

            return orders;
        }

        private async Task<List<ExchangeOrder>> GetExchangeOpenOrders()
        {
            var orderList = new List<ExchangeOrder>();
            var exchange = currentHub.GetExchange();
            var exchangeCoins = coinList.Where(c => c.ExchangeCoinList.Any(e => e.Exchange == StringToExchange(exchange)));

            foreach (var coin in exchangeCoins)
            {
                orderList.AddRange(await GetOpenOrdersForASymbol(coin.Symbol));
            }

            return orderList;
        }
        
        /// <summary>
        /// Get Open orders for a currency symbol
        /// </summary>
        /// <param name="symbol">String of symbol</param>
        /// <returns>Collection of ExchangeOrders</returns>
        public async Task<List<ExchangeOrder>> GetOpenOrdersForASymbol(string symbol)
        {
            var pairs = await GetMarketsForACoin(symbol);
            var orders = await GetExchangeOpenOrdersByPairs(pairs);
            var exchangeOrderList = new List<ExchangeOrder>();

            foreach (var order in orders)
            {
                var exchangeOrder = OrderResponseToExchangeOrder(order);
                exchangeOrderList.Add(exchangeOrder);
            }

            return exchangeOrderList;
        }


        #region Converters

        private Exchange StringToExchange(string exchangeName)
        {
            Exchange exchange;
            Enum.TryParse(exchangeName, out exchange);

            return exchange;
        }

        private ExchangeHub.Contracts.Exchange ExchangeHubExchangeEnumConverter(Exchange exchange)
        {
            ExchangeHub.Contracts.Exchange hubExchange;
            Enum.TryParse(exchange.ToString(), out hubExchange);

            return hubExchange;
        }

        /// <summary>
        /// Create an exchange coin from a ExchangeHub Balance object
        /// </summary>
        /// <param name="balance">Balance object to convert</param>
        /// <returns>new ExchangeCoin object</returns>
        public ExchangeCoin CreateExchangeCoin(ExchangeHub.Contracts.Balance balance)
        {
            var exchangeCoin = new ExchangeCoin
            {
                Quantity = balance.Available + balance.Frozen,
                Symbol = balance.Symbol,
                Exchange = StringToExchange(currentExchange),
                CoinBuyList = new List<CoinBuy>(),
                ExchangeCoinId = 0,
                OpenOrderList = new List<ExchangeOrder>()
            };

            return exchangeCoin;
        }

        /// <summary>
        /// Create a CoinBuy from an ExchangeHub OrderResponse
        /// </summary>
        /// <param name="orderResponse">OrderResponse to convert</param>
        /// <param name="quantityApplied">Quantity to apply to this order</param>
        /// <returns>new CoinBuy object</returns>
        public CoinBuy GetCoinBuy(ExchangeHub.Contracts.OrderResponse orderResponse, decimal quantityApplied)
        {
            var coinBuy = new CoinBuy
            {
                BTCPrice = orderResponse.Pair.EndsWith("BTC") ? orderResponse.Price : 0,
                Exchange = StringToExchange(currentExchange),
                OrderId = orderResponse.OrderId,
                Pair = orderResponse.Pair,
                Price = orderResponse.Price,
                OrderQuantity = quantityApplied,
                TransactionDate = orderResponse.TransactTime
            };

            return coinBuy;
        }

        /// <summary>
        /// Convert an OrderResponse to an ExchangeOrder
        /// </summary>
        /// <param name="orderResponse">OrderResponse to convert</param>
        /// <returns>new ExchangeOrder object</returns>
        public ExchangeOrder OrderResponseToExchangeOrder(ExchangeHub.Contracts.OrderResponse orderResponse)
        {
            var exchangeOrder = new ExchangeOrder
            {
                Exchange = StringToExchange(currentExchange),
                FilledQuantity = orderResponse.FilledQuantity,
                OrderId = orderResponse.OrderId,
                Pair = orderResponse.Pair,
                PlaceDate = orderResponse.TransactTime,
                Price = orderResponse.Price,
                Quantity = orderResponse.OrderQuantity,
                Side = (Side)orderResponse.Side
            };

            return exchangeOrder;
        }

        #endregion Converters

        #region ExchangeApi converters

        private ExchangeApi ExchangeApiEntityToContract(Entities.Trade.ExchangeApi entity)
        {
            var contract = new ExchangeApi
            {
                ApiExtra = entity.ApiExtra,
                ApiKey = entity.ApiKey,
                ApiKeyName = entity.ApiKeyName,
                ApiSecret = entity.ApiSecret,
                Exchange = entity.Exchange,
                ExchangeApiId = entity.Id
            };

            return contract;
        }

        private Entities.Trade.ExchangeApi ExchangeApiContractToEntity(ExchangeApi contract)
        {
            var entity = new Entities.Trade.ExchangeApi
            {
                ApiExtra = contract.ApiExtra,
                ApiKey = contract.ApiKey,
                ApiKeyName = contract.ApiKeyName,
                ApiSecret = contract.ApiSecret,
                Exchange = contract.Exchange,
                Id = contract.ExchangeApiId
            };

            return entity;
        }

        #endregion ExchangeApi converters
    }
}