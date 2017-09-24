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

        public async Task<Dictionary<string, byte[]>> GetUsersAsync()
        {
            Dictionary<string, byte[]> users = new Dictionary<string, byte[]>();
            using (FileStream fileStream = new FileStream(_filePath, FileMode.Open))
            {
                byte[] result = new byte[fileStream.Length];
                await fileStream.ReadAsync(result, 0, (int)fileStream.Length);
				try
                {
                    users = ObjectSerializer.DeserializeObject<Dictionary<string, byte[]>>(result);
                }
                catch (Exception ex) 
                {
                    /* handle exception omitted */ 
                }
                return users;
            }
        }

        public async Task SaveUsersAsync(Dictionary<string, byte[]> users)
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
