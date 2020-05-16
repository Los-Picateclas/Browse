using System;
using System.Collections.Generic;
namespace BrowseLib.MiniSQL
{
    public class CreateProfile : MiniSQLQuery
    {

        private string name;
        private List<TablePermission> tablePermissions;

        public CreateProfile(string n)
        {
            tablePermissions = new List<TablePermission>();
            name = n;
        }

        public string Execute(Database database)
        {
            return database.createProfile(name);
        }
    }
}
