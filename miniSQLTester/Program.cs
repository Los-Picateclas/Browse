using BrowseLib;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Text.RegularExpressions;

namespace MiniSQLTest
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
            string createDatabasePattern = "(\\w+),(\\w+),(\\w+)";

            line = inputFile.ReadLine();
            string output = "";

            while (line != null)
            {
                Match match = Regex.Match(line, createDatabasePattern);

                if (line == "")
                {
                    output = "TOTAL TIME: " + sumTime + "ms\n\n# TEST " + testNumber;
                    testNumber = testNumber + 1;
                    sumTime = 0;
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
                    if (br.Databases.Contains(br.Databases.Find(d => d.databaseName == databaseName)))
                    {
                        Database dbaux = br.Databases.Find(d => d.databaseName == databaseName);
                        dbaux.setActualUser(userName);
                        sw.Stop();
                        output = "Database opened " + sw.ElapsedMilliseconds + " ms";
                    }
                    //if it doesnt exists
                    else
                    {
                        db = new Database(databaseName, userName, profileName);
                        db.saveDatabase();
                        br.addDatabase(db);
                        db.setActualUser(userName);
                        output = "Database created " + sw.ElapsedMilliseconds + " ms";
                    }
                    
                }
                else if (line != "" && line != null && !match.Success)
                {
                    if (db != null)
                    {
                        Stopwatch sw = new Stopwatch();
                        sw.Start();
                        output = db.ExecuteMiniSQLQuery(line);
                        sw.Stop();
                        double miliSec = sw.Elapsed.TotalMilliseconds;
                        output += " (" + miliSec + " ms)";
                        sumTime += miliSec;
                    }
                }
                else if (line == null)
                {
                    output = "TOTAL TIME: " + sumTime + "ms";
                }
                Console.WriteLine(output);
                lines.Add(output);
                line = inputFile.ReadLine();
            }
            File.WriteAllLines("../../../Input-Output/output-file.txt", lines);
            Console.WriteLine("Querys Finished");
        }
    }
}
