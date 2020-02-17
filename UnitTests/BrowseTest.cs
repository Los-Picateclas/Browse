using System;
using BrowseLib;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests
{
    [TestClass]
    public class BrowseTest
    {
        Browse browse1 = new Browse();
        Browse browse2 = new Browse();
        Database db = new Database("db");
        [TestMethod]
        public void createBrowse()
        {
            Assert.AreEqual(browse1, browse2);
        }
        public void addDatabase()
        {
            browse1.addDatabase(db);
        }
    }
}
