using BrowseLib.MiniSQL;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using BrowseLib.MiniSQL;

namespace BrowseLib
{
    public class Database
    {
        public List<Table> tables;
        public Database db;
        public string databaseName;
        public string username;
        public string password;

        public Database(string dN, string uN, string pW)
        {
            tables = new List<Table>();
            this.databaseName = dN;
            this.username = uN;
            this.password = pW;
        }
        //It saves each table from the list
        public void saveAllTables(Database d)
        {
            for (int i = 0; i < tables.Count; i++)
            {
                tables.ElementAt(i).save(tables.ElementAt(i),d );
            }

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

        public void loadTables (string path)
        {
            try
            {
                DirectoryInfo di = new DirectoryInfo(path);
                DirectoryInfo[] directories = di.GetDirectories();
                foreach (DirectoryInfo fi in directories)
                {
                    tables.Add(new Table(fi.Name));
                }
            }
            catch (Exception e)
            {
               throw e;
            }

        }
        public string ExecuteMiniSQLQuery(string query) {

            MiniSQLQuery miniSQLQuery = MiniSQLParser.Parse(query);

            if (miniSQLQuery == null)
                return "Error";




            return miniSQLQuery.Execute(this);

        }

        public void saveTables(string path)
        {
            try
            {
                foreach (Table tb in tables)
                {
                    Directory.CreateDirectory(Path.Combine(path, tb.getName()));
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void saveDatabase()
        {
            try
            {
                Directory.CreateDirectory("../data/Browse/" + databaseName);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public string Select(string table, List<string> columns)
        {
            //Do whatever you have to do
            return null;
        }
        public string Insert(string table, List<string> columns, List<string> values)
        {
            //Do whatever you have to do
            return null;
        }

        public string ExecuteMiniSQLQuery(string query)
        {
            //Parse the query
            MiniSQLQuery miniSQLQuery = MiniSQLParser.Parse(query);

            if (miniSQLQuery == null)
                return "Error";

            return miniSQLQuery.Execute(this);
        }
    }
}
