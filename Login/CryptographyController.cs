using System;
using System.Security.Cryptography;
using System.Text;

namespace Login
{
    public class CryptographyController
    {
        public CryptographyController()
        {
            
        }

		public static string EncryptPassword(string password)
		{
			UnicodeEncoding ByteConverter = new UnicodeEncoding();
            using (var provider = new SHA512Managed())
            {
                var encryptedPassword = provider.ComputeHash(ByteConverter.GetBytes(password));
                return ByteConverter.GetString(encryptedPassword);
            }
		}

    }
}
