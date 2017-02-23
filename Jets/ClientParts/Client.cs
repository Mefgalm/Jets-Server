using SendModels;
using SendModels.Requests;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Jets.ClientParts
{
    public partial class Client
    {
        public const int BufferSize = 1024;

        private TcpClient _tcpClient;
        private NetworkStream _networkStream;

        private object _thisWriteLock = new object();
      

        public Client(TcpClient tcpClient)
        {
            _tcpClient = tcpClient;
        }

        public void Write(byte[] data)
        {
            lock (_thisWriteLock)
            {
                if (_networkStream == null)
                    return;

                try
                {
                    _networkStream.Write(data, 0, data.Length);
                }
                catch (Exception e)
                {
                    Console.Write(e.Message);
                    _networkStream.Close();
                    _tcpClient.Close();

                    OnDisconnected(this);
                }                
            }
        }

        public Task Start()
        {
            return Task.Run(() =>
            {
                bool isNoError = true;
                try
                {
                    _networkStream = _tcpClient.GetStream();
                }
                catch
                {
                    isNoError = false;
                    _tcpClient.Close();
                    OnDisconnected(this);
                    return;
                }

                try
                { 
                    while (isNoError)
                    {
                        byte[] data = new byte[BufferSize];
                        using (var ms = new MemoryStream())
                        {
                            int numBytesRead;
                            while ((numBytesRead = _networkStream.Read(data, 0, data.Length)) == BufferSize)
                            {
                                ms.Write(data, 0, numBytesRead);
                            }

                            ms.Write(data, 0, numBytesRead);

                            //fire particular event
                            FireEvent(ms.ToArray());
                        }
                    }
                } catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    isNoError = false;
                } finally
                {
                    _networkStream?.Close();
                    _networkStream?.Dispose();
                    _tcpClient.Close();
                    OnDisconnected(this);
                }

            });
        }

        public void Stop()
        {
            _networkStream?.Close();
            _tcpClient.Close();

            OnDisconnected(this);
        }

        private void FireEvent(byte[] data)
        {
            Type type;
            var obj = Utils.ByteArrayToObject(data, out type);

            if (type == typeof(Request<ChatMessageRequest>))
            {
                OnMessageSend(this, obj as Request<ChatMessageRequest>);
            } else if(type == typeof(Request<SignUpRequest>))
            {
                OnSignUp(this, obj as Request<SignUpRequest>);
            } else if(type == typeof(Request<SignInRequest>))
            {
                OnSignIn(this, obj as Request<SignInRequest>);
            }
        }

        public override string ToString()
        {
            return "Client";
        }
    }
}
