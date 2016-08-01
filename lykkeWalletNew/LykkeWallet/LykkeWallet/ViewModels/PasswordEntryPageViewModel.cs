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
    class PasswordEntryPageViewModel : INotifyPropertyChanged
    {
        private string _passwordEntry;
        private string _passwordRepeatEntry;
        private bool _passwordValid;

        public string PasswordEntry
        {
            set
            {
                if (value != _passwordEntry)
                {
                    _passwordEntry = value;
                    OnPropertyChanged();
                    CheckIntegrity();
                }
            }
            get { return _passwordEntry; }
        }
        public string PasswordRepeatEntry
        {
            set
            {
                if (value != _passwordRepeatEntry)
                {
                    _passwordRepeatEntry = value;
                    OnPropertyChanged();
                    CheckIntegrity();
                }
            }
            get { return _passwordRepeatEntry; }
        }
        public bool PasswordValid
        {
            set
            {
                if (value != _passwordValid)
                {
                    _passwordValid = value;
                    OnPropertyChanged();
                }
            }
            get { return _passwordValid; }
        }

        private void CheckIntegrity()
        {
            PasswordValid = !(string.IsNullOrEmpty(PasswordEntry) || string.IsNullOrEmpty(PasswordRepeatEntry) ||
                              PasswordEntry.Length < 6 || PasswordRepeatEntry.Length < 6 ||
                              PasswordEntry != PasswordRepeatEntry);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
