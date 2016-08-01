using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace LykkeWallet.Converters
{
    public class MailExistsToButtonTextConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var status = (MailStatus)value;

            if (status == MailStatus.InvalidEmail || status == MailStatus.NewEmail)
                return "Sign up";
            return "Sign in";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}

namespace LykkeWallet
{
    public enum MailStatus
    {
        InvalidEmail = 0,
        NewEmail = 1,
        ExistingEmail = 2
    }

    public enum ApiServer
    {
        Test = 0,
        Dev = 1,
        Demo = 2
    }

    public static class ApiServerEnum
    {
        public static string GetFriendlyName(this ApiServer code)
        {
            switch (code)
            {
                case ApiServer.Demo:
                    return "lykke-api-demo";
                case ApiServer.Dev:
                    return "lykke-api-dev";
                case ApiServer.Test:
                    return "lykke-api-test";
            }
            return string.Empty;

        }
        public static string GetLink(this ApiServer code)
        {
            switch (code)
            {
                case ApiServer.Demo:
                    return "https://lykke-api-demo.azurewebsites.net/api/";
                case ApiServer.Dev:
                    return "https://lykke-api-dev.azurewebsites.net/api/";
                case ApiServer.Test:
                    return "https://lykke-api-test.azurewebsites.net/api/";
            }
            return string.Empty;
        }
    }

}