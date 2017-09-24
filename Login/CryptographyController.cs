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

		public static byte[] EncryptPassword(string password)
		{
			UnicodeEncoding ByteConverter = new UnicodeEncoding();
			byte[] encrypterPassword;
			using (RSACryptoServiceProvider provider = new RSACryptoServiceProvider())
				encrypterPassword = provider.Encrypt(ByteConverter.GetBytes(password), false);
			return encrypterPassword;
		}

		public static string DecryptPassword(byte[] encryptedPassword)
		{
			UnicodeEncoding byteConverter = new UnicodeEncoding();
			string password;
			using (RSACryptoServiceProvider provider = new RSACryptoServiceProvider())
				password = byteConverter.GetString(provider.Decrypt(encryptedPassword, false));
			return password;
		}
    }
}
