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

        public void loadDatabases()
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
