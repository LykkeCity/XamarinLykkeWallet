using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace LykkeWallet.Pages
{
    public partial class MainTabbedPage : TabbedPage
    {
        public MainTabbedPage(bool requirePin)
        {
            if (requirePin)
            {
                Navigation.PushModalAsync(new PinEntryPage(this));
            }
            InitializeComponent();

        }

        public void InitializeChildren()
        {
            try
            {
                var walletPage = new WalletPage();
                walletPage.RefreshData();

                var exchangePage = new ExchangePage();
                exchangePage.RefreshButtons();
                exchangePage.RefreshData();
                exchangePage.RefreshDataOnNextAppearing = false;

                var settingsPage = new SettingsPage();
                settingsPage.RefreshData();

                Children.Add(walletPage);
                Children.Add(exchangePage);
                Children.Add(settingsPage);
            }
            catch (Exception ex)
            {
                var a = 33;;
            }
        }

        protected override bool OnBackButtonPressed()
        {
            return base.OnBackButtonPressed();
        }
    }
}
