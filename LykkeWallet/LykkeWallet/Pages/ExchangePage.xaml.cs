using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Reflection;
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
    public class ExhcangeRateModel : INotifyPropertyChanged
    {
        private readonly int _invertedAccuracy;
        public int InvertedAccuracy => _invertedAccuracy;
        private readonly int _regularAccuracy;
        public int RegularAccuracy => _regularAccuracy;

        public ExhcangeRateModel(string id, bool isInverted, int regularAccuracy, int invertedAccuracy, decimal ask, decimal bid, decimal percentage)
        {
            Id = id;
            _invertedAccuracy = invertedAccuracy;
            _regularAccuracy = regularAccuracy;

            AssetFrom = isInverted ? id.Substring(3) : id.Substring(0, 3);
            AssetTo = isInverted ? id.Substring(0, 3) : id.Substring(3);

            if (isInverted)
            {
                _invertedAsk = ask;
                _invertedBid = bid;
                _invertedPercentage = percentage;
            }
            else
            {
                _regularAsk = ask;
                _regularBid = bid;
                _regularPercentage = percentage;
            }

            IsInverted = isInverted;

            Evaluate();
        }

        public string Id { internal set; get; }

        private string _assetFrom;
        public string AssetFrom
        {
            get { return _assetFrom; }
            set
            {
                if (value != _assetFrom)
                {
                    _assetFrom = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _assetTo;
        public string AssetTo
        {
            get { return _assetTo; }
            set
            {
                if (value != _assetTo)
                {
                    _assetTo = value;
                    OnPropertyChanged();
                }
            }
        }

        private decimal _percentage;
        public decimal Percentage
        {
            get { return _percentage; }
            set
            {
                if (value != _percentage)
                {
                    _percentage = value;
                    OnPropertyChanged();
                }
            }
        }

        private decimal _exchangeRate;
        public decimal ExchangeRate
        {
            get { return _exchangeRate; }
            set
            {
                if (value != _exchangeRate)
                {
                    _exchangeRate = value;
                    OnPropertyChanged();
                }
            }
        }

        private decimal _ask;
        public decimal Ask
        {
            get { return _ask; }
            set
            {
                if (value != _ask)
                {
                    _ask = value;
                    OnPropertyChanged();
                }
            }
        }

        private decimal _bid;
        public decimal Bid
        {
            get { return _bid; }
            set
            {
                if (value != _bid)
                {
                    _bid = value;
                    OnPropertyChanged();
                }

            }
        }

        private decimal _invertedAsk;

        private decimal _invertedBid;

        private decimal _regularAsk;

        private decimal _regularBid;

        private decimal _regularPercentage;

        private decimal _invertedPercentage;

        //private decimal _regularExchangeRate;

        //private decimal _invertedExchangeRate;

        private bool _isInverted;

        public bool IsInverted
        {
            get { return _isInverted; }
            set
            {
                if (value != _isInverted)
                {
                    _isInverted = value;
                    Invert();
                    OnPropertyChanged();

                }
            }
        }

        private void Invert()
        {
            Ask = IsInverted ? _invertedAsk : _regularAsk;
            Bid = IsInverted ? _invertedBid : _regularBid;
            Percentage = IsInverted ? _invertedPercentage : _regularPercentage;
            ExchangeRate = Ask;

            var temp = AssetFrom;
            AssetFrom = AssetTo;
            AssetTo = temp;
        }

        private void Evaluate()
        {
            if (IsInverted)
            {
                _regularAsk = _invertedAsk != 0m ? Math.Round(1m / _invertedAsk, _regularAccuracy) : 0m;

                _regularBid = _invertedBid != 0m ? Math.Round(1m / _invertedBid, _regularAccuracy) : 0m;

                var temp = _regularAsk;

                _regularAsk = _regularBid;

                _regularBid = temp;

                _regularPercentage = (1m / (_invertedPercentage / 100m + 1m) - 1m) * 100m; //TODO make sure _invertedPercentage != 0

                //_regularExchangeRate = _invertedExchangeRate != 0m ? Math.Round(1/ _invertedExchangeRate, _regularAccuracy) : 0m;
            }
            else
            {
                _invertedAsk = _regularAsk != 0m ? Math.Round(1m / _regularAsk, _invertedAccuracy) : 0m;

                _invertedBid = _regularBid != 0m ? Math.Round(1m / _regularBid, _invertedAccuracy) : 0m;

                var temp = _regularAsk;

                _regularAsk = _regularBid;

                _regularBid = temp;

                _invertedPercentage = (1m / (_regularPercentage / 100m + 1m) - 1m) * 100m; //TODO make sure _regularPercentage != 0

                //_invertedExchangeRate = _regularExchangeRate != 0m ? Math.Round(1 / _regularExchangeRate, _invertedAccuracy) : 0m;
            }

            Ask = IsInverted ? _invertedAsk : _regularAsk;

            Bid = IsInverted ? _invertedBid : _regularBid;

            Percentage = IsInverted ? _invertedPercentage : _regularPercentage;

            ExchangeRate = Ask;
        }


        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ExchangePage : ContentPage
    {
        private string _asset;
        private List<Button> _assetsButtons;

        public ExchangePageViewModel ViewModel => exchangePageViewModel;
        public ExchangePage()
        {
            InitializeComponent();
        }

        protected override void OnDisappearing()
        {
            exchageRatesListView.SelectedItem = null;
            base.OnDisappearing();
        }

        public void RefreshButtons()
        {
            Task.Run(() =>
            {
                _assetsButtons = new List<Button>();

                var result = WalletApiSingleton.Instance.GetAllBaseAssets().Result;
                foreach (var asset in result.Assets)
                {
                    var button = new Button
                    {
                        FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Button)),
                        HeightRequest = 50,
                        WidthRequest = 80,
                        Text = asset.Id,
                    };

                    _assetsButtons.Add(button);

                    var frame = new Frame
                    {
                        Content = button,
                        Padding = new Thickness(0, 0, 0, 10)
                    };

                    button.Clicked += OnAssetSelected;
                    Device.BeginInvokeOnMainThread(() => buttonsStack.Children.Add(frame));
                }
            });
        }

        private async void OnAssetSelected(object sender, EventArgs e)
        {
            var selectedButton = (Button)sender;
            var selectedAsset = selectedButton.Text;

            foreach (var button in _assetsButtons)
            {
                button.Opacity = 0.3;
                button.BackgroundColor = Color.Transparent;
            }

            selectedButton.Opacity = 1;
            selectedButton.BackgroundColor = Color.Default;

            if (_asset != selectedAsset)
            {
                await WalletApiSingleton.Instance.SetBaseAsset(selectedAsset);
                RefreshData(selectedAsset);
            }
        }


        public void RefreshData(string selectedAsset = null)
        {
            Debug.WriteLine("Fetching exchange data.......");
            Task.Run(() =>
            {
                try
                {
                    var list = new ObservableCollection<ExhcangeRateModel>();
                    var assetsPairs = WalletApiSingleton.Instance.GetAssetPairRates().Result;
                    foreach (var pair in assetsPairs.Rates)
                    {
                        decimal er;
                        bool erParsed = decimal.TryParse(pair.Ask, NumberStyles.Any, null, out er);
                        decimal p;
                        bool pParsed = decimal.TryParse(pair.PChng.ToString(), NumberStyles.Any, null, out p);
                        decimal ask;
                        bool askParsed = decimal.TryParse(pair.Ask, NumberStyles.Any, null, out ask);
                        decimal bid;
                        bool bidParsed = decimal.TryParse(pair.Bid, NumberStyles.Any, null, out bid);

                        var assetPair = WalletApiSingleton.Instance.GetAssetPair(pair.Id).Result.AssetPair;

                        var m = new ExhcangeRateModel(pair.Id, pair.Inverted, assetPair.Accuracy,
                            assetPair.InvertedAccuracy, askParsed ? ask : 0m, bidParsed ? bid : 0m, pParsed ? p : 0m);
                        list.Add(m);
                    }
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        ViewModel.ExchangeRates = list;
                        if (exchageRatesListView.IsRefreshing)
                            exchageRatesListView.EndRefresh();
                    });
                    if (selectedAsset != null)
                        _asset = selectedAsset;
                }
                catch (Exception ex)
                {
                    var a = 234;
                }
            });

        }

        private async void OnPairSelected(object sender, ItemTappedEventArgs e)
        {

            var s = (ExhcangeRateModel)((ListView)sender).SelectedItem;

            ((ListView)sender).SelectedItem = null;

            var detailsPage = new ExchangeDetailsPage();
            detailsPage.RefreshButtons();
            detailsPage.RefreshData(s.Id);
            detailsPage.SetAssetDescription(s.Id);
            await Navigation.PushAsync(detailsPage);
        }

        private void OnListRefreshed(object sender, EventArgs e)
        {
            RefreshData();
        }
    }
}
