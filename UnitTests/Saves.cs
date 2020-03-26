using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BrowseLib;



namespace UnitTests
{
    [TestClass]
    public class Saves
    {
        
        [TestMethod]
        public void TestMethod()
        {
            Database db1 = new Database("db1", "user1", "password1");
            Table t1 = new Table("t1");
            Column c1 = new Column("c1", "String");
            c1.insert("olaKase");
            t1.addColumn(c1);
            db1.addTable(t1);
            db1.saveAllTables(db1);
        }
    }
}
