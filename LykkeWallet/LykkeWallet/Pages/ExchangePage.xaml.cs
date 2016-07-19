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

namespace LykkeWallet.Pages
{
    public class ExhcangeRateModel : INotifyPropertyChanged
    {
        private int _accuracy;
        private int _invertedAccuracy;

        public ExhcangeRateModel(string id, int accuracy, int invertedAccuracy)
        {
            Id = id;
            _accuracy = accuracy;
            _invertedAccuracy = invertedAccuracy;
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
        public decimal InvertedAsk
        {
            get { return _invertedAsk; }
            set
            {
                if (value != _invertedAsk)
                {
                    _invertedAsk = value;
                    OnPropertyChanged();
                }
            }
        }

        private decimal _invertedBid;
        public decimal InvertedBid
        {
            get { return _invertedBid; }
            set
            {
                if (value != _invertedBid)
                {
                    _invertedBid = value;
                    OnPropertyChanged();
                }
            }
        }

        private bool _isInverted;
        public bool IsInverted
        {
            get { return _isInverted; }
            set
            {
                if (value != _isInverted)
                {
                    _isInverted = value;
                    OnPropertyChanged();


                }
            }
        }

        private void ReEvaluate()
        {
            
        }


        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public partial class ExchangePage : ContentPage
    {
        private string _asset;
        public bool RefreshDataOnNextAppearing { set; get; }
        private List<Button> _assetsButtons;


        public ExchangePageViewModel ViewModel => exchangePageViewModel;
        public ExchangePage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if(RefreshDataOnNextAppearing)
                RefreshData();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            RefreshDataOnNextAppearing = true;
        }

        public void RefreshButtons()
        {
            Task.Run(
                () =>
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
            var selectedButton = (Button) sender;
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
            Task.Run(
                () =>
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

                        var m = new ExhcangeRateModel
                        {
                            Id = pair.Id,
                            AssetFrom = pair.Inverted ? pair.Id.Substring(3) : pair.Id.Substring(0, 3),
                            AssetTo = pair.Inverted ? pair.Id.Substring(0, 3) : pair.Id.Substring(3),
                            ExchangeRate = erParsed ? er : 0m,
                            Percentage = pParsed ? p : 0m,
                            Ask = askParsed ? ask : 0m,
                            Bid = bidParsed ? bid : 0m,
                            IsInverted = pair.Inverted
                        };
                        list.Add(m);
                    }
                    Device.BeginInvokeOnMainThread(() => ViewModel.ExchangeRates = list);
                    if (selectedAsset != null)
                        _asset = selectedAsset;

                });
        }

        private void OnPairSelected(object sender, ItemTappedEventArgs e)
        {
            var s = (ExhcangeRateModel)((ListView) sender).SelectedItem;

            var detailsPage = new ExchangeDetailsPage();
            detailsPage.SetAssetDescription(s.Id);
            detailsPage.SetAssetPairDetials(s.Id);
            detailsPage.SetPairDetail(s.Id, s.Ask, s.Bid, s.Percentage);
            Navigation.PushAsync(detailsPage);
        }
    }
}
