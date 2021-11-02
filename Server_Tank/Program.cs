using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Tank_Server_Client_lib;

namespace Server_Tank
{
    class Program
    {
        static void Main(string[] args)
        {
            

            Server server = new Server(8000, "127.0.0.1");

            server.Start();
            Task task = new Task(() => server.Conection());
            task.Start();

            Task.Factory.StartNew(() => { server.SendLIstToALLClients(); });


            //if (server.clients.Count > 0)
            //    {

            //     //Console.WriteLine(server.GetMsg(server.clients.Last()));

                 
                    


            //    }



            Console.ReadLine();

        }
    }
}
