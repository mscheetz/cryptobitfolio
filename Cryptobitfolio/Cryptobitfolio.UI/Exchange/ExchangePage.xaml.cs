using System.Collections.ObjectModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Cryptobitfolio.UI.Exchange
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ExchangePage : ContentPage
    {
        public ObservableCollection<string> Items { get; set; }

        public ExchangePage()
        {
            InitializeComponent();

            Items = new ObservableCollection<string>
            {
                "Binance",
                "Bittrex",
                "Coinbase Pro",
                "Idex",
                "KuCoin"
            };

            MyListView.ItemsSource = Items;
        }

        async void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item == null)
                return;

            await DisplayAlert("Clicked", "You clicked an exchange.", "OK");

            //Deselect Item
            ((ListView)sender).SelectedItem = null;
        }
    }
}
