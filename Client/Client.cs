using Microsoft.VisualBasic.CompilerServices;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using BrowseLib;
using BrowseLib.MiniSQL;

namespace Client
{
    class Client
    {
        //Send the different type queries
        public void sendQuery(String text, Socket socket)
        {

            Console.WriteLine("");
            Console.WriteLine("Sending information to the server...");
            byte[] byteText = Encoding.Default.GetBytes(text);
            socket.Send(byteText, 0, byteText.Length, 0);
            Console.WriteLine();
            Console.WriteLine("Done");
            Console.WriteLine();
        }

        static void Main(string[] args)
        {
            //Create the client
            Client cliente = new Client();

            //Create socket and conexion with server 
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            //Now we specify the IP and the port. 127.0.0.1 is our local pc direction
            IPEndPoint localEndPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 1234);
            socket.Connect(localEndPoint);

            Console.WriteLine("Connected to the local server");
            Console.WriteLine("Introduce a SQL sentence:");
            Console.WriteLine("");
            Console.WriteLine("For example: INSERT INTO mytable VALUES (name);");
            Console.WriteLine("             SELECT * FROM mytable;");
            Console.WriteLine("             CREATE TABLE mytable (name String, age Integer);");
            Console.WriteLine("             DELETE FROM mytable WHERE age = 20;");
            Console.WriteLine("             UPDATE mytable SET age = 22 WHERE year = 1998;");
            Console.WriteLine("             ...");
            Console.WriteLine("             (Write exit in case you want to end)");
            Console.WriteLine("");
            string query = Console.ReadLine();

            while (query != "exit")
            {
                cliente.sendQuery(query, socket);
                query = Console.ReadLine();
            }
        }
    }
}
