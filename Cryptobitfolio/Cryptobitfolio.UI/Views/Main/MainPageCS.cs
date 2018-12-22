using Xamarin.Forms;

namespace Cryptobitfolio.UI.Views
{
    public class MainPageCS : TabbedPage
    {
        public MainPageCS()
        {
            var navigationPage = new NavigationPage(new PortfolioPageCS());
            //navigationPage.Icon = "portfolio.png";
            navigationPage.Title = "Portfoio";

            Children.Add(navigationPage);
            Children.Add(new ExchangePage());
            Children.Add(new BotPage());
            Children.Add(new SettingsPage());
        }
    }
}