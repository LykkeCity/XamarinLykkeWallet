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
    class WalletDetailsPageViewModel : INotifyPropertyChanged
    {
        private string _assetId;
        public string AssetId
        {
            get { return _assetId; }
            set
            {
                if (value != _assetId)
                {
                    _assetId = value;
                    OnPropertyChanged();
                }
            }
        }

        private decimal _balance;
        public decimal Balance
        {
            get { return _balance; }
            set
            {
                if (value != _balance)
                {
                    _balance = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _symbol;
        public string Symbol
        {
            get { return _symbol; }
            set
            {
                if (value != _symbol)
                {
                    _symbol = value;
                    OnPropertyChanged();
                }
            }
        }

        private bool _depositButtonVisible;
        public bool DepositButtonVisible
        {
            get { return _depositButtonVisible; }
            set
            {
                if (value != _depositButtonVisible)
                {
                    _depositButtonVisible = value;
                    OnPropertyChanged();
                }
            }
        }

        private bool _withdrawButtonVisible;
        public bool WithdrawButtonVisible
        {
            get { return _withdrawButtonVisible; }
            set
            {
                if (value != _withdrawButtonVisible)
                {
                    _withdrawButtonVisible = value;
                    OnPropertyChanged();
                }
            }
        }

        private ObservableCollection<HistoryCellData> _historyData;
        public ObservableCollection<HistoryCellData> HistoryData
        {
            get { return _historyData; }
            set
            {
                if (_historyData != value)
                {
                    _historyData = value;
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
