using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace TCP.Server
{
    public class Server
    {
        public void Listen()
        {
            TcpListener myList = new TcpListener(IPAddress.Any, 7000);
            myList.Start();

            while (true)
            {
                Socket clientsocket = myList.AcceptSocket();

                NetworkStream s = new NetworkStream(clientsocket);
            
                BinaryWriter w = new BinaryWriter(s);
                BinaryReader r = new BinaryReader(s);


                string input = r.ReadString();

                // Local echo
                Console.WriteLine("Echoing Input: " + input);

                w.Write("Echo from server: " + input);

                s.Close();
            }
        }
    }
}
