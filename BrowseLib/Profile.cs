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
            TablePermission tpToDelete = tablePermissions.Find(tper => tper.getTableName() == tp.getTableName());
            if (tp.samePrivileges(tpToDelete))
            {
                tablePermissions.Remove(tpToDelete);
            }
        }
        public string getName() {
            return name;
        }
        public List<TablePermission> getTablePermissions() { return tablePermissions; }


    }

}
