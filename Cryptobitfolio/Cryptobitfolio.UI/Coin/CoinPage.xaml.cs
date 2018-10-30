using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Cryptobitfolio.UI.Coin
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class CoinPage : ContentPage
	{
		public CoinPage ()
		{
			InitializeComponent ();
		}

        async void OnCancelClicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
	}
}