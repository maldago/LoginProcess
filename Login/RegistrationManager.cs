using System.Collections.Generic;

namespace Login
{

    public class RegistrationManager : IRegistrationManager
    {
        public Dictionary<string, string> Users { get; private set; }

        public RegistrationManager(Dictionary<string, string> users)
        {
            Users = users;
        }

        public RegistrationStatus Register(IUser user)
        {
            var status = CheckUserExists(user);
            if (status == RegistrationStatus.NewUser)
            {
                try
                {
                    string encrypterPassword = CryptographyController.EncryptPassword(user.Password);
                    Users.Add(user.EmailAddress, encrypterPassword);
                    status = RegistrationStatus.Success;
                }
                catch
                {
                    status = RegistrationStatus.Failed;
                }
            }
            return status;
        }

        public RegistrationStatus CheckUserExists(IUser user)
        {
            try
            {
                if (Users.ContainsKey(user.EmailAddress))
                    return RegistrationStatus.UserExists;
                return RegistrationStatus.NewUser;
            }
            catch
            {
                return RegistrationStatus.Failed;
            }
        }
    }
}
