using System.Threading.Tasks;

namespace Login
{
    public interface ISignUpManager
    {
        Task<UserResult> RegisterAsync(IUser user);
        LoginStatus Login();
    }
}