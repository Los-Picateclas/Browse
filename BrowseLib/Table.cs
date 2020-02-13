using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrowseLib
{
    class Table
    {
        private string name;

       private List<Column> columns;

        public Table(string n) {
            name = n;
            columns = new List<Column>();
         }

        public void addColumn(Column c) {
            columns.Add(c);
        }

        public void dropColumn(Column c)
        {
            columns.Remove(c);
        }







































    }
}
