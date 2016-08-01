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
    public class PhoneConfirmationPageViewModel : INotifyPropertyChanged
    {
        private string _phone;

        public string Phone
        {
            set
            {
                if (value != _phone)
                {
                    _phone = value;
                    OnPropertyChanged();
                }
            }
            get { return _phone; }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
