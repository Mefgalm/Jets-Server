using Jets.ClientParts;
using Jets.Interfaces;
using Jets.Rooms;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Threading.Tasks;
using System;
using SendModels.Requests;

namespace Jets
{
    public class RoomManager : IEventHandle
    {
        private readonly SynchronizedCollection<IRoom> _roomList;
        private readonly SynchronizedCollection<Client> _clientList;

        public RoomManager()
        {
            _clientList = new SynchronizedCollection<Client>();

            _roomList = new SynchronizedCollection<IRoom>();
        }

        public void AddClient(TcpClient tcpClient)
        {
            var client = new Client(tcpClient);

            Subscribe(client);

            //var messageRoom = new MessageRoom();
            //messageRoom.OnCloseRoom += MessageRoom_OnCloseRoom;
            //messageRoom.Cycle();

            //messageRoom.AddClient(client);

            //_roomList.Add(messageRoom);

            _clientList.Add(client);

            client.Start();
        }

        private void Client_OnDisconnected(Client client)
        {
            Unsubscribe(client);

            Parallel.ForEach(_roomList, (room) =>
            {
                room.RemoveClient(client);
            });

            _clientList.Remove(client);      
        }

        private void MessageRoom_OnCloseRoom(IRoom room)
        {
            _roomList.Remove(room);
        }

        private void Client_OnSignIn(Client client, Request<SignInRequest> logIn)
        {
            throw new NotImplementedException();
        }

        private void Client_OnSignUp(Client client, Request<SignUpRequest> signUp)
        {
            throw new NotImplementedException();
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
