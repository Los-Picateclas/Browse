using System;
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
    }
}
