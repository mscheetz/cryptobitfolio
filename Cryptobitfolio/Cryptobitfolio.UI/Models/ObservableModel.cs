// -----------------------------------------------------------------------------
// <copyright file="ObservableModel" company="Matt Scheetz">
//     Copyright (c) Matt Scheetz All Rights Reserved
// </copyright>
// <author name="Matt Scheetz" date="12/21/2018 11:20:21 AM" />
// -----------------------------------------------------------------------------

namespace Cryptobitfolio.UI.Models
{
    #region Usings

    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    #endregion Usings

    public class ObservableModel : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;

        protected bool SetProperty<T>(ref T storage, T value,
            [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(storage, value))
                return false;

            storage = value;
            OnPropertyChanged(propertyName);
            return true;
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}