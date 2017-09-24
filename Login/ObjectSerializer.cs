using System;
using System.IO;
using System.IO.Compression;
using System.Runtime.Serialization.Formatters.Binary;

namespace Login
{
    public class ObjectSerializer
    {

        public static T DeserializeObject<T>(byte[] result)
            where T : class
        {

            T item = default(T);
            try
            {
                var formatter = new BinaryFormatter();
                using (var ms = new MemoryStream(result))
                {
                    using (var ds = new DeflateStream(ms, CompressionMode.Decompress, true))
                    {
                        item = (T)formatter.Deserialize(ds);
                    }
                }
            }
            catch (Exception ex)
            {
                // removed error handling logic!
            }
            return item;
        }

        public static byte[] SerializeObject<T>(T item)
            where T : class
        {
            byte[] content = null;
            try
            {
                var formatter = new BinaryFormatter();

                using (var memoryStream = new MemoryStream())
                {
                    using (var deflateStream = new DeflateStream(memoryStream, CompressionMode.Compress, true))
                    {
                        formatter.Serialize(deflateStream, item);
                    }
                    memoryStream.Position = 0;
                    content = memoryStream.GetBuffer();
                }
            }
            catch (Exception ex)
            {
                // removed error handling logic!
            }
            return content;
        }
    }
}
