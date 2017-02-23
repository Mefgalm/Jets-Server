using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SendModels.Requests
{
    [Serializable]
    public class Request<T>
    {
        public Guid AccessToken { get; set; }

        public T DataObject { get; set; }

        public override string ToString()
        {
            return $"AccessToken : {AccessToken}, DataObject: {DataObject?.ToString()}";
        }
    }
}
