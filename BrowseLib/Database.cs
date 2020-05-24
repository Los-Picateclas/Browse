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
        public User actualUser;

        public Database(string dN, string uN, string pW)
        {
            tables = new List<Table>();
            this.databaseName = dN;
            this.username = uN;
            this.password = pW;
            users = new List<User>();
            profiles = new List<Profile>();
          
            Profile adminProfile = new Profile("admin");
            profiles.Add(adminProfile);
            User adminUser = new User("admin", "admin", adminProfile);
            users.Add(adminUser);
        }
        public void initialize() {
            profiles = new List<Profile>();
            users = new List<User>();


        }
        public Boolean hasSelectPrivilege(string table) {
            
            Boolean has = false;
            User au = getActualUser();
            string actualuserName = au.getName() ;
            
            if (actualuserName=="admin") {
                
                has = true;

               }
            else {
             
                Profile actualProfile = actualUser.getProfile();

                TablePermission tp;
                foreach (TablePermission TABPER in actualProfile.getTablePermissions()) {
                    if (TABPER.getTableName()==table) {
                        
                        tp = TABPER;
                        if (tp.hasPrivilege("SELECT")) {
                            
                            has = true;

                        } ;
                    }

                }
               //TablePermission tp = actualProfile.getTablePermissions().Find(t => t.getTableName() == table);
               
                //if (tp!=null) {

                 //   Console.WriteLine();
               // }

                //if (tp.hasPrivilege()){ has = true; }
                // Privileges p = tp.getPrivileges().Find(pr => pr == Privileges.SELECT);
                //if (p != null) { has = true; }

                
            }
            

            return has;

        }




        public Boolean hasInsertPrivilege(string table)
        {
            Boolean has = false;
            User au = getActualUser();
            string actualuserName = au.getName();
           if (actualuserName == "admin")
            {
      has = true;
               }
            else
            {
                Profile actualProfile = actualUser.getProfile();
                TablePermission tp;
                foreach (TablePermission TABPER in actualProfile.getTablePermissions())
                {
                    if (TABPER.getTableName() == table)
                    {
                        tp = TABPER;
                        if (tp.hasPrivilege("INSERT"))
                        {
                            has = true;
                        };
                    }
                }
               }
            return has;
        }



        public Boolean hasUpdatePrivilege(string table)
        {
            Boolean has = false;
            User au = getActualUser();
            string actualuserName = au.getName();
            if (actualuserName == "admin")
            {
                has = true;
            }
            else
            {
                Profile actualProfile = actualUser.getProfile();
                TablePermission tp;
                foreach (TablePermission TABPER in actualProfile.getTablePermissions())
                {
                    if (TABPER.getTableName() == table)
                    {
                        tp = TABPER;
                        if (tp.hasPrivilege("UPDATE"))
                        {
                            has = true;
                        };
                    }
                }
            }
            return has;
        }



        public Boolean hasDeletePrivilege(string table)
        {
            Boolean has = false;
            User au = getActualUser();
            string actualuserName = au.getName();
            if (actualuserName == "admin")
            {
                has = true;
            }
            else
            {
                Profile actualProfile = actualUser.getProfile();
                TablePermission tp;
                foreach (TablePermission TABPER in actualProfile.getTablePermissions())
                {
                    if (TABPER.getTableName() == table)
                    {
                        tp = TABPER;
                        if (tp.hasPrivilege("DELETE"))
                        {
                            has = true;
                        };
                    }
                }
            }
            return has;
        }


        public User getActualUser() { return actualUser; }


        public void setActualUser(string u) {

           User aux = users.Find(us => us.getName().Equals(u));
            
           
            actualUser = aux;


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
            if (hasInsertPrivilege(tab))
            {
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
                        result = "Tuple added";
                    }
                   
                    tb.save(tb, databaseName);
                }
                
            }
            else { result = "This user does not have insert privilege"; }
           

            return result;
        }

        public string Select(string table, List<string> columns, string condition)
        {
            string select = "";
           
            
            if (hasSelectPrivilege(table))
            {

                string datos = "";
                List<int> numCl = new List<int>();

                char[] delimiterChars = { '<', '=', '>' };
                string[] words = condition.Split(delimiterChars);
                bool tableFound = false;
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
                         tableFound = true;
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
                if (tableFound)
                {
                    return select;
                }
                else
                {

                    select = "Table not found";
                    return select;
                }

                
           }
            else {
                select = "This user does not have select privilege";
                return select; }
           
        }

        // Delete the tuples from the given table that satisfy the condition (only accept a condition of type) 
        public string delete(string table, string condition)
        {
           
            string result = "";
            if (hasDeletePrivilege(table)) {
                char[] delimiterChars = { '<', '=', '>' };
                string[] words = condition.Split(delimiterChars);
                bool tableFound = false;
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
               

                foreach (Table tb in tables)
                {
                    if (table.Equals(tb.getName()))
                    {
                        
                        tableFound = true;

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

                if (tableFound) {
                    result = "Tuple(s) deleted";
                    return result;
                }
                else
                {
                    
                    result = "Table not found";
                    return result;
                }
              
            
            }
            else {
                result = "It does not have delete privilege";
                return result;
            }
        }

        public Boolean checkPassword(string us, string pass)
        {
            Boolean userExists = false;
            User user = users.Find(u => u.getName() == us);
            if (user.getPassword() == pass) { userExists = true; }

            return userExists;
        }
        //'targetColumn' must be the column where you want to make changes
        // 'update' parameter must be the new value that we are setting 
        public string update(string table, string condition, string update, string targetColumn)
        {
            string result = " ";

            if (hasUpdatePrivilege(table)) {
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
                return result; }
            else {
                result = "It does not have Update privilege";
                return result;
            }
        }
        public string createTable(string table, List<String> columns, List<string> types, Database db)
        {
            if (tables.Contains(tables.Find(t => t.getName() == table)))
            {
                return "Table exists";
            }
            else
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
        }
        
        public string createProfile(string name) {

            Profile profile = new Profile(name);
            profiles.Add(profile);
            
            return "Profile created";
        }
        public string dropProfile(string name, Database db)
        {

            Profile toDelete = null;

           

                 toDelete = profiles.Find(pr => pr.getName()==name);

            

            profiles.Remove(toDelete);
            
            return "Profile droped";
        }
        public string addUser(string name, string pass, string prof)
        {
            
            
           
            Profile aux = profiles.Find(pr => pr.getName() == prof);
          
            User u = new User(name, pass, aux);
            users.Add(u);

          

            return "User added";
        }
        public List<User> getUsers() { return users; }
        public void setUsers(List<User> u) { users = u; }

        public string deleteUser(string name, Database db)
        {
            User toDelete = null;


            
                toDelete = users.Find(us => us.getName() == name);
            users.Remove(toDelete);



            return "User deleted";
        }
        public string grant(string privilege, string table, string profile)
        {
            TablePermission tb = new TablePermission(table);
            
            if (privilege.Equals("INSERT")) { tb.addPrivilege(Privileges.INSERT); }
            else if (privilege.Equals("SELECT")) {  tb.addPrivilege(Privileges.SELECT); }
            else if (privilege.Equals("UPDATE")) { tb.addPrivilege(Privileges.UPDATE); }
            else if (privilege.Equals("DELETE")) { tb.addPrivilege(Privileges.DELETE); }

            foreach (Profile pr in profiles) { 
            if (profile.Equals(pr.getName()))
            {
                pr.addTablePermission(tb);
            }
        }
            return "Privileges granted";
        }
        public string revoke(string privilege, string table, string profile)
        {

            Profile toDelete = null;



            toDelete = profiles.Find(pr => pr.getName() == profile);
            Profile aux = toDelete;


            profiles.Remove(toDelete);

            TablePermission tb = new TablePermission(table);
            if (privilege.Equals("INSERT")) { tb.removePrivilege(Privileges.INSERT); }
            else if (privilege.Equals("SELECT")) { tb.removePrivilege(Privileges.SELECT); }
            else if (privilege.Equals("UPDATE")) { tb.removePrivilege(Privileges.UPDATE); }
            else if (privilege.Equals("DELETE")) { tb.removePrivilege(Privileges.DELETE); }
            
            if (aux != null) { 
            aux.removePrivilege(tb);
            profiles.Add(aux);
        }
            return "Privileges revoked";
        }
    }
}


