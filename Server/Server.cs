using System;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using BrowseLib;
using BrowseLib.MiniSQL;

namespace Server
{
    class Server
    {
        public static Database db;

        public void initialize()
        {

            //Create the table
            Table tb = new Table("mytable");

            //Create the columns
            Column cm = new Column("mycolumn", "text");

            //Create profile and give permissions
            Profile prof = new Profile("admin");
            TablePermission tbp = new TablePermission(tb.getName());
            tbp.addPrivilege(Privileges.DELETE);
            tbp.addPrivilege(Privileges.INSERT);
            tbp.addPrivilege(Privileges.SELECT);
            tbp.addPrivilege(Privileges.UPDATE);
            prof.addTablePermission(tbp);

            //Give privileges
            User user = new User("admin", "admin", prof);
           
            //Create the database
            db = new Database("mydatabase", user.getName(), user.getPassword());

            //Initialize the database
            db.addUser(user.getName(), user.getPassword(), prof.getName());
            db.setActualUser(user.getName());
            tb.addColumn(cm);
            db.addTable(tb);

        }
        public static void Main(string[] args)
        {

            //Create the server and initialize
            Server server = new Server();
            server.initialize();

            //We will use sockets to make the server. 
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            //Now we specify the IP and the port. 127.0.0.1 is our local pc direction
            IPEndPoint direction = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 1234);
            socket.Bind(direction);

            //We only accept one connection for our socket
            socket.Listen(1); 
            Console.WriteLine("Waiting for a client...");

            //This new socket will return the client response
            Socket listen = socket.Accept(); 
            Console.WriteLine("Connected");

            //We can only use bytes to transfer the information
            byte[] info = new byte[255];

            while (true)
            {

                Array.Clear(info, 0, info.Length);
                int receive = listen.Receive(info, 0, info.Length, 0);

                //We tranform the info to string
                string infoString = Encoding.Default.GetString(info); 
                Console.WriteLine("Client says: " + infoString);

                //The server doesn´t receive all the 0s frome the query
                int found = infoString.IndexOf("\0");
                String s = infoString.Substring(0, found);

                //Execute MiniSQLQuery and do the Parser
                String valor = db.ExecuteMiniSQLQuery(s);
                Console.WriteLine("Server says: " + valor);
                socket.Close();

            }           
        }
    } 
}



