using Xamarin.Forms;

namespace Cryptobitfolio.UI.Views
{
    public class WatchCoinCS : ContentPage
    {
        public WatchCoinCS()
        {
            BindingContext = new Business.Contracts.Portfolio.WatchList();
            Title = "Watch Coin Info";

            var nameEntry = new Entry();
            nameEntry.SetBinding(Entry.TextProperty, "Name");

            var symbolEntry = new Entry();
            symbolEntry.SetBinding(Entry.TextProperty, "Symbol");

            var watchPrice = new Entry();
            watchPrice.SetBinding(Entry.TextProperty, "WatchPrice");

            var setDate = new Entry();
            setDate.SetBinding(Entry.TextProperty, "DateAdded");

            var hitDate = new Entry();
            hitDate.SetBinding(Entry.TextProperty, "WatchHit");

            var cancelButton = new Button { Text = "Cancel" };
            cancelButton.Clicked += async (sender, e) =>
            {
                await Navigation.PopAsync();
            };

            Content = new StackLayout
            {
                Margin = new Thickness(20),
                VerticalOptions = LayoutOptions.StartAndExpand,
                Children =
                {
                    new Label { Text = "Name" },
                    nameEntry,
                    new Label { Text = "Symbol" },
                    symbolEntry,
                    new Label { Text = "Watch Price" },
                    watchPrice,
                    new Label { Text = "Created" },
                    setDate,
                    new Label { Text = "Hit" },
                    hitDate,
                    cancelButton,
                }
            };
        }
    }
}
