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




        //public class Server
        //{
        //    private bool serverUp = true;
        //    private int connectionCounter = 1;

        //    public void Listen()
        //    {
        //        Console.WriteLine("Instantiating TCP Listener");
        //        TcpListener myList = new TcpListener(IPAddress.Any, 7000);
        //        myList.Start();

        //        while (true)
        //        {
        //            try {
        //                var clientConnection = new ClientSocketModel(myList.AcceptSocket());


        //                Thread workerThread new Thread(clientConnection.CallToChildThread);
        //                Console.WriteLine("In Main: Creating the Child thread");

        //                Console.WriteLine("Server Listening...");
        //                Socket clientsocket = 

        //                Thread childThread = new Thread(childref);
        //                childThread.Start();





        //            } catch { throw new Exception("Failed to connect to socket"); }
        //        }
        //    }




        //    private async Task HandleClientResponse(NetworkStream s, BinaryWriter w, BinaryReader r)
        //    {
        //        //string prefix = "CLIENT:" + clientID + " ";
        //        //Console.WriteLine(prefix + "Awaiting Client Input");

        //        while (serverUp)
        //        {
        //            var response = r.ReadString();
        //            //Console.WriteLine(prefix + "Sent response:");
        //            Console.WriteLine(response);

        //            switch (response)
        //            {
        //                case "Marco":
        //                    w.Write("Polo");
        //                    break;
        //                default:
        //                    w.Write(response);
        //                    break;
        //            }
        //        }
        //    }



        //    private class ClientSocketModel{

        //        private Socket _socket;

        //        public ClientSocketModel(Socket socket)
        //        {
        //            _socket = socket;
        //        }

        //        public static void CallToChildThread()
        //        {
        //            Console.WriteLine("Child thread starts");

        //            Console.WriteLine("New Client Connection Request");
        //            //Console.WriteLine("Establishing Connection to - CLIENT:" + connectionCounter);
        //            EstablishConnection(_socket);
        //            Console.WriteLine("PING");

        //            // the thread is paused for 5000 milliseconds
        //            int sleepfor = 5000;

        //            Console.WriteLine("Child Thread Paused for {0} seconds", sleepfor / 1000);
        //            Thread.Sleep(sleepfor);
        //            Console.WriteLine("Child thread resumes");
        //        }

        //        private void EstablishConnection(Socket? _socket)
        //        {
        //            NetworkStream s = new NetworkStream(_socket);
        //            BinaryWriter w = new BinaryWriter(s);
        //            BinaryReader r = new BinaryReader(s);
        //            //string prefix = "CLIENT:" + clientID + " ";



        //            Console.WriteLine("Connected");

        //            HandleClientResponse(s, w, r);
        //        }
        //    }

        //}
    }
}