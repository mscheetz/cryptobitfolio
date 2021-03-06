﻿// -----------------------------------------------------------------------------
// <copyright file="ArbitragePath" company="Matt Scheetz">
//     Copyright (c) Matt Scheetz All Rights Reserved
// </copyright>
// <author name="Matt Scheetz" date="11/26/2018 1:46:43 PM" />
// -----------------------------------------------------------------------------

namespace Cryptobitfolio.Business.Entities.Portfolio
{
    #region Usings

    using System;
    using System.Collections.Generic;
    using System.Text;

    #endregion Usings

    public class ArbitragePath : EntityBase
    {
        #region Properties

        public DateTime Created { get; set; }
        public string Path { get; set; }

        #endregion Properties
    }
}