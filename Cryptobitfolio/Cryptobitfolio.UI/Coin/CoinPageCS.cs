using Xamarin.Forms;

namespace Cryptobitfolio.UI.Coin
{
    public class CoinPageCS : ContentPage
    {
        public CoinPageCS()
        {
            Title = "Coin Info";

            var nameEntry = new Entry();
            nameEntry.SetBinding(Entry.TextProperty, "Name");

            var symbolEntry = new Entry();
            symbolEntry.SetBinding(Entry.TextProperty, "Symbol");

            var qtyEntry = new Entry();
            qtyEntry.SetBinding(Entry.TextProperty, "Quanity");

            var avBuyEntry = new Entry();
            avBuyEntry.SetBinding(Entry.TextProperty, "Avg Buy");

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
                    new Label { Text = "Quanity" },
                    qtyEntry,
                    new Label { Text = "Avg Buy" },
                    avBuyEntry,
                    cancelButton,
                }
            };
        }
    }
}
