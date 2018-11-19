using Cryptobitfolio.Business.Contracts.Portfolio;
using Cryptobitfolio.Business.Contracts.Trade;
using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;

namespace Cryptobitfolio.UI.Coin
{
    public class CoinPageCS : ContentPage
    {
        public Business.Contracts.Portfolio.Coin _coin;
        public ListView coinLowerListView = new ListView
        {
            SeparatorVisibility = SeparatorVisibility.Default,
            BackgroundColor = Color.White,
            SeparatorColor = Color.Blue
        };

        //public CoinPageCS(Business.Contracts.Portfolio.Coin thisCoin) : this()
        //{
        //    _coin = thisCoin;
        //}

        public CoinPageCS()
        {
            Title = "Coin Info";

            var nameEntry = new Label
            {
                FontSize = 4
            };
            nameEntry.SetBinding(Label.TextProperty, "Currency.Name");

            var symbolEntry = new Label
            {
                FontSize = 4
            };
            symbolEntry.SetBinding(Label.TextProperty, "Currency.Symbol");

            var qtyEntry = new Label
            {
                FontSize = 4
            };
            qtyEntry.SetBinding(Label.TextProperty, "Quantity");

            var avBuyEntry = new Label
            {
                FontSize = 4
            };
            avBuyEntry.SetBinding(Label.TextProperty, "AverageBuyString");

            var cancelButton = new Button { Text = "Cancel" };
            cancelButton.Clicked += async (sender, e) =>
                        {
                            await Navigation.PopAsync();
                        };
            
            var trxButton = new Button
            {
                Text = "Txns",
                BackgroundColor = Color.White,
                TextColor = Color.Blue,
                WidthRequest = 110
            };
            trxButton.Clicked += (sender, e) =>
            {
                OnShowTransactions();
            };

            var watchListButton = new Button
            {
                Text = "Watch List",
                BackgroundColor = Color.Blue,
                TextColor = Color.White,
                WidthRequest = 110
            };
            watchListButton.Clicked += (sender, e) =>
            {
                OnShowWatchList();
            };

            StackLayout btn = new StackLayout()
            {
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                Orientation = StackOrientation.Horizontal,
                Children = { trxButton, watchListButton }
            };

            Content = new StackLayout
            {
                Margin = new Thickness(20),
                VerticalOptions = LayoutOptions.StartAndExpand,
                Children =
                {
                    new Label { Text = "Name", FontSize = 4 },
                    nameEntry,
                    new Label { Text = "Symbol", FontSize = 4 },
                    symbolEntry,
                    new Label { Text = "Quanity", FontSize = 4 },
                    qtyEntry,
                    new Label { Text = "Avg Buy", FontSize = 4 },
                    avBuyEntry,
                    new Label { Text= "WatchList", FontSize = 4 },
                    btn,
                    this.coinLowerListView,
                    cancelButton,
                }
            };
        }

        async protected override void OnAppearing()
        {
            base.OnAppearing();
            this._coin = (Business.Contracts.Portfolio.Coin)this.BindingContext;
            OnShowTransactions();
        }

        private void OnShowTransactions()
        {
            var transactions = App.GetTransactions(_coin.Currency.Symbol).Result;

            var transactionDT = new DataTemplate(() =>
            {
                Grid grid = new Grid();
                var exchange = new Label
                {
                    FontSize = 6
                };
                exchange.SetBinding(Label.TextProperty, "Exchange");
                var pair = new Label
                {
                    FontSize = 6
                };
                pair.SetBinding(Label.TextProperty, "Pair");
                var price = new Label
                {
                    FontSize = 6
                };
                price.SetBinding(Label.TextProperty, "Price");
                var quantity = new Label
                {
                    FontSize = 6
                };
                quantity.SetBinding(Label.TextProperty, "Quantity");
                var date = new Label
                {
                    FontSize = 6
                };
                date.SetBinding(Label.TextProperty, "FillDate");

                grid.Children.Add(exchange);
                grid.Children.Add(pair, 1, 0);
                grid.Children.Add(price, 2, 0);
                grid.Children.Add(quantity, 3, 0);
                grid.Children.Add(date, 4, 0);

                return new ViewCell { View = grid };
            });

            this.coinLowerListView.ItemsSource = transactions;
            this.coinLowerListView.ItemTemplate = transactionDT;
        }

        private void OnShowWatchList()
        {
            var watchListCoin = App.GetWatchListCoins(_coin.Currency.Symbol).Result;

            var watchers = watchListCoin.Watchers;

            var watchListDT = new DataTemplate(() =>
            {
                Grid grid = new Grid();
                var exchange = new Label
                {
                    FontSize = 6
                };
                exchange.SetBinding(Label.TextProperty, "Exchange");
                var pair = new Label
                {
                    FontSize = 6
                };
                pair.SetBinding(Label.TextProperty, "Pair");
                var price = new Label
                {
                    FontSize = 6
                };
                price.SetBinding(Label.TextProperty, "WatchPrice");
                var watchHit = new Label
                {
                    FontSize = 6
                };
                watchHit.SetBinding(Label.TextProperty, "WatchHit");

                grid.Children.Add(exchange);
                grid.Children.Add(pair, 1, 0);
                grid.Children.Add(price, 2, 0);
                grid.Children.Add(watchHit, 3, 0);

                return new ViewCell { View = grid };
            });

            this.coinLowerListView.ItemsSource = watchers;
            this.coinLowerListView.ItemTemplate = watchListDT;
        }
    }
}
