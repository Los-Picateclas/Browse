using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrowseLib
{
    public class Database
    {
        public List<Table> tables;
        public Database db = new Database("databaseName", "username", "password");
        public string databaseName;

        public Database(string databaseName, string username, string password)
        {
            tables = new List<Table>();
            db = new Database(databaseName, username, password);
        }



        public void addTable(Table table)
        {
            tables.Add(table);
        }

        public void dropTable(Table table)
        {
            tables.Remove(table);
        }

        public void updateName(string databaseName)
        {
            this.databaseName = databaseName;
        }

        public Table getTable(int position)
        {
            return tables.ElementAt(position);
        }
    }
}
