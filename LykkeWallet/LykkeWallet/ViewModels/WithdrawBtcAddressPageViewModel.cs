using LykkeWallet.Annotations;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace LykkeWallet.ViewModels
{
    class WithdrawBtcAddressPageViewModel : INotifyPropertyChanged
    {
        private string _assetId;
        public string AssetId
        {
            get { return _assetId; }
            set
            {
                if(value != _assetId)
                {
                    _assetId = value;
                    OnPropertyChanged();
                }
            }
        }

        private decimal _amount;
        public decimal Amount
        {
            get { return _amount; }
            set
            {
                if(value != _amount)
                {
                    _amount = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _address;
        public string Address
        {
            get { return _address; }
            set
            {
                if(value != _address)
                {
                    _address = value;
                    OnPropertyChanged();
                    Validate();
                }
            }
        }

        private bool _addressIsValid;
        public bool AddressIsValid
        {
            get { return _addressIsValid; }
            set
            {
                if(value != _addressIsValid)
                {
                    _addressIsValid = value;
                    OnPropertyChanged();
                }
            }
        }

        private void Validate()
        {
            AddressIsValid = !string.IsNullOrEmpty(Address.Trim());
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
