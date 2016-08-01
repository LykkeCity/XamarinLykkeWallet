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
    class AuthentificationPageViewModel : INotifyPropertyChanged
    {
        private string _email;
        private string _password;
        private bool _dataValid;

        public string Email
        {
            set
            {
                if (value != _email)
                {
                    _email = value;
                    OnPropertyChanged();
                }
            }
            get { return _email; }
        }
        public string Password
        {
            set
            {
                if (value != _password)
                {
                    _password = value;
                    OnPropertyChanged();
                    CheckIntegrity();
                }
            }
            get { return _password; }
        }

        public bool DataValid
        {
            set
            {
                if (value != _dataValid)
                {
                    _dataValid = value;
                    OnPropertyChanged();
                    CheckIntegrity();
                }
            }
            get { return _dataValid; }
        }

        private void CheckIntegrity()
        {
            if (string.IsNullOrEmpty(Email) || string.IsNullOrEmpty(Password))
                DataValid = false;
            else
                DataValid = true;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
