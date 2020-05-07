using System;
using System.Collections.Generic;

namespace BrowseLib
{
    public class User
    {

        private string user, password;
        private List<TablePermission> privileges;





        public User(string name, string pass)
        {
            user = name;
            password = pass;
            privileges = new List<TablePermission>();
        }
        public void addPermission(TablePermission p)
        {
            privileges.Add(p);

        }
        public void removePermission(TablePermission p)
        {
            privileges.Remove(p);

        }
        public List<TablePermission> getTablePermission()
        {

            return privileges;
        }


    }

}
