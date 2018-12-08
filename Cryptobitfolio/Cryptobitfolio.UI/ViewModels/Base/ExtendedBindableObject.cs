// -----------------------------------------------------------------------------
// <copyright file="ExtendedBindableObject" company="Matt Scheetz">
//     Copyright (c) Matt Scheetz All Rights Reserved
// </copyright>
// <author name="Matt Scheetz" date="12/7/2018 8:29:02 PM" />
// -----------------------------------------------------------------------------

namespace Cryptobitfolio.UI.ViewModels.Base
{
    #region Usings

    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Text;
    using Xamarin.Forms;

    #endregion Usings

    public abstract class ExtendedBindableObject : BindableObject
    {
        public void RaisePropertyChanged<T>(Expression<Func<T>> property)
        {
            var name = GetMemberInfo(property).Name;
            OnPropertyChanged(name);
        }

        private MemberInfo GetMemberInfo(Expression expression)
        {
            MemberExpression operand;
            LambdaExpression lambdaExpression = (LambdaExpression)expression;
            if (lambdaExpression.Body as UnaryExpression != null)
            {
                UnaryExpression body = (UnaryExpression)lambdaExpression.Body;
                operand = (MemberExpression)body.Operand;
            }
            else
            {
                operand = (MemberExpression)lambdaExpression.Body;
            }
            return operand.Member;
        }
    }
}