using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrowseLib.MiniSQL
{
    public class CreateTable : MiniSQLQuery
    {
        public string Table;
        public List<string> Columns;
        public List<string> Types;

        public CreateTable(string table, List<string> columns, List<string> types)
        {
            Table = table;
            Columns = columns;
            Types = types;
        }

        public string Execute(Database database)
        {
            return database.createTable(Table, Columns, Types, database);
        }
    }
}