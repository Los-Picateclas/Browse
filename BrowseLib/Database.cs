using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrowseLib
{
    public class Database
    {
        public string name;
        public List<Table> tables;

        public Database(string name) {
            this.name = name;
            tables = new List<Table>();
         }

        public void addTable(Table table) {
            tables.Add(table);
        }

        public void dropTable(Table table)
        {
            tables.Remove(table);
        }

        public void updateName(string name)
        {
            this.name = name;
        }

        public Table getTable(int position)
        {
            return tables.ElementAt(position);
        }
    }
}
