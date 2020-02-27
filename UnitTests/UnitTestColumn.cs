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
            Column column1 = new Column("prueba1", "INT");
            column1.insert("22");
            column1.getIntFromColumn(0);

            Column column2 = new Column("prueba2", "DOUBLE");
            column2.insert("24,02");
            column2.getDoubleFromColumn(0);

            Column column3 = new Column("prueba3", "TEXT");
            column3.insert("Ola k ase");
            column3.getTextFromColumn(0);

            column3.insert("a tomar por cleta");
            column3.getTextFromColumn(1);

        }
    }
}
