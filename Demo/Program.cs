using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Demo
{
    class Program
    {
        public class ColumnDefinition
        {
            public string Name { get; set; }
            public string Type { get; set; }
        }
        static void Main(string[] args)
        {
            string input = "lkasjdflkjs SELECT * FROM table1 alskdjf 23k 2j slkdjflk ss SELECT * FROM table2";
            string regExp = @"SELECT\s+(\*)\s+FROM\s+(\w+)";

            //buscamos solo 1 coincidencia
            Console.WriteLine("Regex.Match()");
            Match match = Regex.Match(input, regExp);
            if (match.Success)
            {
                Console.WriteLine("Groups[0]= " + match.Groups[0].Value);
                Console.WriteLine("Groups[1]= " + match.Groups[1].Value);
                Console.WriteLine("Groups[2]= " + match.Groups[2].Value);
            }
            Console.WriteLine("Regex.Matches()");
            foreach (Match m in Regex.Matches(input, regExp))
            {
                Console.WriteLine("Groups[0]= " + m.Groups[0].Value);
                Console.WriteLine("Groups[1]= " + m.Groups[1].Value);
                Console.WriteLine("Groups[2]= " + m.Groups[2].Value);
            }

            using (StreamWriter writer = File.CreateText("../../../db/test.txt"))
            {
                writer.WriteLine("Name String");
                writer.WriteLine("Age Int");
                writer.WriteLine("Id PRIMARY_KEY(Int)");
                writer.Write("Address String");
            }

            string allFile = File.ReadAllText("../../../db/test.txt");
            string[] lines = allFile.Split('\n');
            List<ColumnDefinition> columnDefinitions = new List<ColumnDefinition>();

            foreach (string line in lines)
            {
                string[] parts = line.Split(' ');
                ColumnDefinition columnDefinition = new ColumnDefinition();
                columnDefinition.Name = parts[0];
                columnDefinition.Type = parts[1];
                columnDefinitions.Add(columnDefinition);

            }


        }
    }
}
