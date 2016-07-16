using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LykkeWallet.ApiAccess;
using LykkeWallet.CustomUI;
using Xamarin.Forms;

namespace LykkeWallet.Pages
{

    public partial class WalletPage : ContentPage
    {
        public WalletPage()
        {
            InitializeComponent();

        }

        public void RefreshData()
        {
            Task.Run(
                () =>
                {
                    var wallets = WalletApiSingleton.Instance.GetWallets().Result;
                    var orderedWallets = wallets.Lykke.Assets.OrderBy(x => x.IssuerId).ThenBy(x => x.Id).ToList();
                    string currentIssuer = null;
                    foreach (var wallet in orderedWallets)
                    {
                        if (currentIssuer != wallet.IssuerId)
                        {
                            currentIssuer = wallet.IssuerId;
                            Device.BeginInvokeOnMainThread(() => tableView.Root.Add(new TableSection(wallet.IssuerId)));
                        }
                        var walletCell = new WalletCell()
                        {
                            Code = wallet.Id,
                            Symbol = wallet.Symbol,
                            Balance = wallet.Balance
                        };
                        walletCell.Tapped += OnWalletTapped;
                        Device.BeginInvokeOnMainThread(() => tableView.Root.LastOrDefault().Add(walletCell));
                    }
                });
        }

        private void OnWalletTapped(object sender, EventArgs e)
        {
            var c = (WalletCell)sender;
            Debug.WriteLine($"{c.Code}");
        }
    }

}
