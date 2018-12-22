// -----------------------------------------------------------------------------
// <copyright file="ViewModelBase" company="Matt Scheetz">
//     Copyright (c) Matt Scheetz All Rights Reserved
// </copyright>
// <author name="Matt Scheetz" date="12/21/2018 11:19:21 AM" />
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

    public class ViewModelBase : ObservableModel, IViewModel
    {


        private PageData _pageData;


        public PageData PageContext
        {
            get { return _pageData; }
            set { SetProperty(ref _pageData, value); }
        }


        private string _title;

        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }


        private string _icon;

        public string Icon
        {
            get { return _icon; }
            set { SetProperty(ref _icon, value); }
        }

        private bool _isBusy;

        public bool IsBusy
        {
            get { return _isBusy; }
            set { SetProperty(ref _isBusy, value); }
        }


        private string _busyText;

        public string BusyText
        {
            get { return _busyText; }
            set { SetProperty(ref _busyText, value); }
        }


        private Page _connectedToPage;


        public Page ConnectedToPage
        {
            get { return _connectedToPage; }
            set { SetProperty(ref _connectedToPage, value); }
        }

        protected void SetBusy(string text)
        {
            BusyText = text;
            IsBusy = true;
        }

        protected void ClearBusy()
        {
            IsBusy = false;
            BusyText = string.Empty;
        }

        public virtual Task LoadAsync()
        {
            throw new NotImplementedException();
        }

        public virtual Task UnLoadAsync()
        {
            throw new NotImplementedException();
        }

    }
}