// -----------------------------------------------------------------------------
// <copyright file="Bootstrapper" company="Matt Scheetz">
//     Copyright (c) Matt Scheetz All Rights Reserved
// </copyright>
// <author name="Matt Scheetz" date="12/20/2018 7:51:31 PM" />
// -----------------------------------------------------------------------------

namespace Cryptobitfolio.UI
{
    #region Usings

    using Autofac;
    using Autofac.Extras.CommonServiceLocator;
    using CommonServiceLocator;
    using Cryptobitfolio.Business;
    using Cryptobitfolio.Business.Common;
    using Cryptobitfolio.UI;
    using Cryptobitfolio.UI.ViewModels;
    using Cryptobitfolio.UI.Views;

    #endregion Usings

    public class Bootstrapper
    {
        #region Properties

        public static void Initialize()
        {
            var builder = new ContainerBuilder();
            builder.RegisterModule<CryptobitfolioModule>();
            App.AppInstance._container = builder.Build();
            //App.AppInstance.MainPage = App.AppInstance._container.Resolve<MainPageCS>();


            //var asl = new AutofacServiceLocator(container);
            //ServiceLocator.SetLocatorProvider(() => asl);
        }

        #endregion Properties
    }
}