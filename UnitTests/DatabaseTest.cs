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
        private string testname;
        Table table = new Table("test-table");
        Database db = new Database("test-db");
        Database db2 = new Database("test-db");

       [TestMethod]
        public void addTable()
        {
            //db.addTable(table);
        }
            
        public void dropTable()
        {
            //db.dropTable(table);
        }

        public void updateName()
        {
            //db.updateName(testname);
        }
    }
        
}

