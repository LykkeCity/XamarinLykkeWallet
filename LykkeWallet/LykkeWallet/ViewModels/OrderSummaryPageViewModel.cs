using LykkeWallet.Annotations;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace LykkeWallet.ViewModels
{
    class OrderSummaryPageViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<TextCell> _listViewData;
        public ObservableCollection<TextCell> ListViewData
        {
            get { return _listViewData; }
            set
            {
                if(value != _listViewData)
                {
                    _listViewData = value;
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
