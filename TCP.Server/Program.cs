// See https://aka.ms/new-console-template for more information
using TCP.Server;

Console.WriteLine("Hello, World!");

var _server = new Server();
_server.Listen();