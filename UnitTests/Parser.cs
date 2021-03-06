﻿using System;
using BrowseLib.MiniSQL;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests
{
    [TestClass]
    public class Parser
    {
        [TestMethod]
        public void Select()
        {
            MiniSQLQuery query = MiniSQLParser.Parse("SELECT Name, Age, Height FROM People;");
            Select selectQuery = query as Select;
            Assert.IsTrue(selectQuery.Columns.Contains("Name"));
            Assert.IsTrue(selectQuery.Columns.Contains("Age"));
            Assert.IsTrue(selectQuery.Columns.Contains("Height"));
            Assert.AreEqual("People", selectQuery.Table);
        }

        [TestMethod]
        public void Insert()
        {
            MiniSQLQuery query = MiniSQLParser.Parse("INSERT INTO Person VALUES (Unai, Foncea, 22);");
            Insert insertQuery = query as Insert;
            Assert.IsTrue(insertQuery.Columns.Contains("Unai"));
            Assert.IsTrue(insertQuery.Columns.Contains("Foncea"));
            Assert.IsTrue(insertQuery.Columns.Contains("22"));
            Assert.AreEqual("Person", insertQuery.Table);
        }

        [TestMethod]
        public void Delete()
        {
            MiniSQLQuery query = MiniSQLParser.Parse("DELETE FROM Person WHERE Age=20;");
            Delete deleteQuery = query as Delete;
            Assert.AreEqual("Age=20", deleteQuery.Condition);
            Assert.AreEqual("Person", deleteQuery.Table);
        }


        [TestMethod]
        public void Update()
        {
            MiniSQLQuery query = MiniSQLParser.Parse("UPDATE Person SET Age=22 WHERE Year=1998;");
            Update updateQuery = query as Update;
            Assert.AreEqual("Person", updateQuery.Table);
            Assert.AreEqual("22", updateQuery.UpdateValue);
            Assert.AreEqual("Age", updateQuery.TargetColumn);
            Assert.AreEqual("Year=1998", updateQuery.Condition);


        }

        [TestMethod]
        public void DropTable()
        {
            MiniSQLQuery query = MiniSQLParser.Parse("DROP TABLE Person;");
            DropTable dropQuery = query as DropTable;
            Assert.AreEqual("Person", dropQuery.table);
            
        }

        [TestMethod]
        public void CreateTable()
        {
            MiniSQLQuery query = MiniSQLParser.Parse("CREATE TABLE MyTable (name1 TEXT, name2 INT);");
            CreateTable createQuery = query as CreateTable;
            Assert.AreEqual("MyTable", createQuery.Table);
            Assert.IsTrue(createQuery.Columns.Contains("name1"));
            Assert.IsTrue(createQuery.Columns.Contains("name2"));
            Assert.IsTrue(createQuery.Types.Contains("TEXT"));
            Assert.IsTrue(createQuery.Types.Contains("INT"));
        }
    }
}
