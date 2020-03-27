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
    }
        
}

