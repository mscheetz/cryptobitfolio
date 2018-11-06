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

            var coins = new List<Business.Contracts.Portfolio.Coin>
            {
                new Business.Contracts.Portfolio.Coin{ Id=0, Currency = new Currency{ Name = "Bitcoin", Symbol = "BTC" }, Quantity = 0.005M, AverageBuy = 6312.78M, CurrentPrice = 6379.44M, Percent24Hr = 0.47, Percent7D = -0.5 },
                new Business.Contracts.Portfolio.Coin{ Id=1, Currency = new Currency{ Name = "Ethereum", Symbol = "ETH"}, Quantity = 1.5M, AverageBuy = 198.4M, CurrentPrice = 242.46M, Percent24Hr = 1.7, Percent7D = 2.5  },
                new Business.Contracts.Portfolio.Coin{ Id=2, Currency = new Currency{ Name = "Stellar", Symbol = "XLM"}, Quantity = 700, AverageBuy = 0.00000942M, CurrentPrice = 0.00001042M, Percent24Hr = 0.00, Percent7D = -1.5 },
                new Business.Contracts.Portfolio.Coin{ Id=3, Currency = new Currency{ Name = "NEO", Symbol = "NEO"}, Quantity = 30, AverageBuy = 0.0021M, CurrentPrice = 0.0019M, Percent24Hr = -7.47, Percent7D = -0.5  },
                new Business.Contracts.Portfolio.Coin{ Id=4, Currency = new Currency{ Name = "Polymath", Symbol = "POLY"}, Quantity = 1000M, AverageBuy = 0.00000274M, CurrentPrice = 0.00000570M, Percent24Hr = 20.47, Percent7D = 10.5  },
            };
            
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