using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrowseLib
{
   public class Browse
    {
        public List<Database> databases;

        public Browse()
        {
            databases = new List<Database>();
        }

        public void loadDatabases(string path)
        {
            DirectoryInfo di = new DirectoryInfo(path);
            DirectoryInfo[] directories = di.GetDirectories();
            foreach (DirectoryInfo fi in directories)
            {
                databases.Add(new Database(fi.Name));
            }
        }

        public void saveDatabases(string path)
        {
            
        }

        public void addDatabase(Database database) {
            databases.Add(database);
        }

        public void deleteDatabase(Database database)
        {
            databases.Remove(database);
        }

        public Database getDatabase(int num)
        {
            return databases.ElementAt(num);
        }
    }
}
