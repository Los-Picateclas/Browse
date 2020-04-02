﻿using System;
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


            Database db = new Database("pruebaInsert","usuario", "123");
            Table table = new Table("MiTabla");
            Column name = new Column("name", "TEXT");
            name.insert("Borja");
            name.insert("Unai");
            table.addColumn(name);
            string query = "INSERT INTO MiTabla VALUES Ander";
            MiniSQLParse mini = new MiniSQLParse(query);
            table.addColumn(name);
            db.addTable(table);
            db.saveDataBase();
        }



    }
}
