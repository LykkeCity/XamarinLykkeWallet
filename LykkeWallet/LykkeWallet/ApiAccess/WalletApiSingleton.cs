using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LykkeWallet.ApiAccess
{
    public sealed class WalletApiSingleton
    {
        private static readonly WalletApi instance = new WalletApi();

        // Explicit static constructor to tell C# compiler
        // not to mark type as beforefieldinit
        static WalletApiSingleton()
        {
        }

        private WalletApiSingleton()
        {
        }

        public static WalletApi Instance
        {
            get
            {
                return instance;
            }
        }
    }
}
