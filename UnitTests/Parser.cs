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

    }
}
