// -----------------------------------------------------------------------------
// <copyright file="IHelper" company="Matt Scheetz">
//     Copyright (c) Matt Scheetz All Rights Reserved
// </copyright>
// <author name="Matt Scheetz" date="12/13/2018 7:03:33 PM" />
// -----------------------------------------------------------------------------

namespace Cryptobitfolio.Business.Common
{
    using Cryptobitfolio.Business.Entities;
    #region Usings

    using System;
    using System.Collections.Generic;
    using System.Text;

    #endregion Usings

    public static class IHelper
    {
        #region Properties
        #endregion Properties
        
        public static Exchange StringToExchange(string exchangeName)
        {
            Exchange exchange;
            Enum.TryParse(exchangeName, out exchange);

            return exchange;
        }
    }
}