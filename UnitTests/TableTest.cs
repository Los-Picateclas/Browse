using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BrowseLib;

namespace UnitTests
{
    /// <summary>
    /// Summary description for UnitTest2
    /// </summary>
    [TestClass]
    public class TableTest
    {
        [TestMethod]
        public void Test() {
           
            Table table = new Table("Tabla1");
            Table t = table;
            Column c1 = new Column("name", "TEXT");
            Column c2 = new Column("age", "INT");
            Column c3 = new Column("date", "DOUBLE");
            table.addColumn(c1);
            table.addColumn(c2);
            table.addColumn(c3);
            table.dropColumn(c2);
            Assert.AreEqual(table, t);
            c2 = table.selectColumn(c1);
            Assert.AreEqual(c1,c2);
            table.save(table);
        }
       


    }
}                                               
