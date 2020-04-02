using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BrowseLib;
using System.Windows;

namespace UnitTests
{
    /// A test of the creation of a table

    [TestClass]
    public class TableTest
    {
        [TestMethod]

        public void CreateTable()
        {
            SQLParser parser = new SQLParser();
            CreateTable sbres = (CreateTable)par.Parser("CREATE TABLE table1 (age INT);");
            string[] a = new string[2];
            a[0] = "age";
            CreateTable sel = new CreateTable("table1", a);
            Assert.AreEqual(sbres.GetType(), sel.GetType());
            Assert.AreEqual(sbres.getTabla(), sel.getTabla());
        }
    }
}
