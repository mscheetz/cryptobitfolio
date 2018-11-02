using System;
using System.Collections.Generic;
using System.Linq;
using Cryptobitfolio.Data.Interfaces;
using Cryptobitfolio.Business.Contracts.Portfolio;
using Binance.NetCore;
using BittrexApi.NetCore;
using Cryptobitfolio.Business.Entities;
using Cryptobitfolio.Business.Contracts.Trade;
using Cryptobitfolio.Business.Common;
using System.Threading.Tasks;

namespace Cryptobitfolio.Business
{
    public class ExchangeBuilder : IExchangeBuilder
    {
        private readonly IExchangeApiRepository _exchangeRepo;
        private BinanceApiClient binance = null;
        private BittrexClient bittrex = null;
        private List<Coin> coinList;
        private List<ExchangeOrder> orderList;
        private List<ExchangeTransaction> transactionList;

        public ExchangeBuilder(IExchangeApiRepository exhangeApiRepo)
        {
            _exchangeRepo = exhangeApiRepo;
            var exchangeApis = _exchangeRepo.Get().Result;
            foreach(var api in exchangeApis)
            {
                if (api.ExchangeName == Exchange.Binance)
                {
                    binance = new BinanceApiClient(api.ApiKey, api.ApiSecret);
                }
                if (api.ExchangeName == Exchange.Bittrex)
                {
                    bittrex = new BittrexClient(api.ApiKey, api.ApiSecret);
                }
            }
            BuildCoins();
            BuildOrders();
            BuildTransactions();
        }

        public void LoadExchange(Exchange exchange)
        {
            var api = _exchangeRepo.Get(exchange.ToString()).Result;
            if (exchange == Exchange.Binance)
            {
                binance = new BinanceApiClient(api.ApiKey, api.ApiSecret);
            }
            if (exchange == Exchange.Bittrex)
            {
                bittrex = new BittrexClient(api.ApiKey, api.ApiSecret);
            }
            BuildCoins();
            BuildOrders();
            BuildTransactions();
        }

        public void BuildCoins()
        {
            coinList = new List<Coin>();
            if(binance != null)
            {
                coinList.AddRange(GetBinanceCoins());
            }
            if (bittrex != null)
            {
                coinList.AddRange(GetBittrexCoins());
            }
        }

        public void BuildTransactions()
        {
            transactionList = new List<ExchangeTransaction>();
            if (binance != null)
            {
                transactionList.AddRange(GetBinanceTransactions());
            }
            if (bittrex != null)
            {
                transactionList.AddRange(GetBittrexTransactions());
            }
        }

        public void BuildOrders()
        {
            orderList = new List<ExchangeOrder>();
            if (binance != null)
            {
                orderList.AddRange(GetBinanceOrders());
            }
            if (bittrex != null)
            {
                orderList.AddRange(GetBittrexOrders());
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

        public List<ExchangeTransaction> GetTransactions()
        {
            return transactionList;
        }

        private List<Coin> GetBinanceCoins()
        {
            var currencyList = new List<Currency>();
            var coinList = new List<Coin>();

            var binanceBalance = binance.GetBalanceAsync().Result;

            foreach(var bCoin in binanceBalance.balances)
            {
                var currency = currencyList.Where(c => c.Symbol.Equals(bCoin.asset)).FirstOrDefault();
                var coin = new Coin
                {
                    Quantity = bCoin.free + bCoin.locked,
                    Currency = currency
                };
                
                coinList.Add(coin);
            }

            return coinList;
        }

        private List<ExchangeTransaction> GetBinanceTransactions()
        {
            var trxnList = new List<ExchangeTransaction>();

            var transactions = binance.GetTransactionsAsync().Result;

            foreach (var transaction in transactions)
            {
                Side side;
                Enum.TryParse(transaction.side.ToLower(), out side);
                var trxn = new ExchangeTransaction
                {
                    transactionId = transaction.orderId.ToString(),
                    pair = transaction.symbol,
                    price = decimal.Parse(transaction.price),
                    quantity = decimal.Parse(transaction.executedQty),
                    side = side,
                    exchange = Exchange.Binance
                };

                trxnList.Add(trxn);
            }

            return trxnList;
        }

        private List<ExchangeOrder> GetBinanceOrders()
        {
            var orderList = new List<ExchangeOrder>();

            //foreach (var coin in coinList.Where(c => c.CoinList.Exchange == Exchange.Binance))
            //{
            //    var orders = binance.GetOpenOrdersAsync(coin.Symbol).Result;

            //    foreach (var order in orders)
            //    {
            //        Side side;
            //        Enum.TryParse(order.side.ToString().ToLower(), out side);
            //        var openOrder = new ExchangeOrder
            //        {
            //            orderId = order.orderId.ToString(),
            //            pair = order.symbol,
            //            price = order.price,
            //            quantity = order.executedQty,
            //            side = side,
            //            exchange = Exchange.Binance
            //        };

            //        orderList.Add(openOrder);
            //    }
            //}
            return orderList;
        }

        private List<Coin> GetBittrexCoins()
        {
            var coinList = new List<Coin>();

            var bittrexBalance = bittrex.GetBalancesAsync().Result;

            //foreach (var bCoin in bittrexBalance)
            //{
            //    var coin = new ExchangeCoin
            //    {
            //        Symbol = bCoin.symbol,
            //        Quantity = bCoin.balance,
            //        Exchange = Exchange.Bittrex
            //    };

            //    coinList.Add(coin);
            //}

            return coinList;
        }

        private List<ExchangeTransaction> GetBittrexTransactions()
        {
            var trxnList = new List<ExchangeTransaction>();

            var transactions = bittrex.GetOrderHistoryAsync().Result;

            foreach (var transaction in transactions)
            {
                Side side;
                Enum.TryParse(transaction.orderType.ToLower(), out side);
                var trxn = new ExchangeTransaction
                {
                    transactionId = transaction.orderId.ToString(),
                    pair = transaction.pair,
                    price = transaction.price,
                    quantity = transaction.quantity,
                    side = side,
                    exchange = Exchange.Bittrex
                };

                trxnList.Add(trxn);
            }

            return trxnList;
        }

        private List<ExchangeOrder> GetBittrexOrders()
        {
            var orderList = new List<ExchangeOrder>();

            //foreach (var coin in coinList.Where(c => c.Exchange == Exchange.Bittrex))
            //{
            //    var orders = bittrex.GetOpenOrdersAsync(coin.Symbol).Result;

            //    foreach (var order in orders)
            //    {
            //        Side side;
            //        Enum.TryParse(order.orderType.ToLower(), out side);
            //        var openOrder = new ExchangeOrder
            //        {
            //            orderId = order.orderId.ToString(),
            //            pair = order.pair,
            //            price = order.price,
            //            quantity = order.quantity,
            //            side = side,
            //            exchange = Exchange.Bittrex
            //        };

            //        orderList.Add(openOrder);
            //    }
            //}
            return orderList;
        }

        #region ExchangeApi converters

        private ExchangeApi ExchangeApiEntityToContract(Business.Entities.Trade.ExchangeApi entity)
        {
            var contract = new ExchangeApi
            {
                ApiExtra = entity.ApiExtra,
                ApiKey = entity.ApiKey,
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
                ApiSecret = contract.ApiSecret,
                ExchangeName = contract.ExchangeName,
                Id = contract.Id
            };

            return entity;
        }

        #endregion ExchangeApi converters
    }
}