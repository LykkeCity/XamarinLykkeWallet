using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using LykkeWallet.Annotations;
using LykkeWallet.Pages;

namespace LykkeWallet.ViewModels
{
    class ExchangeDetailsPageViewModel : INotifyPropertyChanged
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

        private string _currentPeriod;
        public string CurrentPeriod
        {
            get { return _currentPeriod; }
            set
            {
                if (value != _currentPeriod)
                {
                    _currentPeriod = value;
                    OnPropertyChanged();
                }
            }
        }

        private ObservableCollection<AssetExchangeDetailModel> _assetDetails;
        public ObservableCollection<AssetExchangeDetailModel> AssetDetails
        {
            get { return _assetDetails; }
            set
            {
                if (value != _assetDetails)
                {
                    _assetDetails = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _pairId;
        public string PairId
        {
            get { return _pairId; }
            set
            {
                if (value != _pairId)
                {
                    _pairId = value;
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

        private decimal _change;
        public decimal Change
        {
            get { return _change; }
            set
            {
                if (value != _change)
                {
                    _change = value;
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

        private decimal _lastPrice;
        public decimal LastPrice
        {
            get { return _lastPrice; }
            set
            {
                if (value != _lastPrice)
                {
                    _lastPrice = value;
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

        private string _baseAsset;
        public string BaseAsset
        {
            get { return _baseAsset; }
            set
            {
                if(value != _baseAsset)
                {
                    _baseAsset = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _baseAssetSymbol;
        public string BaseAssetSymbol
        {
            get { return _baseAssetSymbol; }
            set
            {
                if (value != _baseAssetSymbol)
                {
                    _baseAssetSymbol = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _secondaryAssetSymbol;
        public string SecondaryAssetSymbol
        {
            get { return _secondaryAssetSymbol; }
            set
            {
                if (value != _secondaryAssetSymbol)
                {
                    _secondaryAssetSymbol = value;
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
}
