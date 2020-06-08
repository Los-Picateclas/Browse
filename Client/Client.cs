﻿using Microsoft.VisualBasic.CompilerServices;
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
            //We need a socket and a IP adrees to connect to the server so we create it.
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPEndPoint direction = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 1234);
            socket.Connect(direction);
            Console.WriteLine("Connected to the server");
            Console.WriteLine("Write exit in case you want to end");
            Console.WriteLine(" Introduce the SQL query...");
            string text = Console.ReadLine();

            //Name of the txt archive 
            while (text != "exit")
            {
                Console.WriteLine("Sending information to the server...");

                //We need to transform the info to bytes so we can send it to the server
                byte[] byteText = Encoding.Default.GetBytes(text);
                socket.Send(byteText, 0, byteText.Length, 0);
                Console.WriteLine("Done");
                Console.WriteLine();
                text = Console.ReadLine();
            }
            /*
            //Insert
                String query = "INSERT INTO db.mytable(id,username,password) VALUES (@id,@username,@password)";

                    
                    //socket.Send(byteText, 0, byteText.Length, 0);
                    command.Parameters.AddWithValue("@username", "abc");
                    command.Parameters.AddWithValue("@password", "abc");
                    command.Parameters.AddWithValue("@email", "abc");

                    connection.Open();
                    int result = command.ExecuteNonQuery();

                    // Check Error
                    if (result < 0)
                        Console.WriteLine("Error inserting data into Database!");
                        */
                    
        }
    }
}
