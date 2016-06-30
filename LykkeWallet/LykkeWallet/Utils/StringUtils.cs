using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;

namespace LykkeWallet.Utils
{
    public static class StringUtils
    {

        public static bool IsValidEmail(this string email)
        {
            if (string.IsNullOrEmpty(email))
                return false;

            email = email.Trim();

            if (email[email.Length - 1] == '.')
                return false;

            var lines = email.Split('@');

            if (lines.Length != 2)
                return false;

            if (lines[0].Trim() == "" || lines[1].Trim() == "")
                return false;

            if (lines[0].Contains(" ") || lines[1].Contains(" "))
                return false;

            var lines2 = lines[1].Split('.');

            return lines2.Length >= 2;
        }

        private static string ConvertParamToString(object value, Type type)
        {
            if (type == typeof(DateTime))
                return ((DateTime)value).ToIsoDateTime();

            if (type == typeof(string))
                return (string)value;

            var objects = value as IEnumerable;
            if (objects != null)
                return ConvertIenumerable(objects);

            return Convert.ToString(value, CultureInfo.InvariantCulture);
        }

        private static string ConvertIenumerable(IEnumerable src)
        {
            var strs = new StringBuilder();
            foreach (var o in src)
            {
                if (strs.Length > 0)
                    strs.Append("|");

                strs.Append(ConvertParamToString(o, o.GetType()));
            }
            return strs.ToString();
        }

        private static string ToUrlParamString(IEnumerable<KeyValuePair<string, string>> data)
        {
            var result = new StringBuilder();
            foreach (var itm in data)
            {
                if (result.Length > 0)
                    result.Append('&');

                result.Append(itm.Key + '=' + EncodeUrl(itm.Value));
            }

            return result.ToString();
        }

        public static string FormatUrlString(this object data, params string[] ignoreFields)
        {
            if (data == null)
                return null;

            var strData = data as string;
            if (strData != null)
                return strData;

            var kvp = data as IEnumerable<KeyValuePair<string, string>>;
            if (kvp != null)
                return ToUrlParamString(kvp);



            var result = new StringBuilder();

            foreach (var pi in data.GetType().GetTypeInfo().DeclaredProperties)
            {


                var value = pi.GetValue(data, null);
                if (value == null)
                    continue;

                var field = pi.Name.ToLower();

                var found = ignoreFields.Any(ignoreField => field == ignoreField.ToLower());

                if (found) continue;

                if (result.Length > 0)
                    result.Append('&');


                var valueAsString = ConvertParamToString(value, pi.PropertyType);


                result.Append(field + '=' + EncodeUrl(valueAsString));

            }

            return result.ToString();
        }

        public static string EncodeUrl(this string s)
        {
            return WebUtility.UrlEncode(s);
        }

        public static byte[] ToUtf8ByteArray(this string s)
        {
            return Encoding.UTF8.GetBytes(s);
        }
    }
}

