using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SendModels.Requests
{
    public class Request<T>
    {
        public Guid AccessToken { get; set; }

        public T DataObject { get; set; }
    }
}
