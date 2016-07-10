using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using LykkeWallet.Annotations;
using Xamarin.Forms;

namespace LykkeWallet.ViewModels
{
    class SelfiePickerPageViewModel : INotifyPropertyChanged
    {
        private ImageSource _imageSource;
        public ImageSource ImageSource
        {
            set
            {
                if (value != _imageSource)
                {
                    _imageSource = value;
                    OnPropertyChanged();
                }
            }
            get { return _imageSource; }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
