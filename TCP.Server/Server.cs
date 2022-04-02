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
        private bool serverUp = true;
        private int connectionCounter = 1;

        public void Listen()
        {
            Console.WriteLine("Instantiating TCP Listener");
            TcpListener myList = new TcpListener(IPAddress.Any, 7000);
            myList.Start();

            while (true)
            {
                try {

                    Console.WriteLine("Server Listening...");
                    Socket clientsocket = myList.AcceptSocket();

                    Console.WriteLine("New Client Connection Request");
                    Console.WriteLine("Establishing Connection to - CLIENT:" + connectionCounter);
                    EstablishConnection(clientsocket, connectionCounter++);
                    Console.WriteLine("PING");

                } catch { throw new Exception("Failed to connect to socket"); }
            }
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
        private async Task HandleClientResponse(int clientID, NetworkStream s, BinaryWriter w, BinaryReader r)
        {
            string prefix = "CLIENT:" + clientID + " ";
            Console.WriteLine(prefix + "Awaiting Client Input");

            while (serverUp)
            {
                var response = r.ReadString();
                Console.WriteLine(prefix + "Sent response:");
                Console.WriteLine(response);

                switch (response)
                {
                    case "Marco":
                        w.Write("Polo");
                        break;
                    default:
                        w.Write(response);
                        break;
                }
            }
        }

        private async Task EstablishConnection(Socket _socket, int clientID)
        {
            NetworkStream s = new NetworkStream(_socket);
            BinaryWriter w = new BinaryWriter(s);
            BinaryReader r = new BinaryReader(s);
            string prefix = "CLIENT:" + clientID + " ";

            //  Acknowledging Connection
            w.Write(true);

            Console.WriteLine(prefix + "Connected");

            HandleClientResponse(clientID, s, w, r);
        }
    }
}
