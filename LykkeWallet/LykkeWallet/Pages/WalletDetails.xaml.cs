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

        private List<HistoryItemModel> _fullList;
        private bool _isActivityRunning;

        private void SetActivityRunning(bool b)
        {
            activityIndicatorFrame.IsVisible = b;
            activityIndicator.IsVisible = b;
            activityIndicator.IsRunning = b;
            _isActivityRunning = b;
        }

        public WalletDetails()
        {
            InitializeComponent();
            ViewModel.WithdrawButtonVisible = false;
            ViewModel.DepositButtonVisible = false;
        }

        public void SetExternalData(string id, decimal amount, string symbol)
        {
            ViewModel.AssetId = id;
            ViewModel.Balance = amount;
            ViewModel.Symbol = symbol;
        }

        public void SetAsset(string assetId)
        {
            
            Task.Run(() =>
            {
                Device.BeginInvokeOnMainThread(() => SetActivityRunning(true));
                var asset = WalletApiSingleton.Instance.GetAsset(assetId).Result.Asset;
                Device.BeginInvokeOnMainThread(() =>
                {
                    ViewModel.Symbol = asset.Symbol;
                    ViewModel.DepositButtonVisible = !asset.HideDeposit;
                    ViewModel.WithdrawButtonVisible = !asset.HideWithdraw;
                });

                var data = WalletApiSingleton.Instance.GetHistory(assetId).Result;
                _fullList = data;
                var collection = new ObservableCollection<HistoryCellData>();

                foreach (var item in data)
                {
                    try
                    {
                        var historyItem = new HistoryCellData();

                        historyItem.Id = item.Id;

                        if (item.CashInOut != null)
                        {
                            //historyItem.Action = item.CashInOut;
                            historyItem.Amount = item.CashInOut.Amount;
                            historyItem.Done = !string.IsNullOrEmpty(item.CashInOut.BlockChainHash);
                            var action = item.CashInOut.Amount > 0m ? "Cash In" : "Cash Out";
                            historyItem.Action = $"{item.CashInOut.Asset} {action}";
                        }
                        if (item.Trade != null)
                        {
                            //historyItem.Action = item.Trade.
                            historyItem.Amount = item.Trade.Volume;
                            historyItem.Done = !string.IsNullOrEmpty(item.Trade.BlockChainHash);
                            var action = item.Trade.Volume > 0m ? "Buy" : "Sell";
                            historyItem.Action = $"{item.Trade.Asset} {action}";
                        }
                        if (item.Transfer != null)
                        {
                            historyItem.Amount = item.Transfer.Volume;
                            historyItem.Done = !string.IsNullOrEmpty(item.Transfer.BlockChainHash);
                            var action = item.Transfer.Volume > 0m ? "Income" : "Outcome";
                            historyItem.Action = $"{item.Transfer.Asset} {action}";
                        }
                        if (item.CashOutAttemp != null)
                        {
                            historyItem.Amount = item.CashOutAttemp.Volume;
                        }
                        if (item.CashOutCancelled != null)
                        {
                            historyItem.Amount = item.CashOutCancelled.Volume;
                        }
                        if (item.CashOutDone != null)
                        {
                            historyItem.Amount = item.CashOutDone.Volume;
                        }

                        historyItem.Date = item.DateTime.ToString();

                        collection.Add(historyItem);
                    }
                    catch (Exception ex)
                    {
                        var a = 234;
                    }
                }

                Device.BeginInvokeOnMainThread(() =>
                {
                    SetActivityRunning(false);
                    ViewModel.HistoryData = collection;
                    if (historyListView.IsRefreshing)
                        historyListView.EndRefresh();
                });
            });
        }


        private async void OnWithdrawClicked(object sender, EventArgs e)
        {
            Page page = new Page();

            switch (ViewModel.AssetId)
            {
                case "BTC":
                    var withdrawAFundsAmountPage = new WithdrawFundsAmountPage();
                    withdrawAFundsAmountPage.SetData(ViewModel.AssetId, ViewModel.Symbol, ViewModel.Balance);
                    page = withdrawAFundsAmountPage;
                    break;
            }

            await Navigation.PushAsync(page);
        }

        private async void OnDepositClicked(object sender, EventArgs e)
        {
            //Page depositPage;
            switch(ViewModel.AssetId)
            {
                case "BTC":
                    var depositPage = new DepositBtcPage(LocalKeyStorageAccess.LocalKeyAccessSingleton.Instance.GetPrivateKey());
                    await Navigation.PushAsync(depositPage);
                    break;
            }
            
        }

        private async void OnHistoryItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var list = (ListView)sender;
            list.ItemSelected -= OnHistoryItemSelected;

            if (list.SelectedItem != null)
            {
                var selectedItem = (HistoryCellData)list.SelectedItem;
                var item = _fullList.FirstOrDefault(x => x.Id == selectedItem.Id);
                var orderPage = new OrderSummaryPage();
                orderPage.SetItem(item);
                await Navigation.PushAsync(orderPage);

            }
            list.SelectedItem = null;
            list.ItemSelected += OnHistoryItemSelected;
        }
    }
}
