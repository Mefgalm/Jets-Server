using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace SendModels
{
    public static class Utils
    {
        public static readonly BinaryFormatter BinaryFormatter = new BinaryFormatter();

        public static byte[] ObjectToByteArray(object obj, byte type)
        {
            if (obj == null)
                return null;

            using (var ms = new MemoryStream())
            {
                BinaryFormatter.Serialize(ms, obj);

                var newMs = new MemoryStream((int)ms.Length + 1);
                newMs.Position = 0;
                newMs.WriteByte(type);
                byte[] bytes = ms.ToArray();
                newMs.Write(bytes, 0, bytes.Length);
                return newMs.ToArray();
            }
        }

        public static object ByteArrayToObject(byte[] arrBytes, out Type type)
        {
            type = null;
            if (arrBytes == null || arrBytes.Length == 0)
            {
                return null;
            }

            using (var memStream = new MemoryStream())
            {
                memStream.Write(arrBytes, 1, arrBytes.Length - 1);
                memStream.Position = 0;

                type = RequestsMap.GetValueByKey(arrBytes[0]);

                var test = memStream.ToArray();

                try
                {
                    return BinaryFormatter.Deserialize(memStream);
                }
                catch
                {
                    return null;
                }
            }
        }

        //public static byte[] ObjectToByteArray(object obj)
        //{
        //    if (obj == null)
        //        return null;

        //    using (var ms = new MemoryStream())
        //    {
        //        BinaryFormatter.Serialize(ms, obj);
        //        return ms.ToArray();
        //    }
        //}

        //public static T ByteArrayToObject<T>(byte[] arrBytes)
        //{
        //    using (var memStream = new MemoryStream())
        //    {
        //        memStream.Write(arrBytes, 0, arrBytes.Length);
        //        memStream.Seek(0, SeekOrigin.Begin);

        //        try
        //        {
        //            return (T)BinaryFormatter.Deserialize(memStream);
        //        } catch
        //        {
        //            return default(T);
        //        }
        //    }
        //}
    }
}
