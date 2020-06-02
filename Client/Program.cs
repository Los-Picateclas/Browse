using Microsoft.VisualBasic.CompilerServices;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            //We need a socket and a IP adrees to connect to the server so we create it.
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPEndPoint direction = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 1234);
            socket.Connect(direction);
            Console.WriteLine("Connected");
            Console.WriteLine("Server is listening...");

            string info = Console.ReadLine();
            //We need to transform the info to bytes so we can send it to the server
            byte[] infoToSocket = Encoding.Default.GetBytes(info);
            socket.Send(infoToSocket, 0, infoToSocket.Length, 0);
            Console.WriteLine("Sending...");
            Console.WriteLine("Press any key to finish");
            Console.ReadKey();


        }
    }
}
