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
                var wallets = WalletApiSingleton.Instance.GetWallets().Result;
                Device.BeginInvokeOnMainThread(() => ViewModel.Wallets = new ObservableCollection<WalletGroup>());
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
                Device.BeginInvokeOnMainThread(() =>
                {
                    if (walletsListView.IsRefreshing)
                        walletsListView.EndRefresh();
                });
            });
                    
        }

        private async void OnWalletTapped(object sender, SelectedItemChangedEventArgs e)
        {
            var list = (ListView) sender;
            if (list.SelectedItem != null)
            {
                var selectedWallet = (WalletCell) list.SelectedItem;
                list.SelectedItem = null;
                var walletDetailsPage = new WalletDetails();
                walletDetailsPage.SetExternalData(selectedWallet.Balance, selectedWallet.Symbol);
                walletDetailsPage.SetAsset(selectedWallet.Code);
                await Navigation.PushAsync(walletDetailsPage);
            }
        }

        private void OnListRefreshed(object sender, EventArgs e)
        {
            RefreshData();
        }
    }
}
