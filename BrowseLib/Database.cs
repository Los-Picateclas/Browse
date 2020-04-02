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
                tables.ElementAt(i).save(tables.ElementAt(i), d);
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

        public void loadTables(string path)
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
            {
                return "Error";
            }

            return miniSQLQuery.Execute(this);

        }
        public Table getTable(string tab) {
            Table aux = null;
            foreach (Table t in tables) {
                if (t.getName().Equals(tab)) {

                    aux = t;
                }


            }
            return aux;
        }

        public void saveTables(string path)
        {
            try
            {
                foreach (Table tb in tables)
                {
                    Directory.CreateDirectory("../data/Browse/" + tables);
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



        public string insert(string tab, List<String> Col){

            foreach (Table tb in tables){
                if (tab.Equals(tb.getName())){
                    int i = 0;
                    List<Column> columnsFromTb = tb.getColumns();
                    foreach (Column c in columnsFromTb) {
                        c.insert(Col[i]);
                        i++;



                    }


            }
        }

            return "";
        }







    public string Select(string table, List<string> columns)
        {
            string select = "";
            List<int> numCl = new List<int>();

            foreach (Table tb in tables)
            {
                if (table == tb.getName())
                {
                    select = "{";
                    foreach (string column in columns)
                    {
                        foreach (Column cl in tb.columns)
                        {
                            if (column == cl.name)
                            {
                                if (numCl.Count()==0)
                                {
                                    numCl.Add(tb.columns.IndexOf(cl));
                                    select += "'" + column + "'";
                                }
                                else
                                {
                                    numCl.Add(tb.columns.IndexOf(cl));
                                    select += ",'" + column + "'";
                                }
                                
                            }
                        }
                    }
                    select += "} => ";
                    
                    int columnLength = tb.columnSize()-1;
                    for (int j = 0; j < columnLength; j++)
                    {
                        select += "{";
                        foreach (int i in numCl)
                        {
                            if (i == 0)
                            {
                                select += tb.selectColumn(i).column[j];
                            }
                            else
                            {
                                select += "," + tb.selectColumn(i).column[j];
                            }
                        }
                        select += "}";
                    }
                }
            }

            return select;
        }
    }
}
