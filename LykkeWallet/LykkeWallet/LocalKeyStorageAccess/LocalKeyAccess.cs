using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LykkeWallet.LocalKeyStorageAccess
{
    public class LocalKeyAccess : ILocalKeyAccess
    {
        private ILocalKeyStorage _localKeyStorage;

        public LocalKeyAccess(ILocalKeyStorage localKeyStorage)
        {
            _localKeyStorage = localKeyStorage;
        }

        public void AddOrUpdateEmailKeyPair(string email, string pk)
        {
            _localKeyStorage.Save($"{email}{Constants.PK_MAIL_SUFFIX}", pk);
        }

        public void GetPrivateKey()
        {
            _localKeyStorage.Get(Constants.ENCODED_PK);
        }

        public string GetToken()
        {
            return _localKeyStorage.Get(Constants.SECURITY_TOKEN);
        }

        public void SavePrivateKey(string pk)
        {
            _localKeyStorage.Save(Constants.ENCODED_PK, pk);
        }

        public void SaveToken(string token)
        {
            _localKeyStorage.Save(Constants.SECURITY_TOKEN, token);
        }

    }
}
