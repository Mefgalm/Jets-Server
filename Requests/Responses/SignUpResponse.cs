using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SendModels.Responses
{
    public class SignUpResponse
    {
        public string Username { get; set; }

        public Guid AccessToken { get; set; }
    }
}
