using SendModels.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading.Tasks;

namespace Jets.ClientParts
{
    public partial class Client
    {
        #region delegates

        public delegate void SendMessageDel(Client client, Request<ChatMessageRequest> message);
        public delegate void DisconnectedDel(Client client);
        public delegate void SignInDel(Client client, Request<SignInRequest> logIn);
        public delegate void SignUpDel(Client client, Request<SignUpRequest> signUp);

        #endregion

        #region events

        public event SendMessageDel OnMessageSend;
        public event DisconnectedDel OnDisconnected;
        public event SignInDel OnSignIn;
        public event SignUpDel OnSignUp;

        #endregion  
    }
}
