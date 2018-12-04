// -----------------------------------------------------------------------------
// <copyright file="Alerter" company="Matt Scheetz">
//     Copyright (c) Matt Scheetz All Rights Reserved
// </copyright>
// <author name="Matt Scheetz" date="12/3/2018 7:24:40 PM" />
// -----------------------------------------------------------------------------

namespace Cryptobitfolio.Business.Contracts.Portfolio
{
    using Cryptobitfolio.Business.Entities;
    #region Usings

    using System;

    #endregion Usings

    public class Alerter
    {
        #region Properties

        public int AlertId { get; set; }
        public int CurrencyId { get; set; }
        public string Pair { get; set; }
        public Exchange Exchange { get; set; }
        public decimal Price { get; set; }
        public bool Enabled { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Hit { get; set; }

        #endregion Properties
    }
}