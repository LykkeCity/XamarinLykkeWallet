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
    class WithdrawFundsAmountPageViewModel : INotifyPropertyChanged
    {
        private string _symbol;
        public string Symbol
        {
            get { return _symbol; }
            set
            {
                if(value != _symbol)
                {
                    _symbol = value;
                    OnPropertyChanged();
                }
            }
        }

        private decimal _availableAmount;
        public decimal AvailableAmount
        {
            get { return _availableAmount; }
            set
            {
                if(_availableAmount != value)
                {
                    _availableAmount = value;
                    OnPropertyChanged();
                }
            }
        }

        private decimal _enteredAmount;
        public decimal EnteredAmount
        {
            get { return _enteredAmount; }
            set
            {
                if (_enteredAmount != value)
                {
                    _enteredAmount = value;
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
