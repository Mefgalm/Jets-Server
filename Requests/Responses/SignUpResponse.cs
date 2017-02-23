using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SendModels.Responses
{
    [Serializable]
    public class SignUpResponse
    {
        public string Username { get; set; }

        public Guid AccessToken { get; set; }

        public override string ToString()
        {
            return $"Username: {Username} AccessToken: {AccessToken}";
        }
    }
}
