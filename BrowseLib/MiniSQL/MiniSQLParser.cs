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
            const string deletePattern = "DELETE FROM (\\w+) WHERE (\\w+\\s?[=<>]\\s?\\d+);";
            const string dropPattern = "DROP TABLE (\\w+);";
            const string updatePattern = "UPDATE (\\w+) SET (\\w+)=(\\w+) WHERE (\\w+\\s?[=<>]\\s?\\d+);";
            const string createPattern = "CREATE TABLE (\\w+)\\s+\\((\\w+)\\s+(INT|DOUBLE|TEXT)(\\,\\s+(\\w+)\\s+(INT|DOUBLE|TEXT))+\\);";

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

            //Update
            match = Regex.Match(miniSQLQuery, updatePattern);
            if (match.Success)
            {
                string table = match.Groups[1].Value;
                string targetColumn = match.Groups[2].Value;
                string update = match.Groups[3].Value;
                string condition = match.Groups[4].Value;
                return new Update(table, condition, update, targetColumn);
            }

            //Delete
            match = Regex.Match(miniSQLQuery, deletePattern);
            if (match.Success)
            {
                string table = match.Groups[1].Value;
                string condition = match.Groups[2].Value;
                return new Delete(table, condition);
            }

            //Drop
            match = Regex.Match(miniSQLQuery, dropPattern);
            if (match.Success)
            {
                string table = match.Groups[1].Value;


                return new DropTable(table);
            }

        
            //CreateTable
            match = Regex.Match(miniSQLQuery, createPattern);

            if (match.Success)
            {
                string table = match.Groups[1].Value;
                List<string> columnNames = CommaSeparatedNames(match.Groups[2].Value);
                return new CreateTable(table, columnNames);

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
