using System;
using System.Collections.Generic;

namespace Login
{
    public class LoginManager : ILoginManager
    {
        private Dictionary<string, byte[]> _users;

        public LoginManager(Dictionary<string, byte[]> users)
        {
            _users = users;
        }

        public LoginStatus Login(IUser user)
        {
            try
            {
                return Validate(user);
            }
            catch
            {
                return LoginStatus.Failed;
            }
        }

        private LoginStatus Validate(IUser user)
        {
            if( _users.ContainsKey(user.EmailAddress))
            {
                var unencryptedPassword = CryptographyController.DecryptPassword(_users[user.EmailAddress]);
                if (user.Password.Equals(unencryptedPassword))
                    return LoginStatus.Success;
                return LoginStatus.WrongPassword;
            }
            return LoginStatus.UserDoesNotExist;

		}
    }
}
