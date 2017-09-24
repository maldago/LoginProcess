using System.Security.Cryptography;
using System.Text;

namespace Login
{
    public class CryptographyController
    {
		/// <summary>
        /// Encrypts the password.
        /// </summary>
        /// <returns>The password.</returns>
        /// <param name="password">Password.</param>
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
