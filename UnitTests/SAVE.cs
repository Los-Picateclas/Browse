using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BrowseLib;



namespace UnitTests
{
    [TestClass]
    public class SAVE
    {
        [TestMethod]
        public void TestMethod1()
        {
            Database db1 = new Database("db1", "b", "123");
            Table t = new Table("t1");
            Column c1 = new Column("c1", "String");
            c1.insert("olaKase");
            t.addColumn(c1);
           
            db1.addTable(t);
           db1.saveAllTables(db1);











        }
    }
}
