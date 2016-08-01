using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using LykkeWallet.Annotations;
using LykkeWallet.ApiAccess;
using LykkeWallet.Utils;

namespace LykkeWallet.ViewModels
{
    public class LoginPageViewModel : INotifyPropertyChanged
    {
        public LoginPageViewModel()
        {
            var tempList = (from ApiServer server in Enum.GetValues(typeof(ApiServer)) select server.GetFriendlyName()).ToList();
            _apiServerList = tempList;
        }

        private MailStatus _mailStatus;
        private ApiServer _apiServer;
        private IEnumerable<string> _apiServerList;

        public IEnumerable<string> ApiServerList
        {
            get { return _apiServerList; }
        }
        public MailStatus MailStatus
        {
            set
            {
                if (value != _mailStatus)
                {
                    _mailStatus = value;
                    OnPropertyChanged();
                }
            }
            get
            {
                return _mailStatus;
            }
        }

        public ApiServer ApiServer
        {
            set
            {
                if (value != _apiServer)
                {
                    _apiServer = value;

                    OnPropertyChanged();
                }
            }
            get
            {
                return _apiServer;
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
