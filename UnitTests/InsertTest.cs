using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BrowseLib;
using System.Windows;

namespace UnitTests
{
    /// Adding a column
	

    [TestClass]
    public class TableTest
    {
        [TestMethod]
        public void Test()
        {


            Browse browse = new Browse();
            Database db = new Database("Borja", "Borja", "Borja");
            Table table = new Table("MiTabla");
            Column name = new Column("name", "TEXT");
            name.insert("Borja");
            name.insert("Unai");
            table.addColumn(name);
            db.addTable(table);
            browse.addDataabse(db);
            string query = "INSERT INTO MiTabla VALUES Ander";
            MiniSQLParse mini = new MiniSQLParse(query);
            browse.saveBrowse();
            db.saveDatabase();
            table.save(table);

        }



    }
}
