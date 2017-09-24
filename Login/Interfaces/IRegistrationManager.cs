using System.Collections.Generic;

namespace Login
{
    public interface IRegistrationManager
    {
        Dictionary<string, string> Users { get; }
        RegistrationStatus Register(IUser user);
        RegistrationStatus CheckUserExists(IUser user);
    }
}