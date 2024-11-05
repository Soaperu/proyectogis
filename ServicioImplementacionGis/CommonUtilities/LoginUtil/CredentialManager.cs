using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CommonUtilities.LoginUtil
{
    public class CredentialManager
    {
        public static string EncryptCredentials(string username, string password, int minutes)
        {
            string data = $"{username};{password};{DateTime.Now.AddMinutes(minutes)}"; // Usuario, contraseña y expiración
            return EncryptorBiz.Encrypt(data);
        }

        public static (string Username, string Password, DateTime Expiration) DecryptCredentials(string encryptedData)
        {
            string decrypted = EncryptorBiz.Decrypt(encryptedData);
            
            var parts = decrypted.Split(';');
            string username = parts[0];
            string password = parts[1];
            DateTime expiration = DateTime.Parse(parts[2]);
            return (username, password, expiration);
                
        }
    }
}
