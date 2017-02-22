using SendModels.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SendModels
{
    public static class RequestsMap
    {
        private static readonly Dictionary<byte, Type> RequestsTypeDictionary;

        static RequestsMap()
        {
            RequestsTypeDictionary = new Dictionary<byte, Type>
            {
                { 1, typeof(Request<SignInRequest>) },
                { 2, typeof(Request<SignUpRequest>) },
                { 3, typeof(Request<ChatMessageRequest>) },
            };
        }

        public static byte? GetKeyByValue(Type value)
        {
            KeyValuePair<byte, Type> pair = RequestsTypeDictionary.FirstOrDefault(x => x.Value == value);
            if (!pair.Equals(default(KeyValuePair<byte, Type>)))
            {
                return pair.Key;
            }
            return null;
        }

        public static Type GetValueByKey(byte key)
        {            
            return RequestsTypeDictionary.ContainsKey(key) ? RequestsTypeDictionary[key] : null;
        }
    }
}
