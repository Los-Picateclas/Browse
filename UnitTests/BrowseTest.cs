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
        Browse browse1 = new Browse();
        Browse browse2 = new Browse();
        Database testDb1 = new Database("TestDb1", "username1", "password1");
        Database testDb2 = new Database("TestDb2", "username2", "password2");

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
            browse1.addDatabase(testDb1);
            Assert.AreEqual(testDb1, browse1.databases.First());
            browse2.addDatabase(testDb1);
            Assert.AreEqual(browse1.databases.First(), browse2.databases.First());
        }

        [TestMethod]
        // Check deleteDatabase(Database database) method delete the given database from the list
        public void deleteDatabase()
        {
            browse1.addDatabase(testDb1);
            Assert.IsFalse(browse1.databases.Count == 0);
            browse1.deleteDatabase(testDb1);
            Assert.IsTrue(browse1.databases.Count == 0);
        }

        [TestMethod]
        // Check deleteAllDatabases() method delete all the databases from the list
        public void deleteAllDatabases()
        {
            browse1.addDatabase(testDb1);
            Assert.IsFalse(browse1.databases.Count == 0);
            browse1.deleteAllDatabases();
            Assert.IsTrue(browse1.databases.Count == 0);
        }

        // Check that the method saveBrowse() saves the file Browse
        [TestMethod]
        public void saveBrowse()
        {
            browse1.saveBrowse();
            Assert.IsTrue(Directory.Exists("../data/Browse"));
        }

        // Check that the method saveDatabases() saves the different directories for the databases in the list
        [TestMethod]
        public void saveDatabases()
        {
            Directory.Delete("../data/Browse", true);
            browse1.addDatabase(testDb1);
            Assert.IsFalse(Directory.Exists("../data/Browse/TestDb1"));
            browse1.saveDatabases();
            Assert.IsTrue(Directory.Exists("../data/Browse/TestDb1"));
            browse1.addDatabase(testDb2);
            Assert.IsFalse(Directory.Exists("../data/Browse/TestDb2"));
            browse1.saveDatabases();
            Assert.IsTrue(Directory.Exists("../data/Browse/TestDb2"));
        }

        // Check that the method loadDatabases() load the databases into the list from the directories
        [TestMethod]
        public void loadDatabases()
        {
            Directory.CreateDirectory("../data/Browse/db1");
            Directory.CreateDirectory("../data/Browse/db2");
            browse1.loadDatabases();
            Assert.IsTrue(browse1.getDatabase(0).databaseName == "db1");
            Assert.IsTrue(browse1.getDatabase(1).databaseName == "db2");
        }
    }
}