using Cryptobitfolio.Business;
using Cryptobitfolio.Business.Common;
using Cryptobitfolio.UI.Main;
using Xamarin.Forms;

namespace Cryptobitfolio.UI
{
    public class App : Application
    {
        //private static IExchangeBuilder _exchangeBldr;

        public App()//IExchangeBuilder exchangeBldr)
        {
            //_exchangeBldr = exchangeBldr;
            MainPage = new MainPage();
        }

        //public static IExchangeBuilder ExchangeBuilder
        //{
        //    get
        //    {
        //        return _exchangeBldr;
        //    }
        //}

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}