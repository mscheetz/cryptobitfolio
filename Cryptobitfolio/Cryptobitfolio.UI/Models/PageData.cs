// -----------------------------------------------------------------------------
// <copyright file="PageData" company="Matt Scheetz">
//     Copyright (c) Matt Scheetz All Rights Reserved
// </copyright>
// <author name="Matt Scheetz" date="12/21/2018 11:17:22 AM" />
// -----------------------------------------------------------------------------

namespace Cryptobitfolio.UI.Models
{
    #region Usings

    using System;
    using System.Collections.Generic;
    using System.Text;

    #endregion Usings

    public class PageData
    {

        public string PageName { get; set; }

        public string PageType { get; set; }

        public string NavigationTitle { get; set; }

        //public ScItemsResponse ItemContext { get; set; }

        //public IList<ScItemsResponse> DataSourceFromField { get; set; }

        //public ScItemsResponse DataSourceFromChildren { get; set; }
    }
}