using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using LykkeWallet.Annotations;

namespace LykkeWallet.ViewModels
{
    class FullNameEntryPageViewModel :INotifyPropertyChanged
    {
        private string _fullNameEntry;
        private bool _fullNameValid;

        public string FullNameEntry
        {
            set
            {
                if (value != _fullNameEntry)
                {
                    _fullNameEntry = value;
                    OnPropertyChanged();
                    CheckIntegrity();
                }
            }
            get { return _fullNameEntry; }
        }
        public bool FullNameValid
        {
            set
            {
                if (value != _fullNameValid)
                {
                    _fullNameValid = value;
                    OnPropertyChanged();
                }
            }
            get { return _fullNameValid; }
        }

        private void CheckIntegrity()
        {
            FullNameValid = !string.IsNullOrEmpty(FullNameEntry);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
