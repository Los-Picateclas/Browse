using System.Collections.Generic;

namespace BrowseLib.MiniSQL
{
    public class Select : MiniSQLQuery
    {
        public string Table = null;
        public List<string> Columns = null;
        public string Condition = null;

        public Select(string table, List<string> columns, string condition)
        {
            Table = table;
            Columns = columns;
            Condition = condition;
        }


        public string Execute(Database database)
        {
            
            return database.Select(Table, Columns, Condition);
        }
    }
}