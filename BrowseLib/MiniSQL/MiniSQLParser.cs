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
            const string selectPattern = "SELECT ([\\w,\\s]+) FROM (\\w+)[\\sWHERE\\s]*([\\w=<>]+)?\\s?;";
            //const string insertPattern = "INSERT INTO (\\w+) VALUES \\(([\\w,\\s]+)\\)\\s?;";
            const string insertPattern = "INSERT INTO (\\w+) VALUES \\((.+)\\);";
            const string deletePattern = "DELETE FROM (\\w+) WHERE (\\w+\\s?[=<>]\\s?\\d+);";
            const string dropPattern = "DROP TABLE (\\w+);";
            const string updatePattern = "UPDATE (\\w+) SET (\\w+)=(\\w+) WHERE (\\w+\\s?[=<>]\\s?\\d+);";
            //const string createPattern = "CREATE TABLE (\\w+)\\s+\\((\\w+)\\s+(INT|DOUBLE|TEXT)(\\,\\s+(\\w+)\\s+(INT|DOUBLE|TEXT))+\\);";
            //const string createPattern2 = "CREATE TABLE (\\w) \\((\\w) \\((INT|DOUBLE|TEXT)));";
            const string createPattern = "CREATE TABLE (\\w+) \\(((\\w+) (\\w+),?\\s?)+\\);";
            const string createProfilePattern = "CREATE SECURITY PROFILE (\\w+);";

            //Select
            Match match = Regex.Match(miniSQLQuery, selectPattern);
            if (match.Success)
            {
                List<string> columnNames = CommaSeparatedNames(match.Groups[1].Value);
                string table = match.Groups[2].Value;
                string condition = match.Groups[3].Value;
                return new Select(table, columnNames, condition);
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
                List<string> columnNames = new List<string>();
                List<string> columnTypes = new List<string>();
                CaptureCollection ccNames = match.Groups[3].Captures;
                CaptureCollection ccTypes = match.Groups[4].Captures;
                for (int i = 0; i < ccNames.Count; i++)
                {
                    columnNames.Add(ccNames[i].Value);
                    columnTypes.Add(ccTypes[i].Value);
                }
                return new CreateTable(table, columnNames, columnTypes);

            }
            //CreateProfile
            match = Regex.Match(miniSQLQuery, createProfilePattern);
            if (match.Success)
            {
                string profile = match.Groups[1].Value;
               

                return new CreateProfile(profile);

            }
            return null;

        }

        // Returns the list of words divided by commas and removes spaces
        static List<string> CommaSeparatedNames(string text)
        {
            List<string> names = new List<string>();
            string[] namesSeparated = text.Split(',');
            foreach (string name in namesSeparated)
            {
                names.Add(name.Trim().Trim('\''));
            }
            return names;
        }




    }
}
