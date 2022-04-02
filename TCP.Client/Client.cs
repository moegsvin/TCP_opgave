using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace TCP.Client
{
    public class Client
    {
        public TcpClient Connection { get; private set; }
        public BinaryReader Reader { get; private set; }
        public BinaryWriter Writer { get; private set; }
        public NetworkStream Stream { get; private set; }

        private string  _ip         = "127.0.0.1";
        private int     _port       = 7000;
        private bool    _connected  = false;
        private bool    _waiting    = false;


        public void Connect()
        {
            try { 

                Connection = new TcpClient();
                Connection.Connect(_ip, _port);

                Stream = Connection.GetStream();
                Reader = new BinaryReader(Stream);
                Writer = new BinaryWriter(Stream);

                _connected = true;

            if (Reader.ReadBoolean())
                { _waiting = true; }

            } catch { throw new Exception("Connection to TCP Server failed"); }
        }



        //  TODO: Deprecated. Prepare for deletion or refactoring.
        public void Start()
        {
            Connection = new TcpClient();
            Connection.Connect(_ip, _port);

            Stream      = Connection.GetStream();
            Reader      = new BinaryReader(Stream);
            Writer      = new BinaryWriter(Stream);

            // Fix for refactored server
            Reader.ReadBoolean();

            while (true)
            {
                Writer.Write(Console.ReadLine());
                Console.WriteLine(Reader.ReadString());
            }

        }


    }
}
