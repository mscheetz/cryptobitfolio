// -----------------------------------------------------------------------------
// <copyright file="ViewModelLocator" company="Matt Scheetz">
//     Copyright (c) Matt Scheetz All Rights Reserved
// </copyright>
// <author name="Matt Scheetz" date="12/20/2018 7:57:10 PM" />
// -----------------------------------------------------------------------------

namespace Cryptobitfolio.UI.ViewModels.Base
{
    #region Usings

    using CommonServiceLocator;

    #endregion Usings

    public class ViewModelLocator
    {
        #region Properties
        #endregion Properties

        static ViewModelLocator()
        {
            Bootstrapper.Initialize();
        }

        public AlertListViewModel AlertVM
        {
            get { return ServiceLocator.Current.GetInstance<AlertListViewModel>(); }
        }

        public ApiViewModel ApiVM
        {
            get { return ServiceLocator.Current.GetInstance<ApiViewModel>(); }
        }

        public ExchangeViewModel ExchangeVM
        {
            get { return ServiceLocator.Current.GetInstance<ExchangeViewModel>(); }
        }

        public PortfolioViewModel PortfolioVM
        {
            get { return ServiceLocator.Current.GetInstance<PortfolioViewModel>(); }
        }

        public WatchListViewModel WatchVM
        {
            get { return ServiceLocator.Current.GetInstance<WatchListViewModel>(); }
        }
    }
}