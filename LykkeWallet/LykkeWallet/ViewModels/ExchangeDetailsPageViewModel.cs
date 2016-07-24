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


        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
