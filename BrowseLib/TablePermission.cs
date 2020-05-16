using System;
using System.Collections.Generic;

namespace BrowseLib
{
    public class TablePermission
    {

        private string table;
        private List<Privileges> privileges;



        public TablePermission(string tbname)
        {
            table = tbname;
            privileges = new List<Privileges>();
        }

        public void addPrivilege(Privileges p) {
            privileges.Add(p);
        }
        public void removePrivilege(Privileges p)
        {
            privileges.Remove(p);
        }



    }


}
