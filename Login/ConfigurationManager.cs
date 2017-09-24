using System;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO.Compression;

namespace Login
{
    public class ConfigurationManager : IConfigurationManager
    {
        private readonly string _filePath;

        public ConfigurationManager(string filePath)
        {
            _filePath = filePath;
        }

        public async Task<Dictionary<string, string>> GetUsersAsync()
        {
            Dictionary<string, string> users = new Dictionary<string, string>();
            using (FileStream fileStream = new FileStream(_filePath, FileMode.OpenOrCreate))
            {
                byte[] result = new byte[fileStream.Length];
                await fileStream.ReadAsync(result, 0, (int)fileStream.Length);
				try
                {
                    if(result.Length > 0)
                        users = ObjectSerializer.DeserializeObject<Dictionary<string, string>>(result);
                }
                catch (Exception ex) 
                {
                    /* handle exception omitted */ 
                }
                return users;
            }
        }

        public async Task SaveUsersAsync(Dictionary<string, string> users)
        {
            using (FileStream fileStream = new FileStream(_filePath, FileMode.OpenOrCreate))
			{
				byte[] result;
				try
				{
					result = ObjectSerializer.SerializeObject(users);

                    await fileStream.WriteAsync(result, 0, result.Length);
				}
				catch (Exception ex)
				{
					/* handle exception omitted */
				}
			}
        }
 

        
    }
}
