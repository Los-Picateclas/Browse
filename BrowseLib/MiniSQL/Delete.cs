using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrowseLib.MiniSQL
{
    public class Delete : MiniSQLQuery
    {
        public string Table = null;
        public string Condition = null;

        public Delete(string table, string condition)
        {
            Table = table;
            Condition = condition;
        }

        public string Execute(Database database)
        {
            return database.delete(Table, Condition);
        }
    }
}
