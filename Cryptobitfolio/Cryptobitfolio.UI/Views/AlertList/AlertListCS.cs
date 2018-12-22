using System.Linq;
using Xamarin.Forms;

namespace Cryptobitfolio.UI.Views
{
    public class AlertListCS : ContentPage
    {
        public AlertListCS(string thisSymbol = null)
        {

            Title = "Alert List";

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
                Text = "Alert List",
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

            var coins = App.GetWatchListCoins().Result;

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
                    BindingContext = e.SelectedItem as Business.Contracts.Portfolio.WatchList
                });
            }
        }
    }
}
