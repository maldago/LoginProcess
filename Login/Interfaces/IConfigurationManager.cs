using System.Collections.Generic;
using System.Threading.Tasks;

namespace Login
{
    public interface IConfigurationManager
    {
        Task<Dictionary<string, string>> GetUsersAsync();
        Task SaveUsersAsync(Dictionary<string, string> users);
    }
}