using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrowseLib.MiniSQL
{
    public class DropTable : MiniSQLQuery
    {
        public string table;
        public DropTable(string Table)
        {
            table = Table;
        }



        public string Execute(Database database) {


            return database.drop(table);
        }
    }
}
