using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace TCP.Server
{
    public class Server
    {
        public void Listen()
        {
            Console.WriteLine("Instantiating TCP Listener");
            TcpListener myList = new TcpListener(IPAddress.Any, 7000);
            myList.Start();

            while (true)
            {
                try
                {
                    Socket clientsocket = myList.AcceptSocket();
                    NetworkStream s = new NetworkStream(clientsocket);

                    Thread clientThread = new Thread(CreateNewClientThread);
                    clientThread.Start(s);

                }
                catch { throw new Exception("Error occured connecting to socket"); }
            }
        }

        public void CreateNewClientThread(object data)
        {
            Console.WriteLine("New Client Stream Opened");
            NetworkStream s = (NetworkStream)data;

            BinaryWriter w = new BinaryWriter(s);
            BinaryReader r = new BinaryReader(s);

            //  Acknowledging Connection
            w.Write(true);

            var response = r.ReadString();

            Console.WriteLine("Client Response: " + response);

            switch (response)
            {
                case "Marco":
                    w.Write("Polo");
                    break;
                default:
                    w.Write(response);
                    break;
            }

            Console.WriteLine("Client Stream Closing");
        }

        public void ListenDEPRECATED()
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