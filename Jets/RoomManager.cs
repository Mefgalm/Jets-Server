using Jets.ClientParts;
using Jets.Interfaces;
using Jets.Rooms;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Threading.Tasks;
using System;
using SendModels.Requests;
using Jets.Services;
using SendModels.Responses;
using SendModels;
using NLog;

namespace Jets
{
    public class RoomManager : IEventHandle
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        private readonly SynchronizedCollection<IRoom> _roomList;
        private readonly SynchronizedCollection<Client> _clientList;
        private readonly ClientService _clientService;

        public RoomManager()
        {
            _clientList = new SynchronizedCollection<Client>();
            _roomList = new SynchronizedCollection<IRoom>();
            _clientService = new ClientService();
        }

        public void AddClient(TcpClient tcpClient)
        {
            var client = new Client(tcpClient);

            Subscribe(client);

            _clientList.Add(client);

            client.Start();

            logger.Log(LogLevel.Info, $"{nameof(AddClient)}{client}");
        }

        private void Client_OnDisconnected(Client client)
        {
            Unsubscribe(client);

            Parallel.ForEach(_roomList, (room) =>
            {
                room.RemoveClient(client);
            });

            _clientList.Remove(client);

            logger.Log(LogLevel.Info, $"{nameof(Client_OnDisconnected)}{client}");
        }

        private void MessageRoom_OnCloseRoom(IRoom room)
        {
            _roomList.Remove(room);
        }

        private void Client_OnSignIn(Client client, Request<SignInRequest> request)
        {
            Response<SignInResponse> response = _clientService.SignIn(request);

            byte[] data = Utils.ObjectToByteArray(response, RequestsMap.GetKeyByValue(typeof(Response<SignInResponse>)).Value);

            client.Write(data);

            logger.Log(LogLevel.Info, $"{nameof(Client_OnSignIn)}{response}");
        }

        private void Client_OnSignUp(Client client, Request<SignUpRequest> request)
        {
            Response<SignUpResponse> response = _clientService.SignUp(request);

            byte[] data = Utils.ObjectToByteArray(response, RequestsMap.GetKeyByValue(typeof(Response<SignUpResponse>)).Value);

            client.Write(data);

            logger.Log(LogLevel.Info, $"{nameof(Client_OnSignUp)}{response}");
        }

        #region subcribe on events

        public void Subscribe(Client client)
        {
            client.OnDisconnected += Client_OnDisconnected;
            client.OnSignIn       += Client_OnSignIn;
            client.OnSignUp       += Client_OnSignUp;

        }

        #endregion

        #region unsubscribe on events

        public void Unsubscribe(Client client)
        {
            client.OnDisconnected -= Client_OnDisconnected;
            client.OnSignIn       -= Client_OnSignIn;
            client.OnSignUp       -= Client_OnSignUp;
        }

        #endregion
    }
}
