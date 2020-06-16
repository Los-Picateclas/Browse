using System;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
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

        public static void broadcast(String data, TcpClient client)
        {
                NetworkStream stream = client.GetStream();

                byte[] buffer = Encoding.ASCII.GetBytes(data);
                stream.Write(buffer, 0, buffer.Length);
            
        }


        public static void Main(string[] args)
        {
            //Create the server and initialize
            Server server = new Server();
            server.initialize();

            //We will use TCP sockets to make the server. 
            TcpListener ServerSocket = new TcpListener(IPAddress.Any, 5000);
            ServerSocket.Start();

            //We only accept one connection for our socket
            TcpClient client = ServerSocket.AcceptTcpClient();
            Console.WriteLine("Client connected...");

            while (true)
            {
                NetworkStream stream = client.GetStream();
                byte[] buffer = new byte[1024];
                int byte_count = stream.Read(buffer, 0, buffer.Length);
                byte[] formated = new Byte[byte_count];
                //Handle  the null characteres in the byte array
                Array.Copy(buffer, formated, byte_count);
                //We tranform the info to string
                string data = Encoding.ASCII.GetString(formated);
                Console.WriteLine(data);
                //Execute MiniSQLQuery and do the Parser
                String valor = db.ExecuteMiniSQLQuery(data);
                Server.broadcast(valor, client);


            }
        }
    } 
}



