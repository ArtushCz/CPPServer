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
        public static string name;
        public static string id;
        public static bool chatReady = true;

        static void Main(string[] args)
        {
            Console.Write("Enter your name: ");
            name = Console.ReadLine();

        A: Console.Write("IP Adress: ");
            string rip = Console.ReadLine();

            master = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            IPEndPoint ip = new IPEndPoint(IPAddress.Parse(rip), 4242);

            try
            {
                master.Connect(ip);
            }
            catch
            {
                Console.WriteLine("Cannot connect...");
                Thread.Sleep(1000);
                goto A;
            }


            Thread t = new Thread(Data_IN);
            t.Start();

            while (true)
            {

                if (chatReady)
                {
                    Console.Write(": ");
                    string input = Console.ReadLine();

                    Packet p = new Packet(PacketType.Chat, id);
                    p.Gdata.Add(name);
                    p.Gdata.Add(input);
                    master.Send(p.ToBytes());
                    chatReady = false;
                }
            }

        }

        public static void Data_IN()
        {
            byte[] Buffer;
            int readBytes;
            while (true)
            {
                try
                {
                    Buffer = new byte[master.SendBufferSize];
                    readBytes = master.Receive(Buffer);

                    if (readBytes > 0)
                    {
                        DataManager(new Packet(Buffer));
                    }
                }
                catch
                {
                    Console.WriteLine("Server dropped");
                }

            }
        }

        static void DataManager(Packet p)
        {
            switch (p.packetType)
            {
                case PacketType.Registration:
                    id = p.Gdata[0];

                    break;
                case PacketType.Chat:
                    ConsoleColor c = Console.ForegroundColor;
                    Console.ForegroundColor = ConsoleColor.Cyan;

                    Console.WriteLine(":> " + p.Gdata[0] + ": " + p.Gdata[1]);
                    Console.ForegroundColor = c;
                    chatReady = true;
                    break;
            }
        }
    }
}
