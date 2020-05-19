using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Text.RegularExpressions;
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
            Database db = null;
            
            string line = "";
            StreamReader inputFile = new StreamReader("../../../Input-Output/input-file.txt");
            List<string> lines = new List<string>();
            Console.WriteLine("# TEST 1");
            lines.Add("# TEST 1");
            int testNumber = 2;
            double sumTime = 0;
            string createDatabasePattern = "(\\w+), (\\w+), (\\w+)";

            line = inputFile.ReadLine();


            while (line != null)
            {
                
               

                Match match = Regex.Match(line, createDatabasePattern);

                if (line == "")
                {
                    string output = "TOTAL TIME: " + sumTime + "ms\n\n# TEST " + testNumber;
                    Console.WriteLine(output);
                    lines.Add(output);
                    testNumber = testNumber + 1;
                    sumTime = 0;
                    line = inputFile.ReadLine();
                }
                else if (match.Success)
                {
                    Console.WriteLine("match succed: " + line);
                    Stopwatch sw = new Stopwatch();
                    sw.Start();
                    string databaseName = match.Groups[1].Value;
                    string userName = match.Groups[2].Value;
                    string profileName = match.Groups[3].Value;

                    //if exists
                    //if it doesnt exists
                    db = new Database(databaseName, userName, profileName);
                    db.saveDatabase();
                    line = inputFile.ReadLine();
                }
                else if (line != "" && line != null && !match.Success)
                {
                    if (db != null)
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
                    line = inputFile.ReadLine();
                }
                else if (line == null)
                {
                    string output = "TOTAL TIME: " + sumTime + "ms";
                    Console.WriteLine(output);
                    lines.Add(output);
                    line = inputFile.ReadLine();
                }
                else { line = inputFile.ReadLine(); }
            }
            File.WriteAllLines("../../../Input-Output/output-file.txt", lines);
            Console.WriteLine("Querys Finished");
        }
    }
}

