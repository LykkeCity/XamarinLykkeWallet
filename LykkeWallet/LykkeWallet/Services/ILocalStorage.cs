using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LykkeWallet.Services
{
    public interface ILocalStorage
    {
        void Save(string field, string value);
        string Get(string field);
    }
}
