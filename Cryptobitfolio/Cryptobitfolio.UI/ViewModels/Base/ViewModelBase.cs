// -----------------------------------------------------------------------------
// <copyright file="ViewModelBase" company="Matt Scheetz">
//     Copyright (c) Matt Scheetz All Rights Reserved
// </copyright>
// <author name="Matt Scheetz" date="12/7/2018 8:28:31 PM" />
// -----------------------------------------------------------------------------

namespace Cryptobitfolio.UI.ViewModels.Base
{
    #region Usings

    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    #endregion Usings

    public abstract class ViewModelBase : ExtendedBindableObject
    {
        //protected readonly IDialogService DialogService;
        //protected readonly INavigationService NavigationService;

        private bool _isBusy;

        public bool IsBusy
        {
            get
            {
                return _isBusy;
            }

            set
            {
                _isBusy = value;
                RaisePropertyChanged(() => IsBusy);
            }
        }

        public ViewModelBase()
        {
            //DialogService = ViewModelRegister.Resolve<IDialogService>();
            //NavigationService = ViewModelRegister.Resolve<INavigationService>();

            //var settingsService = ViewModelRegister.Resolve<ISettingsService>();

            //GlobalSetting.Instance.BaseIdentityEndpoint = settingsService.IdentityEndpointBase;
            //GlobalSetting.Instance.BaseGatewayShoppingEndpoint = settingsService.GatewayShoppingEndpointBase;
            //GlobalSetting.Instance.BaseGatewayMarketingEndpoint = settingsService.GatewayMarketingEndpointBase;
        }

        public virtual Task InitializeAsync(object navigationData)
        {
            return Task.FromResult(false);
        }
    }
}