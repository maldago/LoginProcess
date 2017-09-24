namespace Login
{
    public interface IRegistrationManager
    {
        RegistrationStatus Register(IUser user);
    }
}