using System.Collections.Generic;
using System.Threading.Tasks;

namespace Login
{
    public interface IConfigurationManager
    {
        Task<Dictionary<string, byte[]>> GetUsersAsync();
        Task SaveUsersAsync(Dictionary<string, byte[]> users);
    }
}