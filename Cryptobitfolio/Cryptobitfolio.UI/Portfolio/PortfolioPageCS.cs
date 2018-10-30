using System;
using Xamarin.Forms;

namespace Cryptobitfolio.UI.Portfolio
{
    public class PortfolioPageCS : ContentPage
    {
        ListView coinList;

        public PortfolioPageCS()
        {
            var button = new Button
            {
                Text = "Portfolio",
                VerticalOptions = LayoutOptions.CenterAndExpand
            };
            button.Clicked += OnSaveButtonClicked;

            Title = "This Week";
            Content = new StackLayout
            {
                Children = {
                    new Label {
                        Text = "This week's appointments go here",
                        HorizontalOptions = LayoutOptions.Center,
                        VerticalOptions = LayoutOptions.CenterAndExpand
                    },
                    button
                }
            };
        }

        async void OnSaveButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}