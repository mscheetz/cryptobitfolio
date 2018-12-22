using Autofac;
using Cryptobitfolio.UI.NewApi;
using Cryptobitfolio.UI.ViewModels;
using System;
using Xamarin.Forms;

namespace Cryptobitfolio.UI.Views
{
    public class ApiPageCS : ContentPage
    {
        private readonly ApiViewModel _apiVM;

        public ApiPageCS() : this(App.AppInstance._container.Resolve<ApiViewModel>())
        {
        }

        public ApiPageCS(ApiViewModel apiVM)
        {
            this._apiVM = apiVM;

            Title = "Api Info";

            var newApiKeyButton = new Button { Text = "Cancel" };
            newApiKeyButton.Clicked += OnNewApiButtonClicked;

            var cancelButton = new Button { Text = "Cancel" };
            cancelButton.Clicked += async (sender, e) =>
            {
                await Navigation.PopAsync();
            };

            StackLayout btn = new StackLayout()
            {
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                Orientation = StackOrientation.Horizontal,
                Children = { newApiKeyButton, cancelButton }
            };

            var apiKeys = _apiVM.GetExchangeApis().Result;

            var headers = new DataTemplate(() =>
            {
                Grid grid = new Grid();
                var exchange = new Label();
                exchange.Text = "Exchange";
                var apiKeyName = new Label();
                apiKeyName.Text = "ApiKeyName";
                var apiKey = new Label();
                apiKey.Text = "ApiKeyShort";

                grid.Children.Add(exchange);
                grid.Children.Add(apiKeyName, 1, 0);
                grid.Children.Add(apiKey, 2, 0);

                return new ViewCell { View = grid };
            });

            var headerView = new ListView();
            headerView.ItemTemplate = headers;

            var listViewTemplate = new DataTemplate(() =>
            {
                Grid grid = new Grid();
                var exchange = new Label();
                exchange.FontSize = 11;
                exchange.SetBinding(Label.TextProperty, "Exchange");
                var apiKeyName = new Label();
                apiKeyName.FontSize = 11;
                apiKeyName.SetBinding(Label.TextProperty, "ApiKeyName");
                var apiKey = new Label();
                apiKey.FontSize = 11;
                apiKey.SetBinding(Label.TextProperty, "ApiKeyShort");

                grid.Children.Add(exchange);
                grid.Children.Add(apiKeyName, 1, 0);
                grid.Children.Add(apiKey, 2, 0);

                return new ViewCell { View = grid };
            });

            var listView = new ListView
            {
                RowHeight = 14,
                SeparatorVisibility = SeparatorVisibility.Default,
                BackgroundColor = Color.White,
                SeparatorColor = Color.Blue
            };
            listView.ItemsSource = apiKeys;
            listView.ItemTemplate = listViewTemplate;
            listView.ItemSelected += OnListItemSelected;

            var stack = new StackLayout()
            {
                Orientation = StackOrientation.Vertical,
                Children = { btn, listView }
            };

            this.Content = stack;
        }

        async void OnNewApiButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new NewApiPage());
        }

        async void OnListItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem != null)
            {
                await Navigation.PushAsync(new ApiDetailPageCS()
                {
                    BindingContext = e.SelectedItem as Business.Contracts.Portfolio.Coin
                });
            }
        }
    }
}