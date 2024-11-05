using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace CommonUtilities
{
    public class EncryptorBiz
    {
        private static readonly byte[] EncryptionKey = Encoding.UTF8.GetBytes("1ng3mmeT4v4nza$$");

        // Método para cifrar datos con AES
        public static string Encrypt(string data)
        {
            using (Aes aes = Aes.Create())
            {
                aes.Key = EncryptionKey;
                aes.GenerateIV();
                ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

                using (MemoryStream ms = new MemoryStream())
                {
                    ms.Write(aes.IV, 0, aes.IV.Length); // Escribir el IV al inicio
                    using (CryptoStream cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                    using (StreamWriter sw = new StreamWriter(cs))
                    {
                        sw.Write(data);
                    }

                    return Convert.ToBase64String(ms.ToArray());
                }
            }
        }

        // Desencriptar Texto
        public static string Decrypt(string encryptedData)
        {
            {
                byte[] cipherTextBytes = Convert.FromBase64String(encryptedData);

                using (Aes aes = Aes.Create())
                {
                    aes.Key = EncryptionKey;
                    byte[] iv = new byte[aes.BlockSize / 8];
                    Array.Copy(cipherTextBytes, 0, iv, 0, iv.Length);
                    aes.IV = iv;

                    ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

                    using (MemoryStream ms = new MemoryStream(cipherTextBytes, iv.Length, cipherTextBytes.Length - iv.Length))
                    using (CryptoStream cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
                    using (StreamReader sr = new StreamReader(cs))
                    {
                        string decrypted = sr.ReadToEnd();
                        return decrypted;
                    }
                }
            }
        }
    }
}
