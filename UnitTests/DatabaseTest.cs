using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BrowseLib;
using System.IO;

namespace UnitTests
{
    [TestClass]
    public class DatabaseTest
    {
        public static Table table = new Table("test-table");
        public static Database db1 = new Database("test-db1", "username1", "password1");
        public static Database db2 = new Database("test-db2", "username2", "password2");
        string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "Tables");

        [TestMethod]
        public void addTable()
        {
            db1.addTable(table);
            db2.addTable(table);
            Assert.AreEqual(db1.getTable(0), db2.getTable(0));


        }
        [TestMethod]
        public void dropTable()
        {
            db1.tables.Clear();
            db1.addTable(table);
            db1.dropTable(table);
            Assert.IsTrue(db1.tables.Count == 0);
           
            
        }
        [TestMethod]
        public void updateName()
        {
            db1.updateName(db1.databaseName);   
        }

        
        [TestMethod]
        public void loadTables()
        {
            db1.tables.Clear();
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            if (Directory.Exists(Path.Combine(path, "Tables")))
            {
                Directory.Delete(Path.Combine(path, "Tables"));
            }

            db1.addTable(table);
            db1.loadTables(path);
            Assert.IsTrue(db1.tables.Count > 0);
        }
        

        [TestMethod]
        public void saveTables()
        {
            db1.tables.Clear();
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            if (Directory.Exists(Path.Combine(path, "Tables")))
            {
                Directory.Delete(Path.Combine(path, "Tables"));
            }
            db1.addTable(table);
            db1.saveTables(path);
            Assert.IsTrue(Directory.Exists(path));
        }

        [TestMethod]
        public void saveDatabase()
        {
            db1.saveDatabase();
            Assert.IsTrue(Directory.Exists("../data/Browse/test-db1"));
        }

        [TestMethod]
        public void select()
        {
            Column clName = new Column("Name", "TEXT");
            Column clSurname = new Column("Surname", "TEXT");
            Column clAge = new Column("Age", "INT");
            clName.insert("Ander");
            clName.insert("Borja");
            clSurname.insert("Pascual");
            clSurname.insert("Rey");
            clAge.insert("20");
            clAge.insert("21");
            Table tbPerson = new Table("Person");
            tbPerson.addColumn(clName);
            tbPerson.addColumn(clSurname);
            tbPerson.addColumn(clAge);
            Database dbWork = new Database("Work", "username", "password");
            dbWork.addTable(tbPerson);
            List<string> selectColumns = new List<string>();
            selectColumns.Add("Name");
            selectColumns.Add("Age");
            String select = dbWork.Select("Person", selectColumns);
            Assert.IsTrue(select == "{'Name''Age'} => {Ander,Borja}{20,21}");
        }
    }    
}

