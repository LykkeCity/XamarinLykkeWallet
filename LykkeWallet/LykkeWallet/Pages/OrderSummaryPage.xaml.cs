using LykkeWallet.ApiAccess;
using LykkeWallet.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LykkeWallet.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OrderSummaryPage : ContentPage
    {
        private OrderSummaryPageViewModel ViewModel => orderSummaryPageViewModel;

        public OrderSummaryPage()
        {
            InitializeComponent();
        }

        public void SetItem(HistoryItemModel item)
        {
            if (item.CashInOut != null && string.IsNullOrEmpty(item.CashInOut.BlockChainHash))
            {
                SetCashInOut(item.CashInOut);
            }
            else if (item.Trade != null && string.IsNullOrEmpty(item.Trade.BlockChainHash))
            {
                SetTrade(item.Trade);
            }
            else if (item.Transfer != null && string.IsNullOrEmpty(item.Transfer.BlockChainHash))
            {

            }
            else if ((item.Transfer != null && !string.IsNullOrEmpty(item.Transfer.BlockChainHash)) || (item.Trade != null && !string.IsNullOrEmpty(item.Trade.BlockChainHash)) || (item.CashInOut != null && !string.IsNullOrEmpty(item.CashInOut.BlockChainHash)))
            {
                SetBlockchainInfo(item.Id);
            }
        }

        public void SetTrade(TransactionTradeModel model)
        {
            
            ViewModel.ListViewData = new ObservableCollection<TextCell>();

            bool isBuy = model.Volume > 0m;

            if (isBuy)
            {
                Title = $"{model.Asset} Buy";
            }
            else
            {
                Title = $"{model.Asset} Sell";
            }
            
            ViewModel.ListViewData.Add(new TextCell { Text = model.MarketOrder.AssetPair, Detail = "Asset name" });
            ViewModel.ListViewData.Add(new TextCell { Text = model.Volume.ToString(), Detail = "Sell" });
            ViewModel.ListViewData.Add(new TextCell { Text = Math.Round(model.MarketOrder.Price, model.MarketOrder.Accuracy).ToString(), Detail = "Execution price" });
            ViewModel.ListViewData.Add(new TextCell { Text = model.MarketOrder.TotalCost.ToString(), Detail = "Buy" });
            ViewModel.ListViewData.Add(new TextCell { Text = "In progress", Detail = "Blockchain settlement" });


        }        

        public void SetCashInOut(TransactionsCashInOutModel model)
        {
            ViewModel.ListViewData = new ObservableCollection<TextCell>();

            bool isCashIn = model.Amount > 0m;

            if(isCashIn)
            {
                Title = $"{model.Asset} Wallet Cash In";
            }
            else
            {
                Title = $"{model.Asset} Wallet Cash Out";
            }

            ViewModel.ListViewData.Add(new TextCell { Text = model.Amount.ToString(), Detail = "Amount" });
            ViewModel.ListViewData.Add(new TextCell { Text = "In progress", Detail = "Blockchain settlement" });
            ViewModel.ListViewData.Add(new TextCell { Text = model.Asset, Detail = "Asset name" });

        }

        public void SetBlockchainInfo(string id)
        {
            Task.Run(() =>
            {
                var data = WalletApiSingleton.Instance.GetBcnTransaction(id).Result.Transaction;
                if (data != null)
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        Title = "Blockchain Explorer";

                        ViewModel.ListViewData = new ObservableCollection<TextCell>();
                        ViewModel.ListViewData.Add(new TextCell { Text = data.Hash, Detail = "Hash" });
                        ViewModel.ListViewData.Add(new TextCell { Text = data.Date.ToString(), Detail = "Date" });
                        ViewModel.ListViewData.Add(new TextCell { Text = data.Confirmations.ToString(), Detail = "Confirm." });
                        ViewModel.ListViewData.Add(new TextCell { Text = data.Block, Detail = "Block" });
                        ViewModel.ListViewData.Add(new TextCell { Text = data.Height, Detail = "Height" });
                        ViewModel.ListViewData.Add(new TextCell { Text = data.SenderId, Detail = "Send ID" });
                        ViewModel.ListViewData.Add(new TextCell { Text = data.AssetId, Detail = "Asset ID" });
                        ViewModel.ListViewData.Add(new TextCell { Text = data.Quantity, Detail = "Quantity" });
                        ViewModel.ListViewData.Add(new TextCell { Text = data.Url, Detail = "URL" });
                    });
            });
        }
    }
}
