using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServerData;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.IO;

namespace Client
{
    class Client
    {
        public static Socket master;
        static void Main(string[] args)
        {
            Console.Write("IP Adress: ");
            string rip = Console.ReadLine();

            master = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            IPEndPoint ip = new IPEndPoint(IPAddress.Parse(rip), 4242);
        }
    }
}
