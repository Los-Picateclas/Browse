using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Collections.Generic;
using BrowseLib;
using System.IO;
using System.Data;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Server
{

    class Server
    {
        public static Table table = new Table("test-table");
        public static Database db1 = new Database("test-db1", "username1", "password1");
        public static Database db2 = new Database("test-db2", "username2", "password2");

        static void Main(string[] args)
        {
            Console.Clear();
            Server server = new Server();

            //La Dirección IP y el Puerto

            IPAddress servidor = IPAddress.Parse("127.0.0.1");  //El servidor
            TcpListener puerto = new TcpListener(IPAddress.Any, 1234); //El puerto
            Console.WriteLine("Conexión con el servidor inicializada");
            Console.WriteLine("Esperando a lo que diga cliente:");


            while (true)
            {
                //Aquí comienza el listener, cuya función es la de atender las solicitudes de los clientes
                puerto.Start();
                TcpClient cliente = puerto.AcceptTcpClient(); //Se crea el cliente

                //Aquí se abre el flujo de datos
                NetworkStream redDatos = cliente.GetStream();
                byte[] buffer = new byte[cliente.ReceiveBufferSize];

                //Con esto lee el buffer y lo convierte a String
                int bytesRead = redDatos.Read(buffer, 0, cliente.ReceiveBufferSize);
                string dataRecieved = Encoding.ASCII.GetString(buffer, 0, bytesRead);

                //Comenzamos como si el cliente quisiera crearse una cuenta nueva
                if (dataRecieved.Substring(dataRecieved.Length) == "True")
                {
                    Console.WriteLine("\n Intentando crear nueva cuenta", dataRecieved.Substring(0, dataRecieved.IndexOf("/////")));
                    if (crearUsuarioNuevo(dataRecieved.Substring(0, dataRecieved.Length)))
                    {
                        // El Proceso va bien
                        Console.WriteLine("Funciona");

                        buffer = ASCIIEncoding.ASCII.GetBytes("True");
                        redDatos.Write(buffer, 0, buffer.Length);
                    }
                    else
                    {
                        // El proceso va mal
                        Console.WriteLine("No funciona");

                        buffer = ASCIIEncoding.ASCII.GetBytes("False");
                        redDatos.Write(buffer, 0, buffer.Length);
                    }
                }
            }

        }
        //Aquí se crea un usuario nuevo
        public static bool crearUsuarioNuevo(string nombreUsuario)
        {
            bool unicoNombreUsuario = true;

            //Comprobamos si el usuario existe
            foreach (string linea in File.ReadAllLines("username-database.txt"))
            {
                if (nombreUsuario.Substring(0, nombreUsuario.IndexOf("/////")) == linea.Substring(0, linea.IndexOf("/////")))
                {
                    unicoNombreUsuario = false;
                }
            }
            //Cuando el nombre del usuario existe, falso
            if (!unicoNombreUsuario)
            {
                return false;
            }
            //Cuando no existe se añade a la lista txt y true
            File.AppendAllText("username-database.txt", nombreUsuario + "\n");
            return true;
        }
    }
    }
