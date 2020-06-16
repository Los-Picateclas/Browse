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
            TcpClient client = new TcpClient();
            
            //Now we specify the IP and the port. 127.0.0.1 is our local pc direction
            IPAddress IP = IPAddress.Parse("127.0.0.1");
            int Port = 5000;

            //We make the conexion with server 
            client.Connect(IP, Port);
            NetworkStream ns = client.GetStream();

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
              
                byte[] buffer = Encoding.ASCII.GetBytes(query);
                ns.Write(buffer, 0, buffer.Length);
                byte[] receivedBytes = new byte[1024];
                int byte_count = ns.Read(receivedBytes, 0, receivedBytes.Length);
                byte[] formated = new byte[byte_count];
                //handle  the null characteres in the byte array
                Array.Copy(receivedBytes, formated, byte_count);
                //We tranform the info to string
                string data = Encoding.ASCII.GetString(formated);
                Console.WriteLine(data);
                query = Console.ReadLine();
            }
            ns.Close();
            client.Close();
            Console.WriteLine("Disconnected");
            Console.ReadKey();
        }
    }
}
