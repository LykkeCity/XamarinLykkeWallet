using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LykkeWallet.LocalKeyStorageAccess
{
    public interface ILocalKeyAccess
    {
        void SaveToken(string token);
        string GetToken();
        void AddOrUpdateEmailKeyPair(string email, string pk);
        void SavePrivateKey(string pk);
        void GetPrivateKey();
    }
}
