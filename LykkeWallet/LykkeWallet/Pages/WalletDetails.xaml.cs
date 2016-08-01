using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LykkeWallet.ApiAccess;
using LykkeWallet.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LykkeWallet.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WalletDetails : ContentPage
    {
        private WalletDetailsPageViewModel ViewModel => walletDetailsPageViewModel;

        public WalletDetails()
        {
            InitializeComponent();
            ViewModel.WithdrawButtonVisible = false;
            ViewModel.DepositButtonVisible = false;
        }

        public void SetExternalData(decimal amount, string symbol)
        {
            ViewModel.Balance = amount;
            ViewModel.Symbol = symbol;
        }

        public void SetAsset(string assetId)
        {
            Task.Run(() =>
            {
                var asset = WalletApiSingleton.Instance.GetAsset(assetId).Result.Asset;
                Device.BeginInvokeOnMainThread(() =>
                {
                    ViewModel.Symbol = asset.Symbol;
                    ViewModel.DepositButtonVisible = !asset.HideDeposit;
                    ViewModel.WithdrawButtonVisible = !asset.HideWithdraw;
                });

                var data = new ObservableCollection<HistoryCellData>
                    {
                        new HistoryCellData {Action = "Sell", Date = "Yesterday", Amount = 6564m},
                        new HistoryCellData {Action = "Sell", Date = "Yesterday", Amount = 6564m},
                        new HistoryCellData {Action = "Sell", Date = "Yesterday", Amount = 6564m},
                        new HistoryCellData {Action = "Sell", Date = "Yesterday", Amount = 6564m},
                        new HistoryCellData {Action = "Sell", Date = "Yesterday", Amount = 6564m},
                        new HistoryCellData {Action = "Sell", Date = "Yesterday", Amount = 6564m},
                        new HistoryCellData {Action = "Sell", Date = "Yesterday", Amount = 6564m},
                        new HistoryCellData {Action = "Sell", Date = "Yesterday", Amount = 6564m},
                        new HistoryCellData {Action = "Sell", Date = "Yesterday", Amount = 6564m},
                        new HistoryCellData {Action = "Sell", Date = "Yesterday", Amount = 6564m},
                        new HistoryCellData {Action = "Sell", Date = "Yesterday", Amount = 6564m},
                        new HistoryCellData {Action = "Sell", Date = "Yesterday", Amount = 6564m},
                        new HistoryCellData {Action = "Sell", Date = "Yesterday", Amount = 6564m},
                        new HistoryCellData {Action = "Sell", Date = "Yesterday", Amount = 6564m},
                        new HistoryCellData {Action = "Sell", Date = "Yesterday", Amount = 6564m}
                    };
                Device.BeginInvokeOnMainThread(() =>
                {
                    ViewModel.HistoryData = data;
                });
            });
        }


        private async void OnWithdrawClicked(object sender, EventArgs e)
        {
            var withdrawAFundsAmountPage = new WithdrawFundsAmountPage();
            withdrawAFundsAmountPage.SetData(ViewModel.Symbol, ViewModel.Balance);
            await Navigation.PushAsync(withdrawAFundsAmountPage);
        }

        private void OnDepositClicked(object sender, EventArgs e)
        {
           
        }

        private void OnHistoryItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            //throw new NotImplementedException();
        }
    }
}
