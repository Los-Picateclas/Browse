using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Server
{
    class Server
    {
        public static void Main(string[] args)
        {
            //We will use sockets to make the server. 
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            //Now we specify the IP and the port. 127.0.0.1 is our local pc direction
            IPEndPoint direction = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 1234);
            socket.Bind(direction);
            socket.Listen(1); //We only accept one connection for our socket.
            Console.WriteLine("Waiting for a client...");
            Socket listen = socket.Accept(); //This new socket will return the client response
            Console.WriteLine("Connected");

            //We can only use bytes to transfer the information.
            byte[] info = new byte[255];
            int receive = listen.Receive(info, 0, info.Length, 0);
            Console.WriteLine("Client says: " + Encoding.Default.GetString(info)); //we tranform the info to a string
            socket.Close();
            Console.WriteLine("Press any key to finish");
            Console.ReadKey();
        }

        }
    
}



