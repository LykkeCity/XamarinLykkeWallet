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

namespace LykkeWallet.Pages
{

    public class AssetExchangeDetailModel
    {
        public string PrimaryText { set; get; }
        public string SecondaryText { set; get; }
        public ImageSource Image { set; get; }
    }

    public partial class ExchangeDetailsPage : ContentPage
    {
        private readonly List<Button> _periodButtons;

        private ExchangeDetailsPageViewModel ViewModel => exchangeDetailsPageViewModel;

        public ExchangeDetailsPage()
        {
            InitializeComponent();

            _periodButtons = new List<Button>();
            foreach (var item in periodsStack.Children)
            {
                var b = (Button)item;
                b.Clicked += NewPeriodSelected;
                _periodButtons.Add(b);
            }

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
                        bool askParsed = decimal.TryParse(assetPair.Ask, NumberStyles.Any, null, out ask);
                        decimal bid;
                        bool bidParsed = decimal.TryParse(assetPair.Bid, NumberStyles.Any, null, out bid);

                        ViewModel.PairId = id;
                        ViewModel.Ask = askParsed
                            ? Math.Round(ask, assetPair.Inverted ? assetPair2.InvertedAccuracy : assetPair2.Accuracy)
                            : 0m;
                        ViewModel.Bid = bidParsed
                            ? Math.Round(bid, assetPair.Inverted ? assetPair2.InvertedAccuracy : assetPair2.Accuracy)
                            : 0m;
                        ViewModel.Change =
                            decimal.Parse(
                                (assetPair3.Rate.ChngGrph.Last() - assetPair3.Rate.ChngGrph[assetPair3.Rate.ChngGrph.Count - 2]).ToString
                                    ());
                        ViewModel.Percentage = assetPair3.Rate.PChange;
                        ViewModel.LastPrice = assetPair3.LastPrice;
                    }
                    catch (Exception ex)
                    {
                        var a = 234;
                    }
                });
        }
    }
}
