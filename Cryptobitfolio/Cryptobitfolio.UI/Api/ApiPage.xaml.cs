using Cryptobitfolio.Business.Contracts.Trade;
using Cryptobitfolio.UI.NewApi;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Cryptobitfolio.UI.Api
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ApiPage : ContentPage
    {
        public ApiPage()
        {
            InitializeComponent();
        }

        async void OnItemAdded(object sender, ItemTappedEventArgs e)
        {
            App.Current.MainPage = new NavigationPage();
            await App.Current.MainPage.Navigation.PushAsync(new NewApiPageCS
            {
                BindingContext = new ExchangeApi()
            });
        }

        async void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if(e.SelectedItem != null)
            {
                await Navigation.PushAsync(new NewApiPageCS
                {
                    BindingContext = e.SelectedItem as ExchangeApi
                });
            }
        }
    }
}
