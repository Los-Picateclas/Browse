using System;
using System.Linq;
using BrowseLib;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests
{
    [TestClass]
    public class BrowseTest
    {
        static Browse browse1 = new Browse();
        static Browse browse2 = new Browse();
        static Database db = new Database("db");

        [TestMethod]
        // Check the constructor in Browse create a new Browse object
        public void createBrowse()
        {
            Browse b1 = new Browse();
            Browse b2 = new Browse();
            Assert.AreEqual(b1, b1);
            Assert.AreNotEqual(b1, b2);
        }
        
        [TestMethod]
        // Check addDatabase(Database database) method in Browse add the given database from the list
        public void addDatabase()
        {
            browse1.databases.Clear();
            browse2.databases.Clear();
            browse1.addDatabase(db);
            Assert.AreEqual(db, browse1.databases.First());
            browse2.addDatabase(db);
            Assert.AreEqual(browse1.databases.First(), browse2.databases.First());
        }
        
        [TestMethod]
        // Check deleteDatabase(Database database) method in Browse delete the given database from the list
        public void deleteDatabase()
        {
            browse1.databases.Clear();
            browse1.addDatabase(db);
            Assert.IsFalse(browse1.databases.Count == 0);
            browse1.deleteDatabase(db);
            Assert.IsTrue(browse1.databases.Count == 0);
        }

    }
}
