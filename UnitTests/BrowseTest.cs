using System;
using System.IO;
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
        Database testDb1 = new Database("db1", "username1", "password1");
        Database testDb2 = new Database("db2", "username2", "password2");
        string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Browse");

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
            browse1.addDatabase(testDb1);
            Assert.AreEqual(testDb1, browse1.databases.First());
            browse2.addDatabase(testDb1);
            Assert.AreEqual(browse1.databases.First(), browse2.databases.First());
        }

        [TestMethod]
        // Check deleteDatabase(Database database) method delete the given database from the list
        public void deleteDatabase()
        {
            browse1.databases.Clear();
            browse1.addDatabase(testDb1);
            Assert.IsFalse(browse1.databases.Count == 0);
            browse1.deleteDatabase(testDb1);
            Assert.IsTrue(browse1.databases.Count == 0);
        }

        // Check that the method saveBrowse() saves the file Browse
        [TestMethod]
        public void saveBrowse()
        {
            browse1.saveBrowse();
            Assert.IsTrue(Directory.Exists("../data/Browse"));
        }
    }
}