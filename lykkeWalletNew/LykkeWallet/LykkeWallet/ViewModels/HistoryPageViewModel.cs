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
    class HistoryPageViewModel : INotifyPropertyChanged
    {
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
