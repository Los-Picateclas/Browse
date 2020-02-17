using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BrowseLib;

namespace UnitTests
{
    public class DatabaseTest
    {
            private readonly string testname;
            public void Database()
            {
                Table table = new Table("test-table");
                Database db = new Database("test-db");
                Database db2 = new Database("test-db");
                db.addTable(table);
                db.dropTable(table);
                db.updateName(testname);
                //save Database
                //load database
                Assert.AreEqual(db, db2);
            }
        
    }
}
