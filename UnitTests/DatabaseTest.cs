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

       [TestMethod]
        public void addTable()
        {

            /*Table table = new Table("test-table");
            Database db1 = new Database("test-db");
            Database db2 = new Database("test-db");
            Table t = table;
            db1.addTable(table);
            db2.addTable(table);
            Assert.AreEqual(db1, db2);*/


        }
        [TestMethod]
        public void dropTable()
        {
            /*Table table = new Table("test-table");
            Database db = new Database("test-db");
            Database db2 = new Database("test-db");
            db.dropTable(table);
            Assert.IsFalse(table.Count == 0);
            db.dropTable(table);*/
            
        }
        [TestMethod]
        public void updateName()
        {
            //private string testname;
            //db.updateName(testname);
            //Assert.AreEqual(db.name = testname);
        }
    }
        
}

