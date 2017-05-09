using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProtoBuf;

namespace NamedPipeWrapper.IO
{
    public class ProtobufNetSerializer<T> : ISerializer<T>
    {
        public byte[] Serialize(T obj)
        {
            using (var memoryStream = new MemoryStream())
            {
                Serializer.Serialize(memoryStream, obj);
                return memoryStream.ToArray();
            }
        }

        public T Deserialize(byte[] data)
        {
            using (var memoryStream = new MemoryStream(data))
            {
                return Serializer.Deserialize<T>(memoryStream);
            }
        }
    }
}
