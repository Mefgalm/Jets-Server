using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Jets
{
    class Program
    {
        static void Main(string[] args)
        {
            var server = new Server();
            server.Start();

            //server.Stop();

            Console.ReadKey();
        }
    }
}
