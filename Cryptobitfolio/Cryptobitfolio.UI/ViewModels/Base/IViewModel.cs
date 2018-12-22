// -----------------------------------------------------------------------------
// <copyright file="IViewModel" company="Matt Scheetz">
//     Copyright (c) Matt Scheetz All Rights Reserved
// </copyright>
// <author name="Matt Scheetz" date="12/21/2018 11:16:07 AM" />
// -----------------------------------------------------------------------------

namespace Cryptobitfolio.UI.ViewModels.Base
{
    using Cryptobitfolio.UI.Models;
    #region Usings

    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;
    using Xamarin.Forms;

    #endregion Usings

    public interface IViewModel
    {
        string Title { get; set; }

        PageData PageContext { get; set; }

        Page ConnectedToPage { get; set; }

        Task LoadAsync();

        Task UnLoadAsync();
    }
}