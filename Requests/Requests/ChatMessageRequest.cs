using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SendModels.Requests
{
    [Serializable]
    public class ChatMessageRequest
    {
        public DateTime? DateSended { get; set; }

        public string Text { get; set; }
    }
}
