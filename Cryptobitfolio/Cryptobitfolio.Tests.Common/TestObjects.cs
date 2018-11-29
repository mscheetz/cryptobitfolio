using System;
using System.Collections.Generic;

namespace Cryptobitfolio.Tests.Common
{
    public class TestObjects
    {

        public TestObjects()
        {
        }

        public string GetExchangeHubExchange()
        {
            return "Binance";
        }

        public IEnumerable<ExchangeHub.Contracts.Balance> GetExchangeHubBalances()
        {
            var balances = new List<ExchangeHub.Contracts.Balance>();

            balances.Add(new ExchangeHub.Contracts.Balance
            {
                Available = 0.001M,
                Frozen = 0.024M,
                Symbol = "BTC"
            });
            balances.Add(new ExchangeHub.Contracts.Balance
            {
                Available = 2.5M,
                Frozen = 1.01M,
                Symbol = "ETH"
            });
            balances.Add(new ExchangeHub.Contracts.Balance
            {
                Available = 1000M,
                Frozen = 0M,
                Symbol = "XLM"
            });
            balances.Add(new ExchangeHub.Contracts.Balance
            {
                Available = 250M,
                Frozen = 0M,
                Symbol = "NANO"
            });

            return balances;
        }

        public IEnumerable<string> GetExchangeHubMarkets()
        {
            var markets = new List<string>();
            markets.Add("BTCUSDT");
            markets.Add("XLMBTC");
            markets.Add("NANOBTC");
            markets.Add("ETHBTC");
            markets.Add("XLMETH");
            markets.Add("NANOETH");
            markets.Add("ETHUSDT");
            markets.Add("XLMUSDT");

            return markets;
        }

        public IEnumerable<ExchangeHub.Contracts.OrderResponse> GetExchangeHubOrders()
        {
            var orders = new List<ExchangeHub.Contracts.OrderResponse>();

            orders.Add(new ExchangeHub.Contracts.OrderResponse
            {
                FilledQuantity = 0.005M,
                OrderId = "1",
                OrderQuantity = 0.005M,
                OrderStatus = ExchangeHub.Contracts.OrderStatus.Filled,
                Pair = "BTCUSDT",
                Price = 4100.00M,
                Side = ExchangeHub.Contracts.Side.Buy,
                StopPrice = 0.0M,
                TransactTime = DateTime.UtcNow.AddHours(-56)
            });
            orders.Add(new ExchangeHub.Contracts.OrderResponse
            {
                FilledQuantity = 0.011M,
                OrderId = "2",
                OrderQuantity = 0.011M,
                OrderStatus = ExchangeHub.Contracts.OrderStatus.Filled,
                Pair = "BTCUSDT",
                Price = 7423.00M,
                Side = ExchangeHub.Contracts.Side.Buy,
                StopPrice = 0.0M,
                TransactTime = DateTime.UtcNow.AddHours(-124)
            });
            orders.Add(new ExchangeHub.Contracts.OrderResponse
            {
                FilledQuantity = 500M,
                OrderId = "3",
                OrderQuantity = 500M,
                OrderStatus = ExchangeHub.Contracts.OrderStatus.Filled,
                Pair = "XLMBTC",
                Price = 0.00003445M,
                Side = ExchangeHub.Contracts.Side.Buy,
                StopPrice = 0.0M,
                TransactTime = DateTime.UtcNow.AddHours(-5)
            });
            orders.Add(new ExchangeHub.Contracts.OrderResponse
            {
                FilledQuantity = 3.51M,
                OrderId = "4",
                OrderQuantity = 3.51M,
                OrderStatus = ExchangeHub.Contracts.OrderStatus.Filled,
                Pair = "ETHUSDT",
                Price = 107.457M,
                Side = ExchangeHub.Contracts.Side.Buy,
                StopPrice = 0.0M,
                TransactTime = DateTime.UtcNow.AddHours(-22)
            });
            orders.Add(new ExchangeHub.Contracts.OrderResponse
            {
                FilledQuantity = 250M,
                OrderId = "5",
                OrderQuantity = 250M,
                OrderStatus = ExchangeHub.Contracts.OrderStatus.Filled,
                Pair = "XLMBTC",
                Price = 0.00004478M,
                Side = ExchangeHub.Contracts.Side.Buy,
                StopPrice = 0.0M,
                TransactTime = DateTime.UtcNow.AddHours(-201)
            });
            orders.Add(new ExchangeHub.Contracts.OrderResponse
            {
                FilledQuantity = 250M,
                OrderId = "6",
                OrderQuantity = 250M,
                OrderStatus = ExchangeHub.Contracts.OrderStatus.Filled,
                Pair = "XLMBTC",
                Price = 0.00002278M,
                Side = ExchangeHub.Contracts.Side.Buy,
                StopPrice = 0.0M,
                TransactTime = DateTime.UtcNow.AddHours(-271)
            });
            orders.Add(new ExchangeHub.Contracts.OrderResponse
            {
                FilledQuantity = 50M,
                OrderId = "7",
                OrderQuantity = 50M,
                OrderStatus = ExchangeHub.Contracts.OrderStatus.Filled,
                Pair = "NANOBTC",
                Price = 0.0002278M,
                Side = ExchangeHub.Contracts.Side.Buy,
                StopPrice = 0.0M,
                TransactTime = DateTime.UtcNow.AddHours(-21)
            });
            orders.Add(new ExchangeHub.Contracts.OrderResponse
            {
                FilledQuantity = 50M,
                OrderId = "8",
                OrderQuantity = 50M,
                OrderStatus = ExchangeHub.Contracts.OrderStatus.Filled,
                Pair = "NANOBTC",
                Price = 0.0005545M,
                Side = ExchangeHub.Contracts.Side.Buy,
                StopPrice = 0.0M,
                TransactTime = DateTime.UtcNow.AddHours(-145)
            });
            orders.Add(new ExchangeHub.Contracts.OrderResponse
            {
                FilledQuantity = 50M,
                OrderId = "9",
                OrderQuantity = 50M,
                OrderStatus = ExchangeHub.Contracts.OrderStatus.Filled,
                Pair = "NANOBTC",
                Price = 0.0003978M,
                Side = ExchangeHub.Contracts.Side.Buy,
                StopPrice = 0.0M,
                TransactTime = DateTime.UtcNow.AddHours(-122)
            });

            return orders;
        }
    }
}
