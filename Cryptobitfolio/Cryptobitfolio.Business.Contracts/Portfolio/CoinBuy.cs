using Cryptobitfolio.Business.Contracts.Trade;
using Cryptobitfolio.Business.Entities;
using System;

namespace Cryptobitfolio.Business.Contracts.Portfolio
{
    public class CoinBuy : ExchangeOrder
    {
        public int CoinBuyId { get; set; }
        public decimal QuantityApplied { get; set; }
        public decimal BTCPrice { get; set; }

        public CoinBuy() { }

        public CoinBuy(ExchangeOrder exchangeOrder)
        {
            this.ClosedDate = exchangeOrder.ClosedDate;
            this.Exchange = exchangeOrder.Exchange;
            this.ExchangeOrderId = exchangeOrder.ExchangeOrderId;
            this.FilledQuantity = exchangeOrder.FilledQuantity;
            this.OrderId = exchangeOrder.OrderId;
            this.Pair = exchangeOrder.Pair;
            this.PlaceDate = exchangeOrder.PlaceDate;
            this.Price = exchangeOrder.Price;
            this.Quantity = exchangeOrder.Quantity;
            this.Side = exchangeOrder.Side;
            this.Status = exchangeOrder.Status;
        }
    }
}
