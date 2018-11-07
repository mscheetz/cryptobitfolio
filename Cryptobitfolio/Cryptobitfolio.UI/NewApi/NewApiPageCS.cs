using Cryptobitfolio.Business.Contracts.Trade;
using System;
using Xamarin.Forms;

namespace Cryptobitfolio.UI.NewApi
{
    public class NewApiPageCS : ContentPage
    {
        public NewApiPageCS()
        {
            var picker = new Picker { Title = "Exchange" };
            picker.FontSize = 6;
            var exchanges = Enum.GetValues(typeof(Business.Entities.Exchange));
            foreach (var exchange in exchanges)
            {
                picker.Items.Add(exchange.ToString());
            }

            var apiKeyName = new Entry();
            apiKeyName.FontSize = 6;
            apiKeyName.SetBinding(Entry.TextProperty, "ApiKeyName");
            var apiKey = new Entry();
            apiKey.FontSize = 6;
            apiKey.SetBinding(Entry.TextProperty, "ApiKey");
            var apiSecret = new Entry();
            apiSecret.FontSize = 6;
            apiSecret.SetBinding(Entry.TextProperty, "ApiSecret");
            var extraValue = new Entry();
            extraValue.FontSize = 6;
            extraValue.SetBinding(Entry.TextProperty, "ExtraValue");

            var saveButton = new Button { Text = "Save" };
            saveButton.Clicked += async (sender, e) =>
            {
                var exchangeApi = (ExchangeApi)BindingContext;
                //await App.ExchangeBuilder.SaveExchangeApi(exchangeApi);
                await Application.Current.MainPage.Navigation.PopAsync();
            };

            var deleteButton = new Button { Text = "Delete" };
            deleteButton.Clicked += async (sender, e) =>
            {
                var exchangeApi = (ExchangeApi)BindingContext;
                //await App.ExchangeBuilder.DeleteExchangeApi(exchangeApi);
                await Application.Current.MainPage.Navigation.PopAsync();
            };

            Title = "Exchange Api Info";
            Content = new StackLayout
            {
                Children = {
                    new Label { Text = "Exchange:", FontSize = 6 },
                    picker,
                    new Label { Text = "Api Key Name:", FontSize = 6 },
                    apiKeyName,
                    new Label { Text = "Api Key:", FontSize = 6 },
                    apiKey,
                    new Label { Text = "Api Secret:", FontSize = 6 },
                    apiSecret,
                    new Label {Text = "Extra Value:", FontSize = 6 },
                    extraValue,
                    saveButton,
                    deleteButton
                }
            };
        }
    }
}