// -----------------------------------------------------------------------------
// <copyright file="HistoricalPrice" company="Matt Scheetz">
//     Copyright (c) Matt Scheetz All Rights Reserved
// </copyright>
// <author name="Matt Scheetz" date="12/3/2018 7:26:47 PM" />
// -----------------------------------------------------------------------------

namespace Cryptobitfolio.Business.Entities.Trade
{
    #region Usings

    using System;
    using System.Collections.Generic;
    using System.Text;

    #endregion Usings

    public class HistoricalPrice : EntityBase
    {
        #region Properties

        public int CurrencyId { get; set; }
        public Exchange Exchange { get; set; }
        public string Pair { get; set; }
        public decimal Price { get; set; }
        public decimal High { get; set; }
        public decimal Low { get; set; }
        public DateTime Snapshot { get; set; }

        #endregion Properties
    }
}