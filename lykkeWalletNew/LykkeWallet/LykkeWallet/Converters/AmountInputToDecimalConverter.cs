using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace LykkeWallet.Converters
{
    class AmountInputToDecimalConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value.ToString() == "0" ? string.Empty : value.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            decimal er;
            bool erParsed = decimal.TryParse(((string)value).Replace(",", ""), NumberStyles.Any, null, out er);
            return erParsed ? er : 0m;
        }
    }
}
