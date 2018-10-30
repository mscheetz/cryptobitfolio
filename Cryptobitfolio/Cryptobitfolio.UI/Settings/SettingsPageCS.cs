using System;
using Xamarin.Forms;

namespace Cryptobitfolio.UI.Settings
{
    public class SettingsPageCS : ContentPage
    {
        public SettingsPageCS()
        {
            var button = new Button
            {
                Text = "Settings",
                VerticalOptions = LayoutOptions.CenterAndExpand
            };
            button.Clicked += OnSaveButtonClicked;

            Title = "Settings";
            Content = new StackLayout
            {
                Children = {
                    new Label {
                        Text = "Settings go here",
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