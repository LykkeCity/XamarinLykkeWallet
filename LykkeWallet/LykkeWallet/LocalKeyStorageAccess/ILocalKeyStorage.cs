using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LykkeWallet.LocalKeyStorageAccess
{
    public interface ILocalKeyStorage
    {
        void Save(string field, string value);
        string Get(string field);
    }
}
