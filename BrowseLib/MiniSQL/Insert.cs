﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrowseLib.MiniSQL
{
    public class Insert : MiniSQLQuery
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
            return database.insert(Table, Columns);
        }
    }
}
