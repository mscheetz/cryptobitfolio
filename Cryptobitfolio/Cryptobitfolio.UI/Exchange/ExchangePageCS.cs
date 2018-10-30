using System;
using Xamarin.Forms;

namespace Cryptobitfolio.UI.Exchange
{
    public class ExchangePageCS : ContentPage
    {
        public ExchangePageCS()
        {
            var button = new Button
            {
                Text = "Exchanges",
                VerticalOptions = LayoutOptions.CenterAndExpand
            };
            button.Clicked += OnNewApiButtonClicked;

            Title = "Exchanges";
            Content = new StackLayout
            {
                Children = {
                    new Label {
                        Text = "List of exchanges go here",
                        HorizontalOptions = LayoutOptions.Center,
                        VerticalOptions = LayoutOptions.CenterAndExpand
                    },
                    button
                }
            };
        }

        async void OnNewApiButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Api.ApiPage());
        }
    }
}