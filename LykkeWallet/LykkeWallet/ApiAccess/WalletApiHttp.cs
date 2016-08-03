using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using LykkeWallet.LocalKeyStorageAccess;
using LykkeWallet.Models.Api;
using LykkeWallet.Utils;

namespace LykkeWallet.ApiAccess
{
    public class ApiErrorModel
    {
        public int Code { get; set; }
        public string Field { get; set; }
        public string Message { get; set; }
    }

    public class ApiResponseModel
    {

        public ApiErrorModel Error { get; set; }
    }


    public class ApiResponseModel<T> : ApiResponseModel
    {
        public T Result { get; set; }
    }

    public partial class WalletApi
    {
        private const string Url = "https://lykke-api-dev.azurewebsites.net/api/";

        public static string CurrentToken;

        private static async Task<string> DoGetHttpRequestAsync(string path, object data)
        {
            try
            {
                var q = data == null ? "" : "?" + data.FormatUrlString();

                var webRequest = (HttpWebRequest)WebRequest.Create(Url + path + q);
                webRequest.Method = "GET";
                webRequest.ContentType = "application/json";
                SetUserAgent(webRequest, $"DeviceType={Constants.UserAgent.DEVICE_TYPE};AppVersion={Constants.UserAgent.APP_VERSION};ClientFeatures={Constants.UserAgent.CLIENT_FEATURES}");

                if (CurrentToken != null)
                    webRequest.Headers["Authorization"] = "Bearer " + CurrentToken;


                Debug.WriteLine("sending request...");
                var webResponse = await webRequest.GetResponseAsync();

                var receiveStream = webResponse.GetResponseStream();

                if (receiveStream == null)
                    return null;

                var sr = new StreamReader(receiveStream);
                var result = await sr.ReadToEndAsync();
                ApiException.CheckException(result);
                Debug.WriteLine("request ended");
                return result;
            }
            catch(Exception ex)
            {
                var a = 34234;
            }
            return null;

        }

        private async Task<T> DoGetRequestAsync<T>(string path, object data = null)
        {
            try
            {
                var response = await DoGetHttpRequestAsync(path, data);
                var result = Newtonsoft.Json.JsonConvert.DeserializeObject<ApiResponseModel<T>>(response);
                return result.Result;
            }
            catch (WebException ex)
            {
                if (((HttpWebResponse)ex.Response).StatusCode == HttpStatusCode.Unauthorized)
                    SaveToken(null);
                throw;
            }
        }
        private static void SetUserAgent(HttpWebRequest request, string userAgent)

        {

            //Type type = request.GetType();
            TypeInfo typeInfo = request.GetType().GetTypeInfo();
            System.Reflection.PropertyInfo prop = typeInfo.DeclaredProperties.FirstOrDefault(x => x.Name.ToLower() == "useragent");

            if (prop != null)

            {
                prop.SetValue(request, userAgent, null);
            }

        }

        private static string GetUserAgent(HttpWebRequest request)

        {

            string ret = "not available";

            TypeInfo typeInfo = request.GetType().GetTypeInfo();
            System.Reflection.PropertyInfo prop = typeInfo.DeclaredProperties.FirstOrDefault(x => x.Name.ToLower() == "useragent");

            if (prop != null)

            {

                ret = (string)prop.GetValue(request, null);

            }



            return ret;

        }



        private static async Task<string> DoPostHttpRequestAsync(string path, object data, object urlData)
        {
            try
            {
                var q = urlData == null ? "" : "?" + urlData.FormatUrlString();

                var webRequest = (HttpWebRequest) WebRequest.Create(Url + path + q);
                webRequest.Method = "POST";
                webRequest.ContentType = "application/json";
                //webRequest. = $"DeviceType={Constants.UserAgent.DEVICE_TYPE};AppVersion={Constants.UserAgent.APP_VERSION};ClientFeatures={Constants.UserAgent.CLIENT_FEATURES}";
                SetUserAgent(webRequest, $"DeviceType={Constants.UserAgent.DEVICE_TYPE};AppVersion={Constants.UserAgent.APP_VERSION};ClientFeatures={Constants.UserAgent.CLIENT_FEATURES}");
                
                if (CurrentToken != null)
                    webRequest.Headers["Authorization"] = "Bearer " + CurrentToken;



                Debug.WriteLine("sending request...");
                var stream = await webRequest.GetRequestStreamAsync();

                if (data == null)
                {
                    var me = new MemoryStream();
                    me.WriteTo(stream);
                }
                else
                {
                    var dataToSend = Newtonsoft.Json.JsonConvert.SerializeObject(data).ToUtf8ByteArray();
                    stream.Write(dataToSend, 0, dataToSend.Length);
                }


                var webResponse = await webRequest.GetResponseAsync();

                var receiveStream = webResponse.GetResponseStream();

                if (receiveStream == null)
                    return null;

                var sr = new StreamReader(receiveStream);


                var result = await sr.ReadToEndAsync();
                ApiException.CheckException(result);
                Debug.WriteLine("request ended");
                return result;
            }
            catch (Exception ex)
            {
                var a = 3;
                throw;
            }


            return null;


        }

        private async Task DoPostRequestAsync(string path, object data = null, object urlData = null)
        {
            try
            {
                await DoPostHttpRequestAsync(path, data, urlData);
            }
            catch (WebException ex)
            {
                if (((HttpWebResponse)ex.Response).StatusCode == HttpStatusCode.Unauthorized)
                    SaveToken(null);
                throw;
            }
        }

        private async Task<T> DoPostRequestAsync<T>(string path, object data = null, object urlData = null)
        {
            try
            {
                var response = await DoPostHttpRequestAsync(path, data, urlData);
                var result = Newtonsoft.Json.JsonConvert.DeserializeObject<ApiResponseModel<T>>(response);
                return result.Result;
            }
            catch (WebException ex)
            {
                if (((HttpWebResponse)ex.Response).StatusCode == HttpStatusCode.Unauthorized)
                    SaveToken(null);
                throw;
            }
        }

        public void SetToken(string token)
        {
            CurrentToken = token;
        }
    }

}
