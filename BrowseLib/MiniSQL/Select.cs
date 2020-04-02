using System.Collections.Generic;

namespace BrowseLib.MiniSQL
{
    public class Select : MiniSQLQuery
    {
        public string Table = null;
        public List<string> Columns = null;

        public Select(string table, List<string> columns)
        {
            Table = table;
            Columns = columns;
        }


        public string Execute(Database database)
        {
            
            return database.Select(Table, Columns);
        }
    }
}