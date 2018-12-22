using Autofac;
using Cryptobitfolio.UI.ViewModels;
using Xamarin.Forms;

namespace Cryptobitfolio.UI.Views
{
    public class PortfolioPageCS : ContentPage
    {
        private readonly PortfolioViewModel _portfolioVM;

        public PortfolioPageCS() : this(App.AppInstance._container.Resolve<PortfolioViewModel>())
        {

        }

        public PortfolioPageCS(PortfolioViewModel portfolioVM)
        {
            this._portfolioVM = portfolioVM;

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

//            var coins = App.GetCoins().Result;
            var coins = _portfolioVM.GetCoins().Result;

            var headers = new DataTemplate(() =>
            {
                Grid grid = new Grid();
                var symbol = new Label();
                symbol.Text = "Coin";
                var avgPrice = new Label();
                avgPrice.Text = "Avg Price";
                var percentDiff = new Label();
                percentDiff.Text = "% Change";

                grid.Children.Add(symbol);
                grid.Children.Add(avgPrice, 1, 0);
                grid.Children.Add(percentDiff, 2, 0);

                return new ViewCell { View = grid };
            });

            var headerView = new ListView();
            headerView.ItemTemplate = headers;

            var listViewTemplate = new DataTemplate(() =>
            {
                Grid grid = new Grid();
                var symbol = new Label();
                symbol.FontSize = 11;
                symbol.SetBinding(Label.TextProperty, "Currency.Symbol");
                var avgPrice = new Label();
                avgPrice.FontSize = 11;
                avgPrice.SetBinding(Label.TextProperty, "AverageBuyString");
                var percentDiff = new Label();
                percentDiff.FontSize = 11;
                percentDiff.SetBinding(Label.TextProperty, "PercentDiff");

                grid.Children.Add(symbol);
                grid.Children.Add(avgPrice, 1, 0);
                grid.Children.Add(percentDiff, 2, 0);
               
                return new ViewCell { View = grid };
            });

            var listView = new ListView
            {
                RowHeight = 14,
                SeparatorVisibility = SeparatorVisibility.Default,
                BackgroundColor = Color.White,
                SeparatorColor = Color.Blue
            };
            listView.ItemsSource = coins;
            listView.ItemTemplate = listViewTemplate;
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