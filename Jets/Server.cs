using Jets.Interfaces;
using Jets.Services.Context;
using NLog;
using System;
using System.Data.Entity;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace Jets
{
    public class Server : IServer
    {
        private TcpListener _tcpListener;
        private RoomManager _roomManager;

        private static Logger logger = LogManager.GetCurrentClassLogger();

        public Server()
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<JetsDatabaseContext, JetsDbConfiguration>());

            var localAddress = IPAddress.Parse("127.0.0.1");
            _tcpListener = new TcpListener(localAddress, 2200);

            _roomManager = new RoomManager();
        }

        ~Server()
        {
            _tcpListener.Stop();
        }

        public Task Start()
        {
            logger.Info("Server start listen");
            return Task.Run(() =>
            {
                bool isNoErros = true;
                try
                {
                    _tcpListener.Start();

                    while (isNoErros)
                    {
                        TcpClient client = _tcpListener.AcceptTcpClient();

                        logger.Info($"Client connected : {client}");

                        _roomManager.AddClient(client);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    isNoErros = false;
                }
            });
        }

        public void Stop()
        {
            _tcpListener.Stop();
        }
    }
}
