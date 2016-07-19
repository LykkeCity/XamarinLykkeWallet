using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
                var b = (Button) item;
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
            var b = (Button) sender;

            b.Opacity = 1;
            b.BackgroundColor = Color.Default;
        }

        public void SetPairDetail(string id, decimal ask, decimal bid, decimal changePercentage)
        {
            ViewModel.BuyAtPrice = ask;
            ViewModel.SellAtPrice = bid;
            ViewModel.ChangePercentage = changePercentage;
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

        public async void SetAssetPairDetials(string id, string period = "1M", int points = 20)
        {
            var data = await WalletApiSingleton.Instance.GetAssetPairDetailedRates(id, period, points);

            ViewModel.AssetPair = id;
            ViewModel.LastPrice = data.LastPrice;
        }
    }
}
