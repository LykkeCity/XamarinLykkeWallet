using LykkeWallet.ApiAccess;
using LykkeWallet.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace LykkeWallet.Pages
{
    public partial class BlockchainExplorerPage : ContentPage
    {
        private BlockchainExplorerPageViewModel ViewModel => blockchainExplorerPageViewModel;

        public BlockchainExplorerPage()
        {
            InitializeComponent();
        }

        public void SetBlockchainInfo(string id)
        {
            Task.Run(() =>
            {
                var data = WalletApiSingleton.Instance.GetBcnTransaction(id).Result.Transaction;
                if( data != null )
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        ViewModel.Hash = data.Hash;
                        ViewModel.DateTime = data.Date;
                        ViewModel.Confirmations = data.Confirmations;
                        ViewModel.Block = data.Block;
                        ViewModel.Height = data.Height;
                        ViewModel.SenderId = data.SenderId;
                        ViewModel.AssetId = data.AssetId;
                        ViewModel.Quantity = data.Quantity;
                        ViewModel.Url = data.Url;
                    });
            });
        }
    }
}
