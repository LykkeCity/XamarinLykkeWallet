using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LykkeWallet.LocalKeyStorageAccess;

namespace LykkeWallet.ApiAccess
{
    public static class WalletApiSingleton
    {
        private static readonly WalletApi _instance = new WalletApi(LocalKeyAccessSingleton.Instance);
        
        static WalletApiSingleton()
        {
        }

        public static WalletApi Instance => _instance;
    }
}
