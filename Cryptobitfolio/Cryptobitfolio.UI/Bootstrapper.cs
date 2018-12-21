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
    using Cryptobitfolio.UI.ViewModels;

    #endregion Usings

    public class Bootstrapper
    {
        #region Properties

        public static void Initialize()
        {
            var builder = new ContainerBuilder();
            // Register View Models
            builder.RegisterType<AlertListViewModel>().AsSelf();
            builder.RegisterType<ApiViewModel>().AsSelf();
            builder.RegisterType<ExchangeViewModel>().AsSelf();
            builder.RegisterType<PortfolioViewModel>().AsSelf();
            builder.RegisterType<WatchListViewModel>().AsSelf();

            // Regiseter Builders
            builder.RegisterType<AlertBuilder>().As<IAlertBuilder>();
            builder.RegisterType<CoinBuilder>().As<ICoinBuilder>();
            builder.RegisterType<CoinBuyBuilder>().As<ICoinBuyBuilder>();
            builder.RegisterType<CurrencyBuilder>().As<ICurrencyBuilder>();
            builder.RegisterType<ExchangeApiBuilder>().As<IExchangeApiBuilder>();
            builder.RegisterType<ExchangeCoinBuilder>().As<IExchangeCoinBuilder>();
            builder.RegisterType<ExchangeHubBuilder>().As<IExchangeHubBuilder>();
            builder.RegisterType<ExchangeOrderBuilder>().As<IExchangeOrderBuilder>();
            builder.RegisterType<WatchBuilder>().As<IWatchBuilder>();

            IContainer container = builder.Build();

            var asl = new AutofacServiceLocator(container);
            ServiceLocator.SetLocatorProvider(() => asl);
        }

        #endregion Properties
    }
}