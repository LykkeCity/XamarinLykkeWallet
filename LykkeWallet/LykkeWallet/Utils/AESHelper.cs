using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PCLCrypto;

namespace LykkeWallet.Utils
{
    class AESHelper
    {
        public static string Encrypt128(string data, string key)
        {
            var paddedPassword = (key + "                ").Substring(0, 16);

            var pk = Encoding.UTF8.GetBytes(data);

            var p = Encoding.UTF8.GetBytes(paddedPassword);

            var cypher = EncryptByteArray(pk, p);

            return BitConverter.ToString(cypher).Replace("-", "");
        }

        public static string Decrypt128(string data, string key)
        {
            var c_reversed = StringToByteArray(data);
            var d = DecryptByteArray(c_reversed, Encoding.UTF8.GetBytes((key + "                ").Substring(0, 16)));
            return System.Text.Encoding.UTF8.GetString(d, 0, d.Length);

        }

        public static string DecryptHex128(string hex, string key)
        {
            byte[] data = FromHex(hex);
            var d = DecryptByteArray(data, Encoding.UTF8.GetBytes((key + "                ").Substring(0, 16)));
            return System.Text.Encoding.UTF8.GetString(d, 0, d.Length);
        }

        private static byte[] FromHex(string hex)
        {
            hex = hex.Replace("-", "");
            byte[] raw = new byte[hex.Length / 2];
            for (int i = 0; i < raw.Length; i++)
            {
                raw[i] = Convert.ToByte(hex.Substring(i * 2, 2), 16);
            }
            return raw;
        }

        private static byte[] StringToByteArray(string hex)
        {
            return Enumerable.Range(0, hex.Length)
                             .Where(x => x % 2 == 0)
                             .Select(x => Convert.ToByte(hex.Substring(x, 2), 16))
                             .ToArray();
        }

        private static byte[] EncryptByteArray(byte[] data, byte[] keyMaterial)
        {
            var provider = WinRTCrypto.SymmetricKeyAlgorithmProvider.OpenAlgorithm(SymmetricAlgorithm.AesEcbPkcs7);
            var key = provider.CreateSymmetricKey(keyMaterial);
            byte[] iv = null; // this is optional, but must be the same for both encrypting and decrypting
            byte[] cipherText = WinRTCrypto.CryptographicEngine.Encrypt(key, data);
            return cipherText;
        }

        /// <summary>
        /// Decrypt a byte array using AES 128
        /// </summary>
        /// <param name="key">key in bytes</param>
        /// <param name="secret">the encrypted bytes</param>
        /// <returns>decrypted bytes</returns>
        private static byte[] DecryptByteArray(byte[] data, byte[] keyMaterial)
        {
            try
            {
                var provider = WinRTCrypto.SymmetricKeyAlgorithmProvider.OpenAlgorithm(SymmetricAlgorithm.AesEcb);
                var key = provider.CreateSymmetricKey(keyMaterial);
                byte[] iv = null;
                return WinRTCrypto.CryptographicEngine.Decrypt(key, data, null);
            } catch(Exception ex)
            {
                var a = 234;
            }
            return null;
        }
    }
}
