using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrowseLib.MiniSQL
{
    public class Update : MiniSQLQuery
    {
        public string Table = null;
        public string Condition = null;
        public string UpdateValue = null;
        public string TargetColumn = null;

        public Update(string table, string condition, string update, string targetColumn) {

            Table = table;
            Condition = condition;
            UpdateValue = update ;
            TargetColumn = targetColumn;


        }

        public string Execute(Database database)
        {
            return database.update(Table, Condition, UpdateValue,TargetColumn);
        }
    }
}
