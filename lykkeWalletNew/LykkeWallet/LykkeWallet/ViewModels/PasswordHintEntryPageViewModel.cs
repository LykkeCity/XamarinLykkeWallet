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
    public class PasswordHintEntryPageViewModel : INotifyPropertyChanged
    {
        private string _passwordHintEntry;
        private bool _hintValid;

        public string PasswordHintEntry
        {
            set
            {
                if (value != _passwordHintEntry)
                {
                    _passwordHintEntry = value;
                    OnPropertyChanged();
                    CheckIntegrity();
                }
            }
            get { return _passwordHintEntry; }
        }
        public bool HintValid
        {
            set
            {
                if (value != _hintValid)
                {
                    _hintValid = value;
                    OnPropertyChanged();
                }
            }
            get { return _hintValid; }
        }

        private void CheckIntegrity()
        {
            HintValid = !string.IsNullOrEmpty(PasswordHintEntry);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
