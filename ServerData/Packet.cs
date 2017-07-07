using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServerData;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Net;

namespace ServerData
{
    [Serializable]
    public class Packet
    {
        public List<string> Gdata;
        public int packetInt;
        public bool packetBool;
        public string senderId;
        public PacketType packetType;

        public Packet(PacketType type, string senderId)
        {
            Gdata = new List<string>();
            this.senderId = senderId;
            this.packetType = type;
        }

        public Packet(Byte[] packetBytes)
        {
            BinaryFormatter bf = new BinaryFormatter();
            MemoryStream ms = new MemoryStream(packetBytes);

            Packet p = (Packet)bf.Deserialize(ms);
            ms.Close();

            this.Gdata = p.Gdata;
            this.packetBool = p.packetBool;
            this.packetInt = p.packetInt;
            this.senderId = p.senderId;
            this.packetType = p.packetType;


        }

        public byte[] ToBytes()
        {
            BinaryFormatter bf = new BinaryFormatter();
            MemoryStream ms = new MemoryStream();

            bf.Serialize(ms, this);
            byte[] bytes = ms.ToArray();
            ms.Close();
            return bytes;
        }
        
        public static string GetIPAdress()
        {
            IPAddress[] ips = Dns.GetHostAddresses(Dns.GetHostName());

            foreach(IPAddress i in ips)
            {
                if(i.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                {
                    return i.ToString();
                }
            }

            return "127.0.0.1";
        }
    }

    public enum PacketType
    {
       
    }
}
