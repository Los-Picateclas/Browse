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
        public List<User> users;
        public List<Profile> profiles;

        public Database(string dN, string uN, string pW)
        {
            tables = new List<Table>();
            this.databaseName = dN;
            this.username = uN;
            this.password = pW;
            users = new List<User>();
            profiles = new List<Profile>();
            initialize();
            Profile adminProfile = new Profile("admin");
            profiles.Add(adminProfile);
            User adminUser = new User("admin", "admin", adminProfile);
            users.Add(adminUser);
        }
        public void initialize() {
            profiles = new List<Profile>();
            users = new List<User>();


        }
        //It saves each table from the list
        public void saveAllTables(Database d)
        {
            for (int i = 0; i < tables.Count; i++)
            {
                tables.ElementAt(i).save(tables.ElementAt(i), d);
            }

        }
        public List<Profile> getProfiles() { return profiles; }

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

            string route = "../Browse/" + databaseName + "/" + table;
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

        public void saveTables(Database db)
        {
            // Console.WriteLine("Se van a guardar las tablas");
            try
            {
                foreach (Table tb in tables)
                {
                    //Directory.CreateDirectory("../../../BrowseProgram/" + tb.getName());
                    tb.save(tb, db);
                    //Console.WriteLine("Tabla guardada: " + tb.getName());
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
                string path = "../../../BrowseProgram/" + databaseName;
                Directory.CreateDirectory(path);
            }
            catch (Exception e)
            {
                throw e;
            }
        }



        public string insert(string tab, List<String> Col)
        {
            string result = "";
            string datos = "";
            foreach (Table tb in tables)
            {
                if (tab.Equals(tb.getName()))
                {
                    int i = 0;
                    List<Column> columnsFromTb = tb.getColumns();
                    foreach (Column c in columnsFromTb)
                    {
                        c.insert(Col[i]);
                        if (i == 0)
                        {
                            datos += Col[i];
                        }
                        else
                        {
                            datos += "," + Col[i];
                        }
                        i++;
                    }
                }
                tb.save(tb, databaseName);
            }
            result = "Tuple added";

            return result;
        }

        public string Select(string table, List<string> columns, string condition)
        {
            string select = "";
            string datos = "";
            List<int> numCl = new List<int>();

            char[] delimiterChars = { '<', '=', '>' };
            string[] words = condition.Split(delimiterChars);

            string columnName;
            string postCon;
            int postNum;
            bool esNumero;
            char symbol;
            List<int> numCon = new List<int>();

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

            foreach (Table tb in tables)
            {
                if (table == tb.getName())
                {
                    select = "{";
                    foreach (string cName in columns)
                    {
                        foreach (Column cl in tb.columns)
                        {
                            if (cName == cl.name)
                            {
                                if (condition != "")
                                {
                                    columnName = words[0];
                                    postCon = words[1];
                                    esNumero = int.TryParse(postCon, out postNum);
                                    for (int i = 0; i < cl.getColumnSize(); i++)
                                    {
                                        string name = cl.name;
                                        if (name == columnName)
                                        {
                                            switch (symbol)
                                            {
                                                case '<':
                                                    {
                                                        if (esNumero && Int32.Parse(cl.column[i]) < postNum)
                                                        {
                                                            numCon.Add(i);
                                                        }
                                                    }
                                                    break;
                                                case '=':
                                                    {
                                                        if (esNumero && Int32.Parse(cl.column[i]) == postNum)
                                                        {
                                                            numCon.Add(i);
                                                        }
                                                        else if (!esNumero && cl.column[i] == postCon)
                                                        {
                                                            numCon.Add(i);
                                                        }
                                                    }
                                                    break;
                                                case '>':
                                                    {
                                                        if (esNumero && Int32.Parse(cl.column[i]) > postNum)
                                                        {
                                                            numCon.Add(i);
                                                        }
                                                    }
                                                    break;
                                            }
                                        }
                                        
                                    }
                                    if (numCl.Count() == 0)
                                    {
                                        numCl.Add(tb.columns.IndexOf(cl));
                                        select += "'" + cName + "'";
                                    }
                                    else
                                    {
                                        numCl.Add(tb.columns.IndexOf(cl));
                                        select += ",'" + cName + "'";
                                    }
                                }
                                else
                                {
                                    if (numCl.Count() == 0)
                                    {
                                        numCl.Add(tb.columns.IndexOf(cl));
                                        select += "'" + cName + "'";
                                    }
                                    else
                                    {
                                        numCl.Add(tb.columns.IndexOf(cl));
                                        select += ",'" + cName + "'";
                                    }
                                }

                            }
                        }
                    }
                    if (condition == "")
                    {
                        for (int i = 0; i < tb.columnSize() - 1; i++)
                        {
                            numCon.Add(i);
                        }
                    }
                    select += "} => ";
                    int columnLength = tb.columnSize() - 1;
                    foreach (int j in numCon)
                    {
                        select += "{";
                        foreach (int k in numCl)
                        {
                            if (k == 0)
                            {
                                select += tb.selectColumn(k).column[j];
                            }
                            else
                            {
                                select += "," + tb.selectColumn(k).column[j];
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

            string result = "";
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
                                                tb.deleteTuple(i);
                                                i = i - 1;
                                            }
                                        }
                                        break;
                                    case '=':
                                        {
                                            if (esNumero && Int32.Parse(c.column[i]) == postNum)
                                            {
                                                tb.deleteTuple(i);
                                                i = i - 1;
                                            }
                                            else if (!esNumero && c.column[i] == postCon)
                                            {
                                                tb.deleteTuple(i);
                                                i = i - 1;
                                            }
                                        }
                                        break;
                                    case '>':
                                        {
                                            if (esNumero && Int32.Parse(c.column[i]) > postNum)
                                            {
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
                tb.save(tb, databaseName);
            }
            result = "Tuple(s) deleted";
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
        public string createTable(string table, List<String> columns, List<string> types, Database db)
        {

            Table tableCreated = new Table(table);
            for (int i = 0; i < columns.Count; i++)
            {
                tableCreated.addColumn(new Column(columns[i], types[i]));
            }
            tables.Add(tableCreated);
            db.saveTables(db);


            return "Table created";
        }
        public string createProfile(string name) {

            Profile profile = new Profile(name);
            profiles.Add(profile);
            

            return "Profile created";
        }
        public string dropProfile(string name, Database db)
        {
            
            profiles = new List<Profile>();
                foreach (Profile pr in profiles)
                    if (name.Equals(pr.getName()))
                    {
                        profiles.Remove(pr);
                    }


            
            return "Profile droped";
        }
        public string addUser(string name, string pass, string prof)
        {
           
            Profile aux = null;
            foreach (Profile pr in profiles)
                if (prof.Equals(pr.getName()))
                {
                    aux = pr;
                }
            
            User u = new User(name, pass, aux);
            users.Add(u);



            return "User added";
        }
        public string deleteUser(string name)
        {

            users = new List<User>();
            foreach (User us in users)
                if (name.Equals(us.getName()))
                {
                    users.Remove(us);
                }

            



            return "User deleted";
        }
        public string grant(string privilege, string table, string profile)
        {
            TablePermission tb = new TablePermission(table);
            if (privilege.Equals("INSERT")) { tb.addPrivilege(Privileges.INSERT); }
            else if (privilege.Equals("SELECT")) { tb.addPrivilege(Privileges.SELECT); }
            else if (privilege.Equals("UPDATE")) { tb.addPrivilege(Privileges.UPDATE); }
            else if (privilege.Equals("DELETE")) { tb.addPrivilege(Privileges.DELETE); }

            foreach (Profile pr in profiles)
                if (profile.Equals(pr.getName()))
                {
                    pr.addTablePermission(tb);
                }

            return "Privileges granted";
        }
    }
}


