using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace TCP.Client
{
    class Client
    {
        public void Start()
        {
            TcpClient tcpclient = new TcpClient();
            tcpclient.Connect("127.0.0.1", 7000);
            NetworkStream s = tcpclient.GetStream();
            BinaryReader r = new BinaryReader(s);
            BinaryWriter w = new BinaryWriter(s);

            while (true)
            {
                w.Write(Console.ReadLine());
                Console.WriteLine(r.ReadString());
            }

        }
    }
}
