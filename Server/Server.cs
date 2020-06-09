using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using BrowseLib;
using BrowseLib.MiniSQL;

namespace Server
{
    class Server
    {
        public void initialize()
        {
            //Initilize the database
            Database db = new Database("mydatabase", "user", "password");
            //Initialize the table
            Table tb = new Table("mytable");

            //Insert
            //MiniSQLQuery query = MiniSQLParser.Parse("INSERT INTO Person VALUES (Javier, Ortiz, 45);");
            //Insert insertQuery = query as Insert;
            //byte[] byteQuery = Encoding.Default.GetBytes(query);
            //socket.Send(insertQuery, 0, insertQuery.Length, 0);

            //Select
            //MiniSQLQuery query = MiniSQLParser.Parse("SELECT Name, Age, Height FROM People;");
            //Select selectQuery = query as Select;
        }
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
            //We can only use bytes to transfer the information
            byte[] info = new byte[255];

            while (true)
            {
                Array.Clear(info, 0, 255);
                int receive = listen.Receive(info, 0, info.Length, 0);
                string infoString = Encoding.Default.GetString(info); //We tranform the info to string
                Console.WriteLine("Client says: " + infoString);
                socket.Close();
            }

           
        }

    }
    
}



