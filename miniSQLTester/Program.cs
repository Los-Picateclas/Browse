using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using BrowseLib;
using BrowseLib.MiniSQL;
using Bundler;

namespace Programa
{
    class Program
    {
        static void Main(string[] args)
        {
            
            Browse br = new Browse();
            br.saveBrowse();
            Database db = new Database("db1", "user", "pass");
            db.saveDatabase();
            string line = "";
            StreamReader inputFile = new StreamReader("../../../Input-Output/input-file.txt");
            List<string> lines = new List<string>();
            Console.WriteLine("# TEST 1");
            lines.Add("# TEST 1");
            int nDB = 2;
            double sumTime = 0;


            while (line != null)
            {
                line = inputFile.ReadLine();
                if (line == "")
                {
                    string output = "TOTAL TIME: " + sumTime + "ms\n\n# TEST " + nDB;
                    Console.WriteLine(output);
                    lines.Add(output);
                    Database dbAux = new Database("db" + nDB, "user", "pass");
                    dbAux.saveDatabase();
                    db = dbAux;
                    nDB++;
                    sumTime = 0;
                }
                else if (line != "" && line != null)
                {
                    Stopwatch sw = new Stopwatch();
                    sw.Start();
                    string output = db.ExecuteMiniSQLQuery(line);
                    sw.Stop();
                    double miliSec = sw.Elapsed.TotalMilliseconds;
                    output += " (" + miliSec + " ms)";
                    Console.WriteLine(output);
                    lines.Add(output);
                    sumTime += miliSec;
                }
                else if (line == null)
                {
                    string output = "TOTAL TIME: " + sumTime + "ms";
                    Console.WriteLine(output);
                    lines.Add(output);
                }
            }
            File.WriteAllLines("../../../Input-Output/output-file.txt", lines);
            Console.WriteLine("Querys Finished");
        }
    }
}

