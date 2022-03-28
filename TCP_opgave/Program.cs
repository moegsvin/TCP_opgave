

// See https://aka.ms/new-console-template for more information
using System.Net.Sockets;

Console.WriteLine("Hello, World!");




            TcpClient tcpclient = new TcpClient();
            // kræver at serveren venter i en accept scoket:
            Console.WriteLine("Start server, hvis den ikke er startet. Tast så retur");
            Console.ReadLine();
            tcpclient.Connect("127.0.0.1", 65535);
            NetworkStream stream = tcpclient.GetStream();
            BinaryReader reader = new BinaryReader(stream);
            BinaryWriter writer = new BinaryWriter(stream);
            string s = null;
            while (s != "q")
            {
                Console.Write("Enter the string to be transmitted (quit med q): ");
                s = Console.ReadLine();
                Console.WriteLine("Transmitting data.");
                writer.Write(s);
                Console.WriteLine("Data transmitted.");
                Console.WriteLine("Waiting for data from server.");
                string txt = reader.ReadString();
                Console.WriteLine("Data from Server: " + txt);
            }
            tcpclient.Close();
        