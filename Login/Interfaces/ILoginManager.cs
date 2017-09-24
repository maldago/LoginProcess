namespace Login
{
    public interface ILoginManager
    {
        LoginStatus Login(IUser user);
    }
}