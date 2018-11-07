using Cryptobitfolio.Business.Contracts.Portfolio;
using Cryptobitfolio.UI.Coin;
using Cryptobitfolio.UI.WatchList;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Xamarin.Forms;

namespace Cryptobitfolio.UI.Portfolio
{
    public class PortfolioPageCS : ContentPage
    {
        public PortfolioPageCS()
        {
            Title = "Portfolio";

            var portfolioButton = new Button
            {
                Text = "Portfolio",
                BackgroundColor = Color.White,
                TextColor = Color.Blue,
                WidthRequest = 110,                
            };
            var watchListButton = new Button
            {
                Text = "Watch List",
                BackgroundColor = Color.Blue,
                TextColor = Color.White,
                WidthRequest = 110
            };

            watchListButton.Clicked += async (sender, e) =>
            {
                await Navigation.PushAsync(new WatchListCS());
            };

            StackLayout btn = new StackLayout()
            {
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                Orientation = StackOrientation.Horizontal,
                Children = { portfolioButton, watchListButton }
            };

            var coins = App.GetCoins().Result;
            
            var dt = new DataTemplate(() =>
            {
                Grid grid = new Grid();
                var symbol = new Label();
                symbol.SetBinding(Label.TextProperty, "Currency.Symbol");
                var avgPrice = new Label();
                avgPrice.SetBinding(Label.TextProperty, "AverageBuy");
                var percentDiff = new Label();
                percentDiff.SetBinding(Label.TextProperty, "PercentDiff");

                grid.Children.Add(symbol);
                grid.Children.Add(avgPrice, 1, 0);
                grid.Children.Add(percentDiff, 2, 0);
               
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
                await Navigation.PushAsync(new CoinPageCS(/*(Business.Contracts.Portfolio.Coin)e.SelectedItem*/)
                {
                    BindingContext = e.SelectedItem as Business.Contracts.Portfolio.Coin
                });
            }
        }
    }
}