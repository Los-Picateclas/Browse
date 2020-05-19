using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BrowseLib
{
    public class Browse
    {
        public List<Database> Databases;
        

        // Constructor
        public Browse()
        {
            Databases = new List<Database>();
           

        }

        // Add a database to the list databases
        public void addDatabase(Database database)
        {
            Databases.Add(database);
        }

        // Remove a database from the list databases
        public void deleteDatabase(Database database)
        {
            Databases.Remove(database);
        }

        // Remove all the databases of the list databases
        public void deleteAllDatabases()
        {
            Databases.Clear();
        }

        // Return the database in the given position
        public Database getDatabase(int pos)
        {
            return Databases.ElementAt(pos);
        }

        // Save the Browse directory
        public void saveBrowse()
        {
            try
            {
                Directory.CreateDirectory("../../../BrowseProgram");
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        // Save the directories for each database in the list databases
        public void saveDatabases()
        {
            foreach (Database db in Databases)
            {
                db.saveDatabase();
            }
        }

        // Load the databases into the list from the directories
        public void loadDatabases()
        {
            DirectoryInfo dirInfo = new DirectoryInfo("../../../BrowseProgram");
            DirectoryInfo[] directoryNames = dirInfo.GetDirectories();

            foreach (DirectoryInfo di in directoryNames)
            {
                addDatabase(new Database(di.Name, "", ""));
            }
        }
    }
}