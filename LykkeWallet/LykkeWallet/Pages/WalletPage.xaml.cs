using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LykkeWallet.ApiAccess;
using LykkeWallet.CustomUI;
using LykkeWallet.ViewModels;
using Xamarin.Forms;

namespace LykkeWallet.Pages
{

    public class WalletGroup : ObservableCollection<WalletCell>
    {
        public string Name { get; private set; }
        public string ShortName { get; private set; }

        public WalletGroup(string name, string shortName)
        {
            Name = name;
            ShortName = shortName;
        }
    }

    public partial class WalletPage : ContentPage
    {
        private WalletPageViewModel ViewModel => walletPageViewModel;

        public WalletPage()
        {
            InitializeComponent();

        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
        }

        public void RefreshData()
        {
            Task.Run(() =>
            {
                Device.BeginInvokeOnMainThread(() => ViewModel.Wallets = new ObservableCollection<WalletGroup>());

                var wallets = WalletApiSingleton.Instance.GetWallets().Result;
                var orderedWallets = wallets.Lykke.Assets.OrderBy(x => x.IssuerId).ThenBy(x => x.Id).ToList();
                string currentIssuer = null;
                foreach (var wallet in orderedWallets)
                {
                    if (currentIssuer != wallet.IssuerId)
                    {
                        currentIssuer = wallet.IssuerId;
                        Device.BeginInvokeOnMainThread(() => ViewModel.Wallets.Add(new WalletGroup(wallet.IssuerId, "")));
                    }
                    var walletCell = new WalletCell()
                    {
                        Code = wallet.Id,
                        Symbol = wallet.Symbol,
                        Balance = Math.Round(wallet.Balance, wallet.Accuracy)
                    };
                    Device.BeginInvokeOnMainThread(() => ViewModel.Wallets.LastOrDefault().Add(walletCell));
                }
            });
                    
        }

        private void OnWalletTapped(object sender, SelectedItemChangedEventArgs e)
        {
            var list = (ListView) sender;
            list.SelectedItem = null;
        }
    }

}
