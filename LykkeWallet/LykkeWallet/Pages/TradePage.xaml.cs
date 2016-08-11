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
    public enum TradeAction
    {
        Sell,
        Buy
    }
    public partial class TradePage : ContentPage
    {
        private TradePageViewModel ViewModel => tradePageViewModel;

        public TradePage(string baseAssetId, string assetPairId, TradeAction tradeAction, string baseAssetSymbol, string secondaryAssetSymbol, decimal ask, decimal bid)
        {
            InitializeComponent();

            actionPicker.SelectedIndexChanged += (sender, args) =>
            {
                ViewModel.SelectedAction = actionPicker.SelectedIndex == 0 ? "buy" : "sell";
            };

            ViewModel.BaseAsset = baseAssetId;
            ViewModel.AssetPair = assetPairId;
            ViewModel.BaseAssetSymbol = baseAssetSymbol;
            ViewModel.SecondaryAssetSymbol = secondaryAssetSymbol;
            ViewModel.Ask = ask;
            ViewModel.Bid = bid;

            actionPicker.SelectedIndex = tradeAction == TradeAction.Buy ? 0 : 1;
            //ViewModel.
        }

        private void OnSelectedIndexChanged()
        {

        }

        private async void OnCheckoutButtonClicked(object sender, EventArgs e)
        {
            try
            {
                var pk = LocalKeyStorageAccess.LocalKeyAccessSingleton.Instance.GetPrivateKey();
                var r = await WalletApiSingleton.Instance.PostPurchaseAsset(ViewModel.BaseAsset, ViewModel.AssetPair, ViewModel.TradeAction == TradeAction.Buy ? ViewModel.BuyValue : -1m * ViewModel.SellValue, ViewModel.Price, pk);
                var b = r.Trades;
            } catch(Exception ex)
            {
                var a = 234;
            }
        }
    }
}
