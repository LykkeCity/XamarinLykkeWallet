using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using LykkeWallet.Annotations;
using LykkeWallet.Pages;

namespace LykkeWallet.ViewModels
{
    class PhoneEntryPageViewModel : INotifyPropertyChanged
    {
        private CountryCode _currentCountryCode;
        private string _phone;
        private bool _phoneValid;

        public CountryCode CurrentCountryCode
        {
            set
            {
                if (value != _currentCountryCode)
                {
                    _currentCountryCode = value;
                    OnPropertyChanged();
                }
            }
            get { return _currentCountryCode; }
        }

        public string Phone
        {
            set
            {
                if (value != _phone)
                {
                    _phone = value;
                    OnPropertyChanged();
                    CheckIntegrity();
                }
            }
            get { return _phone; }
        }
        public bool PhoneValid
        {
            set
            {
                if (value != _phoneValid)
                {
                    _phoneValid = value;
                    OnPropertyChanged();
                    CheckIntegrity();
                }
            }
            get { return _phoneValid; }
        }

        private void CheckIntegrity()
        {
            PhoneValid = !string.IsNullOrEmpty(Phone);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
