using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ServerData;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.IO;
using System;


namespace ChatWindow
{
    public partial class chatForm : Form
    {
        public chatForm()
        {
            InitializeComponent();
            start();
        }

        public Socket master;
        public string name { get; set; }
        public string id;
        public bool chatReady = true;

        void start()
        {
       

        A: string rip = loginForm.ip;
            name = loginForm.name;


            master = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            IPEndPoint ip = new IPEndPoint(IPAddress.Parse(rip), 4242);

            try
            {
                master.Connect(ip);
            }
            catch
            {
                //loginForm.ShowForm();
                goto A;
            }
            
            Thread t = new Thread(Data_IN);
            t.Start();

        }

     

        public void Data_IN()
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
                    // Console.WriteLine("Server dropped");
                }

            }
        }

        void DataManager(Packet p)
        {
            switch (p.packetType)
            {
                case PacketType.Registration:
                    id = p.Gdata[0];

                    break;
                case PacketType.Chat:
                    recieveMessage(p.Gdata[0] + ": " + p.Gdata[1] + "\r\n");


                    break;
            }
        }

        private void recieveMessage(string message)
        {
            if (InvokeRequired)
            {
                this.Invoke(new Action<string>(recieveMessage), new object[] { message });
                return;
            }
            this.chatWindow.AppendText(message);
            this.chatWindow.ScrollToCaret();
            Thread.Sleep(200);
        }

        private void submit_Click(object sender, EventArgs e)
        {
            string input = inputBox.Text;
            inputBox.Text = "";

            Packet p = new Packet(PacketType.Chat, id);
            p.Gdata.Add(name);
            p.Gdata.Add(input);
            master.Send(p.ToBytes());
        }


        private void onEnter_submit(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                submit_Click(sender, e);
            }
        }
    }
}
