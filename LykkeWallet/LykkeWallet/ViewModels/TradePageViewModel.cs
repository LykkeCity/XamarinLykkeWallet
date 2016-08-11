using LykkeWallet.Annotations;
using LykkeWallet.Pages;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace LykkeWallet.ViewModels
{
    class TradePageViewModel : INotifyPropertyChanged
    {
        private IEnumerable<string> _actionsList;
        public IEnumerable<string> ActionsList
        {
            get { return _actionsList; }
            set
            {
                if (value != _actionsList)
                {
                    _actionsList = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _selectedAction;
        public string SelectedAction
        {
            get { return _selectedAction; }
            set
            {
                if (value != _selectedAction)
                {
                    _selectedAction = value;
                    OnPropertyChanged();
                    if (_selectedAction.ToLower().Contains("sell"))
                    {
                        this.TradeAction = TradeAction.Sell;
                        SellSymbol = BaseAssetSymbol;
                        BuySymbol = SecondaryAssetSymbol;
                        Price = Bid;
                        SellValue = 0m;
                    }
                    else
                    {
                        this.TradeAction = TradeAction.Buy;
                        SellSymbol = SecondaryAssetSymbol;
                        BuySymbol = BaseAssetSymbol;
                        Price = Ask;
                        SellValue = 0m;
                    }
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

        private string _sellSymbol;
        public string SellSymbol
        {
            get { return _sellSymbol; }
            set
            {
                if (value != _sellSymbol)
                {
                    _sellSymbol = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _buySymbol;
        public string BuySymbol
        {
            get { return _buySymbol; }
            set
            {
                if (value != _buySymbol)
                {
                    _buySymbol = value;
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

        private TradeAction _tradeAction;
        public TradeAction TradeAction
        {
            get { return _tradeAction; }
            set
            {
                if (value != _tradeAction)
                {
                    _tradeAction = value;
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
                if (value != _baseAsset)
                {
                    _baseAsset = value;
                    OnPropertyChanged();
                    ActionsList = new List<string> { $"Buy {value}", $"Sell {value}" };
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

        private decimal _price;
        public decimal Price
        {
            get { return _price; }
            set
            {
                if (value != _price)
                {
                    _price = value;
                    OnPropertyChanged();
                }
            }
        }

        private decimal _sellValue;
        public decimal SellValue
        {
            get { return _sellValue; }
            set
            {
                if(value != _sellValue)
                {
                    _sellValue = value;
                    OnPropertyChanged();
                    BuyValue = SellValue / Price;
                }
            }
        }


        private decimal _buyValue;
        public decimal BuyValue
        {
            get { return _buyValue; }
            set
            {
                if (value != _buyValue)
                {
                    _buyValue = value;
                    OnPropertyChanged();
                    SellValue = BuyValue * Price;
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
