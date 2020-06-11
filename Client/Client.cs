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

            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPEndPoint direction = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 1234);
            socket.Connect(direction);
            Console.WriteLine("Connected to the server");
            string text = "";
            string option = "";
            Client cliente = new Client();

            while (option != "exit")
            {

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
                        Console.WriteLine("Introduce nombre");
                        text = Console.ReadLine();
                        String insertQuery = "INSERT INTO mytable VALUES (" + text + ");";
                        cliente.sendQuery(insertQuery, socket);
                        break;

                    case "2":
                        Console.WriteLine("Seleccionamos todo de la tabla");
                        String selectQuery = "SELECT * FROM mytable;";
                        cliente.sendQuery(selectQuery, socket);
                        break;

                    case "3":
                        Console.WriteLine("Case 3");
                        cliente.sendQuery(text, socket);
                        break;

                    case "4":
                        Console.WriteLine("Selecciona el nombre de la tabla a borrar");
                        text = Console.ReadLine();
                        String deleteQuery = "DELETE FROM mytable WHERE (" + text + ");";
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
