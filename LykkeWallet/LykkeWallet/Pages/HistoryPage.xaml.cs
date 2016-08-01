using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LykkeWallet.ViewModels;
using Xamarin.Forms;
using LykkeWallet.ApiAccess;
using LykkeWallet.CustomUI;

namespace LykkeWallet.Pages
{
    public class HistoryCellData
    {
        public string Id { set; get; }
        public string Action { set; get; }
        public string Date { set; get; }
        public decimal Amount { set; get; }
    }

    public partial class HistoryPage : ContentPage
    {
        private HistoryPageViewModel ViewModel => historyPageViewModel;

        private object _storage;

        public HistoryPage()
        {
            InitializeComponent();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            // _storage = historyListView.SelectedItem;
            //historyListView.SelectedItem = null;
        }

        protected override void OnAppearing()
        {
            //historyListView.SelectedItem = _storage;
            base.OnAppearing();
        }


        public void RefreshData()
        {
            Task.Run(
                () =>
                {
                    var data = WalletApiSingleton.Instance.GetHistory().Result;
                    var collection = new ObservableCollection<HistoryCellData>();

                    foreach (var item in data)
                    {
                        try
                        {

                            var historyItem = new HistoryCellData();

                            historyItem.Id = item.Id;
                            var asset = string.Empty;
                            if (item.CashInOut != null)
                            {
                                //historyItem.Action = item.CashInOut;
                                historyItem.Amount = item.CashInOut.Amount;
                                var action = item.CashInOut.Amount > 0m ? "Cash In" : "Cash Out";
                                historyItem.Action = $"{item.CashInOut.Asset} {action}";
                            }
                            if (item.Trade != null)
                            {
                                //historyItem.Action = item.Trade.
                                historyItem.Amount = item.Trade.Volume;
                                var action = item.Trade.Volume > 0m ? "Buy" : "Sell";
                                historyItem.Action = $"{item.Trade.Asset} {action}";
                            }
                            if (item.Transfer != null)
                            {
                                historyItem.Amount = item.Transfer.Volume;
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
                        ViewModel.HistoryData = collection;
                        if (historyListView.IsRefreshing)
                            historyListView.EndRefresh();
                    });
                });
        }

        private void OnHistoryRefresh(object sender, EventArgs e)
        {
            RefreshData();
        }

        private async void OnHistoryItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var list = (ListView)sender;
            if (list.SelectedItem != null)
            {
                var selectedItem = (HistoryCellData)list.SelectedItem;
                list.SelectedItem = null;
                var blockchainExplorerPage = new BlockchainExplorerPage();
                blockchainExplorerPage.SetBlockchainInfo(selectedItem.Id);
                await Navigation.PushAsync(blockchainExplorerPage);
            }
        }
    }
}
