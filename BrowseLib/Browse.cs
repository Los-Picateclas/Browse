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
        public List<Database> databases;

        // Constructor
        public Browse()
        {
            databases = new List<Database>();
        }

        // Add a database to the list databases
        public void addDatabase(Database database)
        {
            databases.Add(database);
        }

        // Remove a database from the list databases
        public void deleteDatabase(Database database)
        {
            databases.Remove(database);
        }

        // Return the database in the given position
        public Database getDatabase(int pos)
        {
            return databases.ElementAt(pos);
        }

        // Save the Browse directory
        public void saveBrowse()
        {
            try
            {
                Directory.CreateDirectory("../data/Browse");
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        // Save the directories for each database in the list databases
        public void saveDatabases()
        {
            foreach (Database db in databases)
            {
                db.saveDatabase();
            }
        }
    }
}