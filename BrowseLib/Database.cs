using BrowseLib.MiniSQL;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using BrowseLib.MiniSQL;
using BrowseLib;


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



        public string drop(string table)
        {

            string route = "../BrowseProgram/" + databaseName + "/" + table;
            File.Delete(route);
            return table + " deleted";

        }




        public string ExecuteMiniSQLQuery(string query)
        {

            MiniSQLQuery miniSQLQuery = MiniSQLParser.Parse(query);

            if (miniSQLQuery == null)
            {
                return "Error";
            }

            return miniSQLQuery.Execute(this);

        }
        public Table getTable(string tab)
        {
            Table aux = null;
            foreach (Table t in tables)
            {
                if (t.getName().Equals(tab))
                {

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
                    Directory.CreateDirectory("../BrowseProgram/" + tables);
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
                Directory.CreateDirectory("../dBrowseProgram/" + databaseName);
            }
            catch (Exception e)
            {
                throw e;
            }
        }



        public string insert(string tab, List<String> Col)
        {
            string resultado = "";
            string datos = "";
            foreach (Table tb in tables)
            {
                if (tab.Equals(tb.getName()))
                {
                    int i = 0;
                    List<Column> columnsFromTb = tb.getColumns();
                    resultado += "{";
                    foreach (Column c in columnsFromTb)
                    {
                        c.insert(Col[i]);
                        if (i == 0)
                        {
                            resultado += "'" + c.name + "'";
                            datos += Col[i];
                        }
                        else
                        {
                            resultado += ",'" + c.name + "'";
                            datos += "," + Col[i];
                        }
                        i++;
                    }
                    resultado += "}";
                }
            }
            resultado += " => {" + datos + "}";
            return resultado;
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
                                if (numCl.Count() == 0)
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

                    int columnLength = tb.columnSize() - 1;
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

        // Delete the tuples from the given table that satisfy the condition (only accept a condition of type) 
        public string delete(string table, string condition)
        {
            char[] delimiterChars = { '<', '=', '>' };
            string[] words = condition.Split(delimiterChars);

            string column = words[0];
            string postCon = words[1];
            int postNum;
            bool esNumero = int.TryParse(postCon, out postNum);
            char symbol;

            if (condition.Contains('<'))
            {
                symbol = '<';
            }
            else if (condition.Contains('='))
            {
                symbol = '=';
            }
            else
            {
                symbol = '>';
            }

            string result = "{'" + column + "'} => ";
            foreach (Table tb in tables)
            {
                if (table.Equals(tb.getName()))
                {
                    foreach (Column c in tb.columns)
                    {
                        if (column.Equals(c.name))
                        {
                            for (int i = 0; i < c.getColumnSize(); i++)
                            {
                                switch (symbol)
                                {
                                    case '<':
                                        {
                                            if (esNumero && Int32.Parse(c.column[i]) < postNum)
                                            {
                                                result += tb.selectTuple(i);
                                                tb.deleteTuple(i);
                                                i = i - 1;
                                            }
                                        }
                                        break;
                                    case '=':
                                        {
                                            if (esNumero && Int32.Parse(c.column[i]) == postNum)
                                            {
                                                result += tb.selectTuple(i);
                                                tb.deleteTuple(i);
                                                i = i - 1;
                                            }
                                            else if (!esNumero && c.column[i] == postCon)
                                            {
                                                result += tb.selectTuple(i);
                                                tb.deleteTuple(i);
                                                i = i - 1;
                                            }
                                        }
                                        break;
                                    case '>':
                                        {
                                            if (esNumero && Int32.Parse(c.column[i]) > postNum)
                                            {
                                                result += tb.selectTuple(i);
                                                tb.deleteTuple(i);
                                                i = i - 1;
                                            }
                                        }
                                        break;
                                }
                            }
                        }
                    }
                }
            }
            return result;
        }
    

        //'targetColumn' must be the column where you want to make changes
        // 'update' parameter must be the new value that we are setting 
    public string update(string table, string condition, string update, string targetColumn)
    {

        char[] delimiterChars = { '<', '=', '>' };
        string[] words = condition.Split(delimiterChars);

        string column = words[0];
        string postCon = words[1];
        int postNum;
        bool esNumero = int.TryParse(postCon, out postNum);
        char symbol;

        if (condition.Contains('<'))
        {
            symbol = '<';
        }
        else if (condition.Contains('='))
        {
            symbol = '=';
        }
        else
        {
            symbol = '>';
        }
      string result = " ";
  
        foreach (Table tb in tables)
        {
            if (table.Equals(tb.getName()))
            {
                foreach (Column searchColumn1 in tb.columns)
                {
                    if (column.Equals(searchColumn1.name))
                    {
                        for (int i = 0; i < searchColumn1.getColumnSize(); i++)
                        {
                            switch (symbol)
                            {
                                case '<':
                                    {
                                        if (esNumero && Int32.Parse(searchColumn1.column[i]) < postNum)
                                        {
                                            foreach (Column searchColumn2 in tb.columns)
                                            {
                                                    if (searchColumn2.name.Equals(targetColumn))
                                                    {
                                                        result = "{'" + searchColumn2.getTextFromColumn(i) + "'} => ";
                                                        searchColumn2.updateColumn(i, update);
                                                        result = result + "{'" + searchColumn2.getTextFromColumn(i) + "'}";
                                                    }
                                                    
                                                }
                                        }
                                    }
                                    break;
                                case '=':
                                    {
                                        if (esNumero && Int32.Parse(searchColumn1.column[i]) == postNum)
                                        {
                                            foreach (Column searchColumn2 in tb.columns)
                                            {
                                                    if (searchColumn2.name.Equals(targetColumn))
                                                    {
                                                        result = "{'" + searchColumn2.getTextFromColumn(i) + "'} => ";
                                                        searchColumn2.updateColumn(i, update);
                                                        result = result + "{'" + searchColumn2.getTextFromColumn(i) + "'}";
                                                    }
                                                   
                                                }
                                        }
                                        else if (!esNumero && searchColumn1.column[i] == postCon)
                                        {
                                            foreach (Column searchColumn2 in tb.columns)
                                            {
                                                    if (searchColumn2.name.Equals(targetColumn))
                                                    {
                                                        result = "{'" + searchColumn2.getTextFromColumn(i) + "'} => ";
                                                        searchColumn2.updateColumn(i, update);
                                                        result = result + "{'" + searchColumn2.getTextFromColumn(i) + "'}";
                                                    }
                                                   
                                                }
                                        }
                                    }
                                    break;
                                case '>':
                                    {
                                        if (esNumero && Int32.Parse(searchColumn1.column[i]) > postNum)
                                        {
                                            foreach (Column searchColumn2 in tb.columns)
                                            {
                                                    if (searchColumn2.name.Equals(targetColumn))
                                                    {
                                                        result = "{'" + searchColumn2.getTextFromColumn(i) + "'} => ";
                                                        searchColumn2.updateColumn(i, update);
                                                        result = result + "{'" + searchColumn2.getTextFromColumn(i) + "'}";
                                                    }
                                                    
                                                }
                                        }
                                    }
                                    break;
                            }
                        }
                    }
                }
            }
        }
        return result;
        }
        public string createTable(string table, List<String> columns)
        {
            string resultado = "";
            string datos = "";
            foreach (Table tb in tables)
            {
                if (table.Equals(tb.getName()))
                {
                    int i = 0;
                    List<Column> columnsFromTb = tb.getColumns();
                    resultado += "{";
                    foreach (Column c in columnsFromTb)
                    {
                        c.insert(columns[i]);
                        if (i == 0)
                        {
                            resultado += "'" + c.name + "'";
                            datos += columns[i];
                        }
                        else
                        {
                            resultado += ",'" + c.name + "'";
                            datos += "," + columns[i];
                        }
                        i++;
                    }
                    resultado += "}";
                }
            }
            resultado += " => {" + datos + "}";
            return resultado;
        }


    }
}
                                             