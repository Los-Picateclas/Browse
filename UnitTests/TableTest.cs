using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BrowseLib;
using System.Windows;

namespace UnitTests
{
    [TestClass]
    public class TableTest
    {
        [TestMethod]
        public void Test() {

            /**Table table = new Table("Tabla1");
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
            table.save(table);**/

            Table table = new Table("MiTabla");
            Column name = new Column("name", "TEXT");
            name.insert("Borja");
            name.insert("Unai");
            table.addColumn(name);
            Column age = new Column("age", "INT");
            age.insert("21");
            age.insert("22");
            table.addColumn(age);
            table.save(table);
            //MessageBox.Show(name.getTextFromColumn(0));
            //MessageBox.Show(name.getTextFromColumn(1));
            //MessageBox.Show(age.getTextFromColumn(0));
            //MessageBox.Show(age.getTextFromColumn(1));
        }
    }
}                                               
