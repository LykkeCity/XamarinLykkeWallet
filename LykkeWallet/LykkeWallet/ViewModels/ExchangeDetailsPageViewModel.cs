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
        public string Id { set; get; }

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

        private string _assetPair;
        public string AssetPair
        {
            get { return _assetPair; }
            set
            {
                if (value != _assetPair)
                {
                    _assetPair = value;
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

        private decimal _changeAmount;
        public decimal ChangeAmount
        {
            get { return _changeAmount; }
            set
            {
                if (value != _changeAmount)
                {
                    _changeAmount = value;
                    OnPropertyChanged();
                }
            }
        }

        private decimal _changePercentage;
        public decimal ChangePercentage
        {
            get { return _changePercentage; }
            set
            {
                if (value != _changePercentage)
                {
                    _changePercentage = value;
                    OnPropertyChanged();
                }
            }
        }

        private decimal _sellAtPrice;
        public decimal SellAtPrice
        {
            get { return _sellAtPrice; }
            set
            {
                if (value != _sellAtPrice)
                {
                    _sellAtPrice = value;
                    OnPropertyChanged();
                }
            }
        }

        private decimal _buyAtPrice;
        public decimal BuyAtPrice
        {
            get { return _buyAtPrice; }
            set
            {
                if (value != _buyAtPrice)
                {
                    _buyAtPrice = value;
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
