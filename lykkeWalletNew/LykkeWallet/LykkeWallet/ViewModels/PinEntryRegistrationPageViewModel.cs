using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using LykkeWallet.Annotations;

namespace LykkeWallet.ViewModels
{
    class PinEntryRegistrationPageViewModel : INotifyPropertyChanged
    {
        private string _pinEntry;
        private string _pinRepeatEntry;
        private bool _pinValid;

        public string PinEntry
        {
            set
            {
                if (value != _pinEntry)
                {
                    _pinEntry = value;
                    OnPropertyChanged();
                    CheckIntegrity();
                }
            }
            get { return _pinEntry; }
        }
        public string PinRepeatEntry
        {
            set
            {
                if (value != _pinRepeatEntry)
                {
                    _pinRepeatEntry = value;
                    OnPropertyChanged();
                    CheckIntegrity();
                }
            }
            get { return _pinRepeatEntry; }
        }
        public bool PinValid
        {
            set
            {
                if (value != _pinValid)
                {
                    _pinValid = value;
                    OnPropertyChanged();
                }
            }
            get { return _pinValid; }
        }

        private void CheckIntegrity()
        {
            PinValid = !(string.IsNullOrEmpty(PinEntry) || string.IsNullOrEmpty(PinRepeatEntry) ||
                              PinEntry.Length != 4 || PinRepeatEntry.Length != 4 ||
                              PinEntry != PinRepeatEntry);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
