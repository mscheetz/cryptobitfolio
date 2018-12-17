// -----------------------------------------------------------------------------
// <copyright file="HistoricalPrice" company="Matt Scheetz">
//     Copyright (c) Matt Scheetz All Rights Reserved
// </copyright>
// <author name="Matt Scheetz" date="12/4/2018 9:19:53 PM" />
// -----------------------------------------------------------------------------

namespace Cryptobitfolio.Business.Contracts.Trade
{
    #region Usings

    using Cryptobitfolio.Business.Entities;
    using System;

    #endregion Usings

    public class HistoricalPrice
    {
        public Exchange Exchange { get; set; }
        public string Pair { get; set; }
        public decimal Price { get; set; }
        public decimal High { get; set; }
        public decimal Low { get; set; }
        public DateTime Snapshot { get; set; }
    }
}