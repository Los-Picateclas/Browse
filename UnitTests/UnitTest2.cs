using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BrowseLib;

namespace UnitTests
{
    [TestClass]
    public class UnitTest2
    {
        [TestMethod]
        public void TestMethod1()
        {

            Table table = new Table("Tabla1");
            
            Column c1 = new Column("name", "TEXT");
            Column c2 = new Column("age", "INT");
            Column c3 = new Column("date", "DOUBLE");
            table.addColumn(c1);
            table.addColumn(c2);
            table.addColumn(c3);
            table.dropColumn(c2);
            

            
        }
    }
}
