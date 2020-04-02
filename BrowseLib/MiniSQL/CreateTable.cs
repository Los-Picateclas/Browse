﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrowseLib.MiniSQL
{
    class CreateTable : MiniSQLQuery
    {
        public string Table = null;
        public List<string> Columns = null;

        public CreateTable(string table, List<string> columns)
        {
            string fileName = "../data/Browse/" + table + ".txt";
            try
            {
                if (!File.Exists(fileName))
                {
                    File.Create(fileName);
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            Table = table;
            Columns = columns;
        }

        public string Execute(Database database)
        {
            throw new NotImplementedException();
        }
    }
}
