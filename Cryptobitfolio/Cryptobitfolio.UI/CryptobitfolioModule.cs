// -----------------------------------------------------------------------------
// <copyright file="CryptobitfolioModule" company="Matt Scheetz">
//     Copyright (c) Matt Scheetz All Rights Reserved
// </copyright>
// <author name="Matt Scheetz" date="12/21/2018 3:09:07 PM" />
// -----------------------------------------------------------------------------

namespace Cryptobitfolio.UI
{
    using Autofac;
    using Cryptobitfolio.Business;
    using Cryptobitfolio.Business.Common;
    using Cryptobitfolio.UI.ViewModels;
    using Cryptobitfolio.UI.Views;
    #region Usings

    using System;
	using System.Collections.Generic;
	using System.Text;
	
	#endregion Usings

    public class CryptobitfolioModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

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

            // Load pages
            builder.RegisterType<AlertListCS>();
            builder.RegisterType<ApiPageCS>();
            builder.RegisterType<ApiDetailPageCS>();
            builder.RegisterType<BotPageCS>();
            builder.RegisterType<CoinPageCS>();
            builder.RegisterType<ExchangePageCS>();
            builder.RegisterType<MainPageCS>();
            builder.RegisterType<PortfolioPageCS>();
            builder.RegisterType<SettingsPageCS>();
            builder.RegisterType<WatchCoinCS>();
            builder.RegisterType<WatchListCS>();

        }
    }
}