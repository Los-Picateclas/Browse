using Microsoft.VisualBasic.CompilerServices;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Client
{
    class Client
    {
        static void Main(string[] args)
        {

            //We need a socket and a IP adrees to connect to the server so we create it
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPEndPoint direction = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 1234);
            socket.Connect(direction);
            Console.WriteLine("Connected to the server");
            Console.WriteLine(" Introduce the name of a txt file:");

            //Name of the txt archive 
            string txt = Console.ReadLine();
            System.IO.StreamReader file = new System.IO.StreamReader("../../../../" + txt + ".txt");
            string info = System.IO.File.ReadAllText("../../../../" + txt + ".txt");
            file.Close();
            Console.WriteLine("Sending information to the server...");

            //We need to transform the info to bytes so we can send it to the server
            byte[] infoToSocket = Encoding.Default.GetBytes(info);
            socket.Send(infoToSocket, 0, infoToSocket.Length, 0);
            Console.WriteLine("Done");
            Console.WriteLine("Press any key to finish");
            Console.ReadKey();


        }
    }
}
