using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BrowseLib;
using System.IO;

namespace UnitTests
{
    [TestClass]
    public class DatabaseTest
    {
        public static Table table = new Table("test-table");
        public static Database db1 = new Database("test-db1", "username1", "password1");
        public static Database db2 = new Database("test-db2", "username2", "password2");
        string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "Tables");

        [TestMethod]
        public void addTable()
        {
            db1.addTable(table);
            db2.addTable(table);
            Assert.AreEqual(db1.getTable(0), db2.getTable(0));


        }
        [TestMethod]
        public void dropTable()
        {
            db1.tables.Clear();
            db1.addTable(table);
            db1.dropTable(table);
            Assert.IsTrue(db1.tables.Count == 0);
           
            
        }
        [TestMethod]
        public void updateName()
        {
            db1.updateName(db1.databaseName);   
        }

        
        [TestMethod]
        public void loadTables()
        {
            db1.tables.Clear();
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            if (Directory.Exists(Path.Combine(path, "Tables")))
            {
                Directory.Delete(Path.Combine(path, "Tables"));
            }

            db1.addTable(table);
            db1.loadTables(path);
            Assert.IsTrue(db1.tables.Count > 0);
        }
        

        [TestMethod]
        public void saveTables()
        {
            db1.tables.Clear();
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            if (Directory.Exists(Path.Combine(path, "Tables")))
            {
                Directory.Delete(Path.Combine(path, "Tables"));
            }
            db1.addTable(table);
            db1.saveTables(path);
            Assert.IsTrue(Directory.Exists(path));
        }

        [TestMethod]
        public void saveDatabase()
        {
            db1.saveDatabase();
            Assert.IsTrue(Directory.Exists("../data/Browse/test-db1"));
        }

        [TestMethod]
        public void select()
        {
            Column clName = new Column("Name", "TEXT");
            Column clSurname = new Column("Surname", "TEXT");
            Column clAge = new Column("Age", "INT");
            clName.insert("Ander");
            clName.insert("Borja");
            clSurname.insert("Pascual");
            clSurname.insert("Rey");
            clAge.insert("20");
            clAge.insert("21");
            Table tbPerson = new Table("Person");
            tbPerson.addColumn(clName);
            tbPerson.addColumn(clSurname);
            tbPerson.addColumn(clAge);
            Database dbWork = new Database("Work", "username", "password");
            dbWork.addTable(tbPerson);
            List<string> selectColumns = new List<string>();
            selectColumns.Add("Name");
            selectColumns.Add("Age");
            
            String select = dbWork.Select("Person", selectColumns);
            Assert.AreEqual(select, "{'Name','Age'} => {Ander,20}{Borja,21}");
        }

        [TestMethod]
        public void ParseAndSelect()
        {
            Column clName = new Column("Name", "TEXT");
            Column clSurname = new Column("Surname", "TEXT");
            Column clAge = new Column("Age", "INT");
            clName.insert("Ander");
            clName.insert("Borja");
            clSurname.insert("Pascual");
            clSurname.insert("Rey");
            clAge.insert("20");
            clAge.insert("21");
            Table tbPerson = new Table("Person");
            tbPerson.addColumn(clName);
            tbPerson.addColumn(clSurname);
            tbPerson.addColumn(clAge);
            Database dbWork = new Database("Work", "username", "password");
            dbWork.addTable(tbPerson);

            string result = dbWork.ExecuteMiniSQLQuery("SELECT Name, Age FROM Person;");
            Assert.AreEqual("{'Name','Age'} => {Ander,20}{Borja,21}", result);
        }
        [TestMethod]
        public void ParseAndInsert()
        {
            Column clName = new Column("Name", "TEXT");
            Column clSurname = new Column("Surname", "TEXT");
            Column clAge = new Column("Age", "INT");
            clName.insert("Ander");
            clName.insert("Borja");
            clSurname.insert("Pascual");
            clSurname.insert("Rey");
            clAge.insert("20");
            clAge.insert("21");
            Table tbPerson = new Table("Person");
            tbPerson.addColumn(clName);
            tbPerson.addColumn(clSurname);
            tbPerson.addColumn(clAge);
            Database dbWork = new Database("Work", "username", "password");
            dbWork.addTable(tbPerson);

            string result = dbWork.ExecuteMiniSQLQuery("INSERT INTO Person VALUES (Unai, Foncea, 22);");
            Assert.AreEqual("{'Name','Surname','Age'} => {Unai,Foncea,22}", result);
        }

        [TestMethod]
        public void delete()
        {
            Column clName = new Column("Name", "TEXT");
            Column clSurname = new Column("Surname", "TEXT");
            Column clAge = new Column("Age", "INT");
            clName.insert("Ander");
            clName.insert("Borja");
            clName.insert("Unai");
            clSurname.insert("Pascual");
            clSurname.insert("Rey");
            clSurname.insert("Foncea");
            clAge.insert("20");
            clAge.insert("21");
            clAge.insert("22");
            Table tbPerson = new Table("Person");
            tbPerson.addColumn(clName);
            tbPerson.addColumn(clSurname);
            tbPerson.addColumn(clAge);
            Database dbWork = new Database("Work", "username", "password");
            dbWork.addTable(tbPerson);

            String delete = dbWork.delete("Person", "Age>20");
            Assert.AreEqual("{'Age'} => {Borja,Rey,21}{Unai,Foncea,22}", delete);
            String delete2 = dbWork.delete("Person", "Name=Ander");
            Assert.AreEqual("{'Name'} => {Ander,Pascual,20}", delete2);
        }

        [TestMethod]
        public void ParseAndDelete()
        {
            Column clName = new Column("Name", "TEXT");
            Column clSurname = new Column("Surname", "TEXT");
            Column clAge = new Column("Age", "INT");
            clName.insert("Ander");
            clName.insert("Borja");
            clSurname.insert("Pascual");
            clSurname.insert("Rey");
            clAge.insert("20");
            clAge.insert("21");
            Table tbPerson = new Table("Person");
            tbPerson.addColumn(clName);
            tbPerson.addColumn(clSurname);
            tbPerson.addColumn(clAge);
            Database dbWork = new Database("Work", "username", "password");
            dbWork.addTable(tbPerson);

            string result = dbWork.ExecuteMiniSQLQuery("DELETE FROM Person WHERE Age>20;");
            Assert.AreEqual("{'Age'} => {Borja,Rey,21}", result);
        }

        [TestMethod]


        public void updateSymbolEquals() {

            Column clName = new Column("Name", "TEXT");
            Column clAge = new Column("Age", "INT");
            Column clYear = new Column("Year", "INT");
            clName.insert("Borja");
            clName.insert("Unai");
            clAge.insert("21");
            clAge.insert("21");
            clYear.insert("1999");
            clYear.insert("1998");
            Table tbPerson = new Table("Person");
            tbPerson.addColumn(clName);
            tbPerson.addColumn(clAge);
            tbPerson.addColumn(clYear);
            Database dbWork = new Database("Work", "username", "password");
            dbWork.addTable(tbPerson);

            String update = dbWork.update("Person", "Year=1998", "22", "Age");
            Assert.AreEqual("{'21'} => {'22'}", update);



        }
        [TestMethod]
        public void updateSymbol()
        {

            Column clName = new Column("Name", "TEXT");
            Column clAge = new Column("Age", "INT");
            Column clYear = new Column("Year", "INT");
            clName.insert("Borja");
            clName.insert("Unai");
            clAge.insert("21");
            clAge.insert("21");
            clYear.insert("1999");
            clYear.insert("1998");
            Table tbPerson = new Table("Person");
            tbPerson.addColumn(clName);
            tbPerson.addColumn(clAge);
            tbPerson.addColumn(clYear);
            Database dbWork = new Database("Work", "username", "password");
            dbWork.addTable(tbPerson);

            String update = dbWork.update("Person", "Year<2000", "old mans", "Age");
            Assert.AreEqual("{'21'} => {'old mans'}", update);



        }
    }    
}

