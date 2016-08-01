using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LykkeWallet.LocalKeyStorageAccess
{
    public static class LocalKeyAccessSingleton
    {
        private static readonly LocalKeyAccess _instance = new LocalKeyAccess(new LocalKeyStorage());

        static LocalKeyAccessSingleton()
        {
        }

        public static LocalKeyAccess Instance => _instance;
    }
}
