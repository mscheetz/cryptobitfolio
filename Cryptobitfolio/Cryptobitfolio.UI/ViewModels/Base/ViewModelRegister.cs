//// -----------------------------------------------------------------------------
//// <copyright file="ViewModelRegister" company="Matt Scheetz">
////     Copyright (c) Matt Scheetz All Rights Reserved
//// </copyright>
//// <author name="Matt Scheetz" date="12/7/2018 8:11:06 PM" />
//// -----------------------------------------------------------------------------

//namespace Cryptobitfolio.UI.ViewModels.Base
//{
//    using Cryptobitfolio.Business;
//    using Cryptobitfolio.Business.Common;
//    #region Usings

//    using System;
//    using System.Collections.Generic;
//    using System.Globalization;
//    using System.Reflection;
//    using System.Text;
//    using TinyIoC;
//    using Xamarin.Forms;

//    #endregion Usings

//    public static class ViewModelRegister
//    {
//        #region Properties

//        private static TinyIoCContainer _container;

//        public static readonly BindableProperty AutoWireViewModelProperty =
//            BindableProperty.CreateAttached("AutoWireViewModel", typeof(bool), typeof(ViewModelRegister), default(bool), propertyChanged: OnAutoWireViewModelChanged);
        
//        public static bool UseMockService { get; set; }
        
//        #endregion Properties

//        public static bool GetAutoWireViewModel(BindableObject bindable)
//        {
//            return (bool)bindable.GetValue(ViewModelRegister.AutoWireViewModelProperty);
//        }

//        public static void SetAutoWireViewModel(BindableObject bindable, bool value)
//        {
//            bindable.SetValue(ViewModelRegister.AutoWireViewModelProperty, value);
//        }

//        static ViewModelRegister()
//        {
//            _container = new TinyIoCContainer();

//            // View models - by default, TinyIoC will register concrete classes as multi-instance.
//            _container.Register<AlertListViewModel>();
//            _container.Register<ApiViewModel>();
//            _container.Register<ExchangeViewModel>();
//            _container.Register<PortfolioViewModel>();
//            _container.Register<WatchListViewModel>();
//            //_container.Register<BasketViewModel>();
//            //_container.Register<CatalogViewModel>();
//            //_container.Register<CheckoutViewModel>();
//            //_container.Register<LoginViewModel>();
//            //_container.Register<MainViewModel>();
//            //_container.Register<OrderDetailViewModel>();
//            //_container.Register<ProfileViewModel>();
//            //_container.Register<SettingsViewModel>();
//            //_container.Register<CampaignViewModel>();
//            //_container.Register<CampaignDetailsViewModel>();

//            // Services - by default, TinyIoC will register interface registrations as singletons.
//            _container.Register<IAlertBuilder, AlertBuilder>();
//            _container.Register<ICoinBuilder, CoinBuilder>();
//            _container.Register<ICoinBuyBuilder, CoinBuyBuilder>();
//            _container.Register<ICurrencyBuilder, CurrencyBuilder>();
//            _container.Register<IExchangeApiBuilder, ExchangeApiBuilder>();
//            _container.Register<IExchangeCoinBuilder, ExchangeCoinBuilder>();
//            _container.Register<IExchangeHubBuilder, ExchangeHubBuilder>();
//            _container.Register<IExchangeOrderBuilder, ExchangeOrderBuilder>();
//            _container.Register<IWatchBuilder, WatchBuilder>();
            
//            _container.Register<IArbitrageBuilder, ArbitrageBuilder>();
//            _container.Register<ICMCBuilder, CMCBuilder>();
//            _container.Register<IExchangeBuilder, ExchangeBuilder>();
//            _container.Register<IPortfolioBuilder, PortfolioBuilder>();
//            //_container.Register<INavigationService, NavigationService>();
//            //_container.Register<IDialogService, DialogService>();
//            //_container.Register<IOpenUrlService, OpenUrlService>();
//            //_container.Register<IIdentityService, IdentityService>();
//            //_container.Register<IRequestProvider, RequestProvider>();
//            //_container.Register<IDependencyService, Services.Dependency.DependencyService>();
//            //_container.Register<ISettingsService, SettingsService>();
//            //_container.Register<IFixUriService, FixUriService>();
//            //_container.Register<ILocationService, LocationService>();
//            //_container.Register<ICatalogService, CatalogMockService>();
//            //_container.Register<IBasketService, BasketMockService>();
//            //_container.Register<IOrderService, OrderMockService>();
//            //_container.Register<IUserService, UserMockService>();
//            //_container.Register<ICampaignService, CampaignMockService>();
//        }

//        public static void UpdateDependencies(bool useMockServices)
//        {
//            // Change injected dependencies
//            if (useMockServices)
//            {
//                //_container.Register<ICatalogService, CatalogMockService>();
//                //_container.Register<IBasketService, BasketMockService>();
//                //_container.Register<IOrderService, OrderMockService>();
//                //_container.Register<IUserService, UserMockService>();
//                //_container.Register<ICampaignService, CampaignMockService>();

//                UseMockService = true;
//            }
//            else
//            {
//                _container.Register<IAlertBuilder, AlertBuilder>();
//                _container.Register<ICoinBuilder, CoinBuilder>();
//                _container.Register<ICoinBuyBuilder, CoinBuyBuilder>();
//                _container.Register<ICurrencyBuilder, CurrencyBuilder>();
//                _container.Register<IExchangeApiBuilder, ExchangeApiBuilder>();
//                _container.Register<IExchangeCoinBuilder, ExchangeCoinBuilder>();
//                _container.Register<IExchangeHubBuilder, ExchangeHubBuilder>();
//                _container.Register<IExchangeOrderBuilder, ExchangeOrderBuilder>();
//                _container.Register<IWatchBuilder, WatchBuilder>();
//                //_container.Register<ICatalogService, CatalogService>();
//                //_container.Register<IBasketService, BasketService>();
//                //_container.Register<IOrderService, OrderService>();
//                //_container.Register<IUserService, UserService>();
//                //_container.Register<ICampaignService, CampaignService>();

//                UseMockService = false;
//            }
//        }

//        public static void RegisterSingleton<TInterface, T>() where TInterface : class where T : class, TInterface
//        {
//            _container.Register<TInterface, T>().AsSingleton();
//        }

//        public static T Resolve<T>() where T : class
//        {
//            return _container.Resolve<T>();
//        }

//        private static void OnAutoWireViewModelChanged(BindableObject bindable, object oldValue, object newValue)
//        {
//            var view = bindable as Element;
//            if (view == null)
//            {
//                return;
//            }

//            var viewType = view.GetType();
//            var viewName = viewType.FullName.Replace(".Views.", ".ViewModels.");
//            var viewAssemblyName = viewType.GetTypeInfo().Assembly.FullName;
//            var viewModelName = string.Format(CultureInfo.InvariantCulture, "{0}Model, {1}", viewName, viewAssemblyName);

//            var viewModelType = Type.GetType(viewModelName);
//            if (viewModelType == null)
//            {
//                return;
//            }
//            var viewModel = _container.Resolve(viewModelType);
//            view.BindingContext = viewModel;
//        }
//    }
//}