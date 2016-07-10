namespace LykkeWallet.LocalKeyStorageAccess
{
    public class LocalKeyStorage : ILocalKeyStorage
    {
        public void Save(string field, string value)
        {
            Settings.GenericStorageSet(field, value);
        }

        public string Get(string field)
        {
            var result = Settings.GenericStorageGet(field);
            return result == string.Empty ? null : result;
        }
    }
}