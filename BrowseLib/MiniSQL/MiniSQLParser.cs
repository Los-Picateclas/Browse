using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BrowseLib.MiniSQL
{
    public class MiniSQLParser
    {
        public static MiniSQLQuery Parse(string miniSQLQuery)
        {
            const string selectPattern = "SELECT ([\\w,\\s]+) FROM (\\w+)\\s*;";
            const string insertPattern = "INSERT INTO (\\w+) VALUES \\(([\\w,\\s]+)\\)\\s?;";
            //Select
            Match match = Regex.Match(miniSQLQuery, selectPattern);
            if (match.Success)
            {
                List<string> columnNames = CommaSeparatedNames(match.Groups[1].Value);
                string table = match.Groups[2].Value;
                return new Select(table, columnNames);
            }
            //Insert
            match = Regex.Match(miniSQLQuery, insertPattern);
            if (match.Success)
            {
                string table = match.Groups[1].Value;
                List<string> columnNames = CommaSeparatedNames(match.Groups[2].Value);
                return new Insert(table, columnNames);
            }
            return null;
        }

        // Returns the list of words divided by commas and removes spaces
        static List<string> CommaSeparatedNames(string text)
        {
            List<string> names = new List<string>();
            string[] namesSeparated = text.Split(',');
            foreach(string name in namesSeparated)
            {
                names.Add(name.Trim());
            }
            return names;
        }
        
    }
}
