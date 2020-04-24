
using System;
using System.Diagnostics;
using BrowseLib;

namespace Programa
{
    class Program
    {

        static void Main(string[] args)
        {
            Database db = new Database("db1","user", "pass");

            // Console.WriteLine(abc[0]);
            //val = Console.ReadLine();

            string linea = "";
            System.IO.StreamReader file = new System.IO.StreamReader(@"input-file.txt");

            while (linea != null)
            {
                linea = file.ReadLine();
                if (linea != "" && linea != null)
                {
                    Stopwatch sw = new Stopwatch();
                    sw.Start();
                    string output = db.ExecuteMiniSQLQuery(linea) + "(";
                    output += sw.Elapsed.TotalMilliseconds + ")";
                    Console.WriteLine(output);
                    sw.Stop();
                }
            }
            Console.WriteLine("Querys Finished");
        }
    }
}