using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LykkeWallet.LocalKeyStorageAccess
{
    class Constants
    {
        public const string PASSWORD = "PASSWORD_TEMP_STORAGE";
        public const string ENCODED_PK = "ENCODED_PK";
        public const string PK_MAIL_SUFFIX = "_PK_MAIL_SUFFIX";
        public const string SECURITY_TOKEN = "SECURITY_TOKEN";

        public class UserAgent
        {
            public const string DEVICE_TYPE = "Android";
            public const string APP_VERSION = "0.0.1";
            public const string CLIENT_FEATURES = "1";
        }
    }
}
