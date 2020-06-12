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

            Console.WriteLine("Sending information to the server...");
            byte[] byteText = Encoding.Default.GetBytes(text);
            socket.Send(byteText, 0, byteText.Length, 0);
            Console.WriteLine("Done");
            Console.WriteLine();
        }

        static void Main(string[] args)
        {

            //Create socket and conexion with server
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPEndPoint direction = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 1234);
            socket.Connect(direction);
            Console.WriteLine("Connected to the server");
            string text = "";
            string option = "";
            Client cliente = new Client();

            while (option != "exit")
            {
                //Create the menu
                Console.WriteLine("Introduce what do you want to do...");
                Console.WriteLine("1: Insert");
                Console.WriteLine("2: Select");
                Console.WriteLine("3: Update");
                Console.WriteLine("4: Delete");
                Console.WriteLine("Write exit in case you want to end");
                option = Console.ReadLine();

                switch (option)
                {
                    case "1":
                        Console.WriteLine("Introduce the name of the tuple you want to add");
                        text = Console.ReadLine();
                        String insertQuery = "INSERT INTO mytable VALUES (" + text + ");";
                        cliente.sendQuery(insertQuery, socket);
                        break;

                    case "2":
                        Console.WriteLine("All the tables selected");
                        String selectQuery = "SELECT * FROM mytable;";
                        cliente.sendQuery(selectQuery, socket);
                        break;

                    case "3":
                        Console.WriteLine("Change the name of the tuples");
                        text = Console.ReadLine();
                        String updateQuery = "UPDATE mytable SET * WHERE (" + text + ");";
                        cliente.sendQuery(updateQuery, socket);
                        break;

                    case "4":
                        Console.WriteLine("Values of the table deleted");
                        String deleteQuery = "DELETE * FROM mytable;";
                        cliente.sendQuery(deleteQuery, socket);
                        break;

                    case "exit":
                        socket.Close();
                        break;

                    default:
                        Console.WriteLine("Incorrect value");
                        break;
                }
            }
        }
    }
}
