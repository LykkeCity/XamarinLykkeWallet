using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using LykkeWallet.Annotations;
using LykkeWallet.ApiAccess;
using LykkeWallet.ViewModels;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LykkeWallet.Pages
{
    public class AssetExchangeDetailModel
    {
        public string PrimaryText { set; get; }
        public string SecondaryText { set; get; }
        public ImageSource Image { set; get; }
    }

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ExchangeDetailsPage : ContentPage
    {
        private List<Button> _periodButtons;

        private ExchangeDetailsPageViewModel ViewModel => exchangeDetailsPageViewModel;

        public ExchangeDetailsPage()
        {
            InitializeComponent();

        }

        public void SetExternalData(string baseAsset)
        {
            ViewModel.BaseAsset = baseAsset;
        }

        public void SetSymbols(string ba, string sa)
        {
            Task.Run(() =>
            {
                ViewModel.BaseAssetSymbol = WalletApiSingleton.Instance.GetAsset(ba).Result.Asset.Symbol;
                ViewModel.SecondaryAssetSymbol = WalletApiSingleton.Instance.GetAsset(sa).Result.Asset.Symbol;
            });
        }

        private void NewPeriodSelected(object sender, EventArgs e)
        {
            foreach (var button in _periodButtons)
            {
                button.Opacity = 0.3;
                button.BackgroundColor = Color.Transparent;
            }
            var b = (Button)sender;

            b.Opacity = 1;
            b.BackgroundColor = Color.Default;
        }

        public async void RefreshButtons()
        {
            var periods = await WalletApiSingleton.Instance.GetGraphPeriods();
            _periodButtons = new List<Button>();
            foreach (var item in periods.AvailablePeriods)
            {
                var b = new Button { Text = item.Name, HorizontalOptions = LayoutOptions.Fill, };
                b.Clicked += NewPeriodSelected;
                _periodButtons.Add(b);
                periodsStack.Children.Add(b);
            }
        }

        public async void SetAssetDescription(string id)
        {
            try
            {
                var data = await WalletApiSingleton.Instance.GetAssetDescription(id);

                ViewModel.AssetDetails = new ObservableCollection<AssetExchangeDetailModel>
                {
                    new AssetExchangeDetailModel
                    {
                        PrimaryText = data.AssetClass,
                        SecondaryText = "Asset class"
                    },
                    new AssetExchangeDetailModel
                    {
                        PrimaryText = data.Issuer,
                        SecondaryText = "Issuer name"
                    },
                    new AssetExchangeDetailModel
                    {
                        PrimaryText = data.Description,
                        SecondaryText = "Description"
                    },
                    new AssetExchangeDetailModel
                    {
                        PrimaryText = data.NumberOfCoins,
                        SecondaryText = "Number of coins issued"
                    },
                    new AssetExchangeDetailModel
                    {
                        PrimaryText = data.PopIndex.ToString(),
                        SecondaryText = "Popularity index"
                    },
                    new AssetExchangeDetailModel
                    {
                        PrimaryText = data.MarketCapitalization,
                        SecondaryText = "Market capitalization"
                    },
                    new AssetExchangeDetailModel
                    {
                        PrimaryText = data.AssetDescriptionUrl,
                        SecondaryText = "Description URL"
                    }
                };

            }
            catch (Exception ex)
            {
                var a = 234;
            }
        }
        public void RefreshData(string id, string period = "1M", int points = 20)
        {

            Task.Run(
                () =>
                {
                    try
                    {
                        var assetPair = WalletApiSingleton.Instance.GetAssetPairRates(id).Result.Rate;

                        var assetPair2 = WalletApiSingleton.Instance.GetAssetPair(id).Result.AssetPair;

                        var assetPair3 = WalletApiSingleton.Instance.GetAssetPairDetailedRates(id, period, points).Result;

                        decimal ask;
                        bool askParsed = decimal.TryParse(assetPair.Inverted ? assetPair.Bid : assetPair.Ask, NumberStyles.Any, null, out ask);
                        decimal bid;
                        bool bidParsed = decimal.TryParse(assetPair.Inverted ? assetPair.Ask : assetPair.Bid, NumberStyles.Any, null, out bid);

                        ViewModel.PairId = id;
                        ViewModel.AssetFrom = assetPair.Inverted ? id.Substring(3) : id.Substring(0, 3);
                        ViewModel.AssetTo = assetPair.Inverted ? id.Substring(0, 3) : id.Substring(3);
                        ViewModel.Ask = askParsed
                            ? Math.Round(assetPair.Inverted ? 1/bid : ask, assetPair.Inverted ? assetPair2.InvertedAccuracy : assetPair2.Accuracy)
                            : 0m;
                        ViewModel.Bid = bidParsed
                            ? Math.Round(assetPair.Inverted ? 1 / ask : bid, assetPair.Inverted ? assetPair2.InvertedAccuracy : assetPair2.Accuracy)
                            : 0m;
                        var b = ((assetPair.Inverted ? 1.0 / assetPair3.Rate.ChngGrph.Last() : assetPair3.Rate.ChngGrph.Last()) - (assetPair.Inverted ? 1.0 / assetPair3.Rate.ChngGrph[assetPair3.Rate.ChngGrph.Count - 2] : assetPair3.Rate.ChngGrph[assetPair3.Rate.ChngGrph.Count - 2])).ToString();
                        ViewModel.Change =
                            Math.Round(Convert.ToDecimal(
                                ((assetPair.Inverted ? 1.0 / assetPair3.Rate.ChngGrph.Last() : assetPair3.Rate.ChngGrph.Last()) - (assetPair.Inverted ? 1.0 / assetPair3.Rate.ChngGrph[assetPair3.Rate.ChngGrph.Count - 2] : assetPair3.Rate.ChngGrph[assetPair3.Rate.ChngGrph.Count - 2]))),
                                    assetPair.Inverted ? assetPair2.InvertedAccuracy : assetPair2.Accuracy);
                        ViewModel.Percentage = assetPair3.Rate.PChange == 0m ? 0m : (assetPair.Inverted ? (1m / (assetPair3.Rate.PChange / 100m + 1m) - 1m) * 100m : assetPair3.Rate.PChange);
                        ViewModel.LastPrice = Math.Round(assetPair.Inverted ? 1m / assetPair3.LastPrice : assetPair3.LastPrice, assetPair.Inverted ? assetPair2.InvertedAccuracy : assetPair2.Accuracy);
                        ViewModel.IsInverted = assetPair.Inverted;
                    }
                    catch (Exception ex)
                    {
                        var a = 234;
                    }
                });
        }

        private async void InvertPair(object sender, EventArgs e)
        {
            invertToolbarItem.Clicked -= InvertPair;
            try
            {
                await WalletApiSingleton.Instance.PostInvertAssetPair(ViewModel.PairId, !ViewModel.IsInverted);
                RefreshData(ViewModel.PairId);
            }
            catch (Exception ex)
            {
                var a = 234;
            }

            invertToolbarItem.Clicked += InvertPair;
        }


        private async void OnBuyClicked(object sender, EventArgs e)
        {
            var p = new TradePage(ViewModel.BaseAsset, ViewModel.PairId, TradeAction.Buy, ViewModel.BaseAssetSymbol, ViewModel.SecondaryAssetSymbol, ViewModel.Ask, ViewModel.Bid);
            await Navigation.PushAsync(p);
        }


        private void OnSellClicked(object sender, EventArgs e)
        {
        }
    }
}
