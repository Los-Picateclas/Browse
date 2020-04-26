
using System;
using System.Diagnostics;
using System.IO;
using BrowseLib;

namespace Programa
{
    class Program
    {
       

        static void Main(string[] args)
        {
            Directory.CreateDirectory("../../../BrowseProgram");
            Database db = new Database("db1","user", "pass");

            // Console.WriteLine(abc[0]);
            //val = Console.ReadLine();

            string linea = "";
            System.IO.StreamReader file = new System.IO.StreamReader("../../../Inputs/input-file.txt");

            while (linea != null)
            {
                linea = file.ReadLine();
                if (linea != "" && linea != null)
                {
                    Stopwatch sw = new Stopwatch();
                    sw.Start();
                    string output = db.ExecuteMiniSQLQuery(linea);
                    double miliSec = sw.Elapsed.TotalMilliseconds;
                    output += "(" + miliSec + ")";
                    Console.WriteLine(output);
                    sw.Stop();
                }
            }
            Console.WriteLine("Querys Finished");
        }
    }
}