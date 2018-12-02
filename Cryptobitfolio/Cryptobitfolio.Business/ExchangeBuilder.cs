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

namespace Cryptobitfolio.Business
{
    public class ExchangeBuilder : IExchangeBuilder
    {
        private readonly IExchangeApiRepository _exchangeApiRepo;
        private readonly IExchangeUpdateRepository _exchangeUpdateRepo;
        private readonly IArbitragePathRepository _arbitragePathRepo;
        private readonly IArbitrageBuilder _arbitrageBldr;
        private readonly ICMCBuilder _cmcBldr;
        //private ExchangeHub.ExchangeHub currentHub = null;
        private IExchangeHubRepository currentHub = null;
        private string currentExchange = string.Empty;
        private DateTime lastUpdated;
        private List<Entities.Trade.ExchangeApi> _exchangeApis;
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
        /// <param name="exhangeApiRepo">Exchange Api repository</param>
        /// <param name="exchangeUpdateRepo">Exchange Update repository</param>
        /// <param name="arbitragePathRepo">Arbitrage Path repository</param>
        /// <param name="arbitrageBuilder">Arbitrage builder</param>
        /// <param name="cmcBuilder">CMC builder</param>
        public ExchangeBuilder(IExchangeApiRepository exhangeApiRepo, 
                               IExchangeUpdateRepository exchangeUpdateRepo,
                               IArbitragePathRepository arbitragePathRepo,
                               IArbitrageBuilder arbitrageBuilder,
                               ICMCBuilder cmcBuilder)
        {
            _exchangeApiRepo = exhangeApiRepo;
            _exchangeUpdateRepo = exchangeUpdateRepo;
            _arbitragePathRepo = arbitragePathRepo;
            _arbitrageBldr = arbitrageBuilder;
            _cmcBldr = cmcBuilder;
            LoadBuilder();
        }

        /// <summary>
        /// Constructor for unit tests
        /// </summary>
        /// <param name="exhangeApiRepo">Exchange Api repository</param>
        /// <param name="exchangeUpdateRepo">Exchange Update repository</param>
        /// <param name="arbitragePathRepo">Arbitrage Path repository</param>
        /// <param name="arbitrageBuilder">Arbitrage builder</param>
        /// <param name="exchangeHubRepo">Exchange Hub repository</param>
        /// <param name="cmcBuilder">CMC builder</param>
        public ExchangeBuilder(IExchangeApiRepository exhangeApiRepo,
                               IExchangeUpdateRepository exchangeUpdateRepo,
                               IArbitragePathRepository arbitragePathRepo,
                               IArbitrageBuilder arbitrageBuilder,
                               IExchangeHubRepository exchangeHubRepo,
                               ICMCBuilder cmcBuilder)
        {
            _exchangeApiRepo = exhangeApiRepo;
            _exchangeUpdateRepo = exchangeUpdateRepo;
            _arbitragePathRepo = arbitragePathRepo;
            _arbitrageBldr = arbitrageBuilder;
            currentHub = exchangeHubRepo;
            _cmcBldr = cmcBuilder;
            currentExchange = "Binance";
            LoadBuilder(true);
        }

        public void LoadBuilder(bool test = false)
        {
            _exchangeApis = _exchangeApiRepo.Get().Result;
            //exchangeHubs = new List<ExchangeHub.ExchangeHub>();
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
            if (exchangeApi.ExchangeName == Exchange.CoinbasePro)
            {
                loadedHub = new ExchangeHubRepository((ExchangeHub.Contracts.Exchange)exchangeApi.ExchangeName, exchangeApi.ApiKey, exchangeApi.ApiSecret, exchangeApi.ApiExtra);
            }
            else if (exchangeApi.ExchangeName == Exchange.Switcheo)
            {
                loadedHub = new ExchangeHubRepository((ExchangeHub.Contracts.Exchange)exchangeApi.ExchangeName, exchangeApi.WIF);
            }
            else
            {
                loadedHub = new ExchangeHubRepository((ExchangeHub.Contracts.Exchange)exchangeApi.ExchangeName, exchangeApi.ApiKey, exchangeApi.ApiSecret);
            }
            exchangeHubs.Add(currentHub);

            OnBuildExchangeCoins(loadedHub);
            OnBuildOrders(loadedHub);
        }

        public DateTime UpdatePortfolio()
        {
            BuildCoinsFromExchanges();
            BuildOrders();
            this.lastUpdated = DateTime.UtcNow;

            var exchanges = _exchangeApis.Select(e => e.Exchange).ToList();
            foreach(var exchange in exchanges)
            {
                var exchg = new Entities.Trade.ExchangeUpdate { Exchange = exchange, UpdateAt = lastUpdated };
                _exchangeUpdateRepo.Add(exchg);
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
        public void BuildCoinsFromExchanges()
        {
            if(coinList == null || coinList.Count == 0)
                coinList = new List<Coin>();

            var exchangeCoins = new List<ExchangeCoin>();

            foreach(var hub in exchangeHubs)
            {
                exchangeCoins.AddRange(OnBuildExchangeCoins(hub));
            }

            CreateCoins(exchangeCoins);
        }

        /// <summary>
        /// Build Coins
        /// </summary>
        /// <param name="hub">ExchangeHub for current exchange</param>
        /// <returns>Collection of ExchangeCoins</returns>
        private IEnumerable<ExchangeCoin> OnBuildExchangeCoins(IExchangeHubRepository hub)
        {
            currentHub = hub;
            currentHubMarkets = currentHub.GetMarkets().Result.ToList();
            return GetExchangeCoins();
        }

        /// <summary>
        /// Create Coins for portfolio
        /// </summary>
        /// <param name="exchangeCoins">Exchange coins to add to Coins</param>
        /// <returns>Collection of Coins</returns>
        public IEnumerable<Coin> CreateCoins(IEnumerable<ExchangeCoin> exchangeCoins)
        {
            var currencies = GetCurrencies(coinSet.ToList());

            foreach(var symbol in coinSet)
            {
                var coin = new Coin
                {
                    Currency = currencies.Where(c => c.Symbol.Equals(symbol)).FirstOrDefault(),
                    ExchangeCoinList = exchangeCoins.Where(e => e.Symbol.Equals(symbol)).ToList(),
                };

                coinList.Add(coin);
            }

            return coinList;
        }

        public void BuildOrders()
        {
            orderList = new List<ExchangeOrder>();
            foreach (var hub in exchangeHubs)
            {
                OnBuildOrders(hub);
            }
        }

        private void OnBuildOrders(IExchangeHubRepository hub)
        {
            currentHub = hub;
            //currentHub.SetMarketsAsync();
            currentHubMarkets = currentHub.GetMarkets().Result.ToList();

        }

        public async Task<IEnumerable<ExchangeApi>> GetExchangeApis()
        {
            var entities = await _exchangeApiRepo.Get();
            var contractList = new List<ExchangeApi>();

            foreach(var entity in entities)
            {
                var contract = ExchangeApiEntityToContract(entity);

                contractList.Add(contract);
            }

            return contractList;
        }

        public async Task<ExchangeApi> SaveExchangeApi(ExchangeApi exchangeApi)
        {
            var entity = ExchangeApiContractToEntity(exchangeApi);

            entity = entity.Id == 0 
                        ? await _exchangeApiRepo.Add(entity) 
                        : await _exchangeApiRepo.Update(entity);

            exchangeApi.Id = entity.Id;

            return exchangeApi;
        }

        public async Task<bool> DeleteExchangeApi(ExchangeApi exchangeApi)
        {
            var entity = ExchangeApiContractToEntity(exchangeApi);

            try
            {
                await _exchangeApiRepo.Delete(entity);
                return true;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<Coin> GetCoins()
        {
            if(coinList == null || coinList.Count == 0)
            {
                BuildCoinsFromExchanges();
            }
            return coinList;
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
        public IEnumerable<ExchangeCoin> GetExchangeCoins()
        {
            var coinList = new List<Coin>();
            var exCoinList = new List<ExchangeCoin>();

            var balances = currentHub.GetBalances().Result.Where(b => b.Available + b.Frozen > 0);

            var currencyList = GetCurrencies(balances.Select(b => b.Symbol).ToList());

            foreach(var bal in balances)
            {
                var currency = currencyList.Where(c => c.Symbol.Equals(bal.Symbol)).FirstOrDefault();
                var coin = GetExchangeCoin(bal);
                coin.CoinBuyList = GetRelevantBuys(coin.Symbol, coin.Quantity);
                coin.OpenOrderList = GetOpenOrdersForASymbol(coin.Symbol);

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
        public List<Currency> GetCurrencies(List<string> symbols)
        {
            var currencies = _cmcBldr.GetCurrencies(symbols).Result;

            return currencies.ToList();
        }

        /// <summary>
        /// Get buys for a given coin that fulfill the a given quantity
        /// </summary>
        /// <param name="symbol">Symbol of coin</param>
        /// <param name="quantity">Quantity of coin</param>
        /// <returns>Collection of CoinBuy objects</returns>
        public List<CoinBuy> GetRelevantBuys(string symbol, decimal quantity)
        {
            var pairs = GetMarketsForACoin(symbol);
            var orders = GetExchangeOrders(pairs).Result.OrderByDescending(o => o.TransactTime);
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
        public IEnumerable<string> GetMarketsForACoin(string symbol)
        {
            var pairs = GetMarkets().Result;

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

        private List<ExchangeOrder> GetExchangeOpenOrders()
        {
            var orderList = new List<ExchangeOrder>();
            var exchange = currentHub.GetExchange();
            var exchangeCoins = coinList.Where(c => c.ExchangeCoinList.Any(e => e.Exchange == StringToExchange(exchange)));

            foreach (var coin in exchangeCoins)
            {
                orderList.AddRange(GetOpenOrdersForASymbol(coin.Currency.Symbol));
            }

            return orderList;
        }
        
        /// <summary>
        /// Get Open orders for a currency symbol
        /// </summary>
        /// <param name="symbol">String of symbol</param>
        /// <returns>Collection of ExchangeOrders</returns>
        public List<ExchangeOrder> GetOpenOrdersForASymbol(string symbol)
        {
            var pairs = GetMarketsForACoin(symbol);
            var orders = GetExchangeOpenOrdersByPairs(pairs).Result;
            var exchangeOrderList = new List<ExchangeOrder>();

            foreach (var order in orders)
            {
                var exchaneOrder = GetExchangeOrder(order);
                exchangeOrderList.Add(exchaneOrder);
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
        public ExchangeCoin GetExchangeCoin(ExchangeHub.Contracts.Balance balance)
        {
            var exchangeCoin = new ExchangeCoin
            {
                Quantity = balance.Available + balance.Frozen,
                Symbol = balance.Symbol,
                Exchange = StringToExchange(currentExchange),
                CoinBuyList = new List<CoinBuy>(),
                Id = 0,
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
                ExchangeName = StringToExchange(currentExchange),
                Id = orderResponse.OrderId,
                Pair = orderResponse.Pair,
                Price = orderResponse.Price,
                Quantity = quantityApplied,
                TransactionDate = orderResponse.TransactTime
            };

            return coinBuy;
        }

        public ExchangeOrder GetExchangeOrder(ExchangeHub.Contracts.OrderResponse orderResponse)
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

        private ExchangeApi ExchangeApiEntityToContract(Business.Entities.Trade.ExchangeApi entity)
        {
            var contract = new ExchangeApi
            {
                ApiExtra = entity.ApiExtra,
                ApiKey = entity.ApiKey,
                ApiKeyName = entity.ApiKeyName,
                ApiSecret = entity.ApiSecret,
                ExchangeName = entity.Exchange,
                Id = entity.Id
            };

            return contract;
        }

        private Business.Entities.Trade.ExchangeApi ExchangeApiContractToEntity(ExchangeApi contract)
        {
            var entity = new Business.Entities.Trade.ExchangeApi
            {
                ApiExtra = contract.ApiExtra,
                ApiKey = contract.ApiKey,
                ApiKeyName = contract.ApiKeyName,
                ApiSecret = contract.ApiSecret,
                Exchange = contract.ExchangeName,
                Id = contract.Id
            };

            return entity;
        }

        #endregion ExchangeApi converters
    }
}