using Microsoft.VisualBasic.CompilerServices;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using BrowseLib;

namespace Client
{
    class Client
    {
        public Database db;       
        public void initialize()
        {
            //Initilize the database
            db = new Database("mydatabase", "user", "password");
            //Initialize the table
            Table table = new Table("mytable");
        }

        static void Main(string[] args)
        {
            //We need a socket and a IP adrees to connect to the server so we create it
            int contador = 0;
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPEndPoint direction = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 1234);
            socket.Connect(direction);
            Console.WriteLine("Connected to the server");
            Console.WriteLine("Write exit in case you want to end");
            Console.WriteLine(" Introduce the SQL query...");

            string text = Console.ReadLine();
            String query = "INSERT INTO db.mytable(id,username,password) VALUES (@" + text + ")";
            byte[] byteQuery = Encoding.Default.GetBytes(query);

            //Name of the txt archive 
            while (text != "exit")
            {
                if (contador != 0)
                {
                    Console.WriteLine(" Introduce the SQL query...");
                    text = Console.ReadLine();
                    query = "INSERT INTO db.mytable(id,username,password) VALUES (@" + text + ")";
                    byteQuery = Encoding.Default.GetBytes(query);
                }
                Console.WriteLine("Sending information to the server...");
                contador = 1;
                //We need to transform the info to bytes so we can send it to the server
                byte[] byteText = Encoding.Default.GetBytes(text);
                //socket.Send(byteText, 0, byteText.Length, 0);
                socket.Send(byteText, 0, byteText.Length, 0);
                Console.WriteLine("Done");
                Console.WriteLine();
                //text = Console.ReadLine();
                text = "";

            }

        }
    }
}
