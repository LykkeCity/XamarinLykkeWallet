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
    class WalletPageViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<WalletGroup> _wallets;
        public ObservableCollection<WalletGroup> Wallets
        {
            get { return _wallets; }
            set
            {
                if (value != _wallets)
                {
                    _wallets = value;
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
