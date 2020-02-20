using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BrowseLib;

namespace UnitTests
{
    [TestClass]
    public class UnitTestColumn
    {
        [TestMethod]
        public void TestMethod1()
        {
            Column columna1 = new Column("prueba1", "TEXT");
            columna1.insertTEXTtoTable("Olaa K asEE");
            Column columna2 = new Column("prueba2", "TEXT");
            columna2.insertTEXTtoTable("Olaa K asEE");
            Assert.Equals(columna1, columna2);

            Column columna3 = new Column("prueba3", "INT");
            columna3.insertINTtoTable(20022020);
            Assert.Equals(columna3, columna2);

        }
    }
}
