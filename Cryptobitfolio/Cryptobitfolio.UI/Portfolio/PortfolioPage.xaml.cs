using Cryptobitfolio.Business.Contracts.Portfolio;
using Cryptobitfolio.UI.Coin;
using Cryptobitfolio.UI.WatchList;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Cryptobitfolio.UI.Portfolio
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PortfolioPage : ContentPage
    {
        public ObservableCollection<Business.Contracts.Portfolio.Coin> Items { get; set; }

        public PortfolioPage()
        {
            InitializeComponent();

            Items = new ObservableCollection<Business.Contracts.Portfolio.Coin>
            {
                new Business.Contracts.Portfolio.Coin{ Id=0, Currency = new Currency{ Name = "Bitcoin", Symbol = "BTC" }, Quantity = 0.005M, AverageBuy = 6312.78M, CurrentPrice = 6379.44M, Percent24Hr = 0.47, Percent7D = -0.5 },
                new Business.Contracts.Portfolio.Coin{ Id=1, Currency = new Currency{ Name = "Ethereum", Symbol = "ETH"}, Quantity = 1.5M, AverageBuy = 198.4M, CurrentPrice = 242.46M, Percent24Hr = 1.7, Percent7D = 2.5  },
                new Business.Contracts.Portfolio.Coin{ Id=2, Currency = new Currency{ Name = "Stellar", Symbol = "XLM"}, Quantity = 700, AverageBuy = 0.00000942M, CurrentPrice = 0.00001042M, Percent24Hr = 0.00, Percent7D = -1.5 },
                new Business.Contracts.Portfolio.Coin{ Id=3, Currency = new Currency{ Name = "NEO", Symbol = "NEO"}, Quantity = 30, AverageBuy = 0.0021M, CurrentPrice = 0.0019M, Percent24Hr = -7.47, Percent7D = -0.5  },
                new Business.Contracts.Portfolio.Coin{ Id=4, Currency = new Currency{ Name = "Polymath", Symbol = "POLY"}, Quantity = 1000M, AverageBuy = 0.00000274M, CurrentPrice = 0.00000570M, Percent24Hr = 20.47, Percent7D = 10.5  },
            };

            coinList.ItemsSource = Items;
        }

        async void OnViewWatchList(object sender, SelectedItemChangedEventArgs e)
        {
            await App.Current.MainPage.Navigation.PushAsync(new WatchListCS());
        }

        async void OnListItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem != null)
            {
                await Navigation.PushAsync(new CoinPage
                {
                    BindingContext = e.SelectedItem as Business.Contracts.Portfolio.Coin
                });
            }
        }

        async void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item == null)
                return;

            await DisplayAlert("Item Tapped", "An item was tapped.", "OK");

            //Deselect Item
            ((ListView)sender).SelectedItem = null;
        }
    }
}
