using Jets.ClientParts;
using Jets.Interfaces;
using SendModels;
using SendModels.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Jets.Rooms
{
    public class ClientData
    {
        public object Obj { get; set; }
        public Type Type { get; set; }
    }

    public class MessageRoom : IRoom
    {
        private SynchronizedCollection<Client> _clientList;

        private SynchronizedCollection<ClientData> _clientDataList;
        private bool _isRunning;

        public event OnCloseRoom OnCloseRoom;

        public MessageRoom()
        {
            _isRunning = true;
            _clientList = new SynchronizedCollection<Client>();
            _clientDataList = new SynchronizedCollection<ClientData>();
        }

        public void Close()
        {
            _isRunning = false;
            OnCloseRoom(this);
        }

        public Task Cycle()
        {
            return Task.Run(() =>
            {
                while (_isRunning)
                {
                    try
                    {
                        foreach (var client in _clientList)
                        {
                            foreach (var data in _clientDataList)
                            {
                                byte? typeByte = RequestsMap.GetKeyByValue(data.Type);
                                if (typeByte.HasValue)
                                {
                                    client.Write(Utils.ObjectToByteArray(data.Obj, typeByte.Value));
                                }
                            }
                        }

                        _clientDataList.Clear();

                        Thread.Sleep(100);
                    }
                    catch
                    {
                        _isRunning = false;
                        OnCloseRoom(this);
                    }
                }
            });
        }

        public void AddClient(Client client)
        {
            client.OnMessageSend += Client_OnMessageSend;

            _clientList.Add(client);
        }

        private void Client_OnMessageSend(Client client, Request<ChatMessageRequest> message)
        {
            _clientDataList.Add(new ClientData()
            {
                Obj = message,
                Type = typeof(ChatMessageRequest),
            });
        }

        public void RemoveClient(Client client)
        {
            _clientList.Remove(client);

            client.OnMessageSend -= Client_OnMessageSend;

            if (!_clientList.Any())
            {
                OnCloseRoom(this);
            }
        }
    }
}
 