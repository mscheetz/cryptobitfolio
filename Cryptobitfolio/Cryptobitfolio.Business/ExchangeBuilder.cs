using System;
using System.Collections.Generic;
using System.Linq;
using Cryptobitfolio.Data.Interfaces;
using Cryptobitfolio.Business.Contracts.Portfolio;
using Cryptobitfolio.Business.Entities;
using Cryptobitfolio.Business.Contracts.Trade;
using Cryptobitfolio.Business.Common;
using System.Threading.Tasks;

namespace Cryptobitfolio.Business
{
    public class ExchangeBuilder : IExchangeBuilder
    {
        private readonly IExchangeApiRepository _exchangeRepo;
        private ExchangeHub.ExchangeHub currentHub = null;
        private DateTime lastUpdated;
        private List<ExchangeHub.ExchangeHub> exchangeHubs;
        private List<string> currentHubMarkets;
        private HashSet<string> coinSet;
        private List<Coin> coinList;
        private List<ExchangeCoin> exchangeCoinList;
        private List<ExchangeOrder> orderList;
        private List<ExchangeTransaction> transactionList;

        public ExchangeBuilder(IExchangeApiRepository exhangeApiRepo, DateTime? updated)
        {
            _exchangeRepo = exhangeApiRepo;
            var exchangeApis = _exchangeRepo.Get().Result;
            exchangeHubs = new List<ExchangeHub.ExchangeHub>();
            coinSet = new HashSet<string>();
            exchangeCoinList = new List<ExchangeCoin>();
            
            lastUpdated = updated == null ? DateTime.UtcNow.AddYears(-2) : (DateTime)updated;

            foreach(var api in exchangeApis)
            {
                if (api.ExchangeName == Exchange.Binance)
                {
                    exchangeHubs.Add(new ExchangeHub.ExchangeHub(ExchangeHub.Contracts.Exchange.Binance, api.ApiKey, api.ApiSecret));
                }
                if (api.ExchangeName == Exchange.Bittrex)
                {
                    exchangeHubs.Add(new ExchangeHub.ExchangeHub(ExchangeHub.Contracts.Exchange.Bittrex, api.ApiKey, api.ApiSecret));
                }
                if (api.ExchangeName == Exchange.KuCoin)
                {
                    exchangeHubs.Add(new ExchangeHub.ExchangeHub(ExchangeHub.Contracts.Exchange.KuCoin, api.ApiKey, api.ApiSecret));
                }
            }
        }

        public void LoadExchange(ExchangeApi exchangeApi)
        {
            if (exchangeApi.ExchangeName == Exchange.CoinbasePro)
            {
                currentHub = new ExchangeHub.ExchangeHub((ExchangeHub.Contracts.Exchange)exchangeApi.ExchangeName, exchangeApi.ApiKey, exchangeApi.ApiSecret, exchangeApi.ApiExtra);
            }
            else if (exchangeApi.ExchangeName == Exchange.Switcheo)
            {
                currentHub = new ExchangeHub.ExchangeHub((ExchangeHub.Contracts.Exchange)exchangeApi.ExchangeName, exchangeApi.WIF);
            }
            else
            {
                currentHub = new ExchangeHub.ExchangeHub((ExchangeHub.Contracts.Exchange)exchangeApi.ExchangeName, exchangeApi.ApiKey, exchangeApi.ApiSecret);
            }
            exchangeHubs.Add(currentHub);

            BuildCoins();
            BuildOrders();
        }

        public DateTime UpdatePortfolio()
        {


            this.lastUpdated = DateTime.UtcNow;

            return lastUpdated;
        }

        public void BuildCoins()
        {
            coinList = new List<Coin>();
            foreach(var hub in exchangeHubs)
            {
                currentHub = hub;
                currentHub.SetMarketsAsync();
                currentHubMarkets = currentHub.GetMarkets().ToList();
                coinList.AddRange(GetExchangeCoins());
            }
        }

        public void BuildOrders()
        {
            orderList = new List<ExchangeOrder>();
            foreach (var hub in exchangeHubs)
            {
                currentHub = hub;
                currentHub.SetMarketsAsync();
                currentHubMarkets = currentHub.GetMarkets().ToList();
            }
        }

        public IEnumerable<ExchangeApi> GetExchangeApis()
        {
            var entities = _exchangeRepo.Get().Result;
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
                        ? await _exchangeRepo.Add(entity) 
                        : await _exchangeRepo.Update(entity);

            exchangeApi.Id = entity.Id;

            return exchangeApi;
        }

        public async Task<bool> DeleteExchangeApi(ExchangeApi exchangeApi)
        {
            var entity = ExchangeApiContractToEntity(exchangeApi);

            try
            {
                await _exchangeRepo.Delete(entity);
                return true;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<Coin> GetCoins()
        {
            return coinList;
        }

        public List<ExchangeOrder> GetOpenOrders()
        {
            return orderList;
        }
        
        private List<Coin> GetExchangeCoins()
        {
            var currencyList = new List<Currency>();
            var coinList = new List<Coin>();

            var balances = currentHub.GetBalanceAsync().Result;

            foreach(var bal in balances)
            {
                var currency = currencyList.Where(c => c.Symbol.Equals(bal.Symbol)).FirstOrDefault();
                var coin = GetExchangeCoin(bal, currentHub.GetExchange());
                coin.CoinBuyList = GetRelevantBuys(coin.Symbol, coin.Quantity);
                coin.OpenOrderList = GetOpenOrdersForASymbol(coin.Symbol);

                exchangeCoinList.Add(coin);
                coinSet.Add(bal.Symbol);
            }

            return coinList;
        }

        private async Task<IEnumerable<string>> GetMarketsForACoin(string symbol)
        {
            var pairs = await currentHub.GetMarketsAsync();

            pairs = pairs.Where(p => p.Contains(symbol));

            return pairs;
        }

        private async Task<IEnumerable<ExchangeHub.Contracts.OrderResponse>> GetExchangeOrders(IEnumerable<string> pairs)
        {
            var orders = new List<ExchangeHub.Contracts.OrderResponse>();

            foreach (var pair in pairs)
            {
                var pairOrders = await currentHub.GetOrdersAsync(pair);

                orders.AddRange(pairOrders);
            }

            return orders;
        }

        private async Task<IEnumerable<ExchangeHub.Contracts.OrderResponse>> GetExchangeOpenOrdersByPairs(IEnumerable<string> pairs)
        {
            var orders = new List<ExchangeHub.Contracts.OrderResponse>();

            foreach (var pair in pairs)
            {
                var pairOrders = await currentHub.GetOpenOrdersAsync(pair);

                orders.AddRange(pairOrders);
            }

            return orders;
        }

        private List<CoinBuy> GetRelevantBuys(string symbol, decimal quantity)
        {
            var pairs = GetMarketsForACoin(symbol).Result;
            var orders = GetExchangeOrders(pairs).Result.OrderByDescending(o => o.TransactTime);
            var coinBuyList = new List<CoinBuy>();

            foreach (var order in orders)
            {
                var coinBuy = GetCoinBuy(order, currentHub.GetExchange());
                coinBuyList.Add(coinBuy);

                quantity -= coinBuy.Quantity;

                if (quantity <= 0)
                {
                    break;
                }
            }

            return coinBuyList;
        }

        private List<ExchangeOrder> GetExchangeOpenOrders()
        {
            var orderList = new List<ExchangeOrder>();
            var exchange = currentHub.GetExchange();
            var exchangeCoins = coinList.Where(c => c.ExchangeCoinList.Any(e => e.Exchange == (Exchange)exchange));

            foreach (var coin in exchangeCoins)
            {
                orderList.AddRange(GetOpenOrdersForASymbol(coin.Currency.Symbol));
            }

            return orderList;
        }
        
        private List<ExchangeOrder> GetOpenOrdersForASymbol(string symbol)
        {
            var pairs = GetMarketsForACoin(symbol).Result;
            var orders = GetExchangeOpenOrdersByPairs(pairs).Result;
            var exchangeOrderList = new List<ExchangeOrder>();

            foreach (var order in orders)
            {
                var exchaneOrder = GetExchangeOrder(order, currentHub.GetExchange());
                exchangeOrderList.Add(exchaneOrder);
            }

            return exchangeOrderList;
        }


        #region Converters

        private ExchangeCoin GetExchangeCoin(ExchangeHub.Contracts.Balance balance, ExchangeHub.Contracts.Exchange exchange)
        {
            var exchangeCoin = new ExchangeCoin
            {
                Quantity = balance.Available + balance.Frozen,
                Symbol = balance.Symbol,
                Exchange = (Exchange)exchange
            };

            return exchangeCoin;
        }

        private CoinBuy GetCoinBuy(ExchangeHub.Contracts.OrderResponse orderResponse, ExchangeHub.Contracts.Exchange exchange)
        {
            var coinBuy = new CoinBuy
            {
                ExchangeName = (Exchange)exchange,
                Id = orderResponse.OrderId,
                Pair = orderResponse.Pair,
                Price = orderResponse.Price,
                Quantity = orderResponse.FilledQuantity,
                TransactionDate = orderResponse.TransactTime
            };

            return coinBuy;
        }

        private ExchangeOrder GetExchangeOrder(ExchangeHub.Contracts.OrderResponse orderResponse, ExchangeHub.Contracts.Exchange exchange)
        {
            var exchangeOrder = new ExchangeOrder
            {
                Exchange = (Exchange)exchange,
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
                ExchangeName = entity.ExchangeName,
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
                ExchangeName = contract.ExchangeName,
                Id = contract.Id
            };

            return entity;
        }

        #endregion ExchangeApi converters
    }
}