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
            //weird();
            //var localStorage = new LocalKeyStorage();
            //localStorage.Save(WalletApi.TokenName, null);
            MainPage = new NavigationPage(new LoginPage()); // new Page1();//new BlockchainExplorerPage(); 
        }

        private void weird()
        {
            var key = new Key();

            var secret = new BitcoinSecret(key, Network.TestNet);

            var privateKey = secret.PrivateKey.GetWif(Network.TestNet).ToWif();

            var publicKey = secret.PubKey.Compress().ToHex();
            
            var a = "f19c523315891e6e15ae0608a35eec2e00ebd6d1984cf167f46336dabd9b2de4";
            var b = "1234560000000000";

            var pk = Encoding.UTF8.GetBytes(publicKey);

            var cypher = AESHelper.EncryptByteArray(Encoding.UTF8.GetBytes(privateKey), Encoding.UTF8.GetBytes(b));


            var c = BitConverter.ToString(cypher).Replace("-", "");

            var d = 234;
        }
        public static byte[] StringToByteArray(String hex)
        {
            int NumberChars = hex.Length;
            byte[] bytes = new byte[NumberChars / 2];
            for (int i = 0; i < NumberChars; i += 2)
                bytes[i / 2] = Convert.ToByte(hex.Substring(i, 2), 16);
            return bytes;
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
