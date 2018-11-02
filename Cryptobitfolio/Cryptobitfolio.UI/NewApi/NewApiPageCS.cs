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
            var exchanges = Enum.GetValues(typeof(Business.Entities.Exchange));
            foreach (var exchange in exchanges)
            {
                picker.Items.Add(exchange.ToString());
            }

            var apiKey = new Entry();
            apiKey.SetBinding(Entry.TextProperty, "ApiKey");
            var apiSecret = new Entry();
            apiSecret.SetBinding(Entry.TextProperty, "ApiSecret");
            var extraValue = new Entry();
            extraValue.SetBinding(Entry.TextProperty, "ExtraValue");

            var saveButton = new Button { Text = "Save" };
            saveButton.Clicked += async (sender, e) =>
            {
                var exchangeApi = (ExchangeApi)BindingContext;
                await App.ExchangeBuilder.SaveExchangeApi(exchangeApi);
                await Navigation.PopAsync();
            };

            var deleteButton = new Button { Text = "Delete" };
            deleteButton.Clicked += async (sender, e) =>
            {
                var exchangeApi = (ExchangeApi)BindingContext;
                await App.ExchangeBuilder.DeleteExchangeApi(exchangeApi);
                await Navigation.PopAsync();
            };

            Title = "Exchange Api Info";
            Content = new StackLayout
            {
                Children = {
                    new Label { Text = "Exchange:" },
                    picker,
                    new Label { Text = "Api Key:" },
                    apiKey,
                    new Label { Text = "Api Secret:" },
                    apiSecret,
                    new Label {Text = "Extra Value:" },
                    extraValue,
                    saveButton,
                    deleteButton
                }
            };
        }
    }
}