using System.Threading.Tasks;

namespace Login {

    public class SignUpManager : ISignUpManager
    {
        public SignUpManager()
        {
            
        }

        public async Task<UserResult> RegisterAsync(IUser user)
        {
            return Task.Factory.StartNew<UserResult>(() => { return null; });
        }

        public LoginStatus  Login()
        {
            return LoginStatus.Succeeded;
        }
    }
}