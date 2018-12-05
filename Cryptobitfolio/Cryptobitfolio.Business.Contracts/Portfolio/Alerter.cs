// -----------------------------------------------------------------------------
// <copyright file="Alerter" company="Matt Scheetz">
//     Copyright (c) Matt Scheetz All Rights Reserved
// </copyright>
// <author name="Matt Scheetz" date="12/3/2018 7:24:40 PM" />
// -----------------------------------------------------------------------------

namespace Cryptobitfolio.Business.Contracts.Portfolio
{
    #region Usings

    using Cryptobitfolio.Business.Contracts.Trade;
    using Cryptobitfolio.Business.Entities;
    using System;

    #endregion Usings

    public class Alerter
    {
        #region Properties

        public int AlertId { get; set; }
        public string Pair { get; set; }
        public Exchange Exchange { get; set; }
        public decimal Price { get; set; }
        public bool Enabled { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Hit { get; set; }
        public HistoricalPrice[] HistoricalPrices { get; set; }

        #endregion Properties
    }
}