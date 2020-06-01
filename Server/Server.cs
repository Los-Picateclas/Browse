using System;
using System.Net;
using System.Net.Sockets;

namespace Server
{
    class Server
    {
        static void Main(string[] args)
        {
            Console.Clear();
            Server server = new Server();

            //La Dirección IP y el Puerto

            IPAddress localIP = IPAddress.Parse("127.0.0.1");  //El servidor
            TcpListener listener = new TcpListener(IPAddress.Any, 1111); //El puerto
            Console.WriteLine("Conexión con el servidor inicializada...");
            Console.WriteLine("Esperando a lo que diga cliente...");
        }
    }
}
