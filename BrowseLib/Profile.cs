using System;
using System.Collections.Generic;

namespace BrowseLib
{
    public class Profile
    {
        private string name;
        public List<TablePermission> tablePermissions;
        public Profile(string n)
        {
            name = n;
            tablePermissions = new List<TablePermission>();
        }
        public void addTablePermission(TablePermission tp)
        {
            tablePermissions.Add(tp);

        }
        public void removePrivilege(TablePermission tp)
        {
            
            
            foreach (TablePermission tper in tablePermissions)
            {
                if (tp.getTableName().Equals(tper.getTableName())) {
                    if (tp.samePrivileges(tper)) {

                        tablePermissions.Remove(tper);
                    }
            }
        }
        }
        public string getName() {
            return name;
        }



    }

}
