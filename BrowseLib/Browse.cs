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

        // Load the directories where there are databases
        public void loadDatabases(string path)
        {
            try
            {
                DirectoryInfo di = new DirectoryInfo(path);
                DirectoryInfo[] directories = di.GetDirectories();
                foreach (DirectoryInfo fi in directories)
                {
                    databases.Add(new Database(fi.Name, "username", "password"));
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }

        }

        // Save the directories of each database in the list databases
        public void saveDatabases(string path)
        {
            try
            {
                foreach (Database db in databases)
                {
                    Directory.CreateDirectory(Path.Combine(path, db.databaseName));
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
    }
}