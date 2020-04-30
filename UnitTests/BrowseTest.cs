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
        Browse Browse1 = new Browse();
        Browse Browse2 = new Browse();
        Database TestDb1 = new Database("TestDb1", "username1", "password1");
        Database TestDb2 = new Database("TestDb2", "username2", "password2");

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
            Browse1.addDatabase(TestDb1);
            Assert.AreEqual(TestDb1, Browse1.Databases.First());
            Browse2.addDatabase(TestDb1);
            Assert.AreEqual(Browse1.Databases.First(), Browse2.Databases.First());
        }

        [TestMethod]
        // Check deleteDatabase(Database database) method delete the given database from the list
        public void deleteDatabase()
        {
            Browse1.addDatabase(TestDb1);
            Assert.IsFalse(Browse1.Databases.Count == 0);
            Browse1.deleteDatabase(TestDb1);
            Assert.IsTrue(Browse1.Databases.Count == 0);
        }

        [TestMethod]
        // Check deleteAllDatabases() method delete all the databases from the list
        public void deleteAllDatabases()
        {
            Browse1.addDatabase(TestDb1);
            Assert.IsFalse(Browse1.Databases.Count == 0);
            Browse1.deleteAllDatabases();
            Assert.IsTrue(Browse1.Databases.Count == 0);
        }

        // Check that the method saveBrowse() saves the file Browse
        [TestMethod]
        public void saveBrowse()
        {
            Browse1.saveBrowse();
            Assert.IsTrue(Directory.Exists("../data/Browse"));
        }

        // Check that the method saveDatabases() saves the different directories for the databases in the list
        [TestMethod]
        public void saveDatabases()
        {
            Directory.Delete("../data/Browse", true);
            Browse1.addDatabase(TestDb1);
            Assert.IsFalse(Directory.Exists("../../../BrowseLib/db1"));
            Browse1.saveDatabases();
            Assert.IsTrue(Directory.Exists("../../../BrowseLib/db1"));
            Browse1.addDatabase(TestDb2);
            Assert.IsFalse(Directory.Exists("../../../BrowseLib/db1"));
            Browse1.saveDatabases();
            Assert.IsTrue(Directory.Exists("../../../BrowseLib/db1"));
        }

        // Check that the method loadDatabases() load the databases into the list from the directories
        [TestMethod]
        public void loadDatabases()
        {
            Directory.CreateDirectory("../../../BrowseLib/db1");
            Directory.CreateDirectory("../../../BrowseLib/db2");
            Browse1.loadDatabases();
            Assert.IsTrue(Browse1.getDatabase(0).databaseName == "db1");
            Assert.IsTrue(Browse1.getDatabase(1).databaseName == "db2");
        }
    }
}