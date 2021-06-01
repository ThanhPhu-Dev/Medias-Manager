using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Manager_Medias.Functions
{
    public class HashPassword
    {
        public static bool ComparePassword(string plainPw, string hashedPw)
        {
            Byte[] salt;
            Rfc2898DeriveBytes pbkdf2;
            byte[] hashBytes;
            byte[] hash;
            new RNGCryptoServiceProvider().GetBytes(salt = new byte[16]);

            hashBytes = Convert.FromBase64String(hashedPw);
            salt = new byte[16];
            Array.Copy(hashBytes, 0, salt, 0, salt.Length);
            pbkdf2 = new Rfc2898DeriveBytes(plainPw, salt, 10000);
            hash = pbkdf2.GetBytes(20);
            for (int i = 0; i < 20; i++)
            {
                if (hashBytes[i + 16] != hash[i])
                {
                    return false;
                }
            }
            return true;
        }

        public static string Hash(string plainPw)
        {
            byte[] salt;
            new RNGCryptoServiceProvider().GetBytes(salt = new byte[16]);

            var pbkdf2 = new Rfc2898DeriveBytes(plainPw, salt, 10000);
            byte[] hash = pbkdf2.GetBytes(20);

            byte[] hashBytes = new byte[36];
            Array.Copy(salt, 0, hashBytes, 0, salt.Length);
            Array.Copy(hash, 0, hashBytes, salt.Length, hash.Length);

            string pwHash = Convert.ToBase64String(hashBytes);

            return pwHash;
        }
    }
}