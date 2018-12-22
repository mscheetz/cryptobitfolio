using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;
using Xamarin.Forms.Xaml;

namespace Cryptobitfolio.UI.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : Xamarin.Forms.TabbedPage
    {
        public MainPage()
        {
            On<Android>().SetToolbarPlacement(ToolbarPlacement.Bottom);
                //.SetBarItemColor(Color.Black)
                //.SetBarSelectedItemColor(Color.Green);

            InitializeComponent();
        }
    }
}