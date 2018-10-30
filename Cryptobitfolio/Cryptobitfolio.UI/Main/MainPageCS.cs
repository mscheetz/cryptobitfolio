using Cryptobitfolio.UI.Bot;
using Cryptobitfolio.UI.Exchange;
using Cryptobitfolio.UI.Portfolio;
using Cryptobitfolio.UI.Settings;
using System;
using Xamarin.Forms;

namespace Cryptobitfolio.UI.Main
{
    public class MainPageCS : TabbedPage
    {
        public MainPageCS()
        {
            var navigationPage = new NavigationPage(new PortfolioPageCS());
            navigationPage.Icon = "portfolio.png";
            navigationPage.Title = "Portfoio";

            Children.Add(new ExchangePageCS());
            Children.Add(new BotPageCS());
            Children.Add(navigationPage);
            Children.Add(new SettingsPage());
        }
    }
}