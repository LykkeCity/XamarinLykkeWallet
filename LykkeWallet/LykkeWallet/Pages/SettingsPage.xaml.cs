using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LykkeWallet.ApiAccess;
using LykkeWallet.LocalKeyStorageAccess;
using Xamarin.Forms;

namespace LykkeWallet.Pages
{
    public partial class SettingsPage : ContentPage
    {
        public SettingsPage()
        {
            InitializeComponent();
        }

        private void OnLogOutButtonClicked(object sender, EventArgs e)
        {
            var storage = new LocalKeyStorage();
            storage.Save(WalletApi.TokenName, null);
            Navigation.PopToRootAsync();
        }
    }
}
