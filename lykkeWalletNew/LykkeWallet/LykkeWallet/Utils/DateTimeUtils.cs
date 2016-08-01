using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LykkeWallet.Utils
{
    public static class DateTimeUtils
    {
        public const string IsoDateTimeMask = "yyyy-MM-dd HH:mm:ss";

        public const string IsoDateMask = "yyyy-MM-dd";

        public static string ToIsoDateTime(this DateTime dateTime)
        {
            return dateTime.ToString(IsoDateTimeMask);
        }
    }


}
