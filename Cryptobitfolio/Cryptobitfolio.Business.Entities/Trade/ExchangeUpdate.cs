// -----------------------------------------------------------------------------
// <copyright file="ExchangeUpdate" company="Matt Scheetz">
//     Copyright (c) Matt Scheetz All Rights Reserved
// </copyright>
// <author name="Matt Scheetz" date="11/19/2018 7:58:33 PM" />
// -----------------------------------------------------------------------------

namespace Cryptobitfolio.Business.Entities.Trade
{
    using SQLite;
    #region Usings

    using System;
    using System.Collections.Generic;
    using System.Text;

    #endregion Usings

    public class ExchangeUpdate
    {

        #region Properties

        [PrimaryKey]
        [NotNull]
        [AutoIncrement]
        public int Id { get; set; }
        public DateTime UpdateAt { get; set; }
        public Exchange Exchange { get; set; }

        #endregion Properties
    }
}