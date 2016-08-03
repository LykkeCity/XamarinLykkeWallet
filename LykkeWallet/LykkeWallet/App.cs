using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LykkeWallet.ApiAccess;
using LykkeWallet.LocalKeyStorageAccess;
using LykkeWallet.Pages;
using LykkeWallet.Utils;
using NBitcoin;
using Xamarin.Forms;

namespace LykkeWallet
{
    public class App : Application
    {
        public App()
        {
            weird();
            //var localStorage = new LocalKeyStorage();
            //localStorage.Save(WalletApi.TokenName, null);
            MainPage = new NavigationPage(new LoginPage());
        }

        private void weird()
        {
            /*
            var key = new Key();

            var secret = new BitcoinSecret(key, Network.TestNet);

            var privateKey = secret.PrivateKey.GetWif(Network.TestNet).ToWif();

            var publicKey = secret.PubKey.Compress().ToHex();
            
            var a = "f19c523315891e6e15ae0608a35eec2e00ebd6d1984cf167f46336dabd9b2de4";
            var b = "1234560000000000";

            var pk = Encoding.UTF8.GetBytes(publicKey);

            var bytepk = Encoding.UTF8.GetBytes(privateKey);
            var cypher = AESHelper.EncryptByteArray(bytepk, Encoding.UTF8.GetBytes(b));
            
            var c = BitConverter.ToString(cypher).Replace("-", "");

            var c_reversed = StringToByteArray(c);
            var d = AESHelper.DecryptByteArray(c_reversed, Encoding.UTF8.GetBytes(b));
            var cypher_reversed = System.Text.Encoding.UTF8.GetString(d, 0, d.Length);

            var v1 = AESHelper.Encrypt128("avoe", "123");
            var v2 = AESHelper.Decrypt128(v1, "123");


            var dd = 234;
            */
        }
        public static byte[] StringToByteArray(string hex)
        {
            return Enumerable.Range(0, hex.Length)
                             .Where(x => x % 2 == 0)
                             .Select(x => Convert.ToByte(hex.Substring(x, 2), 16))
                             .ToArray();
        }

        static byte[] GetBytes(string str)
        {
            byte[] bytes = new byte[str.Length * sizeof(char)];
            System.Buffer.BlockCopy(str.ToCharArray(), 0, bytes, 0, bytes.Length);
            return bytes;
        }

        static string GetString(byte[] bytes)
        {
            char[] chars = new char[bytes.Length / sizeof(char)];
            System.Buffer.BlockCopy(bytes, 0, chars, 0, bytes.Length);
            return new string(chars);
        }

        protected override void OnStart()
        {

        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
