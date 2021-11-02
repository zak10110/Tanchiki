using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Tank_Server_Client_lib
{
    public class Server
    {

        public int port { get; set; }
        public string IpAdr { get; set; }
        public int bytes { get; set; }
        public Socket socket { get; set; }
        public IPEndPoint ipPoint { get; set; }
        public List<Socket> clients { get; set; }
        public List<Tank> tanks { get; set; }
        public int ClientID { get; set; }
        public Server(int port, string IpAdr)
        {
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            ipPoint = new IPEndPoint(IPAddress.Parse(IpAdr), port);
            this.port = port;
            this.IpAdr = IpAdr;
            clients = new List<Socket>();
            tanks = new List<Tank>();
            ClientID = 0;
        }


        public void Start()
        {

            socket.Bind(ipPoint);
            socket.Listen(10);

        }

        public void SendLIstToALLClients()
        {
      

            while (true)
            {

                try
                {
                    string json = JsonSerializer.Serialize<List<Tank>>(tanks);
                    foreach (var item in clients)
                    {
                        SendMsgToClien(item,json);
                    }
                    Thread.Sleep(10);

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);

                }

            }
        
        
        }


        public void Conection()
        {
            while (true)
            {


                this.clients.Add(socket.Accept());
                this.SendMsgToClien(this.clients.Last(), ClientID.ToString());
                tanks.Add(new Tank());
                Task.Factory.StartNew(() => { Console.WriteLine(GetMsg(clients.Last())); GetTank(); });
                ClientID++;


            }

        }

        public void GetTank()
        {
            
            int ID = clients.Count-1;
            string msg = string.Empty;
            while (true)
            {
                try
                {
                    msg = GetMsg(clients[ID]);
                    this.tanks[ID] = JsonSerializer.Deserialize<Tank>(msg);
                    Console.WriteLine("Client ID:" + ID.ToString()+" Coord X:"+tanks[ID].X.ToString()+" Coord Y:"+tanks[ID].Y.ToString());

                }
                catch (Exception)
                {

                }

            }

        }

        public void SendMsg(List<byte> data, int index)
        {
            clients[index].Send(data.ToArray());
        }

        public static List<byte> StringToBytes(string str)
        {
            return Encoding.Unicode.GetBytes(str).ToList();
        }
        public void SendMsgToClien(Socket clientSoc, string msg)
        {

            clientSoc.Send(Encoding.Unicode.GetBytes(msg));


        }

        public void SendMsgToALL()
        {
            for (int i = 0; i < this.clients.Count(); i++)
            {

                SendMsgToClien(this.clients[i], "Welcome To Server");

            }


        }


        public string GetMsg(Socket clientSoc)
        {

            StringBuilder stringBuilder = new StringBuilder();
            byte[] data = new byte[256];

            do
            {
                this.bytes = clientSoc.Receive(data);
                stringBuilder.Append(Encoding.Unicode.GetString(data, 0, this.bytes));
            } while (socket.Available > 0);

            return stringBuilder.ToString();
        }


    }
}
