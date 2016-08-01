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
    class BlockchainExplorerPageViewModel : INotifyPropertyChanged
    {
        private string _hash;
        public string Hash
        {
            get { return _hash; }
            set
            {
                if(value != _hash)
                {
                    _hash = value;
                    OnPropertyChanged();
                }
            }
        }

        public DateTime _dateTime;
        public DateTime DateTime
        {
            get { return _dateTime; }
            set
            {
                if (value != _dateTime)
                {
                    _dateTime = value;
                    OnPropertyChanged();
                }
            }
        }

        public int _confirmations;
        public int Confirmations
        {
            get { return _confirmations; }
            set
            {
                if (value != _confirmations)
                {
                    _confirmations = value;
                    OnPropertyChanged();
                }
            }
        }

        public string _block;
        public string Block
        {
            get { return _block; }
            set
            {
                if (value != _block)
                {
                    _block = value;
                    OnPropertyChanged();
                }
            }
        }

        public string _height;
        public string Height
        {
            get { return _height; }
            set
            {
                if (value != _height)
                {
                    _height = value;
                    OnPropertyChanged();
                }
            }
        }

        public string _senderId;
        public string SenderId
        {
            get { return _senderId; }
            set
            {
                if (value != _senderId)
                {
                    _senderId = value;
                    OnPropertyChanged();
                }
            }
        }

        public string _assetId;
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

        public string _quantity;
        public string Quantity
        {
            get { return _quantity; }
            set
            {
                if (value != _quantity)
                {
                    _quantity = value;
                    OnPropertyChanged();
                }
            }
        }

        public string _url;
        public string Url
        {
            get { return _url; }
            set
            {
                if (value != _url)
                {
                    _url = value;
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
