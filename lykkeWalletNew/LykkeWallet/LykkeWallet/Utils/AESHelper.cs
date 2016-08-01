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
        public static byte[] EncryptByteArray(byte[] data, byte[] keyMaterial)
        {
            var provider = WinRTCrypto.SymmetricKeyAlgorithmProvider.OpenAlgorithm(SymmetricAlgorithm.AesEcbPkcs7);
            var key = provider.CreateSymmetricKey(keyMaterial);
            byte[] iv = null; // this is optional, but must be the same for both encrypting and decrypting
            byte[] cipherText = WinRTCrypto.CryptographicEngine.Encrypt(key, data, iv);
            return cipherText;
        }

        /// <summary>
        /// Decrypt a byte array using AES 128
        /// </summary>
        /// <param name="key">key in bytes</param>
        /// <param name="secret">the encrypted bytes</param>
        /// <returns>decrypted bytes</returns>
        public static byte[] DecryptByteArray(byte[] key, byte[] secret)
        {
            return null;
        }
    }
}
