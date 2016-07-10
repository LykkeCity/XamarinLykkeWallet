using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LykkeWallet.ApiAccess;
using LykkeWallet.LocalKeyStorageAccess;
using LykkeWallet.Pages;
using Xamarin.Forms;

namespace LykkeWallet
{
    public class App : Application
    {
        public App()
        {
            var localStorage = new LocalKeyStorage();
            localStorage.Save(WalletApi.TokenName, null);
            MainPage = new NavigationPage(new LoginPage());
        }

        protected override void OnStart()
        {

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
