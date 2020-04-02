using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BrowseLib.MiniSQL
{
    class Insert : MiniSQLQuery
    {
        public string Table = null;
        public List<string> Columns = null;

        public Insert(string table, List<string> columns)
        {
            Table = table;
            Columns = columns;
        }
        
        public string Execute(Database database)
        {
            int i = 0;
            string name = Table;
            Table t = database.getTable(name);
            foreach (Column c in t.getColumns()) {
                c.insert(Columns[i]);
                MessageBox.Show("escrito");

            }
            
            return "";// database.Insert(Table, Columns);
        }
    }
}
