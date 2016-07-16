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
    public class ExchangePageViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<string> _baseAssets;
        private ObservableCollection<ExhcangeRateModel> _exchangeRates;

        public ObservableCollection<string> BaseAssets
        {
            get { return _baseAssets; }
            set
            {
                if (_baseAssets != value)
                {
                    _baseAssets = value;
                    OnPropertyChanged();
                }
            }
        }
        public ObservableCollection<ExhcangeRateModel> ExchangeRates
        {
            get { return _exchangeRates; }
            set
            {
                if (_exchangeRates != value)
                {
                    _exchangeRates = value;
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
