using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BrowseLib;
using System.Windows;

namespace UnitTests
{
    /// A test thaat saves a table
	

    [TestClass]
    public class TableTest
    {
        [TestMethod]
        public void Test()
        {



            Table table = new Table("MiTabla");
            Column name = new Column("name", "TEXT");
            name.insert("Borja");
            name.insert("Unai");
            table.addColumn(name);
            table.save(table);

        }



    }
}
