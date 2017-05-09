using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace NamedPipeWrapper.IO
{
    public interface ISerializer<T>
    {
        byte[] Serialize(T obj);
        T Deserialize(byte[] data);
    }

    /// <summary>
    /// Uses BinaryFormatter of serialization
    /// </summary>
    public class BinaryFormatterSerializer<T> : ISerializer<T>
    {
        /// <exception cref="SerializationException">An object in the graph of type parameter <typeparamref name="T"/> is not marked as serializable.</exception>
        public byte[] Serialize(T obj)
        {
            try
            {
                
                using (var memoryStream = new MemoryStream())
                {
                    var binaryFormatter = new BinaryFormatter();
                    binaryFormatter.Serialize(memoryStream, obj);
                    return memoryStream.ToArray();
                }
            }
            catch
            {
                //if any exception in the serialize, it will stop named pipe wrapper, so there will ignore any exception.
                return null;
            }
        }

        public T Deserialize(byte[] data)
        {
            using (var memoryStream = new MemoryStream(data))
            {
                var binaryFormatter = new BinaryFormatter();
                return (T)binaryFormatter.Deserialize(memoryStream);
            }
        }
    }
}