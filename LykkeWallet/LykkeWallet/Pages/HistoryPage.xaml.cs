using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LykkeWallet.ViewModels;
using Xamarin.Forms;

namespace LykkeWallet.Pages
{
    public class HistoryCellData
    {
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

            var data = new ObservableCollection<HistoryCellData>
                    {
                        new HistoryCellData {Action = "Sell", Date = "Yesterday", Amount = 6564m},
                        new HistoryCellData {Action = "Sell", Date = "Yesterday", Amount = 6564m},
                        new HistoryCellData {Action = "Sell", Date = "Yesterday", Amount = 6564m},
                        new HistoryCellData {Action = "Sell", Date = "Yesterday", Amount = 6564m},
                        new HistoryCellData {Action = "Sell", Date = "Yesterday", Amount = 6564m},
                        new HistoryCellData {Action = "Sell", Date = "Yesterday", Amount = 6564m}
                    };
            ViewModel.HistoryData = data;
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
                    ViewModel.HistoryData.Add(new HistoryCellData { Amount = 654, Action = "added during refresh", Date = DateTime.Now.ToString() });
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        if (historyListView.IsRefreshing)
                            historyListView.EndRefresh();
                    });
                });
        }

        private void OnHistoryRefresh(object sender, EventArgs e)
        {
            RefreshData();
        }

        private void OnHistoryItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            ((ListView)sender).SelectedItem = null;
        }
    }
}
