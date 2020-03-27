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
            const string insertPattern = "...";
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
                //...
            }
            return null;
        }

        // This method is not finished
        static List<string> CommaSeparatedNames(string text)
        {
            List<string> names = new List<string>();
            return names;
        }
        
    }
}
