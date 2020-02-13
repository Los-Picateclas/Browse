﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrowseLib
{
    class Browse
    {
        List<Database> databases;

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
    }
}
