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
            var selectedAsset = ((Button)sender).Text;
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
                        bool pParsed = decimal.TryParse(pair.Ask, NumberStyles.Any, null, out p);
                        list.Add(new ExhcangeRateModel
                        {
                            AssetFrom = pair.Id.Substring(0, 3),
                            AssetTo = pair.Id.Substring(3),
                            ExchangeRate = erParsed ? er : 0m,
                            Percentage = pParsed ? p : 0m
                        });
                    }
                    Device.BeginInvokeOnMainThread(() => ViewModel.ExchangeRates = list);
                    if (selectedAsset != null)
                        _asset = selectedAsset;

                });
        }

        private void OnPairSelected(object sender, ItemTappedEventArgs e)
        {
            Navigation.PushAsync(new ExchangeDetailsPage());
        }
    }
}
