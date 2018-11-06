using Cryptobitfolio.Business.Contracts.Portfolio;
using Cryptobitfolio.UI.Portfolio;
using Cryptobitfolio.UI.WatchCoin;
using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;

namespace Cryptobitfolio.UI.WatchList
{
    public class WatchListCS : ContentPage
    {
        public WatchListCS(string thisSymbol = null)
        {

            Title = "Portfolio";

            var portfolioButton = new Button
            {
                Text = "Portfolio",
                BackgroundColor = Color.Blue,
                TextColor = Color.White,
                WidthRequest = 110
            };

            portfolioButton.Clicked += async (sender, e) =>
            {
                await Navigation.PushAsync(new PortfolioPageCS());
            };

            var watchListButton = new Button
            {
                Text = "Watch List",
                BackgroundColor = Color.White,
                TextColor = Color.Blue,
                WidthRequest = 110
            };

            StackLayout btn = new StackLayout()
            {
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                Orientation = StackOrientation.Horizontal,
                Children = { portfolioButton, watchListButton }
            };

            var coins = new List<WatchListCoin>
            {
                new WatchListCoin { Id = 1, Name = "Bitcoin", Symbol = "BTC", Watchers = new Watcher[] { new Watcher { DateAdded = DateTime.UtcNow.AddDays(-1), Enabled = true, Exchange = Business.Entities.Exchange.Binance, WatchListId = 1, WatchPrice = 6199.0M, Pair = "BTCUSDT" } }},
                new WatchListCoin { Id = 2, Name = "Ethereum", Symbol = "ETH", Watchers = new Watcher[] { new Watcher { DateAdded = DateTime.UtcNow.AddDays(-1), Enabled = true, Exchange = Business.Entities.Exchange.Binance, WatchListId = 2, WatchPrice = 185.0M, Pair = "ETHUSDT" } }},
                new WatchListCoin { Id = 3, Name = "Stellar", Symbol = "XLM", Watchers = new Watcher[] { new Watcher { DateAdded = DateTime.UtcNow.AddDays(-4), Enabled = true, Exchange = Business.Entities.Exchange.Binance, WatchListId = 3, WatchPrice = 0.00002900M, Pair = "XLMBTC", WatchHit = DateTime.UtcNow.AddHours(-20) } }},
                new WatchListCoin { Id = 4, Name = "NEO", Symbol = "NEO", Watchers = new Watcher[] { new Watcher { DateAdded = DateTime.UtcNow.AddDays(-2), Enabled = true, Exchange = Business.Entities.Exchange.Binance, WatchListId = 4, WatchPrice = 0.001999M, Pair = "NEOBTC" } }},
            };

            if (thisSymbol != null)
            {
                coins = coins.Where(c => c.Symbol.Equals(thisSymbol)).ToList();
            }

            var dt = new DataTemplate(() =>
            {
                Grid grid = new Grid();
                var symbol = new Label();
                symbol.SetBinding(Label.TextProperty, "Symbol");
                var watchPrice = new Label();
                watchPrice.SetBinding(Label.TextProperty, "WatchPrice[0]");
                var watchHit = new Label();
                watchHit.SetBinding(Label.TextProperty, "WatchHit[0]");

                grid.Children.Add(symbol);
                grid.Children.Add(watchPrice, 1, 0);
                grid.Children.Add(watchHit, 2, 0);

                return new ViewCell { View = grid };
            });

            var listView = new ListView
            {
                SeparatorVisibility = SeparatorVisibility.Default,
                BackgroundColor = Color.White,
                SeparatorColor = Color.Blue
            };
            listView.ItemsSource = coins;
            listView.ItemTemplate = dt;
            listView.ItemSelected += OnListItemSelected;

            var stack = new StackLayout()
            {
                Orientation = StackOrientation.Vertical,
                Children = { btn, listView }
            };

            this.Content = stack;
        }

        async void OnListItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem != null)
            {
                await Navigation.PushAsync(new WatchCoinCS
                {
                    BindingContext = e.SelectedItem as WatchListCoin
                });
            }
        }
    }
}
