using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BrowseLib;

namespace UnitTests
{
    [TestClass]
    public class DatabaseTest
    {
        public static Table table = new Table("test-table");
        public static Database db1 = new Database("test-db");
        public static Database db2 = new Database("test-db");

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
            db1.updateName(db1.name);   
        }
    }
        
}

