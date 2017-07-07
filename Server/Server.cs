using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServerData;
using System.Net.Sockets;
using System.IO;
using System.Threading;
using System.Net;

namespace Server
{
    class Server
    {
        static Socket lisenerSocket;
        static List<ClientData> _clients;

        //server start
        static void Main(string[] args)
        {
            Console.WriteLine("Starting server... Server IP:" + Packet.GetIPAdress());
            lisenerSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            _clients = new List<ClientData>();

            IPEndPoint ip = new IPEndPoint(IPAddress.Parse(Packet.GetIPAdress()), 4242); 
            lisenerSocket.Bind(ip);

            Thread lisenerThred = new Thread(LisenThread);

            lisenerThred.Start();
        }

        //lisener - listens for clients
        static void LisenThread()
        {
            while (true) { 
                lisenerSocket.Listen(0);
                _clients.Add(new ClientData(lisenerSocket.Accept()));
            }
        }

        //client data thread - recievs data from clients
        public static void Data_IN(object cSocket)
        {
            Socket clientSocket = (Socket)cSocket;

            byte[] Buffer;
            int readBytes;

            while (true)
            {
                try
                {
                    Buffer = new byte[clientSocket.SendBufferSize];

                    readBytes = clientSocket.Receive(Buffer);

                    if (readBytes > 0)
                    {
                        Packet packet = new Packet(Buffer);
                        DataManager(packet);
                    }
                }
                catch
                {
                    Console.WriteLine("Client Disconnected");
                    
                }
                
            }
        }

        //data manager
        public static void DataManager(Packet p)
        {
            switch (p.packetType)
            {
                case PacketType.Chat:
                    Console.WriteLine(p.Gdata[0] + ": " + p.Gdata[1]);
                    foreach(ClientData c in _clients)
                    {
                        c.clientSocket.Send(p.ToBytes());
                    }
                    break;
            }
        }

    }

    class ClientData
    {
        public Socket clientSocket;
        public Thread clientThread;
        public string id;

        public ClientData()
        {
            id = Guid.NewGuid().ToString();
            clientThread = new Thread(Server.Data_IN);
            clientThread.Start(clientSocket);
            SendRegistrationPacket();
        }

        public ClientData(Socket clientSocket)
        {
            this.clientSocket = clientSocket;
            id = Guid.NewGuid().ToString();
            clientThread = new Thread(Server.Data_IN);
            clientThread.Start(clientSocket);
            SendRegistrationPacket();
        }

        public void SendRegistrationPacket()
        {
            Packet p = new Packet(PacketType.Registration, "sever");
            p.Gdata.Add(id);
            clientSocket.Send(p.ToBytes());
           
        }
    }
}
