using Cryptobitfolio.UI.NewApi;
using System;
using Xamarin.Forms;

namespace Cryptobitfolio.UI.Views
{
    public class BotPageCS : ContentPage
    {
        public BotPageCS()
        {
            var button = new Button
            {
                Text = "Add Api",
                VerticalOptions = LayoutOptions.CenterAndExpand
            };
            button.Clicked += OnNewApiButtonClicked;

            Title = "Bot";
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

        async void OnNewApiButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new NewApiPage());
        }
    }
}